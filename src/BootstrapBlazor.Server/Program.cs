// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

builder.Services.AddCors();
builder.Services.AddResponseCompression();

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddHubOptions(o => o.MaximumReceiveMessageSize = null);
builder.Services.AddBootstrapBlazorServices();

// 获得当前主题配置
var themes = builder.Configuration.GetSection("Themes")
    .GetChildren()
    .Select(c => new KeyValuePair<string, string>(c.Key, c.Value));

builder.Services.ConfigureBootstrapBlazorOption(options =>
{
    // 统一设置 Toast 组件自动消失时间
    options.ToastDelay = 4000;
    options.Themes.AddRange(themes);
});

builder.Services.Configure<HubOptions>(option => option.MaximumReceiveMessageSize = int.MaxValue);

var app = builder.Build();

// 启用本地化
var option = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
if (option != null)
{
    app.UseRequestLocalization(option.Value);
}

// 启用转发中间件
app.UseForwardedHeaders(new ForwardedHeadersOptions() { ForwardedHeaders = ForwardedHeaders.All });

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseResponseCompression();
app.UseStaticFiles();
app.UseRouting();
app.UseCors(builder => builder.WithOrigins(app.Configuration["AllowOrigins"].Split(',', StringSplitOptions.RemoveEmptyEntries))
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials());

app.UseBootstrapBlazor();

app.MapDefaultControllerRoute();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
