// See https://aka.ms/new-console-template for more information
using LinkedinConsoleApp.Services;
using LinkedinConsoleApp.Services.AccessTokenService;
using LinkedinConsoleApp.Services.CodeService;
using LinkedinConsoleApp.Services.SavePhotoService;
using LinkedinConsoleApp.Services.UserInfoService;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;

var codeService = new AuthorizationCodeService();

var code = await codeService.GetOAuthCode();

var accessTokenService = new AccessTokenService();

var token = await accessTokenService.GetAccessToken(code);

var userInfoService = new UserInfoService();

var userInfo = await userInfoService.GetUserInfo(token);

var savePhotoService = new SavePhotoService();

await savePhotoService.SavePhoto(userInfo.Picture, token);

Console.WriteLine("The end!");


