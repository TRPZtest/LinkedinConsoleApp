// See https://aka.ms/new-console-template for more information
using LinkedinConsoleApp.Services;
using LinkedinConsoleApp.Services.AccessTokenService;
using LinkedinConsoleApp.Services.CodeService;
using LinkedinConsoleApp.Services.LocalStorageService;
using LinkedinConsoleApp.Services.SavePhotoService;
using LinkedinConsoleApp.Services.UserInfoService;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;

var storageService = new LocalStorageService();

var token = storageService.GetToken();

if (String.IsNullOrEmpty(token))
{
    var codeService = new AuthorizationCodeService();

    var code = await codeService.GetOAuthCode();

    var accessTokenService = new AccessTokenService();

    token = await accessTokenService.GetAccessToken(code);

    storageService.SetToken(token);
}

var userInfoService = new UserInfoService();

var userInfo = await userInfoService.GetUserInfo(token);

var savePhotoService = new SavePhotoService();

await savePhotoService.SavePhoto(userInfo.Picture, token);

Console.WriteLine("The end!");


