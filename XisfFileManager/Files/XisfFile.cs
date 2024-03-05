using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using XisfFileManager.Enums;

namespace XisfFileManager.Files
{
    public class XisfFile
    {
        // ************************************************************************************************
        // ************************************************************************************************

        public XDocument mXDoc { get; set; }
        public KeywordList KeywordList { get; set; }
        public string XmlString { get; set; }

        // ************************************************************************************************
        // ************************************************************************************************

        public XisfFile()
        {
            mXDoc = new XDocument();
            KeywordList = new KeywordList();
        }
        // Properties
        public void Clear()
        {
            mXDoc = new XDocument();
            KeywordList.Clear();
            TargetAttachmentLength = 0;
            TargetAttachmentStart = 0;
            TargetAttachmentPadding = 0;
            ThumbnailAttachmentLength = 0;
            ThumbnailAttachmentStart = 0;
            ThumbnailAttachmentPadding = 0;
        }

        public eKeywordUpdateMode KeywordUpdateMode { get; set; } = eKeywordUpdateMode.PROTECT;

        public string XmlVersionText { get; set; } = "<?xml version=\"1.0\" encoding=\"UTF-8\">";
        public string XmlCommentText { get; set; } = "<!--\r\nExtensible Image Serialization Format - XISF version 1.0\r\nCreated with PixInsight software - http://pixinsight.com/\r\n-->";
        public void AddKeyword(string keyword, string value, string comment = "Xisf File Manager")
        {
            KeywordList.AddKeyword(keyword, value, comment);
        }

