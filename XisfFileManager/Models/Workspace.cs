using XisfFileManager.Calculations;
using XisfFileManager.Files;

namespace XisfFileManager.Models;

/// <summary>
/// Owns the shared, mutable data for one working session: the loaded XISF files,
/// the per-image numeric lists used for statistics, and the directory statistics.
/// MainForm partials read these through a single mWorkspace instance instead of
/// holding bare fields, so data flow and ownership are explicit. Services receive
/// this data as parameters; Workspace itself has no UI or behavior dependencies.
/// </summary>
public sealed class Workspace
{
    /// <summary>The set of XISF files currently loaded (sorted oldest-first after a Browse).</summary>
    public List<XisfFile> Files { get; } = [];

    /// <summary>Transient cursor: the file being created/read inside a load loop.</summary>
    public XisfFile? CurrentFile { get; set; }

    /// <summary>Per-image numeric lists (focuser position/temperature, ambient temperature) for statistics.</summary>
    public ImageCalculations ImageParameters { get; } = new();

    /// <summary>Directory statistics computed during rename.</summary>
    public DirectoryProperties DirectoryProperties { get; } = new();

    /// <summary>Clears the loaded session data (files + derived numeric lists).</summary>
    public void Clear()
    {
        Files.Clear();
        ImageParameters.Clear();
    }
}
