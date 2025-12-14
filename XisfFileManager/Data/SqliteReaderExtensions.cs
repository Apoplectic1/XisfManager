using Microsoft.Data.Sqlite;

namespace XisfFileManager.Data;

/// <summary>
/// Extension methods for SqliteDataReader to simplify null-safe reading
/// </summary>
public static class SqliteReaderExtensions
{
    /// <summary>
    /// Gets an Int32 value by column name
    /// </summary>
    public static int GetInt32(this SqliteDataReader reader, string columnName) =>
        reader.GetInt32(reader.GetOrdinal(columnName));

    /// <summary>
    /// Gets a nullable Int32 value by column name
    /// </summary>
    public static int? GetInt32OrNull(this SqliteDataReader reader, string columnName)
    {
        var ordinal = reader.GetOrdinal(columnName);
        return reader.IsDBNull(ordinal) ? null : reader.GetInt32(ordinal);
    }

    /// <summary>
    /// Gets a String value by column name
    /// </summary>
    public static string GetString(this SqliteDataReader reader, string columnName) =>
        reader.GetString(reader.GetOrdinal(columnName));

    /// <summary>
    /// Gets a String value by column name, returning empty string if null
    /// </summary>
    public static string GetStringOrEmpty(this SqliteDataReader reader, string columnName)
    {
        var ordinal = reader.GetOrdinal(columnName);
        return reader.IsDBNull(ordinal) ? string.Empty : reader.GetString(ordinal);
    }

    /// <summary>
    /// Gets a Double value by column name
    /// </summary>
    public static double GetDouble(this SqliteDataReader reader, string columnName) =>
        reader.GetDouble(reader.GetOrdinal(columnName));

    /// <summary>
    /// Gets a nullable Double value by column name
    /// </summary>
    public static double? GetDoubleOrNull(this SqliteDataReader reader, string columnName)
    {
        var ordinal = reader.GetOrdinal(columnName);
        return reader.IsDBNull(ordinal) ? null : reader.GetDouble(ordinal);
    }

    /// <summary>
    /// Gets a byte array value by column name
    /// </summary>
    public static byte[] GetBytes(this SqliteDataReader reader, string columnName) =>
        reader.GetFieldValue<byte[]>(reader.GetOrdinal(columnName));
}
