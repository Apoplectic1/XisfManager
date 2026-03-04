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

## Phase 5A: Fix Exception Handling ✅ COMPLETE

**Result:** Removed dangerous `Environment.Exit(-1)` and bare `catch` pattern

### Files Modified
- `Files\XisfFileUpdate.cs` - Fixed exception handling in UpdateFile method

### Changes Made
```csharp
// Before - DANGEROUS
catch
{
    DialogResult status = MessageBox.Show("Update Write File Failed", ...);
    if (status == DialogResult.OK)
        return false;
    Environment.Exit(-1);  // Kills app without cleanup!
}

// After - Safe
catch (IOException ex)
{
    MessageBox.Show($"Update Write File Failed: {ex.Message}", ...);
    return false;
}
catch (Exception ex)
{
    MessageBox.Show($"Unexpected error updating file: {ex.Message}", ...);
    return false;
}
```

---

## Phase 5C: XisfFileRename Cleanup ✅ COMPLETE

**Result:** Reduced XisfFileRename.cs from 334 lines to 263 lines (21% reduction), consolidated duplicates

### Files Created
- `Helpers\FileHelpers.cs` - Shared file utilities (IsFileLocked, GetUniqueFileName)

### Files Modified
- `Files\XisfFileRename.cs` - Major cleanup and modernization
- `Files\XisfFileUpdate.cs` - Now uses FileHelpers.IsFileLocked()
- `MainForm\FileSelection.cs` - Updated for new tuple syntax

### Changes Made
| Change | Description |
|--------|-------------|
| Removed dead code | `mFileList` field (never used), instance `MoveDuplicates()` method |
| Consolidated duplicates | `IsFileLocked()` moved to shared `FileHelpers` class |
| Added constant | `NoTemperature = -273.0` replaces magic value |
| Modernized syntax | `Tuple<int,string>` → `(int Status, string FileName)` value tuple |
| Extracted helpers | 8 format helper methods for cleaner BuildFileName logic |

### Helper Methods Added
- `BuildMasterFileName()`, `BuildMasterLightName()`
- `BuildDarkFileName()`, `BuildBiasFileName()`, `BuildFlatFileName()`, `BuildLightFileName()`
- `FormatCommonDetails()`, `FormatCameraInfo()`, `FormatCaptureInfo()`
- `FormatTelescopeInfo()`, `FormatFocuserInfo()`, `FormatRotation()`
- `FormatAlgorithmSuffix()`, `FormatWeightIndex()`

---

## Phase 5B: Async/Await Conversion ✅ COMPLETE

**Result:** Replaced all `Application.DoEvents()` and `Thread.Sleep()` with proper `async/await`

### Files Modified
- `Files\XisfFileUpdate.cs` - `Thread.Sleep` → `await Task.Delay`
- `MainForm\FluxDensity.cs` - Removed 12 `Application.DoEvents` calls, replaced with `await Task.Yield()`
- `MainForm\FileSelection.cs` - Removed `Application.DoEvents`
- `MainForm\MainForm.cs` - Removed `Application.DoEvents` calls

### Verification ✅
- Build succeeds
- UI remains responsive during file operations

---

## Phase 6: Configuration & Constants ✅ COMPLETE

**Result:** Extracted all hardcoded paths, magic numbers, and exclude lists into 3 new Configuration classes

