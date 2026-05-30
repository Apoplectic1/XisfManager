using XisfFileManager.Files;

namespace XisfFileManager.Models;

/// <summary>
/// Base class for camera configuration. Each camera model has a concrete implementation.
/// </summary>
public abstract class CameraConfiguration
{
    /// <summary>Camera identifier string used to match files (e.g., "533", "183")</summary>
    public abstract string Identifier { get; }

    /// <summary>Short camera name (e.g., "Z533")</summary>
    public abstract string Name { get; }

    /// <summary>Full camera description for INSTRUME keyword</summary>
    public abstract string Description { get; }

    /// <summary>Pixel size in microns at 1x1 binning</summary>
    public abstract double PixelSize { get; }

    /// <summary>Pixel size in microns at 2x2 binning</summary>
    public abstract double PixelSizeBinned { get; }

    /// <summary>True if color camera, false if monochrome</summary>
    public abstract bool IsColor { get; }

    /// <summary>Bayer pattern (e.g., "RGGB") or null for monochrome</summary>
    public abstract string? BayerPattern { get; }

    /// <summary>True if camera supports gain adjustment</summary>
    public abstract bool HasGain { get; }

    /// <summary>True if camera supports offset adjustment</summary>
    public abstract bool HasOffset { get; }

    /// <summary>True if camera uses focuser temperature instead of sensor temperature</summary>
    public abstract bool UsesFocuserTemperature { get; }

    /// <summary>Narrowband preset (Gain, Offset)</summary>
    public abstract (int Gain, int Offset) NarrowbandPreset { get; }

    /// <summary>Broadband preset (Gain, Offset)</summary>
    public abstract (int Gain, int Offset) BroadbandPreset { get; }

    /// <summary>
    /// Check if a camera name string matches this camera configuration
    /// </summary>
    public bool MatchesCamera(string cameraName) =>
        !string.IsNullOrEmpty(cameraName) && cameraName.Contains(Identifier, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Get pixel size based on binning value
    /// </summary>
    public double GetPixelSizeForBinning(int binning) =>
        binning == 2 ? PixelSizeBinned : PixelSize;

    /// <summary>
    /// Get temperature from file based on camera type
    /// </summary>
    public double GetTemperature(XisfFile file) =>
        UsesFocuserTemperature ? file.FocuserTemperature : file.SensorTemperature;

    /// <summary>
    /// Set temperature on file based on camera type
    /// </summary>
    public void SetTemperature(XisfFile file, double temperature)
    {
        if (UsesFocuserTemperature)
        {
            file.FocuserTemperature = temperature;
            file.SensorTemperature = temperature;
        }
        else
        {
            file.SensorTemperature = temperature;
        }
    }

    /// <summary>
    /// Apply camera-specific keywords to an XISF file
    /// </summary>
    public virtual void ApplyKeywords(XisfFile file)
    {
        file.AddKeyword("INSTRUME", Name, Description);
        file.AddKeyword("NAXIS1", file.TargetAttachmentWidth.ToString(), "Sensor Horizontal Pixel Width");
        file.AddKeyword("NAXIS2", file.TargetAttachmentHeight.ToString(), "Sensor Vertical Pixel Height");

        if (IsColor)
        {
            file.AddKeyword("BAYERPAT", BayerPattern!);
            file.AddKeyword("COLORSPC", "Color", "Color Image");
        }
        else
        {
            file.AddKeyword("COLORSPC", "Grayscale", "Monochrome Image");
        }

        var pixelSize = GetPixelSizeForBinning(file.Binning).ToString("F2");
        file.AddKeyword("XPIXSZ", pixelSize, "Sensor Horizontal Pixel Size in Microns");
        file.AddKeyword("YPIXSZ", pixelSize, "Sensor Vertical Pixel Size in Microns");
    }
}

/// <summary>
/// Result of analyzing a property across files for a specific camera
/// </summary>
public class PropertyAnalysis<T>
{
    public bool NoValues { get; init; }
    public bool MissingValues { get; init; }
    public bool DifferentValues { get; init; }
    public bool UniqueValue { get; init; }
    public List<T> DistinctValues { get; init; } = [];
    public int TotalCameraFiles { get; init; }
}
