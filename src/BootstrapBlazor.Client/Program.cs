// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.Shared.Data;
using BootstrapBlazor.Shared.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using System.Globalization;
using System.Net.Http.Json;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
using var client = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
var settings = await client.GetFromJsonAsync<LayoutSettings>("api/layout-settings")
    ?? throw new InvalidOperationException("Layout settings are unavailable.");
builder.Services.AddBootstrapBlazorLayoutServices();
builder.Services.AddHttpClient<ISampleDocumentationService, HttpSampleDocumentationService>(options =>
    options.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.Configure<BootstrapBlazor.Components.BootstrapBlazorOptions>(options =>
{
    options.SupportedCultures = settings.SupportedCultures;
    options.ConnectionHubOptions.Enable = false;
});
builder.Services.Configure<BootstrapBlazor.Shared.Data.WebsiteOptions>(options =>
{
    options.GithubRepositoryUrl = settings.GithubRepositoryUrl;
    options.GiteeRepositoryUrl = settings.GiteeRepositoryUrl;
    options.WikiUrl = settings.WikiUrl;
    options.Themes = settings.Themes;
});
builder.Services.Configure<BootstrapBlazor.Components.MeiliSearchOptions>(options =>
{
    options.Url = new Uri(client.BaseAddress!, "api/layout-search").AbsoluteUri;
    options.Index = settings.SearchIndex;
});

var host = builder.Build();
var js = host.Services.GetRequiredService<IJSRuntime>();
string cultureName;
try
{
    cultureName = await js.InvokeAsync<string>("bbCulture.get");
}
catch (JSException ex)
{
    host.Services.GetRequiredService<ILoggerFactory>().CreateLogger("BootstrapBlazor.Client")
        .LogError(ex, "Unable to read the application culture.");
    throw;
}
if (!string.IsNullOrEmpty(cultureName))
{
    var culture = CultureInfo.GetCultureInfo(cultureName);
    CultureInfo.DefaultThreadCurrentCulture = culture;
    CultureInfo.DefaultThreadCurrentUICulture = culture;
}
await host.RunAsync();
