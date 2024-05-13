using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinConsoleApp.Services.AccessTokenService
{
    public class AccessTokenService
    {
        private const string CLIENT_ID = "86r16wywo4xmaz";
        private const string CLIENT_SECRET = "WPL_AP0.qmJqyR2czSodGy9m.MTUxMTIwMzQ1";
        private const string URL = "https://www.linkedin.com/oauth/v2/accessToken";
        private const string REDIRECT_URL = "http://127.0.0.1:5278/";

        public async Task<string> GetAccessToken(string code)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("client_id", CLIENT_ID),
                new KeyValuePair<string, string>("client_secret", CLIENT_SECRET),
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", REDIRECT_URL)
            });

            using var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, URL);

            request.Content = formContent;

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Exchanging code to token error");

            var tokenResponse = await response.Content.ReadFromJsonAsync<AccessTokenResponse>();
   
            return tokenResponse.AccessToken;
        }
    }
}
