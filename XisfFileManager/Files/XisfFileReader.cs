using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using XisfFileManager.XML;

namespace XisfFileManager.Files
{
    public class XisfFileReader
    {
        private byte[] mBuffer = new byte[65536];
        private int bytesRead;

        private Match xmlVersionBlockMatch;
        private Match xmlCommentBlockMatch;
        private Match xmlKeywordBlockMatch;

        public async Task ReadXisfFileHeaderKeywords(XisfFile xFile)
        {
            await Task.Run(async () =>
            {
                using (FileStream xFileStream = new(xFile.FilePath, FileMode.Open, FileAccess.Read))
                {
                    // Try to read the minium amount of data from each Xisf File.
                    // mBuffer size has been set read most Xisf files xml section in a single read pass.
                    // We MUST read enough to include the first "<xisf" delimiter (<xisf is after comment section)

                    xmlVersionBlockMatch = Match.Empty;
                    xmlCommentBlockMatch = Match.Empty;
                    xmlKeywordBlockMatch = Match.Empty;

                    bytesRead = 0;
                    int nXisfSignatureBlockSize = 16;
                    string xmlString;

                    // Read the first 16 bytes of the file
                    bytesRead = xFileStream.Read(mBuffer, 0, nXisfSignatureBlockSize);
                    if (bytesRead != 16)
                        return;

                    // Find the length of the <xisf>...</xisf> section
                    int xisfSectionSize = (mBuffer[11] << 24);
                    xisfSectionSize = (mBuffer[10] << 16);
                    xisfSectionSize |= (mBuffer[9] << 8);
                    xisfSectionSize |= mBuffer[8];

                    if (xisfSectionSize > 65535)
                    {
                        mBuffer = new byte[xisfSectionSize + nXisfSignatureBlockSize];
                    }

                    bytesRead = xFileStream.Read(mBuffer, nXisfSignatureBlockSize, xisfSectionSize);

                    xmlString = Encoding.UTF8.GetString(mBuffer.Skip(nXisfSignatureBlockSize).ToArray());

                    xmlVersionBlockMatch = Regex.Match(xmlString, @"<\?xml[\s\S]*?\?>");
                    xmlCommentBlockMatch = Regex.Match(xmlString, @"<!--[\s\S]*?-->");
                    xmlKeywordBlockMatch = Regex.Match(xmlString, @"<xisf[\s\S]*?xisf>");

                    // return <xisf>...</xisf> section
                    xmlString = xmlKeywordBlockMatch.ToString();

                    // Remove any blatent garbage from xmlString
                    xmlString = Xml.FixXisfXml(xmlString);

                    // Remove any malformed xml from xmlString
                    xmlString = Xml.ValidateXisfXml(xmlString);

                    // Make an isolated copies
                    xFile.XmlVersionText = xmlVersionBlockMatch.ToString().Clone() as string;
                    xFile.XmlCommentText = xmlCommentBlockMatch.ToString().Clone() as string;
                    xFile.XmlString = xmlString.Clone() as string;

                    xFile.mXDoc = new XDocument();

                    try
                    {
                        xFile.mXDoc = XDocument.Parse(xmlString);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Could not Parse xml in file:\n\n" + xFile.FilePath + "\n\nin:\n\nReadXisfFileHeaderKeywords(XisfFile xFile)\n" + ex.Message,
                            "Parse XISF File Error",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Error);

                        return;
                    }

                    // ***********************************************************************************

                    XElement root = xFile.mXDoc.Root;
                    XNamespace ns = root.GetDefaultNamespace();

                    FindXisfAttachments(xFile, ns);

                    FindXisfIccProfiles(xFile, ns);

                    FindXisfThumbnails(xFile, ns);

                    FindXisfFitsKeywords(xFile, ns);

                    FindXisfProperties(xFile, ns);

                    // ***********************************************************************************
                }
            });
        }

        // ***********************************************************************************

        public static void FindXisfAttachments(XisfFile xFile, XNamespace ns)
        {
            // Create a collection of XML "Image" elements from the xFile.mXDoc document that either have no "id" attribute or
            // have an "id" attribute with a value of "integration", "rejection_high", or "rejection_low"

            HashSet<string> validIds = ["integration", "rejection_high", "rejection_low"];

            IEnumerable<XElement> imageElements = xFile.mXDoc.Descendants(ns + "Image")
                                 .Where(element =>
                                 {
                                     string id = (string)element.Attribute("id");
                                     return id == null || validIds.Contains(id);
                                 });

            foreach (XElement element in imageElements)
            {
                string idValue = element.Attribute("id")?.Value;

                switch (idValue)
                {
                    case null:
                    case "integration":
                        xFile.ImageAttachment(element);
                        break;
                    case "rejection_high":
                        xFile.ImageRejectionHighAttachment(element);
                        break;
                    case "rejection_low":
                        xFile.ImageRejectionLowAttachment(element);
                        break;
                }
            }
        }

        // ***********************************************************************************

        public static void FindXisfIccProfiles(XisfFile xFile, XNamespace ns)
        {
            XElement iccProfileElement = xFile.mXDoc.Descendants(ns + "ICCProfile").FirstOrDefault();
            if (iccProfileElement != null)
            {
                xFile.IccAttachment(iccProfileElement);
            }
        }

        // ***********************************************************************************

        public static void FindXisfThumbnails(XisfFile xFile, XNamespace ns)
        {
            XElement thumbnailElement = xFile.mXDoc.Descendants(ns + "Thumbnail").FirstOrDefault();
            if (thumbnailElement != null)
            {
                xFile.ThumbnailAttachment(thumbnailElement);
            }
        }

        // ***********************************************************************************

        public static void FindXisfFitsKeywords(XisfFile xFile, XNamespace ns)
        {
            // Place all XML formated FITS Keyword Name, Value, Comment triples into 'elements'
            IEnumerable<XElement> elements = xFile.mXDoc.Descendants(ns + "FITSKeyword");
            foreach (XElement element in elements)
            {
                xFile.AddXMLKeyword(element);
            }
        }

        // ***********************************************************************************

        public static void FindXisfProperties(XisfFile xFile, XNamespace ns)
        {
            // Extract various property values from the XISF file
            IEnumerable<XElement> properties = xFile.mXDoc.Descendants(ns + "Property");
            foreach (XElement property in properties)
            {
                xFile.ParseProperties(property);
            }
        }

        // ***********************************************************************************
        // ***********************************************************************************
    }
}
