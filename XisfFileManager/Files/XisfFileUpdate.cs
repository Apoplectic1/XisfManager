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
using XisfFileManager.Files.XML;
using XisfFileManager.Globals;

namespace XisfFileManager.Files
{
    public class XisfFileUpdate
    {
        private Buffer mBuffer;
        private List<Buffer> mBufferList;


        /// <summary>
        /// Extracts a substring from the source string that is between the specified start and end tokens.
        /// </summary>
        /// <param name="source">The source string to extract the value from.</param>
        /// <param name="startToken">The token that marks the beginning of the value to extract.</param>
        /// <param name="endToken">The token that marks the end of the value to extract.</param>
        /// <returns>The extracted value if found; otherwise, an empty string.</returns>
        private static string ExtractValue(string source, string startToken, string endToken)
        {
            int start = source.IndexOf(startToken);
            if (start == -1) return "";

            start += startToken.Length; // Move the start index to the end of the startToken
            if (start >= source.Length) return "";

            int end = source.IndexOf(endToken, start);
            if (end == -1) return "";

            return source.Substring(start, end - start).Trim();
        }


        /// <summary>
        /// Extracts a substring from the source string that is between the specified start and end tokens,
        /// and occurs after the preToken.
        /// </summary>
        /// <param name="source">The source string to extract the value from.</param>
        /// <param name="preToken">The token that must precede the startToken in the source string.</param>
        /// <param name="startToken">The token that marks the beginning of the value to extract.</param>
        /// <param name="endToken">The token that marks the end of the value to extract.</param>
        /// <returns>The extracted value if found; otherwise, an empty string.</returns>
        private static string ExtractValue(string source, string preToken, string startToken, string endToken)
        {
            int preStart = source.IndexOf(preToken);
            if (preStart == -1) return "";

            // Adjust the starting position to search for startToken and endToken after preToken
            string adjustedSource = source.Substring(preStart + preToken.Length);

            int start = adjustedSource.IndexOf(startToken);
            if (start == -1) return "";

            int end = adjustedSource.IndexOf(endToken, start + startToken.Length);
            if (end == -1) return "";

            return adjustedSource.Substring(start + startToken.Length, end - (start + startToken.Length));
        }


