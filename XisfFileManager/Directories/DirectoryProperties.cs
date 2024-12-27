using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Windows.Media.Capture;
using XisfFileManager.Files;
using XisfFileManager.Globals;

namespace XisfFileManager.Files
{
    sealed class DirectoryProperties
    {
        public Dictionary<string, string> DirectoryStatistics { get; } = new();

        /// <summary>
        /// Sets the directory file statistics by removing old statistics files and creating new ones based on the provided XisfFile list.
        /// If the flag bNoStatistics is true, the method only removes old statistics files and skips the creation of new ones.
        /// </summary>
        /// <param name="xFileList">A list of XisfFile objects used to determine the directory and compute statistics.</param>
        /// <param name="bNoStatistics">A boolean flag indicating whether to skip the creation of new statistics files.</param>
        public void SetDirectoryFileStatistics(List<XisfFile> xFileList, bool bNoStatistics)
        {
            // Determine the root directory to search, based on the first file in the list
            var selectedDirectory = DirectoryOperations.SelectedFolder;

            // Search for all directories under the selectedDirectory that include "Captures"
            Directory
               .EnumerateFiles(selectedDirectory, "*", SearchOption.AllDirectories) // Recursively get all files
               .Where(file =>
                   // Match files ending with " - <digits>, <real number>" and no file extension
                   Regex.IsMatch(file, @" - \d+, \d+(\.\d+)?$"))
               .ToList()
               .ForEach(file =>
               {
                   if (file.Contains("Captures") || file.Contains("Mosaic"))
                       File.Delete(file);
               });

            // If the flag bNoStatistics is set to true, return after removing the old statistics files
            if (bNoStatistics)
                return;

            // Compute new directory statistics based on the provided list of XisfFile objects
            GetCameraAndFilterStatistics(xFileList);

            // Create new statistics files directly in the "Captures" directory
            // If the selected path contains "Mosaic", prepend the panel number to statistics file name
            DirectoryStatistics
                .ToList() // Iterate over the dictionary entries (key: directory path, value: statistics string)
                .ForEach(kvp =>
                {
                    string statisticsFilePath;
                    string capturesPath = Regex.Match(kvp.Key, @"^(.*?\\)Captures", RegexOptions.IgnoreCase).Groups[1].Value;

                    // Mosiac panel directory names can take two forms: "Panel xofy" or "Panel Name"
                    if (capturesPath.Contains("Mosaic"))
                    {
                        // First attempt to match the panel name in the form "Panel xofy"
                        // If the match fails, extract the panel name in the form "Panel Name"
                        string panelName = "P" + Regex.Match(kvp.Key, @"(?<=\\Panel\s*)\d+(?=of)").Value;

                        // If the panel name is not in the form "Panel xofy", match the panel name in the form "Panel Name"
                        if (panelName == "P")
                        {
                            // panelName is in the form "Panel Name"; extract the panel name
                            panelName = Regex.Match(kvp.Key, @"(?<=\\Panel\s*)[^\\]+").Value;
                            statisticsFilePath = Path.Combine(capturesPath, panelName + "  " + kvp.Value);
                        }
                        else
                        {
                            // panelName is in the form "Panel xofy"; extract the panel number
                            string panelNumber = "P" + Regex.Match(kvp.Key, @"(?<=\\Panel\s*)\d+(?=of)").Value;
                            statisticsFilePath = Path.Combine(capturesPath, panelNumber + "  " + kvp.Value);
                        }
                    }
                    else
                    {
                        // Single target captures (not mosaic)
                        statisticsFilePath = Path.Combine(capturesPath, kvp.Value);
                    }

                    // Create the new file in the "Captures" directory and immediately close it
                    File.Create(statisticsFilePath).Dispose();
                });
        }

        /// <summary>
        /// Computes and updates the directory statistics for a list of XisfFile objects.
        /// The statistics include the total number of files and the total exposure time in hours for each directory.
        /// The results are stored in the DirectoryStatistics dictionary.
        /// </summary>
        /// <param name="xFileList">A list of XisfFile objects used to compute the directory statistics.</param>
        public void GetCameraAndFilterStatistics(List<XisfFile> xFileList)
        {
            // Clear any existing directory statistics before computing new values
            DirectoryStatistics.Clear();

            foreach (var camera in GlobalValues.Cameras)
            {
                // Group the provided files by their directory paths
                xFileList
                    .GroupBy(file => Path.GetDirectoryName(file.FilePath)).Distinct().ToList() // Group files by their parent directory
                    .ForEach(group =>
                    {
                        // Store the name of the current group (directory path)
                        var groupName = group.Key;

                        if (groupName.Contains(camera))
                        {
                            // Calculate the total exposure time for all files in this group (convert seconds to hours)
                            double totalExposureTime = group.Sum(file => file.ExposureSeconds) / 3600.0;

                            // Construct the statistics string in the format " - [file count], [total exposure time]"
                            string statistics = $"{camera} - {Path.GetFileName(groupName)} - {group.Count()}, {totalExposureTime:F1}";
                            //groupName += statistics;

                            // Add the computed statistics to the DirectoryStatistics dictionary
                            DirectoryStatistics[group.Key] = statistics; // groupName;
                        }
                    });
            }
        }
    }
}
