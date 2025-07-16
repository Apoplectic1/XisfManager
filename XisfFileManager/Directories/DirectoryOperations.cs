using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace XisfFileManager.Files
{
    public enum ExcludeType
    {
        None,
        Contains,
        Exact
    }

    public static class DirectoryOperations
    {
        // ***********************************************************************************
        // Static Properties
        // ***********************************************************************************

        public static List<System.IO.FileInfo> FileInfoList { get; private set; } = new List<System.IO.FileInfo>();
        public static bool Recurse { get; set; } = true;
        public static string SelectedFolder { get; set; }

        // ***********************************************************************************
        // Methods
        // ***********************************************************************************

        /// <summary>
        /// Prompts the user to select a folder and searches for .xisf files within the selected folder and its subdirectories.
        /// </summary>
        public static DialogResult FindTargetFilesDialog(string defaultFolder, List<string> exclude, ExcludeType excludeType = ExcludeType.Exact)
        {
            FileInfoList = new List<FileInfo>();

            var dlg = new CommonOpenFileDialog
            {
                Title = "Select Xisf Folder Tree",
                IsFolderPicker = true,
                InitialDirectory = Directory.Exists(defaultFolder)
                                   ? defaultFolder
                                   : @"E:\Photography\Astro Photography\Processing\"
            };

            if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
            {
                SelectedFolder = dlg.FileName;
                SearchXisfFilesInDirectory(SelectedFolder, exclude, !Recurse, excludeType);
                return DialogResult.OK;
            }

            return DialogResult.Cancel;
        }

        /// <summary>
        /// Searches for .xisf files in a specified directory path without showing a dialog.
        /// </summary>
        /// <param name="searchPath">The absolute path of the directory to search.</param>
        /// <param name="excludeList">A list of strings used to exclude certain files or directories from the search.</param>
        /// <returns>DialogResult.OK if the directory exists and the search completes; otherwise, DialogResult.Cancel.</returns>
        public static DialogResult FindTargetFilesString(string searchPath, List<string> excludeList, ExcludeType excludeType = ExcludeType.None)
        {
            // Clear the results from any previous search
            FileInfoList = new List<FileInfo>();

            // Check if the provided directory path is valid
            if (string.IsNullOrEmpty(searchPath) || !Directory.Exists(searchPath))
            {
                return DialogResult.Cancel; // Return Cancel if path is not found
            }

            // Set the selected folder property for consistency with your other methods
            SelectedFolder = searchPath;

            // Call the existing private search method, preserving the recursion logic
            SearchXisfFilesInDirectory(SelectedFolder, excludeList, !Recurse, excludeType);

            // Return OK to indicate the operation was successful
            return DialogResult.OK;
        }

        /// <summary>
        /// Finds calibration files (.xisf) in the specified folder and its subdirectories.
        /// </summary>
        public static bool FindCalibrationFiles(string defaultFolderPath, List<string> mXisfExclude, bool continueRecursion = true, ExcludeType excludeType = ExcludeType.Exact)
        {
            FileInfoList = new List<System.IO.FileInfo>();

            if (!string.IsNullOrEmpty(defaultFolderPath) && Directory.Exists(defaultFolderPath))
            {
                string previousCurrentDirectory = Environment.CurrentDirectory;
                Environment.CurrentDirectory = defaultFolderPath;

                SearchXisfFilesInDirectory(defaultFolderPath, mXisfExclude, !continueRecursion, excludeType);

                Environment.CurrentDirectory = previousCurrentDirectory;
            }
            else if (!string.IsNullOrEmpty(defaultFolderPath))
            {
                SearchXisfFilesInDirectory(SelectedFolder, mXisfExclude, !continueRecursion, excludeType);
            }

            return !string.IsNullOrEmpty(defaultFolderPath);
        }

        /// <summary>
        /// Searches for .xisf files in the specified directory and its subdirectories.
        /// </summary>
        private static int SearchXisfFilesInDirectory(string directoryPath, List<string> excludeList, bool noRecursion, ExcludeType excludeType)
        {
            try
            {
                var xisfFiles = Directory.EnumerateFiles(directoryPath, "*.xisf")
                                         .Select(filePath => new FileInfo(filePath))
                                         .Where(fileInfo => !IsExcluded(fileInfo, excludeList, excludeType));
               

                FileInfoList.AddRange(xisfFiles);

                if (!noRecursion)
                {
                    Directory.EnumerateDirectories(directoryPath)
                             .ToList()
                             .ForEach(subDirectory => SearchXisfFilesInDirectory(subDirectory, excludeList, noRecursion, excludeType));
                }

                return xisfFiles.Count();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching directory '{directoryPath}': {ex.Message}");
                return 0;
            }
        }

        // Update all references from ExludeType/exludeType to ExcludeType
        private static bool IsExcluded(FileInfo fileInfo, List<string> excludeList, ExcludeType excludeType = ExcludeType.Exact)
        {
            // Get the full path of the directory where the file resides.
            string directoryPath = fileInfo.DirectoryName;

            // If the directory path is null or the exclude list is empty, don't exclude.
            if (string.IsNullOrEmpty(directoryPath) || !excludeList.Any())
            {
                return false;
            }

            switch (excludeType)
            {
                case ExcludeType.Exact:
                    // If excludeType is Exact, return true (exclude the file) if any directories in the directory path
                    // matche any of the items from the exclusion list exactly.
                    var equalsSet = new HashSet<string>(excludeList, StringComparer.Ordinal);
                    return directoryPath.Split(Path.DirectorySeparatorChar).Any(dir => equalsSet.Contains(dir));
                case ExcludeType.Contains:
                    // If excludeType is Contains, return true (exclude the file) if the directory path
                    // contains any of the items from the exclusion list.
                    return excludeList.Any(excludeItem => directoryPath.Contains(excludeItem, StringComparison.Ordinal));
                default:
                    // If no specific exclusion type is provided, default to not excluding.
                    return false;
            }
        }
    }
}
