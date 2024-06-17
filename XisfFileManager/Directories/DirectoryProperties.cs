using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using XisfFileManager.Files;

namespace XisfFileManager.DirectoryOperations;

sealed class DirectoryProperties
{
    public Dictionary<string, string> DirectoryStatistics { get; } = [];


    /// <summary>
    /// Sets the directory file statistics by removing old statistics files and creating new ones based on the provided XisfFile list.
    /// If the flag bNoStatistics is true, the method only removes old statistics files and skips the creation of new ones.
    /// </summary>
    /// <param name="xFileList">A list of XisfFile objects used to determine the directory and compute statistics.</param>
    /// <param name="bNoStatistics">A boolean flag indicating whether to skip the creation of new statistics files.</param>
    public void SetDirectoryFileStatistics(List<XisfFile> xFileList, bool bNoStatistics)
    {
        // Remove any existing statistics files of the form " - 1, 0.0" from the directory
        Directory.GetFiles(Directory.GetParent(Path.GetDirectoryName(xFileList.First().FilePath)).FullName)
            .Where(file => Regex.IsMatch(file, @"^(.*\\(?:Stars [LRGBSHO]|[LRGBSHO]|Shutter)) - \d+, \d+\.\d+"))
            .ToList()
            .ForEach(file => File.Delete(file));

        // If no statistics are to be generated, return after removing old statistics files
        if (bNoStatistics)
            return;

        // Compute the directory statistics using the provided XisfFile list
        GetDirectoryStatistics(xFileList);

        // Create new statistics files of the form " - 1, 0.0" for each found directory
        DirectoryStatistics
            .Select(group => group.Value)
            .ToList()
            .ForEach(newStatistics => File.Create(newStatistics).Dispose());
    }


    /// <summary>
    /// Computes and updates the directory statistics for a list of XisfFile objects.
    /// The statistics include the total number of files and the total exposure time in hours for each directory.
    /// The results are stored in the DirectoryStatistics dictionary.
    /// </summary>
    /// <param name="xFileList">A list of XisfFile objects used to compute the directory statistics.</param>
    public void GetDirectoryStatistics(List<XisfFile> xFileList)
    {
        // Clear existing directory statistics
        DirectoryStatistics.Clear();

        // Group the files by their directory paths
        xFileList
            .GroupBy(file => Path.GetDirectoryName(file.FilePath))
            .ToList()
            .ForEach(group =>
            {
                var groupName = group.Key;

                // Match the first occurrence of any of these words after the last backslash
                var match = Regex.Match(groupName, @"^(.*\\(?:Stars [LRGBSHO]|[LRGBSHO]|Shutter))");

                if (match.Success)
                {
                    groupName = match.Groups[1].Value;
                }

                // Calculate the total exposure time in hours
                double totalExposureTime = group.Sum(file => file.ExposureSeconds) / 3600.0;
                string statistics = $" - {group.Count()}, {totalExposureTime:F1}";
                groupName += statistics;

                // Update the DirectoryStatistics dictionary with the computed statistics
                DirectoryStatistics[group.Key] = groupName;
            });
    }
}