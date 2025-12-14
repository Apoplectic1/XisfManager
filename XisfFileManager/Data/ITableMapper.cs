using Microsoft.Data.Sqlite;

namespace XisfFileManager.Data;

/// <summary>
/// Interface for mapping database rows to entity objects
/// </summary>
/// <typeparam name="T">The entity type to map to</typeparam>
public interface ITableMapper<T> where T : new()
{
    /// <summary>
    /// The database table name
    /// </summary>
    string TableName { get; }

    /// <summary>
    /// Map a single row from the reader to an entity
    /// </summary>
    T Map(SqliteDataReader reader);
}
