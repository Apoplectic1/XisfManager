namespace XisfFileManager.Models.Telescopes;

/// <summary>
/// 10-inch Newtonian Reflector configuration
/// </summary>
public sealed class Newtonian254Telescope : TelescopeConfiguration
{
    public static Newtonian254Telescope Instance { get; } = new();

    public override string Identifier => "NWT";
    public override string Name => "NWT254";
    public override string Description => "10 Inch Newtonian";
    public override int FocalLength => 1100;
    public override int ReducedFocalLength => 825;
    public override double? ApertureDiameter => null;
    public override double? ApertureArea => null;
}
