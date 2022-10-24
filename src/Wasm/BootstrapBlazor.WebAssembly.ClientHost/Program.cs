// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using BootstrapBlazor.Shared.Extensions;
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
    var cultureName = await jsRuntime.GetCulture();

    if (!string.IsNullOrEmpty(cultureName))
    {
        var culture = new CultureInfo(cultureName);

        // 注意 wasm 模式此处必须使用 DefaultThreadCurrentCulture 不可以使用 CurrentCulture
        CultureInfo.DefaultThreadCurrentCulture = culture;
        CultureInfo.DefaultThreadCurrentUICulture = culture;
    }
}
