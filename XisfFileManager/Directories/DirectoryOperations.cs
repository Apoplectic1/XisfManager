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
        // Static Properties
        // ***********************************************************************************

        public static List<System.IO.FileInfo> FileInfoList { get; private set; } = new List<System.IO.FileInfo>();
        public static bool Recurse { get; set; } = true;
        public static string SelectedFolder { get; private set; }

        // ***********************************************************************************
        // Methods
        // ***********************************************************************************

        /// <summary>
        /// Prompts the user to select a folder and searches for .xisf files within the selected folder and its subdirectories.
        /// </summary>
        public static DialogResult FindTargetFiles(string defaultFolderPath, List<string> mXisfExclude)
        {
            FileInfoList = new List<System.IO.FileInfo>();

            using (FolderBrowserDialog folderDialog = new())
            {
                folderDialog.Description = "Select Xisf Folder Tree";
                folderDialog.ShowNewFolderButton = false;

                // Set the initial directory to display
                if (!string.IsNullOrEmpty(defaultFolderPath) && Directory.Exists(defaultFolderPath))
                {
                    folderDialog.SelectedPath = @"E:\Photography\Astro Photography\Processing\";
                }

                // Show the folder dialog
                if (folderDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(folderDialog.SelectedPath))
                {
                    SelectedFolder = folderDialog.SelectedPath;

                    // Search for Xisf files in the selected directory
                    SearchXisfFilesInDirectory(SelectedFolder, mXisfExclude, !Recurse);
                }

                // Return OK or Cancel based on the user's selection
                return string.IsNullOrEmpty(SelectedFolder) ? DialogResult.Cancel : DialogResult.OK;
            }
        }


        /// <summary>
        /// Finds calibration files (.xisf) in the specified folder and its subdirectories.
        /// </summary>
        public static bool FindCalibrationFiles(string defaultFolderPath, List<string> mXisfExclude, bool continueRecursion = true)
        {
            FileInfoList = new List<System.IO.FileInfo>();

            if (!string.IsNullOrEmpty(defaultFolderPath) && Directory.Exists(defaultFolderPath))
            {
                string previousCurrentDirectory = Environment.CurrentDirectory;
                Environment.CurrentDirectory = defaultFolderPath;

                SearchXisfFilesInDirectory(defaultFolderPath, mXisfExclude, !continueRecursion);

                Environment.CurrentDirectory = previousCurrentDirectory;
            }
            else if (!string.IsNullOrEmpty(defaultFolderPath))
            {
                SearchXisfFilesInDirectory(SelectedFolder, mXisfExclude, !continueRecursion);
            }

            return !string.IsNullOrEmpty(defaultFolderPath);
        }

        /// <summary>
        /// Searches for .xisf files in the specified directory and its subdirectories.
        /// </summary>
        private static int SearchXisfFilesInDirectory(string directoryPath, List<string> excludeList, bool noRecursion)
        {
            try
            {
                var xisfFiles = Directory.EnumerateFiles(directoryPath, "*.xisf")
                                         .Select(filePath => new FileInfo(filePath))
                                         .Where(fileInfo => !IsExcluded(fileInfo, excludeList));
                /*
                // Alternative method to exclude files based on the exclusion list - directory names are excluded
                var xisfFiles = Directory.EnumerateFiles(directoryPath, "*.xisf")
                         .Select(filePath => new FileInfo(filePath))
                         .Where(fileInfo => !excludeList.Contains(fileInfo.DirectoryName));
                */

                FileInfoList.AddRange(xisfFiles);

                if (!noRecursion)
                {
                    Directory.EnumerateDirectories(directoryPath)
                             .ToList()
                             .ForEach(subDirectory => SearchXisfFilesInDirectory(subDirectory, excludeList, noRecursion));
                }

                return xisfFiles.Count();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching directory '{directoryPath}': {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Checks if the given file should be excluded based on the exclusion list.
        /// </summary>
        private static bool IsExcluded(FileInfo fileInfo, List<string> excludeList)
        {
            string name = fileInfo.FullName.Replace(SelectedFolder, string.Empty);
            return excludeList.Any(excludeItem => name.Contains(excludeItem));
        }

        /// <summary>
        /// Gets a list of unique file paths from a list of XisfFile objects.
        /// </summary>
        public static List<string> GetUniqueFilePaths(List<XisfFile> xFileList)
        {
            return xFileList.Select(xFile => Path.GetDirectoryName(xFile.FilePath)).Distinct().ToList();
        }
    }
}
