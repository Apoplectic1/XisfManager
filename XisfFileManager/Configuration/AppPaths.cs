using System.IO;

namespace XisfFileManager.Configuration
{
    public static class AppPaths
    {
        // Calibration library
        public static string CalibrationLibrary => @"E:\Photography\Astro Photography\Calibration";

        // Default browse location
        public static string DefaultProcessingFolder => @"E:\Photography\Astro Photography\Processing\";

        // FluxDensity preprocessing paths
        public static string PreProcessingRoot => @"F:\PreProcessing";
        public static string FluxDensityDir => Path.Combine(PreProcessingRoot, "FluxDensity");
        public static string DebayeredDir => Path.Combine(PreProcessingRoot, "debayered");
        public static string CosmetizedDir => Path.Combine(PreProcessingRoot, "cosmetized");
        public static string CalibratedDir => Path.Combine(PreProcessingRoot, "calibrated");
    }
}
