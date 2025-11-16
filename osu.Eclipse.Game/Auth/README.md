# osu! Account Login Integration

This module enables osu! OAuth2 login for osu!eclipse via the official API.

## Setup

1. Go to https://osu.ppy.sh/home/account/edit#new-oauth-application and register your app:
   - **Redirect URI:** `http://localhost:3500/callback`
   - Copy your `client_id` and `client_secret`, put in `OsuOAuthService.cs`.

2. Add `Newtonsoft.Json` NuGet to your project.

3. In your game/main menu screen:
   - Instantiate `LoginOverlay` and display.
   - On successful login, consume the `OsuUser` info for your UI, etc.

## Security

- NEVER check in your client secret to your public repo.
- For distribution, you may want to obfuscate or otherwise secure your client secret.
- For mobile, use a custom URI and OS-specific browser launching/callback.
