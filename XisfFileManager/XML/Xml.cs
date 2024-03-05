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
            // Remove anthing after </xisf> in xmlString
            xmlString = xmlString.Substring(0, xmlString.IndexOf("</xisf>") + "</xisf>".Length);

            // Remove all non-ASCII characters
            xmlString = Regex.Replace(xmlString, @"[^\x00-\x7F]", "");

            // Some XISF files have single quotes inside FITS Keywords - Remove them.
            xmlString = Regex.Replace(xmlString, @"'", "");

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
                    catch (XmlSchemaValidationException)
                    {
                        // Handle schema validation error here
                        return null; // Indicate failure by returning null
                    }
                    catch (XmlException ex)
                    {
                        int errorPosition = ex.LinePosition;
                        int startIndex = xmlString.LastIndexOf('>', errorPosition) + 1;
                        int endIndex = xmlString.IndexOf('<', errorPosition);

                        if (startIndex >= 0 && endIndex >= 0)
                            xmlString = xmlString.Remove(startIndex, endIndex - startIndex);
                        else
                            return null; // Indicate failure by returning nul
                    }
                    catch (Exception)
                    {
                        // Handle other exceptions here
                        return null; // Indicate failure by returning null
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

            string result = "";

            foreach (Match match in matches)
            {
                if (HasEvenPairs(match.Value, openString, closeString))
                {
                    result += match.Value;
                }
            }

            return result;
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

            int quoteCount = 0;

            foreach (char c in input)
            {
                if (c == '"')
                {
                    quoteCount++;
                }
            }

            bool quotesMatch = quoteCount % 2 == 0;


            return containsNonAscii;
        }

        // ***********************************************************************************
        // ***********************************************************************************
    }
}
