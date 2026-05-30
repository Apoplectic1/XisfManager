# XISF File Manager

A Windows Forms desktop application for managing XISF (Extensible Image Serialization Format) files used in astrophotography.

## Refactoring Status

All planned refactoring phases complete (.NET 10 upgrade, camera/telescope/capture-software abstractions, async/await, nullable annotations, CA cleanup). Details preserved in git history.

**Cameras supported:** Z533, Z183, Q178, A144 (all active)
**Telescopes supported:** APM107, EvoStar150, Newtonian254 (with Riccardi 0.75x reducer support)
**Capture Software supported:** NINA, TheSkyX, SGPro, Voyager, SharpCap

## Build Commands

```bash
# Build the solution
dotnet build XisfFileManager.sln -c Release

# Build for specific platform (project defaults to x64)
dotnet build XisfFileManager.sln -c Release -r win-x64

# Run the application
dotnet run --project XisfFileManager/XisfFileManager.csproj
```

## Branches & Releases

- **`dev`** — active development branch (default; new work lands here).
- **`main`** — stable/release branch; merge `dev` into `main` when cutting a release.
- Releases are tag-triggered: pushing an annotated `vX.Y.Z` tag runs `.github/workflows/release.yml`, which injects the tag into `AssemblyInformationalVersion` (shown in the window title). Latest released tag: `v1.3.1`.

## Project Architecture

### Technology Stack
- **.NET 10.0** Windows Forms application (Windows 11 SDK 26100)
- **Nullable reference types** enabled (0 warnings)
- **SQLite** via Microsoft.Data.Sqlite for Target Scheduler database
- **MathNet.Numerics** for scientific calculations
- **GeoTimeZone/TimeZoneConverter** for timezone handling

### Directory Structure
```
XisfFileManager/
├── MainForm/           # UI controllers (partial classes of MainForm)
│   ├── MainForm.cs     # Main form initialization and core logic
│   ├── Camera.cs       # Camera settings and configuration (refactored)
│   ├── Calibration.cs  # Calibration frame UI handling
│   ├── FileSelection.cs # File browser and selection
│   ├── Telescope.cs    # Telescope configuration
│   ├── CaptureSoftware.cs # N.I.N.A., TSX, SGP detection
│   └── TargetScheduler.cs # Target Scheduler tab logic
├── Models/             # Domain models
│   ├── CameraConfiguration.cs # Base camera config + PropertyAnalysis<T>
│   ├── TelescopeConfiguration.cs # Base telescope config + TelescopeAnalysis
│   ├── CaptureSoftwareConfiguration.cs # Base capture software config
│   ├── Cameras/        # Camera-specific configurations
│   │   └── Z533Camera.cs, Z183Camera.cs, Q178Camera.cs, A144Camera.cs
│   ├── Telescopes/     # Telescope-specific configurations
│   │   └── APM107Telescope.cs, EvoStar150Telescope.cs, Newtonian254Telescope.cs
│   └── CaptureSoftware/ # Capture software configurations
│       └── NinaSoftware.cs, TheSkyXSoftware.cs, etc.
├── Services/           # Business logic services
│   ├── CameraService.cs # Camera detection and analysis
│   ├── TelescopeService.cs # Telescope detection and analysis
│   └── CaptureSoftwareService.cs # Software detection and analysis
├── Helpers/            # UI and utility helpers
│   ├── UIHelpers.cs    # Common control manipulation methods
│   └── FileHelpers.cs  # File operation utilities
├── Configuration/      # Centralized constants and paths
│   ├── AppPaths.cs     # Machine-specific drive paths
│   ├── XisfConstants.cs # XISF format constants (signature size, buffer size)
│   └── DirectoryFilters.cs # Directory exclude lists
├── Data/               # Database infrastructure
│   ├── ITableMapper.cs # Generic mapper interface
│   ├── SqliteReaderExtensions.cs # Null-safe reading helpers
│   └── TableMappers.cs # Mappers for all 8 Target Scheduler tables
├── Files/              # XISF file I/O operations
│   ├── XisfFile.cs     # Core XISF file representation
│   ├── XisfXmlReader.cs # XML metadata parsing
│   ├── XisfFileRename.cs # File renaming logic
│   ├── XisfFileUpdate.cs # File modification/writing
│   ├── Buffer.cs       # Binary buffer operations
│   └── XML/            # XML processing
│       └── Xml.cs      # XML metadata utilities
├── Keyword/            # FITS/XISF keyword handling
│   ├── Keyword.cs      # Single keyword (Name, Value, Comment)
│   └── KeywordList.cs  # Keyword collection with typed accessors
├── Calibration/        # Calibration frame library
├── TargetScheduler/    # N.I.N.A. Target Scheduler SQLite integration
│   ├── SqlLiteManager.cs   # Database orchestration
│   ├── SqlLiteReader.cs    # Read operations
│   ├── SqlLiteWriter.cs    # Write operations
│   └── Tables/         # Database table models
├── Calculations/       # Image statistics and math
├── Directories/        # Directory traversal and properties
├── Utility/            # General-purpose utilities
│   └── ToolTip.cs      # ToolTip helpers
└── Globals/            # Enums and global constants
```