        public void AddXMLKeyword(string keyword, string value, string comment = "Xisf File Manager")
        {
            KeywordList.AddKeyword(keyword, value, comment);
        }
        public void RemoveKeyword(string keyword)
        {
            KeywordList.RemoveKeyword(keyword);
        }
        public void RemoveKeyword(string keyword, object oValue)
        {
            KeywordList.RemoveKeyword(keyword, oValue);
        }
        public Keyword GetKeyword(string keyword)
        {
            return KeywordList.GetKeyword(keyword);
        }
        public string GetKeywordValue(string keyword)
        {
            return KeywordList.GetKeywordValue(keyword);
        }
        public string GetKeywordComment(string keyword)
        {
            return KeywordList.GetKeywordComment(keyword);
        }
        public double AmbientTemperature
        {
            get => KeywordList.AmbientTemperature;
            set => KeywordList.AmbientTemperature = value;
        }
        public int Binning
        {
            get => KeywordList.Binning;
            set => KeywordList.Binning = value;
        }
        public int BlockAlignmentSize { get; set; } = -1;
        public string Camera
        {
            get => KeywordList.Camera;
            set => KeywordList.Camera = value;
        }
        public string CaptureSoftware
        {
            get
            {
                string value = string.Empty;

                Keyword creator = GetKeyword("SWCREATE");
                if (creator != null)
                {
                    value = creator.Value;
                    RemoveKeyword("CREATOR");
                    if (value.Equals("NINA"))
                        return "NINA";
                }

                creator = GetKeyword("CREATOR");
                if (creator != null)
                {
                    value = creator.Value;
                    RemoveKeyword("CREATOR");
                }

                if (value.Contains("N.I.N.A.") || value.Equals("NINA"))
                {
                    AddKeyword("SWCREATE", "NINA", value);  // Value is version number
                    return "NINA";
                }
                if (value.Contains("SkyX") || value.Equals("TSX"))
                {
                    AddKeyword("SWCREATE", "TSX", "Software Bisque The SkyX");
                    return "TSX";
                }
                if (value.Contains("Sequence") || value.Equals("SGP"))
                {
                    AddKeyword("SWCREATE", "SGP", "Sequence Generator Pro");
                    return "SGP";
                }
                if (value.Contains("Voy") || value.Equals("VOY"))
                {
                    AddKeyword("SWCREATE", "VOY", "Starkeeper Voyager");
                    return "VOY";
                }
                if (value.Contains("Cap") || value.Equals("SCP"))
                {
                    AddKeyword("SWCREATE", "SCP", "SharpCap");
                    return "SCP";
                }

                AddKeyword("SWCREATE", string.Empty, "Unknown Equipment Control and Automation Application");
                return string.Empty;
            }
            set
            {
                AddKeyword("SWCREATE", value, "[name] Equipment Control and Automation Application");
            }
        }
        public DateTime CaptureTime
        {
            get => KeywordList.CaptureTime;
            set => KeywordList.CaptureTime = value;
        }
        public string CBIAS
        {
            get => KeywordList.CBIAS;
            set => KeywordList.CBIAS = value;
        }
        public string CDARK
        {
            get => KeywordList.CDARK;
            set => KeywordList.CDARK = value;
        }
        public string CFLAT
        {
            get => KeywordList.CFLAT;
            set => KeywordList.CFLAT = value;
        }
        public string CPANEL
        {
            get => KeywordList.CPANEL;
            set => KeywordList.CPANEL = value;
        }
        public string CSTARS
        {
            get => KeywordList.CSTARS;
            set => KeywordList.CSTARS = value;
        }
        public double ExposureSeconds
        {
            get => KeywordList.ExposureSeconds;
            set => KeywordList.ExposureSeconds = value;
        }
        public string FilePath { get; set; } = string.Empty;
        public string FilterName
        {
            get => KeywordList.FilterName;
            set => KeywordList.FilterName = value;
        }
        public double FocalLength
        {
            get => KeywordList.FocalLength;
            set => KeywordList.FocalLength = value;
        }
        public int FocuserPosition
        {
            get => KeywordList.FocuserPosition;
            set => KeywordList.FocuserPosition = value;
        }
        public double FocuserTemperature
        {
            get => KeywordList.FocuserTemperature;
            set => KeywordList.FocuserTemperature = value;
        }
        public eFrame FrameType
        {
            get => KeywordList.FrameType;
            set
            {
                switch (value)
                {
                    case eFrame.LIGHT:
                        AddKeyword("IMAGETYP", "Light", "Type of frame capture");
                        break;
                    case eFrame.DARK:
                        AddKeyword("IMAGETYP", "Dark", "Type of frame capture");
                        break;
                    case eFrame.FLAT:
                        AddKeyword("IMAGETYP", "Flat", "Type of frame capture");
                        break;
                    case eFrame.BIAS:
                        AddKeyword("IMAGETYP", "Bias", "Type of frame capture");
                        break;
                    default:
                        AddKeyword("IMAGETYP", "", "Type of frame capture");
                        break;
                }
            }
        }
        public int Gain
        {
            get => KeywordList.Gain;
            set => KeywordList.Gain = value;
        }
        public int IccAttachmentLength { get; set; }
        public int IccAttachmentStart { get; set; }
        public int IccAttachmentPadding { get; set; }
        public int TargetAttachmentLength { get; set; }
        public int TargetAttachmentStart { get; set; }
        public int TargetAttachmentNewStart { get; set; }
        public int TargetAttachmentPadding { get; set; }
        public int TargetAttachmentRejectionHighLength { get; set; }
        public int TargetAttachmentRejectionHighStart { get; set; }
        public int TargetAttachmentRejectionHighNewStart { get; set; }
        public int TargetAttachmentRejectionHighPadding { get; set; }
        public int TargetAttachmentRejectionLowLength { get; set; }
        public int TargetAttachmentRejectionLowStart { get; set; }
        public int TargetAttachmentRejectionLowNewStart { get; set; }
        public int TargetAttachmentRejectionLowPadding { get; set; }
        public int FileNameNumberIndex { get; set; }
        public int Offset
        {
            get => KeywordList.Offset;
            set => KeywordList.Offset = value;
        }
        public string MSTRALG
        {
            get => KeywordList.MSTRALG;
            set => KeywordList.MSTRALG = value;
        }
        public string RotationAngle
        {
            get
            {
                double angle = RotatorSkyAngle;
                if (angle != double.MinValue)
                    return "S" + angle.FormatRotationAngle();

                angle = RotatorPosition;
                if (angle != double.MinValue)
                    return "M" + angle.FormatRotationAngle();

                return string.Empty;
            }
        }