### Files Created
- `Configuration\AppPaths.cs` - Machine-specific paths (`E:\`, `F:\` drive literals)
- `Configuration\XisfConstants.cs` - XISF format constants (`SignatureSize = 16`, `MaxFileReadBytes = 1_000_000_000`)
- `Configuration\DirectoryFilters.cs` - Directory exclude lists (CalibrationExcludes, BrowseExcludes, FluxDensityCfaExcludes)

### Files Modified
- `MainForm\FluxDensity.cs` - 7 path literals → `AppPaths.*`, 1 exclude list → `DirectoryFilters.FluxDensityCfaExcludes`
- `MainForm\Calibration.cs` - 1 path literal → `AppPaths.CalibrationLibrary`
- `Directories\DirectoryOperations.cs` - 1 path literal → `AppPaths.DefaultProcessingFolder`
- `Files\XisfFileUpdate.cs` - 2× `(int)1e9` → `XisfConstants.MaxFileReadBytes`, 3× `16` → `XisfConstants.SignatureSize`
- `Files\XisfXmlReader.cs` - 2× `16` → `XisfConstants.SignatureSize`
- `Calibration\Calibration.cs` - Inline exclude list → `DirectoryFilters.CalibrationExcludes`
- `MainForm\MainForm.cs` - Inline exclude list → `DirectoryFilters.BrowseExcludes`

### Verification ✅
- Build succeeds with 0 errors
- No `@"E:\` or `@"F:\` literals remain outside `Configuration/`

---

## Phase 7: Nullable Reference Types ✅ COMPLETE

**Result:** Resolved all 107 CS8xxx nullable warnings across 22 files

### Verification ✅
- Build succeeds with 0 CS warnings
- All nullable reference type annotations applied project-wide

---

## Phase 8: FluxDensity Duplicate Code Consolidation ✅ COMPLETE

**Result:** Reduced FluxDensity.cs from 310 lines to 161 lines (48% reduction)

### Files Modified
- `MainForm\FluxDensity.cs`

### Changes Made
| Change | Description |
|--------|-------------|
| `ResetFluxDensityState()` | Extracted duplicated UI/state reset code (clear lists, combo boxes, progress bars, calibration groups) |
| `ReadFluxDensityFilesAsync()` | Extracted duplicated file discovery + read loop + Find*() calls |
| `WriteFilesToFluxDensityAsync()` | Extracted duplicated write loop (keywords, output path, create directory, UpdateFileAsync) |
| `SetupFluxDensity()` | Simplified to clean 2-pass pipeline calling the 3 helpers |
| Removed redundant double-assignment | `Label = Label = ...` → `Label = ...` |
| Removed unnecessary `.ToString()` | On a string value |

### Verification ✅
- Build succeeds with 0 errors

---

## Implementation Order

| Step | Phase | Risk | Status |
|------|-------|------|--------|
| 1 | Phase 1: .NET 9 upgrade | Low | ✅ Complete |
| 2 | Phase 2: CameraConfiguration | Medium | ✅ Complete (-986 lines) |
| 3 | Phase 2B: TelescopeConfiguration | Low | ✅ Complete (-28 lines) |
| 4 | Phase 3: UIHelpers + CaptureSoftware | Low | ✅ Complete (-69 lines) |
| 5 | Phase 4: Generic repository | Medium | ✅ Complete (-174 lines) |
| 6 | Phase 5A: Exception handling | Low | ✅ Complete |
| 7 | Phase 5C: XisfFileRename cleanup | Low | ✅ Complete (-71 lines) |
| 8 | Phase 5B: Async/DoEvents | Medium | ✅ Complete |
| 9 | Phase 6: Constants | Low | ✅ Complete |
| 10 | Phase 7: Nullable | Low | ✅ Complete |
| 11 | Phase 8: FluxDensity | Low | ✅ Complete (-149 lines) |

---

## Testing Checkpoints

| After | Test | Status |
|-------|------|--------|
| Phase 1 | Build, launch, browse files | ✅ Verified |
| Phase 2 | All 4 cameras detected, SetAll/SetByFile work | ✅ Verified |
| Phase 2B | All 3 telescopes detected, Riccardi reducer works | ✅ Verified |
| Phase 3 | All 5 capture software detected, UIHelpers work | ✅ Verified |
| Phase 4 | Target Scheduler tab loads, all 8 tables read | ✅ Verified |
| Phase 5A | File errors handled gracefully, no app crash | ✅ Verified |
| Phase 5C | File rename works, duplicate detection works | ✅ Verified |
| Phase 5B | UI responsive during file operations | ✅ Verified |
| Phase 6 | Build succeeds, no hardcoded paths outside Configuration/ | ✅ Verified |
| Phase 7 | Build succeeds with 0 CS warnings | ✅ Verified |
| Phase 8 | Full regression test | ✅ Build verified |

---

## Critical Files Summary

| File | Changes |
|------|---------|
| `XisfFileManager.csproj` | .NET 9, nullable enable |
| `MainForm\Camera.cs` | Major refactor (-986 lines) ✅ |
| `MainForm\Telescope.cs` | Refactor (-28 lines) ✅ |
| `MainForm\CaptureSoftware.cs` | Refactor (-69 lines) ✅ |
| `TargetScheduler\SqlLiteReader.cs` | Generic repository (-174 lines) ✅ |
| `Files\XisfFileRename.cs` | Cleanup, helpers, modern syntax (-71 lines) ✅ |
| `Files\XisfFileUpdate.cs` | Exception handling ✅, uses FileHelpers ✅ |
| `MainForm\FluxDensity.cs` | Consolidate duplicates (-149 lines) ✅ |
| `Globals\Globals.cs` | Add camera registry |

### New Files
- `Helpers\UIHelpers.cs`
- `Helpers\FileHelpers.cs`
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
- `Configuration\AppPaths.cs`, `XisfConstants.cs`, `DirectoryFilters.cs`
