using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedinConsoleApp.Services.CodeService
{
    public static class Helpers
    {
        public static string GetSuccessHtmlPage()
        {
            return @"
                <!DOCTYPE html>
                <html lang=""en"">
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>Authorized</title>
                </head>
                <body>
                    <h1>Authorized</h1>
                    <p>You are now authorized to return to the app.</p>
                </body>
                </html>";
        }

        public static string GetErrorHtmlPage()
        {
            return @"
                <!DOCTYPE html>
                <html lang=""en"">
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>Authorized</title>
                </head>
                <body>
                    <h1>Authorization Error</h1>                   
                </body>
                </html>";
        }
    }
}
