using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using XisfFileManager.Enums;
using XisfFileManager.XML;

namespace XisfFileManager.Files
{
    public class XisfFileUpdate
    {
        private Buffer mBuffer;
        private List<Buffer> mBufferList;

        public bool UpdateFile2(string sourceFile, string destinationPath)
        {
            int delay = 0;
            while (IsFileLocked(sourceFile) && delay < 100)
                delay++;

            if (delay == 100)
            {
                MessageBox.Show("File is locked", sourceFile, MessageBoxButtons.OK);
                return false;
            }

            int xmlStart;
            int xisfStart;
            int xmlEnd;

            byte[] binaryFileData = new byte[(int)1e9];
            mBufferList = new List<Buffer>();

            try
            {
                using (Stream stream = new FileStream(sourceFile, FileMode.Open))
                {
                    // *******************************************************************************************************************************
                    // *******************************************************************************************************************************

                    // Read entire XISF file (up to 1 GB) into binaryFileData
                    BinaryReader br = new BinaryReader(stream);
                    binaryFileData = br.ReadBytes((int)1e9);
                    br.Close();

                    // *******************************************************************************************************************************
                    // *******************************************************************************************************************************

                    xmlStart = BinaryFind(binaryFileData, "<?xml "); // returns the position of '<'
                    xisfStart = BinaryFind(binaryFileData, "<xisf "); // returns the position of '<'
                    xmlEnd = BinaryFind(binaryFileData, "</xisf>") + "</xisf>".Length;  // returns the position immediately after '>'

                    string xmlString = Encoding.UTF8.GetString(binaryFileData, xisfStart, xmlEnd);

                    // Remove anything after </xisf> in xmlString
                    xmlString = xmlString.Substring(0, xmlString.IndexOf("</xisf>") + "</xisf>".Length);

                    // Remove all non-ASCII characters
                    xmlString = Regex.Replace(xmlString, @"[^\x00-\x7F]", "");

                    // Some XISF files have single quotes inside FITS Keywords - Remove them
                    xmlString = Regex.Replace(xmlString, @"'", "");

                    // Remove Processing History Property if it exists
                    string pattern = Regex.Escape("<Property") + @"(.*?)" + Regex.Escape(";</Property>");
                    xmlString = Regex.Replace(xmlString, pattern, "");

                    // *******************************************************************************************************************************
                    // *******************************************************************************************************************************

                    string attachment = ExtractValue(xmlString, "location=\"", "\"");
                    string[] attachmentParts = attachment.Split(':');
                    string start = attachmentParts[1];
                    string length = attachmentParts[2];

                    string sampleFormat = ExtractValue(xmlString, "sampleFormat=\"", "\"");
                    string width = ExtractValue(xmlString, "name=\"NAXIS1\"", "value=\"", "\"");
                    string height = ExtractValue(xmlString, "name=\"NAXIS2\"", "value=\"", "\"");
                    string blockAlignmentSize = ExtractValue(xmlString, "id=\"XISF:BlockAlignmentSize\"", "value=\"", "\"");

                    // *******************************************************************************************************************************
                    // *******************************************************************************************************************************

                    // Create a fresh Buffer List. The list will contain the XISF Signature and length, the xml, and the image data
                    // This ordered List sets up pointers to the data to by written, its type, and length

                    mBufferList.Clear();

                    // Copy from INPUT file start to the end of the XML
                    mBuffer = new Buffer
                    {
                        Type = eBufferData.BINARY,
                        BinaryDataStart = 0,
                        BinaryByteLength = Convert.ToInt32(start),
                        BinaryData = binaryFileData
                    };
                    mBufferList.Add(mBuffer);

                    // Main Image
                    // In INPUT file, add the binary image data from binaryFileData
                    mBuffer = new Buffer
                    {
                        Type = eBufferData.BINARY,
                        BinaryDataStart = Convert.ToInt32(start),
                        BinaryByteLength = Convert.ToInt32(length),
                        BinaryData = binaryFileData
                    };
                    mBufferList.Add(mBuffer);

                    // Ignore any other attachments (e.g. Thumbnail, High Rejection, Low Rejection, etc,) in the input file

                    // *******************************************************************************************************************************
                    // *******************************************************************************************************************************

                    // Now that the mBuffer List is done, use it to write a new/updated XISF File
                    bool bStatus = WriteBinaryFile(destinationPath);
                    if (bStatus == false)
                        return false;
                }
            }
            catch
            {
                DialogResult status = MessageBox.Show("Update Write File Failed", destinationPath, MessageBoxButtons.OKCancel);
                if (status == DialogResult.OK)
                    return false;
                Environment.Exit(-1);
            }

            return true;
        }

