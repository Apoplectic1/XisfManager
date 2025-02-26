using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;

namespace XisfFileManager.XML
{
    internal sealed class Xml
    {
        // ***********************************************************************************
        // ***********************************************************************************

        public static (string FixedXml, bool Modified) FixXisfXml(string xmlString)
        {
            bool modified = false;
            string step;

            // Step 1: Remove all non-ASCII characters
            step = new string(xmlString.Where(c => c < 128).ToArray());
            if (!string.Equals(step, xmlString, StringComparison.Ordinal))
            {
                modified = true;
            }
            xmlString = step;

            // Step 2: Remove single quotes inside FITS Keywords
            step = xmlString.Replace("'", "");
            if (!string.Equals(step, xmlString, StringComparison.Ordinal))
            {
                modified = true;
            }
            xmlString = step;

            // Step 3: Remove anything after </xisf> in xmlString
            int endIndex = xmlString.IndexOf("</xisf>") + "</xisf>".Length;
            if (endIndex > 0)
            {
                if (endIndex < xmlString.Length)
                {
                    modified = true;
                }
                step = xmlString.Substring(0, endIndex);
                xmlString = step;
            }
            else
            {
                throw new XisfXmlException("Closing </xisf> tag not found in the XML string.");
            }

            // Step 4: Remove Processing History Property if it exists
            string pattern = Regex.Escape("<Property") + @"(.*?)" + Regex.Escape(";</Property>");
            step = Regex.Replace(xmlString, pattern, "");
            if (!string.Equals(step, xmlString, StringComparison.Ordinal))
            {
                modified = true;
            }
            xmlString = step;

            // Step 5: Remove all <Property ... /Property> or <Property ... /> lines inside <Image> blocks
            step = Regex.Replace(
                xmlString,
                @"(<Image.*?>)(.*?)(</Image>)",
                m =>
                {
                    // Capture the content inside the <Image> block.
                    string imageContent = m.Groups[2].Value;
                    // Build a pattern that matches segments starting with "<Property" and ending with either "/Property>" or "/>"
                    string propertyPattern = Regex.Escape("<Property")
                        + ".*?(?:"
                        + Regex.Escape("/Property>")
                        + "|"
                        + Regex.Escape("/>")
                        + ")";
                    // Remove any such segment from the <Image> block content.
                    string newContent = Regex.Replace(imageContent, propertyPattern, "", RegexOptions.Singleline);
                    // Reassemble the <Image> block.
                    return m.Groups[1].Value + newContent + m.Groups[3].Value;
                },
                RegexOptions.Singleline);
            if (!string.Equals(step, xmlString, StringComparison.Ordinal))
            {
                modified = true;
            }
            xmlString = step;

            return (xmlString, modified);
        }


        // ***********************************************************************************
        // ***********************************************************************************

        public static string ValidateXisfXml(string xmlString)
        {
            XmlReaderSettings settings = new XmlReaderSettings();

            while (!string.IsNullOrEmpty(xmlString))
            {
                using (XmlReader reader = XmlReader.Create(new StringReader(xmlString), settings))
                {
                    try
                    {
                        while (reader.Read())
                        {
                            // Reading the XML will trigger validation
                        }
                        return xmlString; // Return the modified string on successful parsing
                    }
                    catch (XmlSchemaValidationException ex)
                    {
                        // Throw an exception with a detailed message
                        throw new XisfXmlException($"Xml.cs ValidateXisfXml() - XML schema validation error: {ex.Message}");
                    }
                    catch (XmlException ex)
                    {
                        int errorPosition = ex.LinePosition;
                        int startIndex = xmlString.LastIndexOf('>', errorPosition) + 1;
                        int endIndex = xmlString.IndexOf('<', errorPosition);

                        if (startIndex >= 0 && endIndex >= 0)
                        {
                            xmlString = xmlString.Remove(startIndex, endIndex - startIndex);
                        }
                        else
                        {
                            // Throw an exception with a detailed message
                            throw new XisfXmlException($"Xml.cs ValidateXisfXml() - XML parsing error at line {ex.LineNumber}, position {ex.LinePosition}: {ex.Message}");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Throw an exception with a detailed message
                        throw new XisfXmlException($"Xml.cs ValidateXisfXml() - Unexpected error: {ex.Message}");
                    }
                }
            }

            // Return null if parsing otherwise fails
            return null;
        }

        // ***********************************************************************************
        // ***********************************************************************************

        public static string RemoveNonEvenPairs(string input, string openString, string closeString)
        {
            string pattern = $@"{Regex.Escape(openString)}[^{Regex.Escape(openString + closeString)}]*{Regex.Escape(closeString)}";
            MatchCollection matches = Regex.Matches(input, pattern);

            var result = new System.Text.StringBuilder();

            foreach (Match match in matches)
            {
                if (HasEvenPairs(match.Value, openString, closeString))
                {
                    result.Append(match.Value);
                }
            }

            return result.ToString();
        }

        // ***********************************************************************************
        // ***********************************************************************************

        public static bool HasEvenPairs(string input, string openString, string closeString)
        {
            int openCount = Regex.Matches(input, Regex.Escape(openString)).Count;
            int closeCount = Regex.Matches(input, Regex.Escape(closeString)).Count;

            return openCount == closeCount;
        }

        // ***********************************************************************************
        // ***********************************************************************************

        public static bool ContainsNonAsciiOrInvalidChars(string input, out char firstInvalidChar)
        {
            // Check for non-ASCII characters
            bool containsNonAscii = Regex.IsMatch(input, @"[^\x00-\x7F]");

            // Check for characters other than the allowed set
            Match match = Regex.Match(input, @"[^A-Za-z0-9+\-./:()_ .<>="",*%]");
            if (match.Success)
            {
                firstInvalidChar = match.Value[0];
                return true;
            }

            firstInvalidChar = '\0';

            return containsNonAscii;
        }

        // ***********************************************************************************
        // ***********************************************************************************
    }

    public class XisfXmlException : Exception
    {
        public XisfXmlException(string message) : base(message) { }
    }
}
