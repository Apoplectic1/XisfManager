using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

using XisfFileManager.Enums;

namespace XisfFileManager.Files
{
    public class XisfFileRename
    {
        private int mDupIndex;

        public List<XisfFile> mFileList;

        public eOrder RenameOrder;

        /// <summary>
        /// Recursively modifies the file name by appending "-Dup" until a unique name is found.
        /// </summary>
        /// <param name="dupFileName">The initial duplicate file name.</param>
        /// <returns>A unique file name that does not already exist.</returns>
        private static string RecurseDupFileName(string dupFileName)
        {
            while (File.Exists(dupFileName))
            {
                int lastParen = dupFileName.LastIndexOf(')');
                dupFileName = dupFileName.Insert(lastParen + 1, "-Dup");
            }
            return dupFileName;
        }


        /// <summary>
        /// Renames the specified XISF file based on its properties and the given index.
        /// </summary>
        /// <param name="file">The XISF file to rename.</param>
        /// <returns>A tuple containing the status code (1 for success, -1 for failure) and the new file name.</returns>
        public Tuple<int, string> RenameFile(XisfFile file)
        {
            try
            {
                string sourceFileDirectory = Path.GetDirectoryName(file.FilePath);
                string newFileName = BuildFileName(file.FileNameNumberIndex, file) + ".xisf";
                string newFilePath = Path.Combine(sourceFileDirectory, newFileName);

                // Rename the file if its name actually changed and the new file name does not already exist
                if (file.FilePath != newFilePath && !File.Exists(newFilePath))
                {
                    File.Move(file.FilePath, newFilePath);
                }

                return new Tuple<int, string>(1, newFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "RenameFile(XisfFile file)");
                return new Tuple<int, string>(-1, "");
            }
        }


        /// <summary>
        /// Moves duplicate files in the given list to a 'Duplicates' directory.
        /// A duplicate is defined as a file with identical properties to another file in the list.
        /// </summary>
        /// <param name="fileList">The list of XisfFile objects to check for duplicates.</param>
        /// <returns>The count of files that were moved as duplicates.</returns>
        public static int MoveDuplicates(List<XisfFile> fileList)
        {
            // Group files by their properties to identify duplicates
            var groupedDuplicates = fileList.GroupBy(item => new
            {
                item.Camera,
                item.FrameType,
                item.Binning,
                item.FilterName,
                item.ExposureSeconds,
                item.Gain,
                item.Offset,
                item.CaptureTime
            })
            .Where(group => group.Skip(1).Any()) // Only keep groups with more than one item
            .ToList();

            // List to keep track of files that have been moved
            List<XisfFile> movedFiles = new List<XisfFile>();

            // Process each group of duplicates
            groupedDuplicates.ForEach(group =>
            {
                var items = group.ToList();
                for (int i = 1; i < items.Count; i++) // Skip the first item to keep it in place
                {
                    var item = items[i];
                    string sourceFilePath = Path.GetDirectoryName(item.FilePath);
                    string duplicatesPath = Path.Combine(sourceFilePath, "Duplicates");

                    // Ensure the 'Duplicates' directory exists
                    if (!Directory.Exists(duplicatesPath))
                        Directory.CreateDirectory(duplicatesPath);

                    string destinationFilePath = Path.Combine(duplicatesPath, Path.GetFileName(item.FilePath));

                    // Move the file to the 'Duplicates' directory
                    File.Move(item.FilePath, destinationFilePath, true); // Overwrite if the file already exists

                    // Add the file to the movedFiles list
                    movedFiles.Add(item);
                }
            });

            // Remove the moved files from the original fileList
            fileList.RemoveAll(file => movedFiles.Contains(file));

            // Return the count of files that were moved as duplicates
            return movedFiles.Count;
        }



