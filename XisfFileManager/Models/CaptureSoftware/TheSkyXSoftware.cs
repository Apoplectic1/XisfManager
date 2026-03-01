namespace XisfFileManager.Models.CaptureSoftware;

/// <summary>
/// TheSkyX configuration
/// </summary>
public sealed class TheSkyXSoftware : CaptureSoftwareConfiguration
{
    public static TheSkyXSoftware Instance { get; } = new();

    public override string Identifier => "TSX";
    public override string DisplayName => "TheSkyX";
}
