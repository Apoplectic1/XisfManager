namespace XisfFileManager.Models.CaptureSoftware;

/// <summary>
/// Sequence Generator Pro configuration
/// </summary>
public sealed class SequenceGeneratorProSoftware : CaptureSoftwareConfiguration
{
    public static SequenceGeneratorProSoftware Instance { get; } = new();

    public override string Identifier => "SGP";
    public override string DisplayName => "SGPro";
}
