# XISF File Manager — Roadmap

Living priority list. Code structure and full history live in `CLAUDE.md` and git;
this file tracks what's next and what recently shipped. Keep entries short.

## Open follow-ups

1. **Cut a release** — merge `dev` → `main` and tag the next `vX.Y.Z` (last released: `v1.3.1`). The FOCRATIO/aperture work is committed on `dev` and verified in-app.
2. **Collapse the triple focal-ratio derivation** — `TelescopeConfiguration.ApplyKeywords` → `XisfFile.FocalRatio` setter → `KeywordList.FocalRatio` setter each recompute FOCALLEN ÷ APTDIA and discard the prior value. Correct but redundant; pick one home if simplifying.
3. **APTAREA obstruction accuracy** — `APTAREA` uses full circular area π·r² and ignores obstructions, so the Newtonian (NWT254) value is optimistic. Subtract the secondary-mirror obstruction before trusting it for light-gathering / SNR / throughput math.
4. **Tidy redundant `using` directives** in `KeywordList.cs` (`System`, `System.Collections.Generic`, `System.Reflection.Metadata` — flagged CS8019/CS8933 against the project's global usings).
5. **Focal-ratio consistency coloring** — if a FOCRATIO readout is added to the Telescope groupbox, mirror the focal-length consistency check in `TelescopeService`/`TelescopeAnalysis` (distinct-ratio detection + red/black label color).

## Recently shipped

- **FOCRATIO + aperture keywords for all telescopes** (`6ff0f60`, `f65e770`) — `ApplyKeywords` now emits `APTDIA`/`APTAREA` and derives reducer-aware `FOCRATIO` for APM107, EvoStar150, Newtonian254; aperture diameter/area hardcoded for all three.
- **Branch/release model documented** (`31e96c6`) — dev/main flow and tag-triggered releases in `CLAUDE.md`.
- **Sensor keyword comments clarified** (`f3cc3ee`).
- **Exposure normalized to `EXPTIME`** (`a7b47d4`) — legacy `EXPOSURE` converted and purged.
- **Window title shows release version** (`9819b42`) instead of git branch.