        private static string ExtractValue(string source, string startToken, string endToken)
        {
            int start = source.IndexOf(startToken) + startToken.Length;
            if (start < startToken.Length) return "";

            int end = source.IndexOf(endToken, start);
            if (end == -1) return "";

            return source.Substring(start, end - start).Trim();
        }

        private static string ExtractValue(string source, string preToken, string startToken, string endToken)
        {
            int preStart = source.IndexOf(preToken);
            if (preStart == -1) return "";

            return ExtractValue(source.Substring(preStart), startToken, endToken);
        }

        // ##############################################################################################################################################
        // ##############################################################################################################################################

        public bool UpdateFile(XisfFile xFile, string destinationPath)
        {
            int delay = 0;
            while (IsFileLocked(xFile.FilePath) && delay < 100)
                delay++;

            if (delay == 100)
            {
                MessageBox.Show("File is locked", xFile.FilePath, MessageBoxButtons.OK);
                return false;
            }

            // Return if KeywordList and the original xml are identical 
            if (xFile.KeywordUpdateMode == eKeywordUpdateMode.PROTECT)
                return false;
            else
                if (xFile.KeywordUpdateMode == eKeywordUpdateMode.UPDATE_NEW)
                if (KeywordsMatchXml(xFile))
                    return true;

            // Not identical or force, so update the xml

          
            int xisfStart;
            int xisfEnd;

            byte[] binaryFileData = new byte[(int)1e9];
            mBufferList = new List<Buffer>();

            try
            {
                using (Stream stream = new FileStream(xFile.FilePath, FileMode.Open))
                {
                    // *******************************************************************************************************************************
                    // *******************************************************************************************************************************

                    // Read entire XISF file (up to 1 GB) into rawFileData and create an xml document
                    BinaryReader br = new BinaryReader(stream);
                    binaryFileData = br.ReadBytes((int)1e9);
                    br.Close();

                    // *******************************************************************************************************************************
                    // *******************************************************************************************************************************

                    xisfStart = BinaryFind(binaryFileData, "<xisf "); // returns the position of '<'
                    // NOTE: This value will change AFTER Keyword Replacement a few lines below
                    xisfEnd = BinaryFind(binaryFileData, "</xisf>") + "</xisf>".Length;  // returns the position immediately after '>'                

                    // convert from and including <xisf to /xisf> to a string and then parse string as xml into a new doc
                    string xmlString = Encoding.UTF8.GetString(binaryFileData, xisfStart, xisfEnd);

                    // Remove any blatent garbage from xmlString
                    xmlString = Xml.FixXisfXml(xmlString);

                    // Remove any malformed xml from xmlString
                    xmlString = Xml.ValidateXisfXml(xmlString);

                    // Create an Xml Document from the xmlString with the proper namespace
                    XmlDocument xmlDoc = new XmlDocument();
                    XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
                    namespaceManager.AddNamespace("ns", "http://www.pixinsight.com/xisf");

                    // The new  does not include the XmLVersion and PixInsight comment section - Add them
                    xmlDoc.LoadXml(xFile.XmlVersionText + xFile.XmlCommentText + xmlString);

                    // *******************************************************************************************************************************
                    // *******************************************************************************************************************************

                    // Remove all descendants from xFile.mXDoc that contain an "attachment" attribute that do not match the criteria specified by imageNode (our main image)

                    RemoveUnwantedAttachments(xmlDoc);

                    // *******************************************************************************************************************************
                    // *******************************************************************************************************************************

                    // A list of unsed FITSKeywords will be removed. This method should be able to be removed in the future.

                    xFile.KeywordList.RemoveUnwantedKeywords();

                    // Replace all existing FITSKeywords with FITSKeywords from our list (mFile.KeywordList)

                    ReplaceAllFitsKeywords(xmlDoc, xFile);

                    // *******************************************************************************************************************************
                    // *******************************************************************************************************************************

                    // We need to set the start address and length of the image attachement due to any changes in the <xisf> to </xisf> length

                    if (xFile.BlockAlignmentSize != -1)
                    {
                        int possibleBogusImageAttachmentLocation = FindExistingImageStartingLocation(binaryFileData);
                        if (possibleBogusImageAttachmentLocation % xFile.BlockAlignmentSize != 0)
                        {
                            if ((possibleBogusImageAttachmentLocation - 1) % xFile.BlockAlignmentSize != 0)
                                MessageBox.Show(Path.GetFileName(xFile.FilePath), "Image Attachment Start Location was not Block Aligned. Continuing...");
                        }
                    }
                    xFile.TargetAttachmentPadding = SetImageAttachmentLocation(xmlDoc, xFile);

                    // *******************************************************************************************************************************
                    // *******************************************************************************************************************************

                    // "XISF0100" is the XISF Signature. The length of the xml section is stored in the 8th and 9th bytes of the signature
                    // Assumes that xmlLength is less than 65536 bytes
                    //                                    X     I     S     F     0     1     0     0     0     0     0     0     0     0     0     0
                    byte[] xisfSignature = new byte[16] { 0x58, 0x49, 0x53, 0x46, 0x30, 0x31, 0x30, 0x30, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

                    // Set the new length of the new <xisf to /xisf> section
                    int xmlLength = xmlDoc.OuterXml.Length;

                    // Change Endianess
                    // Find the length of the <xisf>...</xisf> section
                    xisfSignature[8] = (byte)(xmlLength & 0xFF); // Least significant byte
                    xisfSignature[9] = (byte)((xmlLength >> 8) & 0xFF); // Most significant byte

                    // *******************************************************************************************************************************
                    // *******************************************************************************************************************************

                    // Create a Buffer List. The list will contain the XISF Signature and length, the xml, and the image data
                    // This ordered List sets up pointers to the data to by written, its type, and length

                    mBufferList.Clear();

                    // XISF Signature and length
                    mBuffer = new Buffer
                    {
                        Type = eBufferData.BINARY,
                        BinaryDataStart = 0,
                        BinaryByteLength = 16,
                        BinaryData = xisfSignature
                    };
                    mBufferList.Add(mBuffer);

                    // Copy "<?xml" to  "/Xisf>"
                    mBuffer = new Buffer
                    {
                        Type = eBufferData.ASCII,
                        AsciiData = xmlDoc.OuterXml
                    };
                    mBufferList.Add(mBuffer);

                    // Main Image
                    // In OUTPUT file, pad from current position to the start of image data
                    mBuffer = new Buffer
                    {
                        Type = eBufferData.ZEROS,
                        BinaryByteLength = xFile.TargetAttachmentPadding
                    };
                    mBufferList.Add(mBuffer);

                    // Main Image
                    // In INPUT file, Add the binary image data from rawFileData after padding
                    int offset = 0; // 128 * 2; // This was to fix an error I indroduced due to improper padding/Image Attachment Start Location
                    mBuffer = new Buffer
                    {
                        Type = eBufferData.BINARY,
                        BinaryDataStart = xFile.TargetAttachmentStart + offset,
                        BinaryByteLength = xFile.TargetAttachmentLength - offset,
                        BinaryData = binaryFileData
                    };
                    mBufferList.Add(mBuffer);

                    if (offset > 0)
                    {
                        mBuffer = new Buffer
                        {
                            Type = eBufferData.ZEROS,
                            BinaryByteLength = offset
                        };
                        mBufferList.Add(mBuffer);
                    }

                    // Ignore any other attachments (e.g. Thumbnail, High Rejection, Low Rejection, etc,) in the input file

                    // *******************************************************************************************************************************
                    // *******************************************************************************************************************************

                    // Now that the mBuffer List is done, use it to write a new/updated XISF File
                    bool bStatus = WriteBinaryFile(destinationPath);
                    if (bStatus == false)
                        return false;
                }
            }
            catch
            {
                DialogResult status = MessageBox.Show("Update Write File Failed", xFile.FilePath, MessageBoxButtons.OKCancel);
                if (status == DialogResult.OK)
                    return false;
                Environment.Exit(-1);
            }

            return true;
        }

        // ##############################################################################################################################################
        // ##############################################################################################################################################

        private static int GetNewPadding(int length, int alignment)
        {
            // Find the difference between length and the nearest larger value that is evenly divisible by alignment

            if ((length % alignment) == 0)
                return 0;
            else
            {
                int nextdivisible = length + (alignment - length % alignment);
                return nextdivisible - length;
            }
        }

        // ****************************************************************************************************
        // ****************************************************************************************************

        private static void RemoveUnwantedAttachments(XmlDocument document)
        {
            XmlNamespaceManager nsManager = new XmlNamespaceManager(document.NameTable);
            string namespaceUri = document.DocumentElement.NamespaceURI;
            nsManager.AddNamespace("ns", namespaceUri);

            // Select all <Image> elements that have an id attribute with a value that is not 'integration' and then remove these selected elements
            // This keeps <Image id="integration"> and <Image> (no id) elements
            XmlNodeList imageNodesToRemove = document.SelectNodes("//ns:Image[@id!='integration']", nsManager);
            foreach (XmlNode imageNodeToRemove in imageNodesToRemove)
            {
                imageNodeToRemove.ParentNode?.RemoveChild(imageNodeToRemove);
            }

            document.SelectSingleNode("//ns:Thumbnail", nsManager)?.ParentNode?.RemoveChild(document.SelectSingleNode("//ns:Thumbnail", nsManager));
            document.SelectSingleNode("//ns:ICCProfile", nsManager)?.ParentNode?.RemoveChild(document.SelectSingleNode("//ns:ICCProfile", nsManager));
            document.SelectSingleNode("//ns:DisplayFunction", nsManager)?.ParentNode?.RemoveChild(document.SelectSingleNode("//ns:DisplayFunction", nsManager));
        }

        // ****************************************************************************************************
        // ****************************************************************************************************

        public static int FindExistingImageStartingLocation(byte[] binaryFileData)
        {
            // Ignore Image attachment start location, set and return the found location

            // Find the end of the xml section, then walk through any run of 0's after the </xisf> tag
            // Return the location of the first non-zero byte after the </xisf> tag

            int xmlEndLocation = BinaryFind(binaryFileData, "</xisf>") + "</xisf>".Length;
            // Walk through any run of 0's after the </xisf> tag
            int padding = 0;
            for (int i = xmlEndLocation; i < binaryFileData.Length; i++)
            {
                if (binaryFileData[i] == 0)
                    padding++;
                else
                    break;
            }

            return xmlEndLocation + padding;
        }

        // ****************************************************************************************************
        // ****************************************************************************************************

        private static int SetImageAttachmentLocation(XmlDocument document, XisfFile xFile)
        {
            int documentLengthBeforeNewStartingAddress;
            int documentLengthAfterNewStartingAddress;
            int newPadding = -1;
            int newStartingAddress;

            XmlNamespaceManager nsManager = new XmlNamespaceManager(document.NameTable);
            string namespaceUri = document.DocumentElement.NamespaceURI;
            nsManager.AddNamespace("ns", namespaceUri);

            // Find the first (but not only for masters) (ns + "Image") descendant
            XmlNode imageNode = document.SelectSingleNode("//ns:Image", nsManager);

            if (imageNode == null)
                return -1;

            // New Xml document length before padding
            // Note the the new xml document may not contain the comment section or other changes (is the comment section needed?)
            // We have also added or changed lots of keywords in this new xml document

            // Iterate until the xml document length does not change
            do
            {
                documentLengthBeforeNewStartingAddress = document.OuterXml.Length;

                // Include the 16 byte XISF Signature. No Comment section in the padding calculation
                newPadding = GetNewPadding(documentLengthBeforeNewStartingAddress + 16, xFile.BlockAlignmentSize);

                // The starting address is the address in the new file (including XISF Signature); (not just in the Xml document section)
                newStartingAddress = documentLengthBeforeNewStartingAddress + 16 + newPadding;

                // Update imageNode with the new starting address and can change the size of the xml document 
                // 1. This can push the image data to a new location - new start address
                // 2. This can change the padding - new padding
                // If the document length changes, we need to iterate until it does not change (padding will change with each iteration)

                XmlAttribute locationAttribute = imageNode.Attributes["location"];
                if (locationAttribute != null)
                {
                    string[] locationParts = locationAttribute.Value.Split(':');
                    locationParts[1] = newStartingAddress.ToString();
                    locationAttribute.Value = string.Join(":", locationParts);
                }

                documentLengthAfterNewStartingAddress = document.OuterXml.Length;
            }
            while (documentLengthAfterNewStartingAddress != documentLengthBeforeNewStartingAddress);


            return newPadding;
        }

        // ****************************************************************************************************
        // ****************************************************************************************************

        private static void ReplaceAllFitsKeywords(XmlDocument document, XisfFile xFile)
        {
            XmlNodeList nodeList = document.GetElementsByTagName("FITSKeyword");
            for (int i = nodeList.Count - 1; i >= 0; i--)
            {
                nodeList[i].ParentNode.RemoveChild(nodeList[i]);
            }

            // At this point, our xmlDoc only contains header boilerplate

            // Find the "<Image" tag. We are going to add the entire contents of the KeywordData keyword list as 
            // individual child elements under it (and selectively includeing SubFrame Selector keywords)
            nodeList = document.GetElementsByTagName("Image");

            foreach (XmlNode item in nodeList)
            {
                // Alphabetize the KeywordData FITSKeywords
                // mFile.KeywordData.KeywordList contains the FITSKeywords from the original xisf file (with explicit removals and changes)
                List<Keyword> keywords = xFile.KeywordList.mKeywordList.OrderBy(p => p.Name).ToList();

                // Now add all FITSKeywords found in KeywordData to the xmlDocument
                foreach (Keyword keyword in keywords)
                {
                    XmlElement newElement = document.CreateElement("FITSKeyword", document.DocumentElement.NamespaceURI);
                    newElement.SetAttribute("name", keyword.Name);
                    newElement.SetAttribute("comment", keyword.Comment);
                    newElement.SetAttribute("value", keyword.Value.ToString());
                    item.AppendChild(newElement);
                }
            }
        }

        // ****************************************************************************************************
        // ****************************************************************************************************

        private static int BinaryFind(byte[] buffer, string findText)
        {
            byte[] xisfFind = Encoding.UTF8.GetBytes(findText);
            int i;
            int j;
            for (i = 0; i <= (buffer.Length - xisfFind.Length); i++)
            {
                if (buffer[i] == xisfFind[0])
                {
                    for (j = 1; j < xisfFind.Length && buffer[i + j] == xisfFind[j]; j++) ;
                    if (j == xisfFind.Length)
                        return i;
                }
            }

            return -1;
        }

        // ****************************************************************************************************
        // ****************************************************************************************************

        private static bool KeywordsMatchXml(XisfFile xFile)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xFile.XmlString);