        /// <summary>
        /// Builds a new file name for the given XISF file based on its properties and the specified index.
        /// </summary>
        /// <param name="index">The index of the file.</param>
        /// <param name="mFile">The XISF file for which to build the new name.</param>
        /// <returns>The newly built file name.</returns>
        private string BuildFileName(int index, XisfFile mFile)
        {
            if (mFile.TargetName.Contains("Master"))
            {
                string newName = "Master ";
                string targetName = mFile.TargetName;
                eFrame frameType = mFile.FrameType;

                if (targetName.Equals("Master"))
                {
                    string frameInfo = mFile.MSTRFRMS != -1
                        ? $"{mFile.ExposureSeconds.FormatExposureTime()}x{mFile.Binning}x{mFile.MSTRFRMS}"
                        : $"{mFile.ExposureSeconds.FormatExposureTime()}x{mFile.Binning}";

                    string commonInfo = $"{mFile.Camera}G{mFile.Gain:D3}O{mFile.Offset}@{mFile.SensorTemperature.FormatTemperature()}C";

                    switch (frameType)
                    {
                        case eFrame.LIGHT:
                            newName += $"Integration  L-{mFile.FilterName}  {frameInfo}  {commonInfo}";
                            if (!string.IsNullOrEmpty(mFile.MSTRALG))
                                newName += $"  ({mFile.MSTRALG}  {mFile.CaptureTime:yyyy-MM-dd}";
                            else
                                newName += $"  ({mFile.CaptureTime:yyyy-MM-dd}";
                            newName += $"  {mFile.CaptureSoftware})";
                            break;

                        case eFrame.DARK:
                            newName += $"Dark  {mFile.CaptureTime:yyyy-MM-dd}  {frameInfo}  {commonInfo}";
                            break;

                        case eFrame.BIAS:
                            newName += $"Bias  {mFile.CaptureTime:yyyy-MM-dd}  {frameInfo}  {commonInfo}";
                            break;

                        case eFrame.FLAT:
                            newName += $"Flat {mFile.FilterName}  {mFile.CaptureTime:yyyy-MM-dd}  {frameInfo}  {commonInfo}  {mFile.Telescope}@{mFile.FocalLength:F0}";
                            break;
                    }

                    if (!string.IsNullOrEmpty(mFile.MSTRALG))
                    {
                        newName += $"  ({mFile.MSTRALG}";
                        if (!string.IsNullOrEmpty(mFile.CaptureSoftware))
                            newName += $" {mFile.CaptureSoftware})";
                        else
                            newName += ")";
                    }
                    else if (!string.IsNullOrEmpty(mFile.CaptureSoftware))
                    {
                        newName += $"  ({mFile.CaptureSoftware})";
                    }

                    return newName;
                }
            }

            string newNameWithIndex = index.ToString("D3");
            string commonDetails = $"{mFile.ExposureSeconds.FormatExposureTime()}x{mFile.Binning}  {mFile.Camera}G{mFile.Gain:D3}O{mFile.Offset}@{mFile.SensorTemperature.FormatTemperature()}C";

            switch (mFile.FrameType)
            {
                case eFrame.DARK:
                    newNameWithIndex += $"  Dark  {commonDetails}  ({mFile.CaptureTime:yyyy-MM-dd  hh-mm-ss tt}  {mFile.CaptureSoftware})";
                    break;

                case eFrame.BIAS:
                    newNameWithIndex += $"  Bias  {commonDetails}  ({mFile.CaptureTime:yyyy-MM-dd  hh-mm-ss tt}  {mFile.CaptureSoftware})";
                    break;

                case eFrame.FLAT:
                    newNameWithIndex += $"  F-{mFile.FilterName}  {commonDetails}  {mFile.Telescope}@{mFile.FocalLength:F0}";

                    if (mFile.AmbientTemperature != -273)
                        newNameWithIndex += $" {mFile.AmbientTemperature.FormatTemperature()}C";
                    else
                        newNameWithIndex += "  ";

                    if (mFile.FocuserPosition != int.MinValue && mFile.FocuserTemperature != -273.0)
                        newNameWithIndex += $" F{mFile.FocuserPosition:D5}@{mFile.FocuserTemperature.FormatTemperature()}C";

                    if (mFile.RotationAngle.StartsWith('S'))
                        newNameWithIndex += $"  {mFile.RotationAngle}";

                    newNameWithIndex += $"  ({mFile.CaptureTime:yyyy-MM-dd  hh-mm-ss tt}  {mFile.CaptureSoftware})";
                    break;

                case eFrame.LIGHT:
                    string weightPart = string.Empty;
                    switch (RenameOrder)
                    {
                        case eOrder.INDEX:
                            weightPart = index.ToString("D3") + " ";
                            break;
                        case eOrder.INDEXWEIGHT:
                            weightPart = !double.IsNaN(mFile.SSWeight) ? $"{Convert.ToInt32(mFile.SSWeight * 1000.0):D4} {index:D3}" : index.ToString("D3");
                            break;
                        case eOrder.WEIGHT:
                            weightPart = !double.IsNaN(mFile.SSWeight) ? Convert.ToInt32(mFile.SSWeight * 1000.0).ToString("D4") : index.ToString("D3");
                            break;
                        case eOrder.WEIGHTINDEX:
                            weightPart = !double.IsNaN(mFile.SSWeight) ? $"{Convert.ToInt32(mFile.SSWeight * 1000.0):D4} {index:D3}" : index.ToString("D3");
                            break;
                    }

                    newNameWithIndex = $"{weightPart} {mFile.TargetName}  L-{mFile.FilterName}  {commonDetails}  {mFile.Telescope}@{mFile.FocalLength:F0}";

                    if (mFile.AmbientTemperature != -273.0)
                        newNameWithIndex += $"{mFile.AmbientTemperature.FormatTemperature()}C";
                    else
                        newNameWithIndex += $"{mFile.FocuserTemperature.FormatTemperature()}C";

                    newNameWithIndex += $"  F{mFile.FocuserPosition:D5}@{mFile.FocuserTemperature.FormatTemperature()}C";

                    if (mFile.RotationAngle.StartsWith('S'))
                        newNameWithIndex += $"  {mFile.RotationAngle}";

                    newNameWithIndex += $"  ({mFile.CaptureTime:yyyy-MM-dd  hh-mm-ss tt}  {mFile.CaptureSoftware})";
                    break;

                default:
                    newNameWithIndex = index.ToString("D3") + " Rename Failed";
                    break;
            }

            return newNameWithIndex;
        }