        public double RotatorPosition
        {
            get => KeywordList.RotatorPosition;
            set => KeywordList.RotatorPosition = value;
        }
        public double RotatorSkyAngle
        {
            get => KeywordList.RotatorSkyAngle;
            set => KeywordList.RotatorSkyAngle = value;
        }
        public double SensorTemperature
        {
            get => KeywordList.SensorTemperature;
            set => KeywordList.SensorTemperature = value;
        }
        public double SSWeight { get; set; }
        public int ThumbnailAttachmentPadding { get; set; }
        public int ThumbnailAttachmentLength { get; set; }
        public int ThumbnailAttachmentStart { get; set; }
        public int ThumbnailAttachmentNewStart { get; set; }
        public bool KeepPanel { get; set; }
        /// <summary>
        /// Updates an Xisf File Target Name.
        /// Appends " Stars" if the Xisf File is contained in a "Stars Filter" Directory.
        /// Sets CPANEL to the returned string value. Set CSTARS when containing directory name contains "Stars ".
        /// </summary>
        /// <returns>The TargetName string</returns>
        public string TargetName
        {
            get { return KeywordList.TargetName; }
            set
            {
                string targetName = value;

                targetName = Regex.Replace(targetName, @"\s+", " "); // Replace multiple spaces with a single space

                // First match Stars
                if (Path.GetDirectoryName(FilePath).Contains("Stars "))
                {
                    KeywordList.CSTARS = "Stars " + KeywordList.FilterName;
                    // Make sure files in "Stars" directories are properly named: "Target Stars"
                    if (!targetName.Contains("Stars"))
                        targetName = targetName + " Stars";
                }
                else
                    KeywordList.RemoveKeyword("CSTARS"); // Wildcard by CPANEL

                // After Stars, replace a TargetName containing the word "Panel" with "P" followed by one or more digits
                // Return original string if replacement fails.
                targetName = Regex.Replace(targetName, @"\bPanel\b", "P");
                targetName = Regex.Replace(targetName, @"(?<=[A-Za-z0-9\s-])\s*P(\d+)$", " P$1");

                if (!KeepPanel)
                    KeywordList.CPANEL = targetName.Replace(" Stars", ""); // Remove " Stars" so we Register and Intergrate by CPanel
                else
                    KeywordList.CPANEL = targetName;

                KeywordList.TargetName = targetName;
            }
        }
        public string Telescope
        {
            get => KeywordList.Telescope;
            set => KeywordList.Telescope = value;
        }
        public int MSTRFRMS
        {
            get => KeywordList.MSTRFRMS;
            set => KeywordList.MSTRFRMS = value;
        }
        public List<string> WeightKeyword
        {
            get { return KeywordList.WeightKeyword; }
        }

        // ***********************************************************************************************************************************

        // Access these properties from anything that instances the XifsFile class

        // ************************************************************************************************
        // ************************************************************************************************

        public void ImageAttachment(XElement element)
        {
            XAttribute attribute = element.Attribute("location");

            if (attribute != null)
            {
                string attachment = attribute.Value;

                string[] values = attachment.Split(':');

                TargetAttachmentStart = Convert.ToInt32(values[1]);
                TargetAttachmentLength = Convert.ToInt32(values[2]);
            }
        }

        public void ImageRejectionHighAttachment(XElement element)
        {
            XAttribute attribute = element.Attribute("location");

            if (attribute != null)
            {
                string attachment = attribute.Value;

                string[] values = attachment.Split(':');

                TargetAttachmentRejectionHighStart = Convert.ToInt32(values[1]);
                TargetAttachmentRejectionHighLength = Convert.ToInt32(values[2]);
            }
        }

        public void ImageRejectionLowAttachment(XElement element)
        {
            XAttribute attribute = element.Attribute("location");

            if (attribute != null)
            {
                string attachment = attribute.Value;

                string[] values = attachment.Split(':');

                TargetAttachmentRejectionLowStart = Convert.ToInt32(values[1]);
                TargetAttachmentRejectionLowLength = Convert.ToInt32(values[2]);
            }
        }

        public void IccAttachment(XElement element)
        {
            XAttribute attribute = element.Attribute("location");

            if (attribute != null)
            {
                string attachment = attribute.Value;

                if (attachment.Contains("inline"))
                {
                    IccAttachmentStart = -1;     // ICC is inlined
                    IccAttachmentLength = -1;    // ICC is inlined
                    return;
                }

                string[] values = attachment.Split(':');

                IccAttachmentStart = Convert.ToInt32(values[1]);
                IccAttachmentLength = Convert.ToInt32(values[2]);
            }
        }

        public void ThumbnailAttachment(XElement element)
        {
            XAttribute attribute = element.Attribute("location");

            if (attribute != null)
            {
                string attachment = attribute.Value;

                if (attachment.Contains("inline"))
                {
                    ThumbnailAttachmentStart = -1;     // Thumbnail is inlined
                    ThumbnailAttachmentLength = -1;    // Thumbnail is inlined
                    return;
                }

                string[] values = attachment.Split(':');

                ThumbnailAttachmentStart = Convert.ToInt32(values[1]);
                ThumbnailAttachmentLength = Convert.ToInt32(values[2]);
            }
        }

