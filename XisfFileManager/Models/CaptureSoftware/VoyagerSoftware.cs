namespace XisfFileManager.Models.CaptureSoftware;

/// <summary>
/// Voyager configuration
/// </summary>
public sealed class VoyagerSoftware : CaptureSoftwareConfiguration
{
    public static VoyagerSoftware Instance { get; } = new();

    public override string Identifier => "VOY";
    public override string DisplayName => "Voyager";
}