            // Create an XmlNamespaceManager and add the necessary namespace
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
            namespaceManager.AddNamespace("ns", "http://www.pixinsight.com/xisf");

            // Select all FITSKeyword elements using the namespace
            XmlNodeList fitsKeywordNodes = xmlDoc.SelectNodes("//ns:FITSKeyword", namespaceManager);


            // Alphabetize KeywordList
            List<Keyword> keywords = xFile.KeywordList.mKeywordList.OrderBy(p => p.Name).ToList();

            // Check if fitsKeywordNodes and keywords have identical elements in the same order
            bool bIdentical = keywords.Count == fitsKeywordNodes.Count && Enumerable.Range(0, keywords.Count)
                                .All(index =>
                                {
                                    XmlNode fitsKeywordNode = fitsKeywordNodes[index];
                                    string nameAttribute = fitsKeywordNode.Attributes["name"]?.Value;
                                    string valueAttribute = fitsKeywordNode.Attributes["value"]?.Value;
                                    string commentAttribute = fitsKeywordNode.Attributes["comment"]?.Value;

                                    Keyword keyword = keywords[index];

                                    return nameAttribute == keyword.Name && valueAttribute == keyword.Value && commentAttribute == keyword.Comment;
                                });
            return bIdentical;

        }

        // ##############################################################################################################################################
        // ##############################################################################################################################################

