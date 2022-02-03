// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

#if NET6_0_OR_GREATER
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Options;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

builder.Services.AddCors();
builder.Services.AddResponseCompression();

builder.Services.AddControllers();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBootstrapBlazorServices(builder.Configuration.GetSection("Themes")
    .GetChildren()
    .Select(c => new KeyValuePair<string, string>(c.Key, c.Value)));

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
#else
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BootstrapBlazor.Server
{
    /// <summary>
    ///
    /// </summary>
    public class Program
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStaticWebAssets();
                    webBuilder.UseStartup<Startup>();
                });
    }
}
#endif
