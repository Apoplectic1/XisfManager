using XisfFileManager.Files;

namespace XisfFileManager.Models;

/// <summary>
/// Base class for capture software configurations
/// </summary>
public abstract class CaptureSoftwareConfiguration
{
    /// <summary>
    /// The identifier stored in SWCREATE keyword (e.g., "NINA", "TSX")
    /// </summary>
    public abstract string Identifier { get; }

    /// <summary>
    /// Display name for UI
    /// </summary>
    public abstract string DisplayName { get; }

    /// <summary>
    /// Comment for SWCREATE keyword
    /// </summary>
    public virtual string Comment => "[name] Equipment Control and Automation Application";

    /// <summary>
    /// Check if a software name matches this configuration
    /// </summary>
    public bool MatchesSoftware(string softwareName) =>
        !string.IsNullOrEmpty(softwareName) &&
        softwareName.Equals(Identifier, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Apply the SWCREATE keyword to a file
    /// </summary>
    public virtual void ApplyKeyword(XisfFile file)
    {
        file.AddKeyword("SWCREATE", Identifier, Comment);
    }
}

/// <summary>
/// Analysis results for capture software across files
/// </summary>
public record CaptureSoftwareAnalysis
{
    public int TotalFiles { get; init; }
    public int FilesWithSoftware { get; init; }
    public Dictionary<CaptureSoftwareConfiguration, int> SoftwareCounts { get; init; } = new();

    public bool AllFilesHaveSoftware => FilesWithSoftware == TotalFiles;
    public bool HasMixedSoftware => SoftwareCounts.Count(kv => kv.Value > 0) > 1;
}
