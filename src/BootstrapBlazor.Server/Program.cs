// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.Server.Components;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Options;
using System.Text;

// 增加中文编码支持用于定位服务
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddBootstrapBlazorServerService();

var app = builder.Build();

// 启用本地化
var option = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
if (option != null)
{
    app.UseRequestLocalization(option.Value);
}

// 启用转发中间件
app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseAntiforgery();
app.UseBootstrapBlazor();

app.MapStaticAssets();

var cors = app.Configuration["AllowOrigins"]?.Split(',', StringSplitOptions.RemoveEmptyEntries);
if (cors?.Length > 0)
{
    app.UseCors(options => options.WithOrigins()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());
}

app.MapDefaultControllerRoute();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
