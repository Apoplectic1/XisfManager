using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using XisfFileManager.Files;

namespace XisfFileManager.DirectoryOperations;

sealed class DirectoryProperties
{
    public Dictionary<string, string> DirectoryStatistics { get; } = new Dictionary<string, string>();
    
    public void SetDirectoryFileStatistics(List<XisfFile> xFileList, bool bNoStatistics)
    {
        // Remove any existing statistics files of the form " - 1, 0.0" from the directory
        Directory.GetFiles(Directory.GetParent(Path.GetDirectoryName(xFileList.First().FilePath)).FullName)
             .Where(file => Regex.IsMatch(file, @"^(.*\\(?:Stars [LRGBSHO]|[LRGBSHO]|Shutter)) - \d+, \d+\.\d+"))
             .ToList()
             .ForEach(file => File.Delete(file));

        if (bNoStatistics)
            return;

        GetDirectoryStatistics(xFileList);

        // Create new statistics files of the form " - 1, 0.0" for each found directory
        DirectoryStatistics
            .Select(group => group.Value)
            .ToList()
            .ForEach(newStatistics => File.Create(newStatistics).Dispose());
    }

    
    public void GetDirectoryStatistics(List<XisfFile> xFileList)
    {
        DirectoryStatistics.Clear();

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

                double totalExposureTime = group.Sum(file => file.ExposureSeconds) / 3600.0;
                string statistics = $" - {group.Count()}, {totalExposureTime:F1}";
                groupName += statistics;

                DirectoryStatistics[group.Key] = groupName;
            });
    }
}