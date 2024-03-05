using System;
using System.Collections.Generic;
using System.IO;
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

        public static DialogResult FindTargetFiles(string defaultFolderPath, List<string> mXisfExclude)
        {
            FileInfoList = new List<System.IO.FileInfo>();

            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Select Xisf Folder Tree";
                folderDialog.ShowNewFolderButton = false;

                if (!string.IsNullOrEmpty(defaultFolderPath) && Directory.Exists(defaultFolderPath))
                {
                    // Set the initial directory to the default folder path
                    folderDialog.SelectedPath = defaultFolderPath;
                }

                if (folderDialog.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(folderDialog.SelectedPath))
                {
                    // Get the selected folder path
                    SelectedFolder = folderDialog.SelectedPath;

                    // Search for .xisf files within the selected folder and its subdirectories
                    SearchXisfFilesInDirectory(SelectedFolder, mXisfExclude, !Recurse);
                }

                return string.IsNullOrEmpty(SelectedFolder) ? DialogResult.Cancel : DialogResult.OK;
            }
        }
       
        // ***********************************************************************************
        // ***********************************************************************************

        public static bool FindCalibrationFiles(string defaultFolderPath, List<string> mXisfExclude, bool continueRecursion = true)
        {
            FileInfoList = new List<System.IO.FileInfo>();

            if (!string.IsNullOrEmpty(defaultFolderPath) && Directory.Exists(defaultFolderPath))
            {
                // Temporarily set the current directory to defaultFolderPath
                string previousCurrentDirectory = Environment.CurrentDirectory;
                Environment.CurrentDirectory = defaultFolderPath;

                // Search for .xisf files within the selected folder and its subdirectories
                SearchXisfFilesInDirectory(defaultFolderPath, mXisfExclude, !continueRecursion);

                // Restore the previous current directory
                Environment.CurrentDirectory = previousCurrentDirectory;
            }
            else
            {
                if (!string.IsNullOrEmpty(defaultFolderPath))
                {
                    // Search for .xisf files within the selected folder and its subdirectories
                    SearchXisfFilesInDirectory(SelectedFolder, mXisfExclude, !continueRecursion);
                }
            }

            return !string.IsNullOrEmpty(defaultFolderPath);
        }

        // ***********************************************************************************
        // ***********************************************************************************

        private static void SearchXisfFilesInDirectory(string directoryPath, List<string> excludeList, bool stopRecursion)
        {
            try
            {
                string[] files = Directory.GetFiles(directoryPath, "*.xisf");

                foreach (string filePath in files)
                {
                    FileInfo fileInfo = new FileInfo(filePath);

                    if (!IsExcluded(fileInfo, directoryPath, excludeList))
                    {
                        FileInfoList.Add(fileInfo);
                    }
                }

                if (!stopRecursion)
                {
                    string[] subDirectories = Directory.GetDirectories(directoryPath);

                    foreach (string subDirectory in subDirectories)
                    {
                        // Recursively search within subdirectories
                        SearchXisfFilesInDirectory(subDirectory, excludeList, stopRecursion);
                    }
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

        private static bool IsExcluded(FileInfo fileInfo, string directoryPath, List<string> excludeList)
        {
            string name = fileInfo.FullName.Replace(SelectedFolder, string.Empty);

            foreach (string excludeItem in excludeList)
            {
                if (name.Contains(excludeItem))
                {
                    return true;
                }
            }
            return false;
        }

        // ***********************************************************************************
        // ***********************************************************************************
    }
}
