using System.IO;

namespace XisfFileManager.Helpers;

/// <summary>
/// Common file utility methods
/// </summary>
public static class FileHelpers
{
    /// <summary>
    /// Checks if a file is locked by another process
    /// </summary>
    /// <param name="path">The path to the file to check</param>
    /// <returns>True if the file is locked; otherwise, false</returns>
    public static bool IsFileLocked(string path)
    {
        try
        {
            using var stream = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            return false;
        }
        catch (IOException)
        {
            return true;
        }
    }

    /// <summary>
    /// Generates a unique filename by appending "-Dup" until no conflict exists
    /// </summary>
    /// <param name="filePath">The initial file path</param>
    /// <returns>A unique file path that does not already exist</returns>
    public static string GetUniqueFileName(string filePath)
    {
        while (File.Exists(filePath))
        {
            int lastParen = filePath.LastIndexOf(')');
            if (lastParen >= 0)
                filePath = filePath.Insert(lastParen + 1, "-Dup");
            else
                filePath = Path.Combine(
                    Path.GetDirectoryName(filePath) ?? "",
                    Path.GetFileNameWithoutExtension(filePath) + "-Dup" + Path.GetExtension(filePath));
        }
        return filePath;
    }
}
