using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinConsoleApp.Services
{
    public class AccessTokenService
    {
        private const string URL = "https://www.linkedin.com/oauth/v2/accessToken";

        public async Task<string> GetAccessToken(string code)
        {
            using var client = new HttpClient();



            return "";
        }
    }
}