## Key Concepts

### XISF Files
XISF (Extensible Image Serialization Format) is an astronomy image format from PixInsight. Files contain:
- XML metadata header with FITS-compatible keywords
- Binary image data (attachments)
- Optional thumbnail attachments

### Keywords
Keywords follow FITS conventions with Name/Value/Comment triplets:
- `IMAGETYP`: Frame type (Light, Dark, Flat, Bias)
- `FILTER`: Filter name (L, R, G, B, Ha, OIII, SII)
- `EXPOSURE`: Exposure time in seconds
- `CCD-TEMP`: Sensor temperature
- `OBJECT`: Target name
- `SWCREATE`: Capture software (NINA, TSX, SGP, VOY, SCP)

### Enums (Globals/Globals.cs)
- `eFrame`: LIGHT, DARK, FLAT, BIAS, ALL, EMPTY
- `eFilter`: L, R, G, B, H, O, S, SHUTTER, ALL, EMPTY
- `eOrder`: File ordering (INDEX, WEIGHT, WEIGHTINDEX, INDEXWEIGHT)
- `eKeywordUpdateMode`: PROTECT, UPDATE_NEW, FORCE

### Target Scheduler Integration
Reads/writes N.I.N.A. Target Scheduler SQLite database:
- Projects, Targets, Exposure Plans
- Acquired Images tracking
- Profile Preferences

## Code Conventions

### Naming
- Private fields: `m` prefix (e.g., `mFileList`, `mCalibration`)
- Boolean fields: `b` prefix (e.g., `bModified`, `mBCancel`)
- UI Controls: Type prefix with underscore separators (e.g., `Label_FileSelection_Statistics_OperationStatus`)
- Enums: `e` prefix (e.g., `eFrame`, `eFilter`)

### Patterns
- MainForm uses partial classes split across feature files
- Event-driven UI updates via delegates (e.g., `CalibrationTabPageEvent`)
- Keyword properties on XisfFile delegate to KeywordList

### Important Files
- `XisfFile.cs`: Central model - all keyword access flows through here
- `KeywordList.cs`: Typed property accessors for common FITS keywords
- `CameraConfiguration.cs`: Base class for camera configs with temperature handling
- `CameraService.cs`: Camera detection, property analysis, and UI color helpers
- `TelescopeConfiguration.cs`: Base class for telescope configs with reducer support
- `TelescopeService.cs`: Telescope detection, analysis, and UI color helpers
- `CaptureSoftwareConfiguration.cs`: Base class for capture software configs
- `CaptureSoftwareService.cs`: Software detection and analysis
- `UIHelpers.cs`: Common UI control manipulation (ClearComboBox, ResetRadioButton, etc.)
- `MainForm.Designer.cs`: Auto-generated UI - TabIndex values manually fixed for proper navigation
- `Globals.cs`: All enums and constants
- `Configuration/AppPaths.cs`: Machine-specific paths (E:\, F:\ drives)
- `Configuration/XisfConstants.cs`: XISF signature size and max buffer size
- `Configuration/DirectoryFilters.cs`: Exclude lists for directory filtering

## Common Tasks

### Adding a New Keyword
1. Add property to `KeywordList.cs` with getter/setter
2. Optionally expose through `XisfFile.cs` as a delegating property

### Adding UI Feature
1. Add controls in Visual Studio Designer (updates MainForm.Designer.cs)
2. Create new partial class file in MainForm/ for logic
3. Wire up events in MainForm.cs constructor

### Database Schema Changes
Table models are in `TargetScheduler/Tables/` - each maps to N.I.N.A. Target Scheduler tables.
