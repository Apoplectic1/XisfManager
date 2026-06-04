namespace XisfFileManager.Configuration
{
    public static class XisfConstants
    {
        public const int SignatureSize = 16;
        public const int MaxFileReadBytes = 1_000_000_000;  // 1 GB

        // The zlib+sh / sha-1 codec constants moved to Astronomy.XISF.Compression (the shared block codec).
    }
}
