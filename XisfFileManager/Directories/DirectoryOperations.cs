using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace XisfFileManager.Files
{
    public static class DirectoryOperations
    {
        // ***********************************************************************************
        // ***********************************************************************************

        public static List<System.IO.FileInfo> FileInfoList { get; private set; }
        public static bool Recurse { get; set; } = true;
        public static string SelectedFolder { get; private set; }

        // ***********************************************************************************
        // ***********************************************************************************

        /// <summary>
        /// Prompts the user to select a folder and searches for .xisf files within the selected folder and its subdirectories.
        /// </summary>
        /// <param name="defaultFolderPath">The default folder path to be selected initially.</param>
        /// <param name="mXisfExclude">List of file paths to be excluded from the search.</param>
        /// <returns>DialogResult indicating whether the user selected a folder or canceled the dialog.</returns>
        public static DialogResult FindTargetFiles(string defaultFolderPath, List<string> mXisfExclude)
        {
            FileInfoList = [];

            using (FolderBrowserDialog folderDialog = new())
            {
                folderDialog.Description = "Select Xisf Folder Tree";
                folderDialog.ShowNewFolderButton = false;

                // Set the initial directory to the default folder path if it exists
                if (!string.IsNullOrEmpty(defaultFolderPath) && Directory.Exists(defaultFolderPath))
                {
                    folderDialog.SelectedPath = defaultFolderPath;
                }

                // Show the folder dialog and process the selected folder
                if (folderDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(folderDialog.SelectedPath))
                {
                    SelectedFolder = folderDialog.SelectedPath;
                    SearchXisfFilesInDirectory(SelectedFolder, mXisfExclude, !Recurse);
                }

                return string.IsNullOrEmpty(SelectedFolder) ? DialogResult.Cancel : DialogResult.OK;
            }
        }

        // ***********************************************************************************
        // ***********************************************************************************

        /// <summary>
        /// Finds calibration files (.xisf) in the specified folder and its subdirectories.
        /// </summary>
        /// <param name="defaultFolderPath">The default folder path to search in.</param>
        /// <param name="mXisfExclude">List of file paths to exclude from the search.</param>
        /// <param name="continueRecursion">Whether to continue recursion into subdirectories. Default is true.</param>
        /// <returns>True if the default folder path is not empty; otherwise, false.</returns>
        public static bool FindCalibrationFiles(string defaultFolderPath, List<string> mXisfExclude, bool continueRecursion = true)
        {
            // Initialize the file info list
            FileInfoList = [];

            // Check if the default folder path is not empty and the directory exists
            if (!string.IsNullOrEmpty(defaultFolderPath) && Directory.Exists(defaultFolderPath))
            {
                // Temporarily set the current directory to the default folder path
                string previousCurrentDirectory = Environment.CurrentDirectory;
                Environment.CurrentDirectory = defaultFolderPath;

                // Search for .xisf files within the selected folder and its subdirectories
                SearchXisfFilesInDirectory(defaultFolderPath, mXisfExclude, !continueRecursion);

                // Restore the previous current directory
                Environment.CurrentDirectory = previousCurrentDirectory;
            }
            else if (!string.IsNullOrEmpty(defaultFolderPath))
            {
                // Search for .xisf files within the selected folder and its subdirectories
                SearchXisfFilesInDirectory(SelectedFolder, mXisfExclude, !continueRecursion);
            }

            // Return true if the default folder path is not empty, false otherwise
            return !string.IsNullOrEmpty(defaultFolderPath);
        }


        // ***********************************************************************************
        // ***********************************************************************************

        /// <summary>
        /// Searches for .xisf files in the specified directory and its subdirectories, excluding files listed in the excludeList.
        /// </summary>
        /// <param name="directoryPath">The path of the directory to search.</param>
        /// <param name="excludeList">A list of file paths or names to exclude from the search.</param>
        /// <param name="stopRecursion">If true, stops the search from recursing into subdirectories.</param>
        private static void SearchXisfFilesInDirectory(string directoryPath, List<string> excludeList, bool stopRecursion)
        {
            try
            {
                // Get all .xisf files in the current directory, excluding those in the excludeList
                var xisfFiles = Directory.EnumerateFiles(directoryPath, "*.xisf")
                                         .Select(filePath => new FileInfo(filePath))
                                         .Where(fileInfo => !IsExcluded(fileInfo, excludeList));

                // Add filtered files to the FileInfoList
                FileInfoList.AddRange(xisfFiles);

                // If recursion is not stopped, search subdirectories
                if (!stopRecursion)
                {
                    Directory.EnumerateDirectories(directoryPath)
                             .ToList()
                             .ForEach(subDirectory => SearchXisfFilesInDirectory(subDirectory, excludeList, stopRecursion));
                }
            }
            catch (Exception ex)
            {
                // Handle directory access errors, if any
                Console.WriteLine($"Error searching directory '{directoryPath}': {ex.Message}");
            }
        }

        // ***********************************************************************************
        // ***********************************************************************************

        /// <summary>
        /// Checks if the given file should be excluded based on the exclusion list.
        /// </summary>
        /// <param name="fileInfo">The file information object.</param>
        /// <param name="excludeList">List of file paths to exclude from the search.</param>
        /// <returns>True if the file should be excluded; otherwise, false.</returns>
        private static bool IsExcluded(FileInfo fileInfo, List<string> excludeList)
        {
            // Get the relative path of the file from the selected folder
            string name = fileInfo.FullName.Replace(SelectedFolder, string.Empty);

            // Check if any excludeItem is a substring of the file's relative path
            return excludeList.Any(excludeItem => name.Contains(excludeItem));
        }

        // ***********************************************************************************
        // ***********************************************************************************
    }
}
