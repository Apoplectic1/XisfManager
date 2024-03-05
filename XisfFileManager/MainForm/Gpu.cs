using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Windows.Forms;
using XisfFileManager;
using XisfFileManager.Enums;
using XisfFileManager.Files;

namespace XisfFileManager
{
    public partial class MainForm
    {
        private Buffer mBuffer;
        private List<Buffer> mBufferList;
        private XisfFile mXisfFile;
        private XisfFile mXisfToGpu;


        private void XisfToGpuTab_Button_ConvertToGpu_Click(object sender, EventArgs e)
        {

            XisfToGpuData(mFileList.First(), @"E:\Temp\input_data.dat");
        }

        private void XisfToGpuTab_Button_ConvertToXisf_Click(object sender, EventArgs e)
        {
            GpuDataToXisf(@"E:\Temp\output_data.dat", @"E:\Temp\output_data.xisf");
        }

        public bool XisfToGpuData(XisfFile xFile, string destinationFilePath)
        {
            byte[] binaryFileData = new byte[(int)1e9];
            mBufferList.Clear();

            try
            {
                using (Stream stream = new FileStream(xFile.FilePath, FileMode.Open))
                {
                    // *******************************************************************************************************************************
                    // *******************************************************************************************************************************

                    // Read entire XISF file (up to 1 GB) into binaryFileData
                    BinaryReader br = new BinaryReader(stream);
                    binaryFileData = br.ReadBytes((int)1e9);
                    br.Close();

                    // *******************************************************************************************************************************
                    // *******************************************************************************************************************************

                    // Create a Buffer List. The list will contain the XISF Signature and length, the xml, and the image data
                    // This ordered List sets up pointers to the data to by written, its type, and length

                    mBufferList.Clear();

                    // Main Image Only
                    mBuffer = new Buffer
                    {
                        Type = eBufferData.BINARY,
                        BinaryDataStart = xFile.TargetAttachmentStart,
                        BinaryByteLength = xFile.TargetAttachmentLength,
                        BinaryData = binaryFileData
                    };
                    mBufferList.Add(mBuffer);

                    // *******************************************************************************************************************************
                    // *******************************************************************************************************************************

                    // Now that the mBuffer List is done, use it to write a new pure binary data file
                    bool bStatus = WriteBinaryFile(destinationFilePath);
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

        public bool GpuDataToXisf(string sourceFilePath, string destinationFilePath)
        {
            byte[] binaryFileData = new byte[(int)1e9];
            mBufferList.Clear();

            try
            {
                using (Stream stream = new FileStream(sourceFilePath, FileMode.Open))
                {
                    // *******************************************************************************************************************************
                    // *******************************************************************************************************************************

                    // Read entire XISF file (up to 1 GB) into binaryFileData
                    BinaryReader br = new BinaryReader(stream);
                    binaryFileData = br.ReadBytes((int)1e9);
                    br.Close();

                    // *******************************************************************************************************************************
                    // *******************************************************************************************************************************

                    // "XISF0100" is the XISF Signature. The length of the xml section is stored in the 8th and 9th bytes of the signature
                    // Assumes that xmlLength is less than 65536 bytes

                    //                                      0     1     2     3     4     5     6     7     8     9     10    11    12    13    14    15
                    //                                      X     I     S     F     0     1     0     0     0     0     0     0     0     0     0     0
                    byte[] xisfSignature = new byte[16] { 0x58, 0x49, 0x53, 0x46, 0x30, 0x31, 0x30, 0x30, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

                    // *******************************************************************************************************************************

                    // Create a new minimal xml string without any keywords that is compatible with PixInsight
                    /*
                    string xmlString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                        "<!--\r\nExtensible Image Serialization Format - XISF version 1.0\r\nCreated with PixInsight software - http://pixinsight.com/\r\n-->" +
                        "<xisf version=\"1.0\" xmlns=\"http://www.pixinsight.com/xisf\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.pixinsight.com/xisf http://pixinsight.com/xisf/xisf-1.0.xsd\">" +
                        "<Image geometry=\"WIDTH:HEIGHT:1\" sampleFormat=\"UInt16\" colorSpace=\"Gray\" location=\"attachment:0:LENGTH\">" +
                        "<Resolution horizontal=\"72\" vertical=\"72\" unit=\"inch\"/>";
                    */
                    string xmlString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                       "<!--\r\nExtensible Image Serialization Format - XISF version 1.0\r\nCreated with PixInsight software - http://pixinsight.com/\r\n-->" +
                       "<xisf version=\"1.0\" xmlns=\"http://www.pixinsight.com/xisf\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.pixinsight.com/xisf http://pixinsight.com/xisf/xisf-1.0.xsd\">" +
                       "<Image geometry=\"WIDTH:HEIGHT:1\" sampleFormat=\"Float32\"  bounds=\"0:1\" colorSpace=\"Gray\" location=\"attachment:0:LENGTH\">" +
                       "<Resolution horizontal=\"72\" vertical=\"72\" unit=\"inch\"/>";

                    // Insert any additional keywords here

                    xmlString +=
                            "</Image>" +
                            "<Metadata>" +
                                "<Property id=\"XISF:CreatorOS\" type=\"String\">Windows</Property>" +
                                "<Property id=\"XISF:BlockAlignmentSize\" type=\"UInt16\" value=\"4096\" />" +
                                "<Property id=\"XISF:MaxInlineBlockSize\" type=\"UInt16\" value=\"3072\" />" +
                            "</Metadata>" +
                        "</xisf>";

                    // *******************************************************************************************************************************

                    // Now we need to update image: geometry, padding, location and length in the xmlString
                    // String replace "WIDTH", "HEIGHT" and "LENGTH" with the correct values
                    // The starting address needs a REGEX to find and replace the correct starting location in the xmlString
                    // Sample Format needs to be upated (UInt16, UInt32, Float32, Float64) // To Do: Generalize this to non-UInt16 images
                    // Color space needs to be upated // To Do: Generalize this to include RGB images

                    // Set width and height to squareroot of binaryFileData.Length if binaryFileData.Length is a perfect square
                    // Otherwise, set width and height to the next lowest perfect square
                    int width = 5496; // (int)Math.Sqrt(binaryFileData.Length / 2);  // To Do: Generalize this to non-square images
                    int height = 3672; // width;
                    int start = xmlString.Length + 16;  // 16 is the length of the XISF Signature;

                    xmlString = xmlString.Replace("WIDTH", width.ToString());
                    xmlString = xmlString.Replace("HEIGHT", height.ToString());
                    xmlString = xmlString.Replace("LENGTH", binaryFileData.Length.ToString());

                    // Iterate until the xml document length does not change
                    int documentLengthBeforeNewStartingAddress;
                    int documentLengthAfterNewStartingAddress;
                    int padding;
                    int newStartingAddress;

                    do
                    {
                        // Update imageNode with the new starting address and can change the size of the xml document 
                        // 1. This can push the image data to a new location and therfore to a new starting address
                        // 2. This can change the padding - new padding
                        // If the document length changes, we need to iterate until it does not change (start address and padding can change with each iteration)

                        documentLengthBeforeNewStartingAddress = xmlString.Length;

                        // Include the 16 byte XISF Signature. No Comment section in the padding calculation
                        padding = GetNewPadding(documentLengthBeforeNewStartingAddress + 16, 4096);

                        // The starting address is the address in the new file (including XISF Signature); (not just in the Xml document section)
                        newStartingAddress = documentLengthBeforeNewStartingAddress + 16 + padding;

                        // Replace any number of digits in xmlString between "location=\"attachment:" and the next ":" with the new starting address
                        xmlString = Regex.Replace(xmlString, @"(?<=location=""attachment:)\d+(?=:)", newStartingAddress.ToString());

                        documentLengthAfterNewStartingAddress = xmlString.Length;
                    }
                    while (documentLengthAfterNewStartingAddress != documentLengthBeforeNewStartingAddress);

                    // We now have a complete/valid xml string that contains a new image starting address along with updated padding 

                    // *******************************************************************************************************************************

                    // Update the XISF Signature with the new document length

                    // Change Endianess
                    xisfSignature[8] = (byte)(documentLengthAfterNewStartingAddress & 0xFF); // Least significant byte
                    xisfSignature[9] = (byte)(documentLengthAfterNewStartingAddress >> 8 & 0xFF); // Most significant byte

                    // *******************************************************************************************************************************

                    // Clear the Buffer List. The list will contain the XISF Signature and length, the xml, and the image data.
                    // This ordered List sets up pointers to the data to by written, its type, and length

                    mBufferList.Clear();

                    // Write the XISF Signature
                    mBuffer = new Buffer
                    {
                        Type = eBufferData.BINARY,
                        BinaryDataStart = 0,
                        BinaryByteLength = 16,
                        BinaryData = xisfSignature
                    };
                    mBufferList.Add(mBuffer);

                    // Write complete <xml ... /xisf> section
                    mBuffer = new Buffer
                    {
                        Type = eBufferData.ASCII,
                        AsciiData = xmlString
                    };
                    mBufferList.Add(mBuffer);

                    // Main Image
                    // In OUTPUT file, pad from current position to the start of image data
                    mBuffer = new Buffer
                    {
                        Type = eBufferData.ZEROS,
                        BinaryByteLength = padding
                    };
                    mBufferList.Add(mBuffer);

                    // Main Image Only
                    // In INPUT file, start at the beginning of the image data
                    mBuffer = new Buffer
                    {
                        Type = eBufferData.BINARY,
                        BinaryDataStart = 0,
                        BinaryByteLength = binaryFileData.Length,
                        BinaryData = binaryFileData
                    };
                    mBufferList.Add(mBuffer);

                    // *******************************************************************************************************************************
                    // *******************************************************************************************************************************

                    // Now that the mBuffer List is done, use it to write a new pure binary data file
                    bool bStatus = WriteBinaryFile(destinationFilePath);
                    if (bStatus == false)
                        return false;

                    // *******************************************************************************************************************************
                    // *******************************************************************************************************************************

                }
            }
            catch
            {
                DialogResult status = MessageBox.Show("Update Write File Failed", destinationFilePath, MessageBoxButtons.OKCancel);
                if (status == DialogResult.OK)
                    return false;
                Environment.Exit(-1);
            }

            return true;
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

                    using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
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

        private static int GetNewPadding(int length, int alignment)
        {
            // Find the difference between length and the nearest larger value that is evenly divisible by alignment

            if (length % alignment == 0)
                return 0;
            else
            {
                int nextdivisible = length + (alignment - length % alignment);
                return nextdivisible - length;
            }
        }

        // ##############################################################################################################################################
        // ##############################################################################################################################################

        private static bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
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
                return true;
            }

            //file is not locked
            return false;
        }

        // ##############################################################################################################################################
        // ##############################################################################################################################################
    }
}
