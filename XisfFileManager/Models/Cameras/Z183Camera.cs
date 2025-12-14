namespace XisfFileManager.Models.Cameras;

/// <summary>
/// ZWO ASI183MM Pro Camera configuration (Monochrome, 2019)
/// </summary>
public sealed class Z183Camera : CameraConfiguration
{
    public static Z183Camera Instance { get; } = new();

    public override string Identifier => "183";
    public override string Name => "Z183";
    public override string Description => "ZWO ASI183MM Pro Camera (2019)";
    public override double PixelSize => 2.4;
    public override double PixelSizeBinned => 4.8;
    public override bool IsColor => false;
    public override string? BayerPattern => null;
    public override bool HasGain => true;
    public override bool HasOffset => true;
    public override bool UsesFocuserTemperature => false;
    public override (int Gain, int Offset) NarrowbandPreset => (111, 10);
    public override (int Gain, int Offset) BroadbandPreset => (53, 10);
}
