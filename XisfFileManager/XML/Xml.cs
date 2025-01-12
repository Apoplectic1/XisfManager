using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;

namespace XisfFileManager.XML
{
    internal sealed class Xml
    {
        // ***********************************************************************************
        // ***********************************************************************************

        public static string FixXisfXml(string xmlString)
        {
            // Remove anything after </xisf> in xmlString
            int endIndex = xmlString.IndexOf("</xisf>") + "</xisf>".Length;
            if (endIndex > 0)
            {
                xmlString = xmlString.Substring(0, endIndex);
            }
            else
            {
                // Throw an exception if </xisf> is not found
                throw new XisfXmlException("Closing </xisf> tag not found in the XML string.");
            }

            // Remove all non-ASCII characters
            xmlString = Regex.Replace(xmlString, @"[^\x00-\x7F]", "");

            // Remove single quotes inside FITS Keywords
            xmlString = xmlString.Replace("'", "");

            // Remove Processing History Property if it exists
            string pattern = Regex.Escape("<Property") + @"(.*?)" + Regex.Escape(";</Property>");
            xmlString = Regex.Replace(xmlString, pattern, "");

            return xmlString;
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
