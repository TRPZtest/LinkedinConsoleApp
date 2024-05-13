using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinConsoleApp.Services.UserInfoService
{
    public class UserInfoService
    {
        private string URL = "https://api.linkedin.com/v2/userinfo";
        public UserInfoService() { }

        public async Task<UserInfoResponse> GetUserInfo(string token)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var request = new HttpRequestMessage(HttpMethod.Get, URL);
            
            var response = await client.SendAsync(request);
       
            if (!response.IsSuccessStatusCode)
                throw new Exception("Error while getting user info");

            var userInfoResponse = await response.Content.ReadFromJsonAsync<UserInfoResponse>();          
       
            return userInfoResponse;
        }
    }
}
