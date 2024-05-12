// See https://aka.ms/new-console-template for more information
using LinkedinConsoleApp.Services;
using System;
using System.Diagnostics;
using System.Net;

var codeService = new AuthorizationCodeService();

var code = await codeService.GetOAuthCode();

var accessTokenService = new AccessTokenService();

var token = await accessTokenService.GetAccessToken(code);





