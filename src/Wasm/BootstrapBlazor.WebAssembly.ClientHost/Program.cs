// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared;
using BootstrapBlazor.Shared.Data;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace BootstrapBlazor.WebAssembly.ClientHost
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
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("app");

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            // 版本号服务
            builder.Services.AddVersionManager();

            // 示例代码服务
            builder.Services.AddExampleService();

            // 增加 BootstrapBlazor 组件
            builder.Services.AddBootstrapBlazor();

            // 增加 Table Excel 导出服务
            builder.Services.AddBootstrapBlazorTableExcelExport();

            builder.Services.AddSingleton<WeatherForecastService>();

            builder.Services.AddSingleton<WebsiteOptions>();

            builder.Services.AddSingleton<ICultureStorage, DefaultCultureStorage>();

            builder.Services.Configure<BootstrapBlazorOptions>(op => op.ToastDelay = 4000);

            var host = builder.Build();

            await GetCultureAsync(host);

            await host.RunAsync();
        }

        // based on https://github.com/pranavkm/LocSample
        private static async Task GetCultureAsync(WebAssemblyHost host)
        {
            var jsRuntime = host.Services.GetRequiredService<IJSRuntime>();
            var cultureName = await jsRuntime.InvokeAsync<string>("$.blazorCulture.get") ?? CultureInfo.CurrentCulture.Name;
            var culture = new CultureInfo(cultureName);
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }

        internal class DefaultCultureStorage : ICultureStorage
        {
            public CultureStorageMode Mode { get; set; } = CultureStorageMode.LocalStorage;
        }
    }
}