        public static Keyword NewKeyWord(string sName, string oValue, string sComment)
        {
            Keyword newKeyword = new Keyword
            {
                Name = sName,
                Value = oValue,
                Comment = sComment
            };

            return newKeyword;
        }

        // ----------------------------------------------------------------------------------------------------------
        public void AddKeywordKeepDuplicates(string sName, string oValue, string sComment = "XISF File Manager")
        {
            Keyword newKeyword = NewKeyWord(sName, oValue, sComment);

            KeywordList.AddKeywordKeepDuplicates(newKeyword);
        }

        public void AddXMLKeyword(XElement element)
        {
            // It looks like NINA 3 may have added a fourth XML Keyword string field called "xmlns=xxx'. We should ignore this field
            // Example: "{<FITSKeyword name="IMAGETYP" value="LIGHT" comment="Type of exposure" xmlns="http://www.pixinsight.com/xisf" />}"

            string elementName = string.Empty;
            string elementValue = string.Empty;
            string elementComment = string.Empty;

            try
            {
                // Split the XML Keyword triple into elementName, elementValue, elementComment
                elementName = element.Attribute("name").Value.Trim();
                elementValue = element.Attribute("value").Value.Trim();
                elementComment = element.Attribute("comment").Value.Trim();

                // Get rid of an extra decimal point at the end of what should be integers
                elementValue = elementValue.TrimEnd('.');

                AddKeywordKeepDuplicates(elementName, elementValue, elementComment);
            }
            catch (Exception ex)
            {
                Console.WriteLine("\nXML to Keyword Exception Thrown:\n" +
                    "\tKeyword Name:" + elementName.ToString() +
                    "\tKeyword Value:" + elementValue.ToString() +
                    "\tKeyword Comment:" + elementComment.ToString() +
                    "\n\n" + ex.Message);
            }
        }

        public void ParseProperties(XElement property)
        {
            string propertyId = property.Attribute("id")?.Value;
            string propertyType = property.Attribute("type")?.Value;
            string propertyValue = property.Attribute("value")?.Value;
            string value = property.Value; // specified as a floating string

            // Handle the property based on its ID
            switch (propertyId)
            {
                case "XISF:BlockAlignmentSize":
                    BlockAlignmentSize = Convert.ToInt32(propertyValue);
                    break;

                default:
                    break;
            }
        }

        // *********************************************************************************************************
        // *********************************************************************************************************
        // Set Who and Where
        public void SetObservationSite()
        {
            AddKeyword("SITENAME", "Penns Park, PA", "841 Durham Rd, Penns Park, PA 18943");
            AddKeyword("SITELONG", "-74.997372", "Logitude of observation site - Degrees East");
            AddKeyword("SITELAT", "40.282852", "Latitude of observation site - Degrees North");
            AddKeyword("SITEELEV", "80.0", "Altitude of observation site - MSL Meters");
            RemoveKeyword("LONG-OBS");
            RemoveKeyword("LAT-OBS");
            RemoveKeyword("ALT-OBS");
            RemoveKeyword("OBSGEO-L");
            RemoveKeyword("OBSGEO-B");
            RemoveKeyword("OBSGEO-H");

            AddKeyword("OBSERVER", "Dan Stark", "P.O. Box 156, Penns Park, PA 18943 djstark@gmail.com (609) 575-5927");
        }
    }

    public static class XisfFileExtensions
    {
        public static string FormatTemperature(this double temperatureValue)
        {
            string fmt = "{00:+00.0;-00.0;+00.0}";
            return string.Format(fmt, Math.Round(temperatureValue, 1));
        }

        public static string FormatRotationAngle(this double rotationAngle)
        {
            return rotationAngle.ToString("000.0");
        }

        public static string FormatExposureTime(this double seconds)
        {
            if (seconds < 10.0)
            {
                if (seconds < 0.00001) // Less than 10 microseconds
                    return "0";

                if (seconds < 1) // Between 10 microseconds and 1 second
                                 //    return ((decimal)seconds / 1.000000000000000000000000000000000m).ToString("0.0####");
                    return seconds.ToString("0.0####"); // Tens of microseconds

                return seconds.ToString("0.0####"); // Milliseconds
            }
            else
            {
                return seconds.ToString("0");
            }
        }
    }
}