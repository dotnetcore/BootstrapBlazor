// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared;
using BootstrapBlazor.Shared.Data;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using System.Globalization;
using System.Net.Http;
using System.Threading;
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

            builder.Services.AddTransient(sp => new HttpClient());

            // 版本号服务
            builder.Services.AddVersionManager();

            // 示例代码服务
            builder.Services.AddExampleService();

            // 增加 BootstrapBlazor 组件
            builder.Services.AddBootstrapBlazor(setupAction: option =>
            {
                option.ResourceManagerStringLocalizerType = typeof(Program);
            });

            // 增加 Table Excel 导出服务
            builder.Services.AddBootstrapBlazorTableExcelExport();

            builder.Services.AddSingleton<WeatherForecastService>();

            builder.Services.AddSingleton<IConfigureOptions<WebsiteOptions>, Microsoft.Extensions.DependencyInjection.ConfigureOptions<WebsiteOptions>>();

            builder.Services.Configure<WebsiteOptions>(options =>
            {
                options.RepositoryUrl = "https://www.blazor.zone/api/docs/";
            });

            builder.Services.AddSingleton<ICultureStorage, DefaultCultureStorage>();

            builder.Services.Configure<BootstrapBlazorOptions>(op =>
            {
                op.ToastDelay = 4000;
                op.SupportedCultures.AddRange(new string[] { "zh-CN", "en-US" });
            });

            builder.Services.AddLocalization();

            var host = builder.Build();

            await SetCultureAsync(host);

            await host.RunAsync();
        }

        private static async Task SetCultureAsync(WebAssemblyHost host)
        {
            // 如果 localStorage 未设置语言使用浏览器请求语言
            var jsRuntime = host.Services.GetRequiredService<IJSRuntime>();
            var cultureName = await jsRuntime.InvokeAsync<string>("$.blazorCulture.get");

            if (!string.IsNullOrEmpty(cultureName))
            {
                var culture = new CultureInfo(cultureName);

                // 注意 wasm 模式此处必须使用 DefaultThreadCurrentCulture 不可以使用 CurrentCulture
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
            }
        }

        internal class DefaultCultureStorage : ICultureStorage
        {
            public CultureStorageMode Mode { get; set; } = CultureStorageMode.LocalStorage;
        }
    }
}
