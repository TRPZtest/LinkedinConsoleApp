// See https://aka.ms/new-console-template for more information
using LinkedinConsoleApp.Services;
using System;
using System.Diagnostics;
using System.Net;

var service = new AuthorizationCodeService();

var code = await service.GetOAuthCode();

Console.WriteLine(code);



