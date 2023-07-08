// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Server.Extensions;

using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;

using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

builder.Services.AddLogging(logBuilder => logBuilder.AddFileLogger());
builder.Services.AddCors();
builder.Services.AddResponseCompression();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// 获得当前主题配置
var themes = builder.Configuration.GetSection("Themes")
    .GetChildren()
    .Select(c => new KeyValuePair<string, string>(c.Key, c.Value ?? ""));

// 增加 BootstrapBlazor 服务
builder.Services.AddBootstrapBlazorServices(options =>
{
    // 统一设置 Toast 组件自动消失时间
    options.Themes.AddRange(themes);
});

builder.Services.Configure<HubOptions>(option => option.MaximumReceiveMessageSize = null);

builder.Services.ConfigureTabItemMenuBindOptions(options =>
{
    options.Binders.Add("layout-demo", new() { Text = "Text 1" });
    options.Binders.Add("layout-demo?text=Parameter", new() { Text = "Text 2" });
    options.Binders.Add("layout-demo/text=Parameter", new() { Text = "Text 3" });
});

builder.Services.ConfigureMaterialDesignIconTheme();
builder.Services.ConfigureIconThemeOptions(options => options.ThemeKey = "fa");

var app = builder.Build();

// 启用本地化
var option = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
if (option != null)
{
    app.UseRequestLocalization(option.Value);
}

// 启用转发中间件
app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
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

app.UseRouting();

var cors = app.Configuration["AllowOrigins"]?.Split(',', StringSplitOptions.RemoveEmptyEntries);
if (cors?.Any() ?? false)
{
    app.UseCors(builder => builder.WithOrigins()
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());
}

app.UseBootstrapBlazor();
app.UseAuthentication();

app.MapDefaultControllerRoute();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
