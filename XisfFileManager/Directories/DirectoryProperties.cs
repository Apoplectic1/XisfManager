using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using XisfFileManager.Files;

namespace XisfFileManager.DirectoryOperations;

sealed class DirectoryProperties
{
    public Dictionary<string, string> DirectoryStatistics = new Dictionary<string, string>();

    public void SetDirectoryStatistics(List<XisfFile> xFileList, bool bNoTotals)
    {
        IEnumerable<IGrouping<string, XisfFile>> directoryGroups = xFileList.GroupBy(path => Path.GetDirectoryName(path.FilePath));

        foreach (IGrouping<string, XisfFile> group in directoryGroups)
        {
            string groupName = group.Key;

            if (!bNoTotals)
            {
                //groupName = Path.GetFileName(groupName);


                // All occurrences of any of these words
                MatchCollection matches = Regex.Matches(groupName, @"(?:Luma|Red|Green|Blue|Ha|O3|S2|Shutter)");
                if (matches.Count > 0)
                {
                    // Get the last occurrence by accessing the last match in the collection
                    Match lastMatch = matches[matches.Count - 1];

                    // Trim the input string to remove anything after the last specified word
                    groupName = groupName.Substring(0, lastMatch.Index + lastMatch.Length);

                    double totalExposureTime = group.Sum(fileItem => fileItem.ExposureSeconds) / 3600.0;
                    string statistics = $" - {group.Count()}, {totalExposureTime:F1}";
                    groupName = groupName + statistics;
                }
            }

            DirectoryStatistics[group.Key] = groupName;
        }
    }
}