        /// <summary>
        /// Moves duplicate files to a 'Duplicates' directory and renames them with a duplicate index.
        /// </summary>
        /// <param name="currentFile">The current XISF file being processed.</param>
        /// <param name="sourceFilePath">The source file path where the file is located.</param>
        /// <param name="newFileName">The new file name to be used for duplicates.</param>
        private void MoveDuplicates(XisfFile currentFile, string sourceFilePath, string newFileName)
        {
            int dupIndex = 1;

            // Ensure the Duplicates directory exists
            string duplicatesPath = Path.Combine(sourceFilePath, "Duplicates");
            Directory.CreateDirectory(duplicatesPath);

            // Loop through the file list to process duplicates
            foreach (XisfFile entry in mFileList.ToList()) // Create a copy of the list to avoid modifying the collection while iterating
            {
                if (entry == currentFile) continue; // Skip the current file

                // Increment the duplicate index
                dupIndex++;

                // Remove the index or SSWEIGHT from the file name (the first four characters) and postfix duplicate index
                string duplicateFileName = newFileName.Remove(0, 4).Insert(0, dupIndex.ToString("D3") + " ");

                // Construct the destination path for the duplicate file
                string destinationFilePath = Path.Combine(duplicatesPath, duplicateFileName);

                // Ensure a unique file name by appending "-Dup" if necessary
                destinationFilePath = RecurseDupFileName(destinationFilePath);

                // Move the duplicate file to the Duplicates directory
                File.Move(entry.FilePath, destinationFilePath);

                // Remove the entry from the file list
                mFileList.Remove(entry);
            }
        }


        /// <summary>
        /// Checks if the specified file is locked.
        /// </summary>
        /// <param name="path">The path to the file to check.</param>
        /// <returns>True if the file is locked; otherwise, false.</returns>
        private static bool IsFileLocked(string path)
        {
            FileInfo file = new FileInfo(path);
            FileStream stream = null;

            try
            {
                // Attempt to open the file with read/write access and exclusive lock
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                // The file is unavailable because it is:
                // still being written to,
                // being processed by another thread,
                // or does not exist (has already been processed)
                return true;
            }
            finally
            {
                // Close the stream if it was successfully opened
                stream?.Close();
            }

            // The file is not locked
            return false;
        }

    }
}