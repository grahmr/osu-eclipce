using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using osu.Eclipse.Game.Auth;
using osu.Framework.Graphics;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Screens;

namespace osu.Eclipse.Game.Overlays.Login
{
    public class LoginOverlay : CompositeDrawable
    {
        private readonly OsuOAuthService osuOAuth = new OsuOAuthService();
        private readonly BasicButton loginButton;

        public LoginOverlay()
        {
            loginButton = new BasicButton
            {
                Text = "Login with osu!",
                Width = 250,
                Height = 50,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
            };
            loginButton.Action = async () => await BeginLogin();

            AddInternal(loginButton);
        }

        private async Task BeginLogin()
        {
            var authUrl = osuOAuth.GetAuthorizeUrl();

            try
            {
                // Open osu! auth in user's browser
                OpenBrowser(authUrl);

                // Start local HTTP listener for redirect URI (for desktop, e.g. http://localhost:3500/callback?code=XYZ)
                var code = await WaitForOsuAuthCode();
                if (string.IsNullOrEmpty(code))
                {
                    // Handle error
                    return;
                }
                var token = await osuOAuth.ExchangeCodeForToken(code);
                var user = await osuOAuth.GetUserProfile(token.AccessToken);

                // Show user info or update state as needed here
                // e.g. trigger event: LoggedIn(user)
            }
            catch (Exception ex)
            {
                // Handle errors (show message to user)
                Console.WriteLine($"Login error: {ex}");
            }
        }

        private void OpenBrowser(string url)
        {
#if WINDOWS
            Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
#elif LINUX
            Process.Start("xdg-open", url);
#elif OSX
            Process.Start("open", url);
#else
            // On mobile, use Xamarin/MAUI to launch browser
            // (Code will differ!)
            throw new PlatformNotSupportedException();
#endif
        }

        // For desktop: basic local HTTP server to catch OAuth redirect
        private async Task<string> WaitForOsuAuthCode()
        {
            var listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:3500/callback/");
            listener.Start();
            try
            {
                var context = await listener.GetContextAsync();
                var query = context.Request.QueryString;
                // Send minimal HTML to let user know login succeeded
                var buffer = Encoding.UTF8.GetBytes("<html><body>osu! login successful, you may close this tab.</body></html>");
                context.Response.ContentLength64 = buffer.Length;
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.OutputStream.Close();
                var code = query.Get("code");
                return code;
            }
            finally
            {
                listener.Stop();
            }
        }
    }
}
