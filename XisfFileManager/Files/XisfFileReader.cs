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
        private Match xmlXisfBlockMatch;
        private Match xmlImageBlockMatch;
        private Match xmlMetadataBlockMatch;

        /// <summary>
        /// Reads the header keywords from an XISF file asynchronously.
        /// The method processes the file to extract XML sections and header keywords, and parses them into an XDocument.
        /// </summary>
        /// <param name="xFile">The XisfFile object representing the file to read.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task ReadXisfFileHeaderKeywords(XisfFile xFile)
        {
            xFile.Modified = false;

            await Task.Run(async () =>
            {
                using (FileStream xFileStream = new(xFile.FilePath, FileMode.Open, FileAccess.Read))
                {
                    // Initialize regex matches and buffer
                    xmlVersionBlockMatch = Match.Empty;
                    xmlCommentBlockMatch = Match.Empty;
                    xmlXisfBlockMatch = Match.Empty;

                    bytesRead = 0;
                    int nXisfSignatureBlockSize = 16;
                    string xmlString;

                    // Read the first 16 bytes of the file
                    bytesRead = xFileStream.Read(mBuffer, 0, nXisfSignatureBlockSize);
                    if (bytesRead != 16)
                        return;

                    // Calculate the length of the <xisf>...</xisf> section
                    int xisfSectionSize = (mBuffer[11] << 24) | (mBuffer[10] << 16) | (mBuffer[9] << 8) | mBuffer[8];

                    // Resize buffer if necessary
                    if (xisfSectionSize > 65535)
                    {
                        mBuffer = new byte[xisfSectionSize + nXisfSignatureBlockSize];
                    }

                    // Read the rest of the <xisf>...</xisf> section
                    bytesRead = xFileStream.Read(mBuffer, nXisfSignatureBlockSize, xisfSectionSize);
                    xmlString = Encoding.UTF8.GetString(mBuffer.Skip(nXisfSignatureBlockSize).ToArray());

                    // Match XML version, comment, and keyword blocks
                    xmlVersionBlockMatch = Regex.Match(xmlString, @"<\?xml[\s\S]*?\?>");
                    xmlCommentBlockMatch = Regex.Match(xmlString, @"<!--[\s\S]*?-->"); 
                    xmlXisfBlockMatch = Regex.Match(xmlString, @"<xisf[\s\S]*?xisf>");
                    xmlImageBlockMatch = Regex.Match(xmlString, @"<Image[\s\S]*?Image>");
                    xmlMetadataBlockMatch = Regex.Match(xmlString, @"<Metadata[\s\S]*?Metadata>");


                    // Convert the <xisf>...</xisf> section to a string
                    xmlString = xmlXisfBlockMatch.ToString();

                    // Clean up and validate XML string
                    var result = Xml.FixXisfXml(xmlString);
                    xFile.Modified = result.Modified;
                    xmlString = Xml.ValidateXisfXml(result.FixedXml);

                    // Store isolated copies of XML sections
                    xFile.XmlVersionText = xmlVersionBlockMatch.ToString().Clone() as string;
                    xFile.XmlCommentText = xmlCommentBlockMatch.ToString().Clone() as string;
                    xFile.XmlString = xmlString.Clone() as string;

                    // Parse XML string into XDocument
                    xFile.mXDoc = new XDocument();

                    try
                    {
                        xFile.mXDoc = XDocument.Parse(xmlString);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Could not parse XML in file:\n\n" + xFile.FilePath + "\n\nin:\n\nReadXisfFileHeaderKeywords(XisfFile xFile)\n" + ex.Message,
                            "Parse XISF File Error",
                            MessageBoxButtons.OKCancel,
                            MessageBoxIcon.Error);
                        return;
                    }

                    // Extract and process various elements from the XISF file
                    XElement root = xFile.mXDoc.Root;
                    XNamespace ns = root.GetDefaultNamespace();

                    FindXisfAttachments(xFile, ns);
                    FindXisfIccProfiles(xFile, ns);
                    FindXisfThumbnails(xFile, ns);
                    FindXisfFitsKeywords(xFile, ns);
                    FindXisfProperties(xFile, ns);
                }
            });
        }


        // ***********************************************************************************

        /// <summary>
        /// Finds and processes image attachments in an XISF file document.
        /// The method identifies "Image" elements in the document that either have no "id" attribute or
        /// have an "id" attribute with values "integration", "rejection_high", or "rejection_low",
        /// and processes them accordingly.
        /// </summary>
        /// <param name="xFile">The XisfFile object containing the XML document to be processed.</param>
        /// <param name="ns">The XML namespace used in the XISF file.</param>
        public static void FindXisfAttachments(XisfFile xFile, XNamespace ns)
        {
            // Define a set of valid IDs for image elements
            HashSet<string> validIds = new HashSet<string> { "integration", "rejection_high", "rejection_low" };

            // Find all "Image" elements that either have no "id" attribute or have a valid "id" attribute
            var imageElements = xFile.mXDoc.Descendants(ns + "Image").Where(element => element.Attribute("id") == null || validIds.Contains((string)element.Attribute("id")));

            // Find each <Image ... >...</Image> 
            imageElements = xFile.mXDoc.Descendants(ns + "Image");

            // Process each found image element based on its "id" attribute
            imageElements.ToList().ForEach(element =>
            {
                string idValue = (string)element.Attribute("id");

                switch (idValue)
                {
                    case "rejection_high":
                        xFile.ImageRejectionHighAttachment(element);
                        break;
                    case "rejection_low":
                        xFile.ImageRejectionLowAttachment(element);
                        break;
                    
                    default:
                        xFile.ImageAttachment(element);
                        break;
                }
            });
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
