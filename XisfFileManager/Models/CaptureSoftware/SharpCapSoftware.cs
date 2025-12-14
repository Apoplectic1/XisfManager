namespace XisfFileManager.Models.CaptureSoftware;

/// <summary>
/// SharpCap configuration
/// </summary>
public sealed class SharpCapSoftware : CaptureSoftwareConfiguration
{
    public static SharpCapSoftware Instance { get; } = new();

    public override string Identifier => "SCP";
    public override string DisplayName => "SharpCap";
}
