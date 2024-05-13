using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinConsoleApp.Services.SavePhotoService
{
    public class SavePhotoService
    {
        public async Task SavePhoto(string photoUrl, string token)
        {
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var request = new HttpRequestMessage(HttpMethod.Get, photoUrl);

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Error while uploading photo");

            var bytes = await response.Content.ReadAsByteArrayAsync();

            File.WriteAllBytes("Linkedin profile picture.jpeg", bytes);
        }
    }
}
