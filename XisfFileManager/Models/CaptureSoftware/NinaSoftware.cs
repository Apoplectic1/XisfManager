namespace XisfFileManager.Models.CaptureSoftware;

/// <summary>
/// N.I.N.A. (Nighttime Imaging 'N' Astronomy) configuration
/// </summary>
public sealed class NinaSoftware : CaptureSoftwareConfiguration
{
    public static NinaSoftware Instance { get; } = new();

    public override string Identifier => "NINA";
    public override string DisplayName => "N.I.N.A.";
}
