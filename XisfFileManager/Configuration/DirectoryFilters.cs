using System.Collections.Generic;

namespace XisfFileManager.Configuration
{
    public static class DirectoryFilters
    {
        public static List<string> CalibrationExcludes => new() { "PreProcessing", "Duplicates", "Registered", "Calibrated", "Project" };
        public static List<string> BrowseExcludes => new() { "Calibration", "Duplicates", "Master", "Project" };
        public static List<string> FluxDensityCfaExcludes => new() { "CFA" };
    }
}
