using XisfFileManager.Files;

namespace XisfFileManager.Models;

/// <summary>
/// Base class for telescope configuration. Each telescope model has a concrete implementation.
/// </summary>
public abstract class TelescopeConfiguration
{
    /// <summary>Telescope identifier string used to match files (e.g., "APM", "EVO", "NWT")</summary>
    public abstract string Identifier { get; }

    /// <summary>Short telescope name (e.g., "APM107")</summary>
    public abstract string Name { get; }

    /// <summary>Full telescope description</summary>
    public abstract string Description { get; }

    /// <summary>Focal length in mm without reducer</summary>
    public abstract int FocalLength { get; }

    /// <summary>Focal length in mm with Riccardi 0.75x reducer</summary>
    public abstract int ReducedFocalLength { get; }

    /// <summary>Aperture diameter in mm (null if not tracked)</summary>
    public abstract double? ApertureDiameter { get; }

    /// <summary>Aperture area in square mm minus obstructions (null if not tracked)</summary>
    public abstract double? ApertureArea { get; }

    /// <summary>Riccardi reducer reduction factor</summary>
    public const double RiccardiReductionFactor = 0.75;

    /// <summary>
    /// Check if a telescope name string matches this telescope configuration
    /// </summary>
    public bool MatchesTelescope(string telescopeName) =>
        !string.IsNullOrEmpty(telescopeName) && telescopeName.Contains(Identifier, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Get focal length based on whether reducer is used
    /// </summary>
    public int GetFocalLength(bool withReducer) =>
        withReducer ? ReducedFocalLength : FocalLength;

    /// <summary>
    /// Get telescope name with optional R suffix for reducer
    /// </summary>
    public string GetTelescopeName(bool withReducer) =>
        withReducer ? $"{Name}R" : Name;

    /// <summary>
    /// Apply telescope-specific keywords to an XISF file
    /// </summary>
    public virtual void ApplyKeywords(XisfFile file, bool withReducer)
    {
        string reducerText = withReducer ? " with Riccardi 0.75 Reducer" : " without Reducer";
        file.AddKeyword("TELESCOP", GetTelescopeName(withReducer), $"{Description}{reducerText}");
        file.AddKeyword("FOCALLEN", GetFocalLength(withReducer).ToString(), $"{Description}{reducerText}");

        if (ApertureDiameter is double diameter)
        {
            file.AddKeyword("APTDIA", diameter.ToString("F1"), "Aperture Diameter in mm");

            if (ApertureArea is double area)
                file.AddKeyword("APTAREA", area.ToString("F2"), "Aperture area in square mm (obstructions ignored)");

            // FOCRATIO is derived inside the FocalRatio setter from the FOCALLEN and APTDIA
            // keywords written above; the value assigned here is recomputed there.
            file.FocalRatio = GetFocalLength(withReducer) / diameter;
        }
    }
}

/// <summary>
/// Result of analyzing telescopes across files
/// </summary>
public class TelescopeAnalysis
{
    public int TotalFiles { get; init; }
    public int FilesWithTelescope { get; init; }
    public int FilesWithReducer { get; init; }
    public int FilesWithFocalLength { get; init; }
    public Dictionary<TelescopeConfiguration, int> TelescopeCounts { get; init; } = [];
    public List<double> DistinctFocalLengths { get; init; } = [];
    public bool HasMultipleTelescopes => TelescopeCounts.Count(kv => kv.Value > 0) > 1;
    public bool HasInconsistentReducer => FilesWithReducer > 0 && FilesWithReducer < FilesWithTelescope;
    public bool HasMultipleFocalLengths => DistinctFocalLengths.Count > 1;
}
