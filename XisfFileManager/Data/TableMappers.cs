using Microsoft.Data.Sqlite;
using XisfFileManager.TargetScheduler.Tables;

namespace XisfFileManager.Data;

/// <summary>
/// Mapper for ProfilePreference table
/// </summary>
internal class ProfilePreferenceMapper : ITableMapper<ProfilePreference>
{
    public string TableName => "profilepreference";

    public ProfilePreference Map(SqliteDataReader reader) => new()
    {
        Id = reader.GetInt32("Id"),
        profileId = reader.GetString("profileId"),
        enableGradeRMS = reader.GetInt32("enableGradeRMS"),
        enableGradeStars = reader.GetInt32("enableGradeStars"),
        enableGradeHFR = reader.GetInt32("enableGradeHFR"),
        maxGradingSampleSize = reader.GetInt32("maxGradingSampleSize"),
        rmsPixelThreshold = reader.GetDouble("rmsPixelThreshold"),
        detectedStarsSigmaFactor = reader.GetDouble("detectedStarsSigmaFactor"),
        hfrSigmaFactor = reader.GetDouble("hfrSigmaFactor"),
        acceptimprovement = reader.GetInt32("acceptimprovement"),
        exposurethrottle = reader.GetDouble("exposurethrottle"),
        parkonwait = reader.GetInt32("parkonwait"),
    };
}

/// <summary>
/// Mapper for Project table
/// </summary>
internal class ProjectMapper : ITableMapper<Project>
{
    public string TableName => "project";

    public Project Map(SqliteDataReader reader) => new()
    {
        Id = reader.GetInt32("Id"),
        profileId = reader.GetString("profileId"),
        name = reader.GetString("name"),
        description = reader.GetString("description"),
        state = reader.GetInt32("state"),
        priority = reader.GetInt32("priority"),
        createdate = reader.GetInt32("createdate"),
        minimumtime = reader.GetInt32("minimumtime"),
        minimumaltitude = reader.GetDouble("minimumaltitude"),
        usecustomhorizon = reader.GetInt32("usecustomhorizon"),
        horizonoffset = reader.GetDouble("horizonoffset"),
        meridianwindow = reader.GetInt32("meridianwindow"),
        filterswitchfrequency = reader.GetInt32("filterswitchfrequency"),
        ditherevery = reader.GetInt32("ditherevery"),
        enablegrader = reader.GetInt32("enablegrader"),
        isMosaic = reader.GetInt32("isMosaic"),
    };
}

/// <summary>
/// Mapper for Target table
/// </summary>
internal class TargetMapper : ITableMapper<Target>
{
    public string TableName => "target";

    public Target Map(SqliteDataReader reader) => new()
    {
        Id = reader.GetInt32("Id"),
        name = reader.GetString("name"),
        active = reader.GetInt32("active"),
        ra = reader.GetDouble("ra"),
        dec = reader.GetDouble("dec"),
        epochcode = reader.GetInt32("epochcode"),
        rotation = reader.GetDouble("rotation"),
        roi = reader.GetDouble("roi"),
        projectid = reader.GetInt32("projectid"),
    };
}

/// <summary>
/// Mapper for ExposurePlan table
/// </summary>
internal class ExposurePlanMapper : ITableMapper<ExposurePlan>
{
    public string TableName => "exposureplan";

    public ExposurePlan Map(SqliteDataReader reader) => new()
    {
        Id = reader.GetInt32("Id"),
        profileId = reader.GetString("profileId"),
        exposure = reader.GetDouble("exposure"),
        desired = reader.GetInt32("desired"),
        acquired = reader.GetInt32("acquired"),
        accepted = reader.GetInt32("accepted"),
        targetid = reader.GetInt32("targetid"),
        exposureTemplateId = reader.GetInt32("exposureTemplateId"),
    };
}

/// <summary>
/// Mapper for ExposureTemplate table
/// </summary>
internal class ExposureTemplateMapper : ITableMapper<ExposureTemplate>
{
    public string TableName => "exposuretemplate";

    public ExposureTemplate Map(SqliteDataReader reader) => new()
    {
        Id = reader.GetInt32("Id"),
        profileId = reader.GetString("profileId"),
        name = reader.GetString("name"),
        filtername = reader.GetString("filtername"),
        gain = reader.GetInt32("gain"),
        offset = reader.GetInt32("offset"),
        bin = reader.GetInt32("bin"),
        readoutmode = reader.GetInt32("readoutmode"),
        twilightlevel = reader.GetInt32("twilightlevel"),
        moonavoidanceenabled = reader.GetInt32("moonavoidanceenabled"),
        moonavoidanceseparation = reader.GetInt32("moonavoidanceseparation"),
        moonavoidancewidth = reader.GetInt32("moonavoidancewidth"),
        maximumhumidity = reader.GetInt32("maximumhumidity"),
        defaultexposure = reader.GetInt32("defaultexposure"),
    };
}

/// <summary>
/// Mapper for AcquiredImage table
/// </summary>
internal class AcquiredImageMapper : ITableMapper<AcquiredImage>
{
    public string TableName => "acquiredimage";

    public AcquiredImage Map(SqliteDataReader reader) => new()
    {
        Id = reader.GetInt32("Id"),
        projectId = reader.GetInt32("projectId"),
        targetId = reader.GetInt32("targetId"),
        acquireddate = reader.GetInt32("acquireddate"),
        filtername = reader.GetString("filtername"),
        accepted = reader.GetInt32("accepted"),
        metadata = reader.GetString("metadata"),
        rejectreason = reader.GetStringOrEmpty("rejectreason"),
    };
}

/// <summary>
/// Mapper for RuleWeight table
/// </summary>
internal class RuleWeightMapper : ITableMapper<RuleWeight>
{
    public string TableName => "ruleweight";

    public RuleWeight Map(SqliteDataReader reader) => new()
    {
        Id = reader.GetInt32("Id"),
        name = reader.GetString("name"),
        weight = reader.GetDouble("weight"),
        projectid = reader.GetInt32("projectid"),
    };
}

/// <summary>
/// Mapper for ImageData table
/// </summary>
internal class ImageDataMapper : ITableMapper<ImageData>
{
    public string TableName => "imagedata";

    public ImageData Map(SqliteDataReader reader) => new()
    {
        Id = reader.GetInt32("Id"),
        tag = reader.GetString("tag"),
        imagedata = reader.GetBytes("imagedata"),
        acquiredimageid = reader.GetInt32("acquiredimageid"),
    };
}
