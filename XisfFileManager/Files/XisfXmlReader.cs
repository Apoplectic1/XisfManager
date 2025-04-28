using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Windows.Foundation;
using XisfFileManager.Files.XML;

namespace XisfFileManager.Files
{
    public class XisfXmlReader
    {
        private byte[] mBuffer = new byte[65536];
        private int mBytesRead;
        private Match mXmlXisfBlockMatch;
        private Match mXmlMetadataBlockMatch;

        // ***********************************************************************************
        // ***********************************************************************************

        /// <summary>
        /// Reads the header keywords from an XISF file asynchronously.
        /// The method processes the file to extract XML sections and header keywords, and parses them into an XDocument.
        /// </summary>
        /// <param name="xFile">The XisfFile object representing the file to read.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        public async Task ReadXisfFileHeaderKeywords(XisfFile xFile)
        {
            xFile.bModified = false;
            bool modified;
            int masterFrames;
            string masterAlgo;

            await Task.Run(async () =>
            {
                using (FileStream xFileStream = new(xFile.FilePath, FileMode.Open, FileAccess.Read))
                {
                    // Setup to match XISF Header Blocks
                    mXmlXisfBlockMatch = Match.Empty;

                    mBytesRead = 0;
                    int nXisfSignatureBlockSize = 16;
                    string xmlString;

                    // Read the first 16 bytes of the file - XISF Signature
                    mBytesRead = xFileStream.Read(mBuffer, 0, nXisfSignatureBlockSize);
                    if (mBytesRead != 16)
                        return;

                    // Find the size of the <xisf>...</xisf> section
                    // The 4 mBuffer bytes are big-endian 
                    // When added to the Header Signature, this will point to the byte after "</xisf>"
                    int xisfSectionSize = (mBuffer[11] << 24) | (mBuffer[10] << 16) | (mBuffer[9] << 8) | mBuffer[8];

                    // Resize buffer if necessary
                    if (xisfSectionSize > 65535)
                    {
                        mBuffer = new byte[xisfSectionSize + nXisfSignatureBlockSize];
                    }

                    // Read the rest of the <xisf>...</xisf> section
                    mBytesRead = xFileStream.Read(mBuffer, nXisfSignatureBlockSize, xisfSectionSize);
                    xmlString = Encoding.UTF8.GetString(mBuffer.Skip(nXisfSignatureBlockSize).ToArray());

                    // ******************************************************************************************
                    // Clean up and validate xmlString

                    // This should not be needed once all xisf files have been passed throught this application
                    (xmlString, modified) = Xml.FixXisfXml(xmlString);
                    //xFile.bModified |= modified;

                    // This should not be needed once all xisf files have been passed throught this application
                    (xmlString, modified) = Xml.ValidateXisfXml(xmlString);
                    //xFile.bModified |= modified;

                    // Remove rejection image blocks
                    (xmlString, modified) = Xml.RemoveImage_ById(xmlString, "rejection_high");
                    xFile.bModified |= modified;

                    (xmlString, modified) = Xml.RemoveImage_ById(xmlString, "rejection_low");
                    xFile.bModified |= modified;

                    // This needs to be before Xml.RemoveImageProperties_ById(xmlString, "integration");
                    (masterFrames, masterAlgo) = Xml.GetMasterFrameKeywords(xmlString, "integration");

                    (xmlString, modified) = Xml.RemoveImageProperties_ById(xmlString, "integration"); // Includdes ProcessHistory
                    xFile.bModified |= modified;


                    // ******************************************************************************************

                    // Match and store copies of various Xisf file xml blocks
                    xFile.XmlVersionText = Xml.XisfFile_XmlVersionBlockRx.Match(xmlString).Value;
                    xFile.XmlCommentText = Xml.XisfFile_CommentBlockRx.Match(xmlString).Value;

                    (xFile.TargetAttachmentStart, xFile.TargetAttachmentLength, xFile.TargetAttachmentWidth, xFile.TargetAttachmentHeight)
                        = Xml.GetImageExtents(xmlString, "integration");

                    mXmlXisfBlockMatch = Xml.XisfFile_XisfBlockRx.Match(xmlString);
                    mXmlMetadataBlockMatch = Xml.XisfFile_MetadataBlockRx.Match(xmlString);

                    // Convert the <xisf>...</xisf> section to a string
                    xmlString = mXmlXisfBlockMatch.Value;
                    xFile.XmlString = xmlString;

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

                    FindXisfIccProfiles(xFile, ns);
                    FindXisfThumbnails(xFile, ns);

                    // It looks like PixInsight wants to move away from FITS Keywords and use Properties instead
                    FindXisfFitsKeywords(xFile, ns);

                    if (masterFrames > 0)
                    {
                        // We just need to add to the KeywordList which will be copied to the updated file
                        xFile.AddKeyword("MSTRFRMS", masterFrames.ToString());
                        xFile.AddKeyword("MSTRALG", masterAlgo);
                    }
                }
            });
        }


        // ***********************************************************************************
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

            // read each element and create a local copy in a keywordList
            foreach (XElement element in elements)
            {
                xFile.AddXMLKeyword(element);
            }
        }

        // ***********************************************************************************
        // ***********************************************************************************
    }
}