        // ##############################################################################################################################################
        // ##############################################################################################################################################
        /// <summary>
        /// Updates the specified XISF file and writes the changes to a destination path.
        /// The method checks if the file is locked, validates the keyword update mode,
        /// and processes the file's XML content, updating keywords and attachments as needed.
        /// </summary>
        /// <param name="xFile">The XISF file to update.</param>
        /// <param name="destinationPath">The destination path to write the updated file.</param>
        /// <returns>True if the file is updated successfully; otherwise, false.</returns>
        public bool UpdateFile(XisfFile xFile, string destinationPath)
        {
            int delay = 0;
            while (IsFileLocked(xFile.FilePath) && delay < 100)
            {
                delay++;
                Thread.Sleep(10); // Sleep to prevent busy-wait loop
            }

            if (delay == 100)
            {
                MessageBox.Show("File is locked", xFile.FilePath, MessageBoxButtons.OK);
                return false;
            }

            // Return if KeywordList and the original XML are identical 
            if (xFile.KeywordUpdateMode == eKeywordUpdateMode.PROTECT)
                return false;

            if (xFile.KeywordUpdateMode == eKeywordUpdateMode.UPDATE_NEW && KeywordsMatchXml(xFile))
                return true;

            int xisfStart;
            int xisfEnd;

            byte[] binaryFileData = new byte[(int)1e9];
            mBufferList = new List<Buffer>();

            try
            {
                xFile.bModified = false;
               
                using (Stream stream = new FileStream(xFile.FilePath, FileMode.Open))
                {
                    bool modified;
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

                    // Convert <xisf to /xisf> to a string and then parse string as xml into a new doc
                    string xmlString = Encoding.UTF8.GetString(binaryFileData, xisfStart, xisfEnd);

                    // ******************************************************************************************
                    // Clean up and validate xmlString

                    // This should not be needed once all xisf files have been passed throught this application
                    (xmlString, modified) = Xml.FixXisfXml(xmlString);
                    //xFile.bModified |= modified;

                    // This should not be needed once all xisf files have been passed throught this application
                    (xmlString, modified) = Xml.ValidateXisfXml(xmlString);
                    //xFile.bModified |= modified;

                    // *******************************************************************************************************************************
                    // *******************************************************************************************************************************

                    // Remove all descendants from xFile.mXDoc that contain an "attachment" attribute that do not match the criteria specified by imageNode (our main image)

                    // Remove rejection image blocks
                    (xmlString, modified) = Xml.RemoveImage_ById(xmlString, "rejection_high");
                    xFile.bModified |= modified;

                    (xmlString, modified) = Xml.RemoveImage_ById(xmlString, "rejection_low");
                    xFile.bModified |= modified;

                    (xmlString, modified) = Xml.RemoveImageProperties_ById(xmlString, "integration"); // Includes ProcessHistory
                    xFile.bModified |= modified;

                    // ******************************************************************************************

                    // Create an Xml Document from the xmlString with the proper namespace
                    XmlDocument xmlDoc = new XmlDocument();
                    XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
                    namespaceManager.AddNamespace("ns", "http://www.pixinsight.com/xisf");

                    // The new document does not include the XmLVersion and PixInsight comment section - Add them
                    xmlDoc.LoadXml(xFile.XmlVersionText + xFile.XmlCommentText + xmlString);

                    
                    //RemoveUnwantedAttachments(xmlDoc);

                    // *******************************************************************************************************************************
                    // *******************************************************************************************************************************

                    // A list of unsed FITSKeywords will be removed. This method call should be able to be removed in the future.

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

        /// <summary>
        /// Calculates the padding needed to align the given length to the specified alignment.
        /// </summary>
        /// <param name="length">The initial length that needs to be aligned.</param>
        /// <param name="alignment">The alignment boundary to align to.</param>
        /// <returns>The number of padding bytes needed to align the length to the specified alignment.</returns>
        private static int GetNewPadding(int length, int alignment)
        {
            // Check if the length is already aligned
            if (length % alignment == 0)
                return 0;

            // Calculate the next value that is evenly divisible by alignment
            int nextDivisible = length + (alignment - length % alignment);

            // Return the difference between the next divisible value and the current length
            return nextDivisible - length;
        }


        // ****************************************************************************************************
        // ****************************************************************************************************

        /// <summary>
        /// Removes unwanted attachments from the XML document.
        /// This method removes all <Image> elements with an id attribute not equal to 'integration' as well as
        /// <Thumbnail>, <ICCProfile>, and <DisplayFunction> elements.
        /// </summary>
        /// <param name="document">The XML document to modify.</param>
        private static void RemoveUnwantedAttachments(XmlDocument document)
        {
            XmlNamespaceManager nsManager = new XmlNamespaceManager(document.NameTable);
            string namespaceUri = document.DocumentElement.NamespaceURI;
            nsManager.AddNamespace("ns", namespaceUri);

            
            // List of id values you want to remove.
            List<string> idsToRemove = new List<string> { "rejection_high", "rejection_low" };

            // Build an XPath predicate that matches any node with an id equal to one of the remove values.
            // This produces a predicate like: "@id='integration' or @id='integration1' or @id='otherId'"
            string predicate = string.Join(" or ", idsToRemove.Select(id => $"@id='{id}'"));

            // Construct the full XPath expression.
            string xpath = $"//ns:Image[{predicate}]";

            // Select the nodes to remove.
            var imageNodesToRemove = document.SelectNodes(xpath, nsManager)
                                             .Cast<XmlNode>()
                                             .ToList();

            // Remove each selected node from its parent.
            imageNodesToRemove.ForEach(imageNode => imageNode.ParentNode?.RemoveChild(imageNode));


            //var imageNodesToRemove = document.SelectNodes("//ns:Image[@id!='integration']", nsManager).Cast<XmlNode>().ToList();
            //imageNodesToRemove.ForEach(imageNode => imageNode.ParentNode?.RemoveChild(imageNode));

            // Remove <Thumbnail> element if it exists
            var thumbnailNode = document.SelectSingleNode("//ns:Thumbnail", nsManager);
            thumbnailNode?.ParentNode?.RemoveChild(thumbnailNode);

            // Remove <ICCProfile> element if it exists
            var iccProfileNode = document.SelectSingleNode("//ns:ICCProfile", nsManager);
            iccProfileNode?.ParentNode?.RemoveChild(iccProfileNode);

            // Remove <DisplayFunction> element if it exists
            var displayFunctionNode = document.SelectSingleNode("//ns:DisplayFunction", nsManager);
            displayFunctionNode?.ParentNode?.RemoveChild(displayFunctionNode);
        }


        // ****************************************************************************************************
        // ****************************************************************************************************

        /// <summary>
        /// Finds the starting location of existing image data in the binary file data.
        /// It identifies the end of the XML section, then skips any padding (runs of 0's) after the </xisf> tag,
        /// returning the location of the first non-zero byte after the </xisf> tag.
        /// </summary>
        /// <param name="binaryFileData">The binary file data to search.</param>
        /// <returns>The location of the first non-zero byte after the </xisf> tag.</returns>
        public static int FindExistingImageStartingLocation(byte[] binaryFileData)
        {
            // Find the end of the </xisf> tag in the binary file data
            int xmlEndLocation = BinaryFind(binaryFileData, "</xisf>") + "</xisf>".Length;

            // Walk through any padding (runs of 0's) after the </xisf> tag
            int padding = 0;
            for (int i = xmlEndLocation; i < binaryFileData.Length; i++)
            {
                if (binaryFileData[i] == 0)
                {
                    padding++;
                }
                else
                {
                    break;
                }
            }

            // Return the location of the first non-zero byte after the </xisf> tag
            return xmlEndLocation + padding;
        }


        // ****************************************************************************************************
        // ****************************************************************************************************

        /// <summary>
        /// Sets the image attachment location in the XML document for an XISF file.
        /// This method updates the location attribute of the first Image node and calculates the necessary padding.
        /// </summary>
        /// <param name="document">The XML document to modify.</param>
        /// <param name="xFile">The XISF file containing block alignment size.</param>
        /// <returns>The calculated padding required for the new starting address.</returns>
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

            // Iterate until the XML document length does not change
            do
            {
                documentLengthBeforeNewStartingAddress = document.OuterXml.Length;

                // Include the 16 byte XISF Signature. No comment section in the padding calculation
                newPadding = GetNewPadding(documentLengthBeforeNewStartingAddress + 16, xFile.BlockAlignmentSize);

                // The starting address is the address in the new file (including XISF Signature); (not just in the XML document section)
                newStartingAddress = documentLengthBeforeNewStartingAddress + 16 + newPadding;

                // Update imageNode with the new starting address and can change the size of the XML document
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

        /// <summary>
        /// Replaces all FITS keywords in the given XML document with the keywords from the XISF file.
        /// The method removes existing FITSKeyword elements and adds new ones from the XISF file's keyword list.
        /// </summary>
        /// <param name="document">The XML document to modify.</param>
        /// <param name="xFile">The XISF file containing the new keywords.</param>
        private static void ReplaceAllFitsKeywords(XmlDocument document, XisfFile xFile)
        {
            // Remove all existing FITSKeyword elements
            var nodeList = document.GetElementsByTagName("FITSKeyword").Cast<XmlNode>().ToList();
            nodeList.ForEach(node => node.ParentNode.RemoveChild(node));

            // Find all <Image> elements in the document
            var imageNodes = document.GetElementsByTagName("Image").Cast<XmlNode>().ToList();

            // Alphabetize the keyword list from the XISF file
            var sortedKeywords = xFile.KeywordList.mKeywordList.OrderBy(p => p.Name).ToList();

            // Add each keyword as a new FITSKeyword element under each <Image> node
            imageNodes.ForEach(imageNode =>
            {
                sortedKeywords.ForEach(keyword =>
                {
                    var newElement = document.CreateElement("FITSKeyword", document.DocumentElement.NamespaceURI);
                    newElement.SetAttribute("name", keyword.Name);
                    newElement.SetAttribute("comment", keyword.Comment);
                    newElement.SetAttribute("value", keyword.Value.ToString());
                    imageNode.AppendChild(newElement);
                });
            });
        }


        // ****************************************************************************************************
        // ****************************************************************************************************

        /// <summary>
        /// Searches for the specified text within the given byte array and returns the index of its first occurrence.
        /// </summary>
        /// <param name="buffer">The byte array to search.</param>
        /// <param name="findText">The text to find within the byte array.</param>
        /// <returns>The index of the first occurrence of the specified text within the byte array, or -1 if not found.</returns>
        private static int BinaryFind(byte[] buffer, string findText)
        {
            // Convert the search text to a byte array
            byte[] searchBytes = Encoding.UTF8.GetBytes(findText);
            int bufferLength = buffer.Length;
            int searchLength = searchBytes.Length;

            // Loop through the buffer to search for the text
            for (int i = 0; i <= bufferLength - searchLength; i++)
            {
                // Check if the first byte matches
                if (buffer[i] == searchBytes[0])
                {
                    int j;
                    // Check subsequent bytes
                    for (j = 1; j < searchLength && buffer[i + j] == searchBytes[j]; j++) ;
                    // If all bytes match, return the starting index
                    if (j == searchLength)
                        return i;
                }
            }

            // Return -1 if the text is not found
            return -1;
        }


        // ****************************************************************************************************
        // ****************************************************************************************************

        /// <summary>
        /// Checks if the FITS keywords in the XISF file match the keywords in the XML string.
        /// The method compares the keywords in the XISF file with the FITSKeyword elements in the XML document.
        /// </summary>
        /// <param name="xFile">The XISF file containing the keywords and XML string.</param>
        /// <returns>True if the keywords match the XML FITSKeyword elements; otherwise, false.</returns>
        private static bool KeywordsMatchXml(XisfFile xFile)
        {
            // Load the XML string into an XmlDocument
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xFile.XmlString);

            // Create an XmlNamespaceManager and add the necessary namespace
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(xmlDoc.NameTable);
            namespaceManager.AddNamespace("ns", "http://www.pixinsight.com/xisf");

            // Select all FITSKeyword elements using the namespace
            var fitsKeywordNodes = xmlDoc.SelectNodes("//ns:FITSKeyword", namespaceManager).Cast<XmlNode>().ToList();

            // Alphabetize the KeywordList from the XISF file
            var keywords = xFile.KeywordList.mKeywordList.OrderBy(p => p.Name).ToList();

            // Check if fitsKeywordNodes and keywords have identical elements in the same order
            bool bIdentical = keywords.Count == fitsKeywordNodes.Count &&
                              !keywords.Where((keyword, index) =>
                              {
                                  var fitsKeywordNode = fitsKeywordNodes[index];
                                  var nameAttribute = fitsKeywordNode.Attributes["name"]?.Value;
                                  var valueAttribute = fitsKeywordNode.Attributes["value"]?.Value;
                                  var commentAttribute = fitsKeywordNode.Attributes["comment"]?.Value;

                                  return nameAttribute != keyword.Name || valueAttribute != keyword.Value || commentAttribute != keyword.Comment;
                              }).Any();

            return bIdentical;
        }


        // ##############################################################################################################################################
        // ##############################################################################################################################################

        /// <summary>
        /// Writes the binary data from the buffer list to a specified file.
        /// Handles different buffer types and ensures data is written correctly.
        /// </summary>
        /// <param name="fileName">The name of the file to write to.</param>
        /// <returns>True if the file is written successfully; otherwise, false.</returns>
        private bool WriteBinaryFile(string fileName)
        {
            byte[] zero = { 0x00 };

            try
            {
                using (MemoryStream rawStream = new MemoryStream())
                using (BinaryWriter binaryWriter = new BinaryWriter(rawStream))
                {
                    // Write each buffer to the memory stream
                    mBufferList.ForEach(buffer =>
                    {
                        long position = rawStream.Position;

                        switch (buffer.Type)
                        {
                            case eBufferData.ASCII:
                                byte[] asciiData = Encoding.UTF8.GetBytes(buffer.AsciiData);
                                binaryWriter.Write(asciiData, 0, asciiData.Length);
                                break;

                            case eBufferData.BINARY:
                                binaryWriter.Write(buffer.BinaryData, buffer.BinaryDataStart, buffer.BinaryByteLength);
                                break;

                            case eBufferData.ZEROS:
                                Enumerable.Range((int)position, buffer.BinaryByteLength)
                                          .ToList()
                                          .ForEach(_ => binaryWriter.Write(zero, 0, 1));
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
                                    throw new InvalidOperationException("POSITION Error");
                                }

                                Enumerable.Range((int)position, (int)(buffer.ToPosition - position))
                                          .ToList()
                                          .ForEach(_ => binaryWriter.Write(zero, 0, 1));
                                break;
                        }
                    });

                    // Write the memory stream to the file
                    byte[] binaryData = rawStream.ToArray();
                    using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
                    using (BinaryWriter fileWriter = new BinaryWriter(fileStream))
                    {
                        fileStream.Write(binaryData, 0, binaryData.Length);
                        fileWriter.Flush();
                    }
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

        /// <summary>
        /// Checks if a file is locked by attempting to open it with exclusive access.
        /// If the file cannot be opened, it is considered locked.
        /// </summary>
        /// <param name="file">The path of the file to check.</param>
        /// <returns>True if the file is locked; otherwise, false.</returns>
        private static bool IsFileLocked(string file)
        {
            try
            {
                // Try to open the file with exclusive access
                using (FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                // The file is unavailable because it is:
                // - still being written to
                // - being processed by another thread
                // - does not exist (has already been processed)

                // Add delay for the file to be released
                Thread.Sleep(100);
                return true;
            }

            // The file is not locked
            return false;
        }


        // ##############################################################################################################################################
        // ##############################################################################################################################################
    }
}
