// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Server.Components;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLogging(logBuilder => logBuilder.AddFileLogger());
builder.Services.AddCors();
builder.Services.AddResponseCompression();

builder.Services.AddControllers();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

// 获得当前主题配置
var themes = builder.Configuration.GetSection("Themes")
    .GetChildren()
    .Select(c => new KeyValuePair<string, string>(c.Key, c.Value ?? ""));

// 增加 BootstrapBlazor 服务
builder.Services.AddBootstrapBlazorServices(options =>
{
    options.Themes.AddRange(themes);
});

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
    app.UseExceptionHandler("/Error");
    app.UseResponseCompression();
    app.UseStaticFiles(new StaticFileOptions { OnPrepareResponse = ctx => ctx.ProcessCache(app.Configuration) });
}

var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".properties"] = "application/octet-stream";
provider.Mappings[".moc"] = "application/x-msdownload";
provider.Mappings[".moc3"] = "application/x-msdownload";
provider.Mappings[".mtn"] = "application/x-msdownload";

app.UseStaticFiles(new StaticFileOptions { ContentTypeProvider = provider });
app.UseStaticFiles();

var cors = app.Configuration["AllowOrigins"]?.Split(',', StringSplitOptions.RemoveEmptyEntries);
if (cors?.Length > 0)
{
    app.UseCors(builder => builder.WithOrigins()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());
}

app.UseBootstrapBlazor();

app.UseAntiforgery();

app.MapDefaultControllerRoute();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
