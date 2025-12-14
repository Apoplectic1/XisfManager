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

## Phase 3: UI Helper Methods

### Files to Modify
- `MainForm\Camera.cs`

### Extract Common Patterns

#### ComboBox Clearing (lines 30-86 repeated 16x)
```csharp
private static void ClearComboBox(ComboBox cb)
{
    cb.DataSource = null;
    cb.Text = string.Empty;
    cb.Items.Clear();
}

private void ClearAllCameraComboBoxes()
{
    foreach (var cb in _allCameraComboBoxes)
        ClearComboBox(cb);
}
```

#### Color Update Pattern (repeated 50+ times)
```csharp
private static void UpdateLabelColor(Label label, PropertyAnalysis analysis)
{
    label.ForeColor = analysis switch
    {
        { NoValues: true } or { MissingValues: true } => Color.Red,
        { DifferentValues: true } => Color.Green,
        _ => Color.Black
    };
}
```

---

## Phase 4: Generic Database Repository

### Files to Modify
- `TargetScheduler\SqlLiteReader.cs`

### New Files to Create
- `Data\IEntityMapper.cs`
- `Data\SqliteRepository.cs`
- `Data\Mappers\ProjectMapper.cs`, `TargetMapper.cs`, etc.

### Pattern
**Before (repeated 8x for each table):**
```csharp
using (SqliteCommand command = new SqliteCommand("SELECT * FROM project", connection))
{
    using (SqliteDataReader reader = command.ExecuteReader())
    {
        while (reader.Read())
        {
            Project row = new Project { Id = reader.GetInt32(...), ... };
            mSqlManager.mProjectList.Add(row);
        }
    }
}
```

**After (generic, once):**
```csharp
public async Task<List<T>> ReadAllAsync<T>(IEntityMapper<T> mapper) where T : new()
{
    await using var cmd = new SqliteCommand($"SELECT * FROM {mapper.TableName}", _connection);
    await using var reader = await cmd.ExecuteReaderAsync();
    return await mapper.MapAllAsync(reader);
}

// Usage
var projects = await _repo.ReadAllAsync(new ProjectMapper());
var targets = await _repo.ReadAllAsync(new TargetMapper());
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
| 3 | Phase 3: UI helpers | Low | Pending |
| 4 | Phase 5: Async/DoEvents | Medium | Pending |
| 5 | Phase 5: Exception handling | Low | Pending |
| 6 | Phase 4: Generic repository | Medium | Pending |
| 7 | Phase 6: Constants | Low | Pending |
| 8 | Phase 7: Nullable | Low | Pending (~90 warnings) |
| 9 | Phase 8: FluxDensity | Low | Pending |

---

## Testing Checkpoints

| After | Test | Status |
|-------|------|--------|
| Phase 1 | Build, launch, browse files | ✅ Verified |
| Phase 2 | All 4 cameras detected, SetAll/SetByFile work | ✅ Verified |
| Phase 3 | UI updates correctly, colors work | Pending |
| Phase 4 | Target Scheduler tab loads | Pending |
| Phase 5 | UI responsive during file operations | Pending |
| Phase 8 | Full regression test | Pending |

---

## Critical Files Summary

| File | Changes |
|------|---------|
| `XisfFileManager.csproj` | .NET 9, nullable enable |
| `MainForm\Camera.cs` | Major refactor (-1000 lines) |
| `TargetScheduler\SqlLiteReader.cs` | Generic repository |
| `Files\XisfFileUpdate.cs` | Async, exception handling |
| `MainForm\FluxDensity.cs` | Consolidate duplicates |
| `Globals\Globals.cs` | Add camera registry |

### New Files
- `Models\CameraConfiguration.cs`
- `Models\Cameras\Z533Camera.cs`, `Z183Camera.cs`, `Q178Camera.cs`, `A144Camera.cs`
- `Services\CameraService.cs`
- `Data\SqliteRepository.cs`
- `Configuration\AppPaths.cs`
