using System.Drawing;
using XisfFileManager.Files;
using XisfFileManager.Models;
using XisfFileManager.Models.CaptureSoftware;

namespace XisfFileManager.Services;

/// <summary>
/// Service for capture software detection, analysis, and keyword management
/// </summary>
public class CaptureSoftwareService
{
    /// <summary>
    /// All registered capture software configurations
    /// </summary>
    public static IReadOnlyList<CaptureSoftwareConfiguration> AllSoftware { get; } =
    [
        NinaSoftware.Instance,
        TheSkyXSoftware.Instance,
        SequenceGeneratorProSoftware.Instance,
        VoyagerSoftware.Instance,
        SharpCapSoftware.Instance
    ];

    /// <summary>
    /// Find capture software configuration by identifier
    /// </summary>
    public static CaptureSoftwareConfiguration? GetSoftware(string identifier) =>
        AllSoftware.FirstOrDefault(s => s.Identifier.Equals(identifier, StringComparison.OrdinalIgnoreCase));

    /// <summary>
    /// Detect capture software from a software name string
    /// </summary>
    public static CaptureSoftwareConfiguration? DetectSoftware(string softwareName) =>
        AllSoftware.FirstOrDefault(s => s.MatchesSoftware(softwareName));

    /// <summary>
    /// Analyze capture software across a collection of files
    /// </summary>
    public static CaptureSoftwareAnalysis AnalyzeSoftware(IEnumerable<XisfFile> files)
    {
        var fileList = files.ToList();
        var filesWithSoftware = fileList.Where(f => !string.IsNullOrEmpty(f.CaptureSoftware)).ToList();

        var softwareCounts = AllSoftware.ToDictionary(
            software => software,
            software => filesWithSoftware.Count(f => software.MatchesSoftware(f.CaptureSoftware)));

        return new CaptureSoftwareAnalysis
        {
            TotalFiles = fileList.Count,
            FilesWithSoftware = softwareCounts.Values.Sum(),
            SoftwareCounts = softwareCounts
        };
    }

    /// <summary>
    /// Get the single software if all files use the same one, otherwise null
    /// </summary>
    public static CaptureSoftwareConfiguration? GetUniqueSoftware(CaptureSoftwareAnalysis analysis)
    {
        var found = analysis.SoftwareCounts.Where(kv => kv.Value > 0).ToList();
        if (found.Count == 1 && found[0].Value == analysis.TotalFiles)
            return found[0].Key;
        return null;
    }

    /// <summary>
    /// Get radio button color based on software detection state
    /// </summary>
    public static Color GetRadioButtonColor(CaptureSoftwareConfiguration software, CaptureSoftwareAnalysis analysis)
    {
        int count = analysis.SoftwareCounts.GetValueOrDefault(software, 0);
        bool allFilesHaveSoftware = analysis.AllFilesHaveSoftware;

        if (!allFilesHaveSoftware)
        {
            // Missing at least one file's software
            return count == 0 ? Color.Red : Color.DarkViolet;
        }

        // All files have software
        if (count == 0 || count == analysis.TotalFiles)
            return Color.Black;

        // Mixed software usage
        return Color.DarkGreen;
    }

    /// <summary>
    /// Get button color based on analysis
    /// </summary>
    public static Color GetButtonColor(CaptureSoftwareAnalysis analysis)
    {
        return analysis.AllFilesHaveSoftware ? Color.Black : Color.Red;
    }

    /// <summary>
    /// Check if radio button should be checked based on analysis
    /// </summary>
    public static bool ShouldBeChecked(CaptureSoftwareConfiguration software, CaptureSoftwareAnalysis analysis)
    {
        if (!analysis.AllFilesHaveSoftware)
            return false;

        int count = analysis.SoftwareCounts.GetValueOrDefault(software, 0);
        return count == analysis.TotalFiles;
    }
}
