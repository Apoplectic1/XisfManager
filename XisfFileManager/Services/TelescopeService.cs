using System.Drawing;
using XisfFileManager.Files;
using XisfFileManager.Models;
using XisfFileManager.Models.Telescopes;

namespace XisfFileManager.Services;

/// <summary>
/// Service for telescope detection, analysis, and keyword management
/// </summary>
public class TelescopeService
{
    /// <summary>
    /// All registered telescope configurations
    /// </summary>
    public static IReadOnlyList<TelescopeConfiguration> AllTelescopes { get; } =
    [
        APM107Telescope.Instance,
        EvoStar150Telescope.Instance,
        Newtonian254Telescope.Instance
    ];

    /// <summary>
    /// Find telescope configuration by identifier
    /// </summary>
    public static TelescopeConfiguration? GetTelescope(string identifier) =>
        AllTelescopes.FirstOrDefault(t => t.Identifier == identifier);

    /// <summary>
    /// Detect telescope from a telescope name string
    /// </summary>
    public static TelescopeConfiguration? DetectTelescope(string telescopeName) =>
        AllTelescopes.FirstOrDefault(t => t.MatchesTelescope(telescopeName));

    /// <summary>
    /// Check if telescope name indicates Riccardi reducer is used
    /// </summary>
    public static bool HasRiccardiReducer(string telescopeName) =>
        !string.IsNullOrEmpty(telescopeName) && telescopeName.EndsWith('R');

    /// <summary>
    /// Analyze telescopes across a collection of files
    /// </summary>
    public static TelescopeAnalysis AnalyzeTelescopes(IEnumerable<XisfFile> files)
    {
        var fileList = files.ToList();
        var filesWithTelescope = fileList.Where(f => !string.IsNullOrEmpty(f.Telescope)).ToList();

        var telescopeCounts = AllTelescopes.ToDictionary(
            telescope => telescope,
            telescope => filesWithTelescope.Count(f => telescope.MatchesTelescope(f.Telescope)));

        var focalLengths = filesWithTelescope
            .Where(f => f.FocalLength > 0)
            .Select(f => f.FocalLength)
            .Distinct()
            .OrderBy(f => f)
            .ToList();

        return new TelescopeAnalysis
        {
            TotalFiles = fileList.Count,
            FilesWithTelescope = filesWithTelescope.Count,
            FilesWithReducer = filesWithTelescope.Count(f => HasRiccardiReducer(f.Telescope)),
            FilesWithFocalLength = filesWithTelescope.Count(f => f.FocalLength > 0),
            TelescopeCounts = telescopeCounts,
            DistinctFocalLengths = focalLengths
        };
    }

    /// <summary>
    /// Get the single telescope if all files use the same one, otherwise null
    /// </summary>
    public static TelescopeConfiguration? GetUniqueTelescope(TelescopeAnalysis analysis)
    {
        var found = analysis.TelescopeCounts.Where(kv => kv.Value > 0).ToList();
        if (found.Count == 1 && found[0].Value == analysis.FilesWithTelescope)
            return found[0].Key;
        return null;
    }

    /// <summary>
    /// Get radio button color based on telescope detection state
    /// </summary>
    public static Color GetRadioButtonColor(TelescopeConfiguration telescope, TelescopeAnalysis analysis)
    {
        int count = analysis.TelescopeCounts.GetValueOrDefault(telescope, 0);

        if (count == 0)
            return Color.Black;
        if (count == analysis.FilesWithTelescope)
            return Color.Black; // All files use this telescope
        return Color.Red; // Mixed telescopes
    }

    /// <summary>
    /// Get label color for focal length based on analysis
    /// </summary>
    public static Color GetFocalLengthLabelColor(TelescopeAnalysis analysis)
    {
        if (analysis.FilesWithFocalLength == 0)
            return Color.Red;
        if (analysis.FilesWithFocalLength != analysis.FilesWithTelescope)
            return Color.Red;
        if (analysis.HasMultipleFocalLengths)
            return Color.Red;
        return Color.Black;
    }

    /// <summary>
    /// Get Riccardi checkbox color based on analysis
    /// </summary>
    public static Color GetRiccardiColor(TelescopeAnalysis analysis)
    {
        if (analysis.FilesWithTelescope == 0)
            return Color.Black;
        if (analysis.FilesWithReducer == 0 || analysis.FilesWithReducer == analysis.FilesWithTelescope)
            return Color.Black;
        return Color.Red; // Inconsistent reducer usage
    }
}
