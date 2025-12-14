using XisfFileManager.Files;

namespace XisfFileManager.Models.Telescopes;

/// <summary>
/// APM Super ED 107mm Refractor configuration
/// </summary>
public sealed class APM107Telescope : TelescopeConfiguration
{
    public static APM107Telescope Instance { get; } = new();

    public override string Identifier => "APM";
    public override string Name => "APM107";
    public override string Description => "APM107 Super ED";
    public override int FocalLength => 700;
    public override int ReducedFocalLength => 531;
    public override double? ApertureDiameter => 107.0;
    public override double? ApertureArea => 8992.02;

    /// <summary>
    /// APM107 adds aperture diameter and area keywords
    /// </summary>
    public override void ApplyKeywords(XisfFile file, bool withReducer)
    {
        base.ApplyKeywords(file, withReducer);
        file.AddKeyword("APTDIA", ApertureDiameter!.Value.ToString("F1"), "Aperture Diameter in mm");
        file.AddKeyword("APTAREA", ApertureArea!.Value.ToString("F2"), "Aperture area in square mm minus obstructions");
    }
}
