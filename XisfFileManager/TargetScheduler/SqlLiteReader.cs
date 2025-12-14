using Microsoft.Data.Sqlite;
using XisfFileManager.Data;
using XisfFileManager.TargetScheduler.Tables;

namespace XisfFileManager.TargetScheduler;

internal class SqlLiteReader
{
    private readonly SqlLiteManager _manager;

    // Singleton mapper instances
    private static readonly ProfilePreferenceMapper _profilePreferenceMapper = new();
    private static readonly ProjectMapper _projectMapper = new();
    private static readonly TargetMapper _targetMapper = new();
    private static readonly ExposurePlanMapper _exposurePlanMapper = new();
    private static readonly ExposureTemplateMapper _exposureTemplateMapper = new();
    private static readonly AcquiredImageMapper _acquiredImageMapper = new();
    private static readonly RuleWeightMapper _ruleWeightMapper = new();
    private static readonly ImageDataMapper _imageDataMapper = new();

    public SqlLiteReader(SqlLiteManager manager)
    {
        _manager = manager;
    }

    private void ClearAllTables()
    {
        _manager.mAcquiredImageList.Clear();
        _manager.mExposurePlanList.Clear();
        _manager.mExposureTemplateList.Clear();
        _manager.mImageDataList.Clear();
        _manager.mProfilePreferenceList.Clear();
        _manager.mProjectList.Clear();
        _manager.mRuleWeightList.Clear();
        _manager.mTargetList.Clear();
    }

    /// <summary>
    /// Generic method to read all rows from a table using a mapper
    /// </summary>
    private static List<T> ReadTable<T>(SqliteConnection connection, ITableMapper<T> mapper) where T : new()
    {
        var results = new List<T>();

        using var command = new SqliteCommand($"SELECT * FROM {mapper.TableName}", connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            results.Add(mapper.Map(reader));
        }

        return results;
    }

    public bool ReadTargetSchedulerDataBaseFile(string sqlLightFileName)
    {
        ClearAllTables();

        using var connection = new SqliteConnection($"Data Source={sqlLightFileName};");
        connection.Open();

        // Read all tables using generic mapper pattern
        _manager.mProfilePreferenceList = ReadTable(connection, _profilePreferenceMapper);
        _manager.mProjectList = ReadTable(connection, _projectMapper)
            .OrderBy(p => p.name).ToList();
        _manager.mTargetList = ReadTable(connection, _targetMapper)
            .OrderBy(t => t.name).ToList();
        _manager.mExposurePlanList = ReadTable(connection, _exposurePlanMapper);
        _manager.mExposureTemplateList = ReadTable(connection, _exposureTemplateMapper);
        _manager.mAcquiredImageList = ReadTable(connection, _acquiredImageMapper);
        _manager.mRuleWeightList = ReadTable(connection, _ruleWeightMapper);
        _manager.mImageDataList = ReadTable(connection, _imageDataMapper);

        return true;
    }
}
