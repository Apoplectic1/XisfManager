# XISF File Manager — Roadmap

Living priority list. Code structure and full history live in `CLAUDE.md` and git;
this file tracks what's next and what recently shipped. Keep entries short.

## Open follow-ups

1. **Collapse the triple focal-ratio derivation** — `TelescopeConfiguration.ApplyKeywords` → `XisfFile.FocalRatio` setter → `KeywordList.FocalRatio` setter each recompute FOCALLEN ÷ APTDIA and discard the prior value. Correct but redundant; pick one home if simplifying.
2. **APTAREA obstruction accuracy** — `APTAREA` uses full circular area π·r² and ignores obstructions, so the Newtonian (NWT254) value is optimistic. Subtract the secondary-mirror obstruction before trusting it for light-gathering / SNR / throughput math.
3. **Tidy redundant `using` directives** in `KeywordList.cs` (`System`, `System.Collections.Generic`, `System.Reflection.Metadata` — flagged CS8019/CS8933 against the project's global usings).
4. **Focal-ratio consistency coloring** — if a FOCRATIO readout is added to the Telescope groupbox, mirror the focal-length consistency check in `TelescopeService`/`TelescopeAnalysis` (distinct-ratio detection + red/black label color).
5. **Bump GitHub Actions versions** in `.github/workflows/release.yml` — `actions/checkout@v4` and `actions/setup-dotnet@v4` run on Node 20, which GitHub forces to Node 24 on 2026-06-16 (removed 2026-09-16). Bump to `@v5` to clear the deprecation; optionally pin the runner image now that `windows-latest` redirects to `windows-2025-vs2026`.

## Recently shipped

- **MainForm maintainability refactor** — extracted shared session state into `Models/Workspace.cs` (`mWorkspace`, behind shim properties so call sites were unchanged); surfaced `Button_Browse_Click` as a named-stage pipeline (`ResetSession → TrySelectSourceFolder → ReadHeadersAsync → PopulateUiFromFiles → RefreshFeatureDetection → BuildTargetFileTree`); split the two oversized partials (`TargetScheduler` → Tree/Events + top-level `CustomTreeView`; `ImageType` → Detection/SetActions/Masters); standardized control resets on `UIHelpers`; documented the "Adding a New Feature Area" convention in `CLAUDE.md`. MVP/presenters were evaluated and rejected (testability isn't the driver; the `Services`/`Models` layer is already the clean separation).
- **Released `v1.4.0`** — merged `dev` → `main` and tagged (`d8d862e`); tag-triggered `release.yml` build.
- **FOCRATIO + aperture keywords for all telescopes** (`6ff0f60`, `f65e770`) — `ApplyKeywords` now emits `APTDIA`/`APTAREA` and derives reducer-aware `FOCRATIO` for APM107, EvoStar150, Newtonian254; aperture diameter/area hardcoded for all three.
- **Branch/release model documented** (`31e96c6`) — dev/main flow and tag-triggered releases in `CLAUDE.md`.
- **Sensor keyword comments clarified** (`f3cc3ee`).
- **Exposure normalized to `EXPTIME`** (`a7b47d4`) — legacy `EXPOSURE` converted and purged.
