using XisfFileManager.Files;

namespace XisfFileManager.Models.Cameras;

/// <summary>
/// Atik Infinity Camera configuration (Color, 2018)
/// Fixed gain camera without gain/offset controls
/// </summary>
public sealed class A144Camera : CameraConfiguration
{
    public static A144Camera Instance { get; } = new();

    public override string Identifier => "144";
    public override string Name => "A144";
    public override string Description => "Atik Infinity (2018) QE:70%";
    public override double PixelSize => 6.45;
    public override double PixelSizeBinned => 12.9;
    public override bool IsColor => true;
    public override string? BayerPattern => "RGGB";
    public override bool HasGain => false;
    public override bool HasOffset => false;
    public override bool UsesFocuserTemperature => true;
    public override (int Gain, int Offset) NarrowbandPreset => (0, 0);
    public override (int Gain, int Offset) BroadbandPreset => (0, 0);

    /// <summary>
    /// A144 has fixed gain and no offset - override to add fixed gain keyword
    /// </summary>
    public override void ApplyKeywords(XisfFile file)
    {
        base.ApplyKeywords(file);
        file.AddKeyword("GAIN", "0.37", "Fixed");
        file.RemoveKeyword("OFFSET");
    }
}
