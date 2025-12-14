namespace XisfFileManager.Models.Telescopes;

/// <summary>
/// Sky-Watcher EvoStar 150ED configuration
/// </summary>
public sealed class EvoStar150Telescope : TelescopeConfiguration
{
    public static EvoStar150Telescope Instance { get; } = new();

    public override string Identifier => "EVO";
    public override string Name => "EVO150";
    public override string Description => "EvoStar 150";
    public override int FocalLength => 1000;
    public override int ReducedFocalLength => 750;
    public override double? ApertureDiameter => null;
    public override double? ApertureArea => null;
}
