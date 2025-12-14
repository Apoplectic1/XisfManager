namespace XisfFileManager.Models.Cameras;

/// <summary>
/// ZWO ASI533MC Pro Camera configuration (Color, 2021)
/// </summary>
public sealed class Z533Camera : CameraConfiguration
{
    public static Z533Camera Instance { get; } = new();

    public override string Identifier => "533";
    public override string Name => "Z533";
    public override string Description => "ZWO ASI533MC Pro Camera (2021)";
    public override double PixelSize => 3.76;
    public override double PixelSizeBinned => 7.52;
    public override bool IsColor => true;
    public override string? BayerPattern => "RGGB";
    public override bool HasGain => true;
    public override bool HasOffset => true;
    public override bool UsesFocuserTemperature => false;
    public override (int Gain, int Offset) NarrowbandPreset => (100, 50);
    public override (int Gain, int Offset) BroadbandPreset => (100, 50);
}
