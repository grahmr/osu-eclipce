# osu! eclipse

*A next-generation rhythm game engine combining the best of osu!stable and osu!lazer, with complete skin support and ready for new features.*

## Features

- Combines gameplay/rulesets from both osu!stable and osu!lazer.
- **Full skin support:** both legacy (osu!stable) and modern (osu!lazer) skins.
- Modular architecture for easy extension.
- Community-focused, modern .NET codebase.

## Directory Structure

- `osu.Eclipse.Game`         — Core game, rulesets, skins.
- `osu.Eclipse.Desktop`      — Desktop entry point.
- `osu.Eclipse.Resources`    — Shared assets (future).
- `osu.Eclipse.Tests`        — Test projects (future).

## Getting Started

1. Install [.NET 8 SDK](https://dotnet.microsoft.com/download).
2. Clone this repo.
3. Open `osu.Eclipse.sln` in Visual Studio or Rider.
4. Run `osu.Eclipse.Desktop`.

## TODO

- Import all rulesets/modes from stable and lazer.
- Implement beatmap and score database import.
- Extend skin manager for user selection and customization.
- Add game menu, settings, and multiplatform support.
