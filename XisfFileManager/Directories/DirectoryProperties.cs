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

    public void GetDirectoryStatistics(List<XisfFile> xFileList)
    {
        var directoryGroups = xFileList.GroupBy(file => Path.GetDirectoryName(file.FilePath));

        foreach (var group in directoryGroups)
        {
            var groupName = group.Key;

            // Match the first occurrence of any of these words after the last backslash
            var match = Regex.Match(groupName, @"^(.*\\(?:Stars R|Stars G|Stars B|L|R|G|B|H|O|S|Shutter))");

            if (match.Success)
            {
                groupName = match.Groups[1].Value;
            }

            double totalExposureTime = group.Sum(file => file.ExposureSeconds) / 3600.0;
            string statistics = $" - {group.Count()}, {totalExposureTime:F1}";
            groupName += statistics;

            DirectoryStatistics[group.Key] = groupName;
        }
    }
}