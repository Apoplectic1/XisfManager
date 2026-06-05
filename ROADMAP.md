# XISF File Manager — Roadmap

Living priority list. Code structure and full history live in `CLAUDE.md` and git;
this file tracks what's next and what recently shipped. Keep entries short.

## Open follow-ups

1. **Collapse the triple focal-ratio derivation** — `TelescopeConfiguration.ApplyKeywords` → `XisfFile.FocalRatio` setter → `KeywordList.FocalRatio` setter each recompute FOCALLEN ÷ APTDIA and discard the prior value. Correct but redundant; pick one home if simplifying.
2. **APTAREA obstruction accuracy** — `APTAREA` uses full circular area π·r² and ignores obstructions, so the Newtonian (NWT254) value is optimistic. Subtract the secondary-mirror obstruction before trusting it for light-gathering / SNR / throughput math.
3. **Focal-ratio consistency coloring** — if a FOCRATIO readout is added to the Telescope groupbox, mirror the focal-length consistency check in `TelescopeService`/`TelescopeAnalysis` (distinct-ratio detection + red/black label color).
4. **Bump GitHub Actions versions** in `.github/workflows/release.yml` — `actions/checkout@v4` and `actions/setup-dotnet@v4` run on Node 20, which GitHub forces to Node 24 on 2026-06-16 (removed 2026-09-16). Bump to `@v5` to clear the deprecation; optionally pin the runner image now that `windows-latest` redirects to `windows-2025-vs2026`.
5. **Wire `XisfBlockCompression.Decompress` into a runtime path** — the codec is compress-only today because XFM treats image blocks as opaque. Needed when the flux feature gains real pixel read/write; at that point re-evaluate `Astronomy.PCL` for true image decode/encode (the codec here only covers the byte-block layer).
6. **Compress thumbnail/ICC blocks** — only the main image attachment is compressed today; thumbnail/ICC blocks are stripped or copied as-is.
7. **Verify input checksums on read** — XFM writes SHA-1 checksums but does not validate an existing block's checksum on load; could warn on mismatch (corruption detection).
8. **Target Scheduler tab → TCM (future, ecosystem)** — XFM's `MainForm/TargetScheduler.*` + `CustomTreeView` are slated to migrate into the new **TargetCatalogManager (TCM)** app, after which XFM reads `Catalog.db` read-only via `Astronomy.Catalog` (`GetShotTargets()` = the actual-only view; XFM only ever deals with shot targets). The catalog foundation this depends on is now built — `Astronomy.Catalog` reconciles disk (actual) ↔ TS (plan) onto one canonical target with goal-vs-actual. Not started here; tracked in `TargetCatalogManager/ROADMAP.md` (Phase 3 UI / Phase 5 cutover).

## Recently shipped

- **Tidied redundant `using` directives** — removed explicit usings already provided by `ImplicitUsings` (and an unused `System.Reflection.Metadata`) from `KeywordList.cs`, `MainForm.cs`, `XisfFileUpdate.cs`, and `Xml.cs`, clearing the CS8019/CS8933 IDE noise (closes former follow-up #3).
- **XISF image-block compression (`zlib+sh` + SHA-1)** — saves now compress the image data block when it isn't already compressed, matching the PixInsight/NINA on-disk format (byte-shuffle → zlib max level → SHA-1 over the compressed bytes). Already-compressed blocks (any codec) are copied verbatim and never re-compressed. Pure-managed codec in `Files/Compression/` (`XisfBlockCompression` + `BlockCompressionInfo`) using built-in `System.IO.Compression.ZLibStream` + `System.Security.Cryptography` — no native/PCL dependency. "Update Keywords" is now save-if-needed (a keyword change **or** an uncompressed block); the status line breaks out compressed / already-compressed counts. Writes go via a temp file + atomic move.
- **Released `v1.5.0`** — merged `dev` → `main` and tagged; tag-triggered `release.yml` build.
- **MainForm maintainability refactor** — extracted shared session state into `Models/Workspace.cs` (`mWorkspace`, behind shim properties so call sites were unchanged); surfaced `Button_Browse_Click` as a named-stage pipeline (`ResetSession → TrySelectSourceFolder → ReadHeadersAsync → PopulateUiFromFiles → RefreshFeatureDetection → BuildTargetFileTree`); split the two oversized partials (`TargetScheduler` → Tree/Events + top-level `CustomTreeView`; `ImageType` → Detection/SetActions/Masters); standardized control resets on `UIHelpers`; documented the "Adding a New Feature Area" convention in `CLAUDE.md`. MVP/presenters were evaluated and rejected (testability isn't the driver; the `Services`/`Models` layer is already the clean separation).
- **Released `v1.4.0`** — merged `dev` → `main` and tagged (`d8d862e`); tag-triggered `release.yml` build.
- **FOCRATIO + aperture keywords for all telescopes** (`6ff0f60`, `f65e770`) — `ApplyKeywords` now emits `APTDIA`/`APTAREA` and derives reducer-aware `FOCRATIO` for APM107, EvoStar150, Newtonian254; aperture diameter/area hardcoded for all three.
- **Branch/release model documented** (`31e96c6`) — dev/main flow and tag-triggered releases in `CLAUDE.md`.
- **Sensor keyword comments clarified** (`f3cc3ee`).
- **Exposure normalized to `EXPTIME`** (`a7b47d4`) — legacy `EXPOSURE` converted and purged.
