using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LinkedinConsoleApp.Services
{
    public class AuthorizationCodeService
    {
        private const string CLIENT_ID = "86r16wywo4xmaz";
        private const string REDIRECT_URL = "http://127.0.0.1:5278/";
        private const string URL_BASE = "https://www.linkedin.com/oauth/v2/authorization";

        public async Task<string> GetOAuthCode()
        {         
            string redirectUri = $"http://{IPAddress.Loopback}:{GetRandomUnusedPort()}/";
            using var client = new HttpClient();          

            var parameters = new Dictionary<string, string>
            {
                { "client_id", CLIENT_ID },
                { "response_type", "code" },
                { "scope", "profile" },
                { "redirect_uri", REDIRECT_URL }
            };

            // Url with query string
            var url = QueryHelpers.AddQueryString(URL_BASE, parameters);

            using var httpListener = new HttpListener();
            httpListener.Prefixes.Add(REDIRECT_URL);
            httpListener.Start();

            Process.Start(new ProcessStartInfo() { FileName = url, UseShellExecute = true });

            // Waits for the OAuth authorization response.
            var context = await httpListener.GetContextAsync();
            var response = context.Response;

            byte[] buffer = Encoding.UTF8.GetBytes(Helpers.GetSuccessHtmlPage());
            response.ContentLength64 = buffer.Length;
            var responseOutput = response.OutputStream;
            await responseOutput.WriteAsync(buffer, 0, buffer.Length);
            responseOutput.Close();
            httpListener.Stop();

            var code = context.Request.QueryString.Get("code");

            if (string.IsNullOrEmpty(code))
                throw new Exception("Error while getting OAuth code");
            return code;
        }

        public static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

    }
}
