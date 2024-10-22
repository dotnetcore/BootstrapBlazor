// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.Server.Components;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Unicode;

// 增加中文编码支持用于定位服务
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var builder = WebApplication.CreateBuilder(args);

// 增加中文编码支持网页源码显示汉字
builder.Services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

builder.Services.AddLogging(logBuilder => logBuilder.AddFileLogger());
builder.Services.AddCors();

#if DEBUG
#else
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});
#endif

builder.Services.AddControllers();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

// 增加 SignalR 服务数据传输大小限制配置
builder.Services.Configure<HubOptions>(option => option.MaximumReceiveMessageSize = null);

// 增加 BootstrapBlazor 服务
builder.Services.AddBootstrapBlazorServices();

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
}
app.UseStaticFiles(new StaticFileOptions { OnPrepareResponse = ctx => ctx.ProcessCache(app.Configuration) });

var provider = new FileExtensionContentTypeProvider
{
    Mappings =
    {
        [".properties"] = "application/octet-stream",
        [".moc"] = "application/x-msdownload",
        [".moc3"] = "application/x-msdownload",
        [".mtn"] = "application/x-msdownload"
    }
};

app.UseStaticFiles(new StaticFileOptions { ContentTypeProvider = provider });
app.UseStaticFiles();

var cors = app.Configuration["AllowOrigins"]?.Split(',', StringSplitOptions.RemoveEmptyEntries);
if (cors?.Length > 0)
{
    app.UseCors(options => options.WithOrigins()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());
}

app.UseBootstrapBlazor();

app.UseAntiforgery();

app.MapDefaultControllerRoute();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
