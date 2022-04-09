// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

#if NET6_0_OR_GREATER
using BootstrapBlazor.Components;
using BootstrapBlazor.Shared;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddWasmServices();

var host = builder.Build();

await SetCultureAsync(host);

await builder.Build().RunAsync();

static async Task SetCultureAsync(WebAssemblyHost host)
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
#else
using BootstrapBlazor.Components;
using BootstrapBlazor.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System.Globalization;
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

            builder.Services.AddWasmServices();

            var host = builder.Build();

            host.Services.RegisterProvider();

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
    }
}
#endif
