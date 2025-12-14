# XisfFileManager Modernization Plan

## Overview
Comprehensive refactoring of the XISF File Manager application: upgrade to .NET 9, eliminate duplicate code, fix anti-patterns, and apply SOLID principles. All 4 cameras (Z533, Z183, Q178, A144) remain active.

---

## Phase 1: .NET 9 Upgrade & Project Configuration ✅ COMPLETE

### Files Modified
- `XisfFileManager\XisfFileManager.csproj`

### Changes Applied
```xml
<TargetFramework>net9.0-windows10.0.22621.0</TargetFramework>
<LangVersion>latest</LangVersion>
<Nullable>enable</Nullable>
<ImplicitUsings>enable</ImplicitUsings>
```

**Note:** Microsoft.CSharp stayed at 4.7.0 (4.8.0 doesn't exist)

### Verification ✅
- Build succeeds (with nullable warnings)
- Application launches and basic file browsing works

---

## Phase 2: Camera Configuration Abstraction ✅ COMPLETE

**Result:** Reduced Camera.cs from 1367 lines to 381 lines (72% reduction)

### Files Created
- `Models\CameraConfiguration.cs` - Base class with PropertyAnalysis<T>
- `Models\Cameras\Z533Camera.cs` - ZWO ASI533MC Pro (cooled color)
- `Models\Cameras\Z183Camera.cs` - ZWO ASI183MM Pro (cooled mono)
- `Models\Cameras\Q178Camera.cs` - QHYCCD QHY5III178M (guide camera)
- `Models\Cameras\A144Camera.cs` - Atik Infinity (fixed gain color)
- `Services\CameraService.cs` - Detection, analysis, color helpers

### Files Modified
- `MainForm\Camera.cs` - Replaced 1000+ lines with dictionary-based service calls

### Key Features Implemented
- Generic `PropertyAnalysis<T>` for analyzing file properties
- Camera-specific temperature handling (sensor vs focuser)
- Proper binning pixel size calculation
- NB/BB preset support per camera

---

## Phase 2B: Telescope Configuration Abstraction ✅ COMPLETE

**Result:** Reduced Telescope.cs from 232 lines to 204 lines (12% reduction)

### Files Created
- `Models\TelescopeConfiguration.cs` - Base class with TelescopeAnalysis record
- `Models\Telescopes\APM107Telescope.cs` - APM107 Super ED (700mm/531mm, includes aperture data)
- `Models\Telescopes\EvoStar150Telescope.cs` - Sky-Watcher EvoStar 150ED (1000mm/750mm)
- `Models\Telescopes\Newtonian254Telescope.cs` - 10-inch Newtonian (1100mm/825mm)
- `Services\TelescopeService.cs` - Detection, analysis, color helpers

### Files Modified
- `MainForm\Telescope.cs` - Replaced hardcoded if/else chains with dictionary-based service calls

### Key Features Implemented
- Singleton pattern with `Instance` property (matches camera pattern)
- `GetFocalLength(bool withReducer)` handles Riccardi 0.75x reduction
- `ApplyKeywords()` virtual method - APM107 overrides to add aperture data
- `TelescopeAnalysis` record for file analysis results
- Color coding logic centralized in `TelescopeService`

---

## Phase 3: UI Helpers + CaptureSoftware Abstraction ✅ COMPLETE

**Result:** Reduced CaptureSoftware.cs from 176 lines to 107 lines (39% reduction)

### Files Created
- `Helpers\UIHelpers.cs` - Common UI control manipulation methods
- `Models\CaptureSoftwareConfiguration.cs` - Base class with CaptureSoftwareAnalysis record
- `Models\CaptureSoftware\NinaSoftware.cs` - N.I.N.A. configuration
- `Models\CaptureSoftware\TheSkyXSoftware.cs` - TheSkyX configuration
- `Models\CaptureSoftware\SequenceGeneratorProSoftware.cs` - SGPro configuration
- `Models\CaptureSoftware\VoyagerSoftware.cs` - Voyager configuration
- `Models\CaptureSoftware\SharpCapSoftware.cs` - SharpCap configuration
- `Services\CaptureSoftwareService.cs` - Detection, analysis, color helpers

### Files Modified
- `MainForm\CaptureSoftware.cs` - Replaced hardcoded if/else chains with dictionary-based service calls

### Key Features Implemented
- `UIHelpers.ClearComboBox()` - Consolidated combobox clearing
- `UIHelpers.ResetRadioButton/ResetCheckBox()` - Common control reset patterns
- `UIHelpers.ResetControlColors/SetControlColors()` - Batch color operations
- Singleton pattern with `Instance` property (matches Camera/Telescope pattern)
- `CaptureSoftwareAnalysis` record for file analysis results
- Color coding logic centralized in `CaptureSoftwareService`

---

## Phase 4: Generic Database Repository ✅ COMPLETE

**Result:** Reduced SqlLiteReader.cs from 251 lines to 77 lines (69% reduction)

### Files Created
- `Data\ITableMapper.cs` - Generic mapper interface
- `Data\SqliteReaderExtensions.cs` - Extension methods for null-safe column reading
- `Data\TableMappers.cs` - 8 mapper implementations for all Target Scheduler tables

### Files Modified
- `TargetScheduler\SqlLiteReader.cs` - Replaced 8 repeated table reading blocks with generic ReadTable<T> method

### Key Features Implemented
- `ITableMapper<T>` interface with TableName and Map() method
- Extension methods: GetInt32, GetString, GetDouble, GetStringOrEmpty, GetBytes
- Generic `ReadTable<T>` method that works with any mapper
- Singleton mapper instances for performance

### Pattern Applied
```csharp
// Generic method reads any table
private static List<T> ReadTable<T>(SqliteConnection connection, ITableMapper<T> mapper) where T : new()
{
    var results = new List<T>();
    using var command = new SqliteCommand($"SELECT * FROM {mapper.TableName}", connection);
    using var reader = command.ExecuteReader();
    while (reader.Read())
        results.Add(mapper.Map(reader));
    return results;
}

// Usage - 8 tables in 8 lines
_manager.mProjectList = ReadTable(connection, _projectMapper);
_manager.mTargetList = ReadTable(connection, _targetMapper);
// ... etc
```

---

## Phase 5: Async/Await & Remove Anti-patterns

### Files to Modify
- `Files\XisfFileUpdate.cs`
- `MainForm\FluxDensity.cs`
- `MainForm\FileSelection.cs`
- `Calibration\Calibration.cs`

### 5.1 Replace Thread.Sleep (XisfFileUpdate.cs:84-88)
```csharp
// Before
while (IsFileLocked(path) && delay < 100) { Thread.Sleep(10); delay++; }

// After
private async Task<bool> WaitForFileUnlockAsync(string path, CancellationToken ct)
{
    for (int i = 0; i < 100 && !ct.IsCancellationRequested; i++)
    {
        if (!IsFileLocked(path)) return true;
        await Task.Delay(10, ct);
    }
    return false;
}
```

### 5.2 Remove Application.DoEvents (14 occurrences)
```csharp
// Before
foreach (var file in files) {
    ProcessFile(file);
    Application.DoEvents();  // BAD
}

// After
private async Task ProcessFilesAsync(IProgress<int> progress, CancellationToken ct)
{
    for (int i = 0; i < files.Count; i++)
    {
        await ProcessFileAsync(files[i], ct);
        progress.Report(i + 1);
    }
}
```

### 5.3 Fix Exception Handling (XisfFileUpdate.cs:284-290)
```csharp
// Before - DANGEROUS
catch { Environment.Exit(-1); }

// After
catch (IOException ex)
{
    _logger?.LogError(ex, "Failed to write {Path}", path);
    throw new FileProcessingException($"Failed to write: {path}", ex);
}
```

---

## Phase 6: Configuration & Constants

### New Files to Create
- `Configuration\AppPaths.cs`
- `Configuration\CameraConstants.cs`

### Extract Magic Numbers
```csharp
public static class AppPaths
{
    public static string DefaultProcessingRoot =>
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                     "Astro Photography", "Processing");
    public static string CalibrationLibrary { get; set; } =
        @"E:\Photography\Astro Photography\Calibration";
}

public static class BufferSizes
{
    public const int MaxFileRead = 1_073_741_824;  // 1 GB
    public const int FileWriteBuffer = 81_920;     // 80 KB
}
```

---

## Phase 7: Nullable Reference Types (Incremental)

### Order of Files (simplest first)
1. `Keyword\Keyword.cs` (9 lines)
2. `Globals\Globals.cs` (35 lines)
3. `Files\Buffer.cs`
4. `Models\CameraConfiguration.cs` (new)
5. `Keyword\KeywordList.cs`
6. `Files\XisfFile.cs`
7. Remaining files with `#nullable enable` directive

---

## Phase 8: Duplicate File I/O (FluxDensity.cs)

### Files to Modify
- `MainForm\FluxDensity.cs`

### Issue
Lines 54-166 and 199-319 are nearly identical file processing loops.

### Solution
Extract to shared method:
```csharp
private async Task<List<XisfFile>> LoadAndProcessFilesAsync(
    string directory,
    string pattern,
    IProgress<FileProgress> progress,
    CancellationToken ct)
{
    var files = Directory.GetFiles(directory, pattern, SearchOption.AllDirectories);
    var result = new List<XisfFile>();

    for (int i = 0; i < files.Length; i++)
    {
        ct.ThrowIfCancellationRequested();
        var xFile = await _xmlReader.ReadAsync(files[i], ct);
        result.Add(xFile);
        progress.Report(new FileProgress(i + 1, files.Length, files[i]));
    }
    return result;
}
```

---

## Implementation Order

| Step | Phase | Risk | Status |
|------|-------|------|--------|
| 1 | Phase 1: .NET 9 upgrade | Low | ✅ Complete |
| 2 | Phase 2: CameraConfiguration | Medium | ✅ Complete (-986 lines) |
| 3 | Phase 2B: TelescopeConfiguration | Low | ✅ Complete (-28 lines) |
| 4 | Phase 3: UIHelpers + CaptureSoftware | Low | ✅ Complete (-69 lines) |
| 5 | Phase 4: Generic repository | Medium | ✅ Complete (-174 lines) |
| 6 | Phase 5: Async/DoEvents | Medium | Pending |
| 7 | Phase 5: Exception handling | Low | Pending |
| 8 | Phase 6: Constants | Low | Pending |
| 9 | Phase 7: Nullable | Low | Pending (~90 warnings) |
| 10 | Phase 8: FluxDensity | Low | Pending |

---

## Testing Checkpoints

| After | Test | Status |
|-------|------|--------|
| Phase 1 | Build, launch, browse files | ✅ Verified |
| Phase 2 | All 4 cameras detected, SetAll/SetByFile work | ✅ Verified |
| Phase 2B | All 3 telescopes detected, Riccardi reducer works | ✅ Verified |
| Phase 3 | All 5 capture software detected, UIHelpers work | ✅ Verified |
| Phase 4 | Target Scheduler tab loads, all 8 tables read | ✅ Verified |
| Phase 5 | UI responsive during file operations | Pending |
| Phase 8 | Full regression test | Pending |

---

## Critical Files Summary

| File | Changes |
|------|---------|
| `XisfFileManager.csproj` | .NET 9, nullable enable |
| `MainForm\Camera.cs` | Major refactor (-986 lines) ✅ |
| `MainForm\Telescope.cs` | Refactor (-28 lines) ✅ |
| `MainForm\CaptureSoftware.cs` | Refactor (-69 lines) ✅ |
| `TargetScheduler\SqlLiteReader.cs` | Generic repository (-174 lines) ✅ |
| `Files\XisfFileUpdate.cs` | Async, exception handling |
| `MainForm\FluxDensity.cs` | Consolidate duplicates |
| `Globals\Globals.cs` | Add camera registry |

### New Files
- `Helpers\UIHelpers.cs`
- `Models\CameraConfiguration.cs`
- `Models\Cameras\Z533Camera.cs`, `Z183Camera.cs`, `Q178Camera.cs`, `A144Camera.cs`
- `Services\CameraService.cs`
- `Models\TelescopeConfiguration.cs`
- `Models\Telescopes\APM107Telescope.cs`, `EvoStar150Telescope.cs`, `Newtonian254Telescope.cs`
- `Services\TelescopeService.cs`
- `Models\CaptureSoftwareConfiguration.cs`
- `Models\CaptureSoftware\NinaSoftware.cs`, `TheSkyXSoftware.cs`, `SequenceGeneratorProSoftware.cs`, `VoyagerSoftware.cs`, `SharpCapSoftware.cs`
- `Services\CaptureSoftwareService.cs`
- `Data\ITableMapper.cs`, `SqliteReaderExtensions.cs`, `TableMappers.cs`
- `Configuration\AppPaths.cs` (planned)
