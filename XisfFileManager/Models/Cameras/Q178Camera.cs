namespace XisfFileManager.Models.Cameras;

/// <summary>
/// QHYCCD QHY5III178M Camera configuration (Monochrome, 2018)
/// </summary>
public sealed class Q178Camera : CameraConfiguration
{
    public static Q178Camera Instance { get; } = new();

    public override string Identifier => "178";
    public override string Name => "Q178";
    public override string Description => "QHYCCD QHY5III178M (2018) QE:81%";
    public override double PixelSize => 2.4;
    public override double PixelSizeBinned => 4.8;
    public override bool IsColor => false;
    public override string? BayerPattern => null;
    public override bool HasGain => true;
    public override bool HasOffset => true;
    public override bool UsesFocuserTemperature => true;
    public override (int Gain, int Offset) NarrowbandPreset => (40, 15);
    public override (int Gain, int Offset) BroadbandPreset => (40, 15);
}
