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

namespace LinkedinConsoleApp.Services.CodeService
{
    public class AuthorizationCodeService
    {
        private const string CLIENT_ID = "86r16wywo4xmaz";
        private const string REDIRECT_URL = "http://127.0.0.1:5278/";
        private const string URL_BASE = "https://www.linkedin.com/oauth/v2/authorization";

        public async Task<string> GetOAuthCode()
        {
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

            // open browser
            Process.Start(new ProcessStartInfo() { FileName = url, UseShellExecute = true });

            // wait for the callback
            var context = await httpListener.GetContextAsync();

            var code = context.Request.QueryString.Get("code");
            var response = context.Response;
            var responseOutput = response.OutputStream;

            byte[] buffer = Array.Empty<byte>();
            try
            {
                if (string.IsNullOrEmpty(code))
                {
                    buffer = Encoding.UTF8.GetBytes(Helpers.GetErrorHtmlPage());
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    response.ContentLength64 = buffer.Length;

                    throw new Exception("Error while getting code");
                }
                else
                {
                    buffer = Encoding.UTF8.GetBytes(Helpers.GetSuccessHtmlPage());
                    response.ContentLength64 = buffer.Length;
                }
            }
            finally
            {
                await responseOutput.WriteAsync(buffer, 0, buffer.Length);
                httpListener.Stop();
                responseOutput.Close();
            }

            return code;
        }
    }
}
