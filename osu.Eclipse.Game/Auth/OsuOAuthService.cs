using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace osu.Eclipse.Game.Auth
{
    public class OsuOAuthService
    {
        // You NEED to fill these from your osu! OAuth registration
        private readonly string clientId = "45808";
        private readonly string clientSecret = "YOUR_CLIENT_SECRET";
        // Suggest using http://localhost:3500/callback for desktop, or a custom scheme for mobile
        private readonly string redirectUri = "http://localhost:3500/callback";
        private readonly string scopes = "identify public";

        private readonly string authUrl = "https://osu.ppy.sh/oauth/authorize";
        private readonly string tokenUrl = "https://osu.ppy.sh/oauth/token";
        private readonly string userUrl = "https://osu.ppy.sh/api/v2/me";

        public string GetAuthorizeUrl()
        {
            var url = $"{authUrl}?client_id={clientId}" +
                      $"&redirect_uri={Uri.EscapeDataString(redirectUri)}" +
                      $"&response_type=code" +
                      $"&scope={Uri.EscapeDataString(scopes)}";
            return url;
        }

        public async Task<OsuToken> ExchangeCodeForToken(string code)
        {
            var values = new Dictionary<string, string>
            {
                { "client_id", clientId },
                { "client_secret", clientSecret },
                { "code", code },
                { "grant_type", "authorization_code" },
                { "redirect_uri", redirectUri }
            };

            using var http = new HttpClient();
            var response = await http.PostAsync(tokenUrl, new FormUrlEncodedContent(values));
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<OsuToken>(json);
        }

        public async Task<OsuUser> GetUserProfile(string accessToken)
        {
            using var http = new HttpClient();
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await http.GetStringAsync(userUrl);
            return JsonConvert.DeserializeObject<OsuUser>(response);
        }
    }

    public class OsuToken
    {
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
    }

    public class OsuUser
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
        // ...add more properties from osu! API v2 if needed
    }
}