        private bool WriteBinaryFile(string fileName)
        {
            byte[] zero = { 0x00 };

            try
            {
                using (MemoryStream rawStream = new MemoryStream())
                {
                    using (BinaryWriter binaryWriter = new BinaryWriter(rawStream))
                    {
                        foreach (Buffer buffer in mBufferList)
                        {
                            long position = rawStream.Position;

                            switch (buffer.Type)
                            {
                                case eBufferData.ASCII:
                                    binaryWriter.Write(Encoding.UTF8.GetBytes(buffer.AsciiData), 0, Encoding.UTF8.GetBytes(buffer.AsciiData).Length);
                                    break;

                                case eBufferData.BINARY:
                                    binaryWriter.Write(buffer.BinaryData, buffer.BinaryDataStart, buffer.BinaryByteLength);
                                    break;

                                case eBufferData.ZEROS:
                                    for (int i = (int)position; i < buffer.BinaryByteLength + position; i++)
                                    {
                                        binaryWriter.Write(zero, 0, 1);
                                    }
                                    break;

                                case eBufferData.POSITION:
                                    if ((int)position > buffer.ToPosition)
                                    {
                                        string title = "WriteBinaryFile(string fileName) POSITION Error";
                                        string message = "\n\nThe length of xml xisfString is after the start of image data:\n\n" +
                                            fileName + "\n\n" +
                                            "Current Write Position:          " + position.ToString() + "\n" +
                                            "Image Attachment Start Position: " + buffer.ToPosition.ToString() + "\n\nAborting.";

                                        MessageBox.Show(message, title, MessageBoxButtons.OK);
                                        return false;
                                    }

                                    for (long i = position; i < buffer.ToPosition; i++)
                                    {
                                        binaryWriter.Write(zero, 0, 1);
                                    }
                                    break;
                            }
                        }
                    }

                    int binaryDataLength = rawStream.ToArray().Length;
                    byte[] binaryData = new byte[binaryDataLength];

                    binaryData = rawStream.ToArray();

                    using (System.IO.FileStream fileStream = new FileStream(fileName, FileMode.Create))
                    {
                        using (BinaryWriter binaryWriter = new BinaryWriter(fileStream))
                        {
                            fileStream.Write(binaryData, 0, binaryDataLength);
                            binaryWriter.Flush();
                            binaryWriter.Close();
                        }
                    }
                    binaryData = null;
                }
                return true;
            }
            catch (Exception ex)
            {
                string title = "WriteBinaryFile() XisfFileUpdate.cs Failed";
                string message = "\n\n" + fileName + "\n\n" + ex.Message;
                MessageBox.Show(message, title, MessageBoxButtons.OK);
                return false;
            }
        }

        // ##############################################################################################################################################
        // ##############################################################################################################################################

        private static bool IsFileLocked(string file)
        {
            try
            {
                // Read from the file, if we can't, it's locked
                using (FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)

                // Add delay for file to be released
                Thread.Sleep(100);
                return true;
            }

            //file is not locked
            return false;
        }

        // ##############################################################################################################################################
        // ##############################################################################################################################################
    }
}
