// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Registers browser-compatible services used by the shared shell.
/// </summary>
public static class BootstrapBlazorLayoutServiceCollectionExtensions
{
    /// <summary>
    /// Adds the shared layout services to either interactive host.
    /// </summary>
    public static IServiceCollection AddBootstrapBlazorLayoutServices(this IServiceCollection services, Action<BootstrapBlazorOptions>? configureOptions = null)
    {
        services.AddBootstrapBlazor(configureOptions);
        services.AddHttpClient();
        if (OperatingSystem.IsBrowser())
        {
            services.AddHttpClient<ISampleDocumentationService, HttpSampleDocumentationService>((provider, client) =>
                client.BaseAddress = new Uri(provider.GetRequiredService<NavigationManager>().BaseUri));
            // WebAssembly 通过同源 HTTP 接口上传，避免在浏览器中访问服务器文件系统。
            // WebAssembly uploads through the same-origin HTTP endpoint instead of accessing the server file system.
            services.AddHttpClient<IEditorUploadService, HttpEditorUploadService>((provider, client) =>
                client.BaseAddress = new Uri(provider.GetRequiredService<NavigationManager>().BaseUri));
        }
        services.AddAuthorizationCore();
        services.AddCascadingAuthenticationState();
        services.AddScoped<Microsoft.AspNetCore.Components.Authorization.AuthenticationStateProvider, MockAuthenticationStateProvider>();
        services.AddBootstrapBlazorMeiliSearch();
        services.AddBootstrapHolidayService();
        services.AddBootstrapBlazorRegionService();
        services.AddBootstrapBlazorHtml2ImageService();
        services.AddBootstrapBlazorDom2ImageService();
        services.AddBootstrapBlazorWinBoxService();
        services.AddBootstrapBlazorTotpService();
        services.ConfigureTabItemMenuBindOptions(options =>
        {
            options.Binders.Add("layout-demo/text=Parameter1", new() { Text = "示例网页" });
            options.Binders.Add("layout-demo", new() { Text = "Text 1" });
            options.Binders.Add("layout-demo?text=Parameter", new() { Text = "Text 2" });
            options.Binders.Add("layout-demo/text=Parameter", new() { Text = "Text 3" });
        });
        services.AddOptionsMonitor<WebsiteOptions>();
        services.AddScoped<MenuService>();
        services.AddScoped<FanControllerDataService>();
        services.AddSingleton(typeof(IDataService<>), typeof(TableDemoDataService<>));
        services.AddSingleton<DashboardService>();
        services.AddSingleton<PackageVersionService>();
        services.AddSingleton<WeatherForecastService>();
        services.AddSingleton<ILookupService, DemoLookupService>();
        return services;
    }
}
