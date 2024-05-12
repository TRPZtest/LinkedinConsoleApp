using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LinkedinConsoleApp.Services.AccessTokenService
{
    internal class AccessTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        [JsonPropertyName("expires_in")]
        public uint ExpiresIn { get; set; }
        [JsonPropertyName("scope")]
        public string Scope { get; set; }
    }
}
