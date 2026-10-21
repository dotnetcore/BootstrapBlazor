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
        services.AddBootstrapBlazorMeiliSearch();
        services.AddBootstrapHolidayService();
        services.AddBootstrapBlazorRegionService();
        services.AddBootstrapBlazorHtml2ImageService();
        services.AddBootstrapBlazorDom2ImageService();
        services.AddOptionsMonitor<WebsiteOptions>();
        services.AddScoped<MenuService>();
        services.AddSingleton<PackageVersionService>();
        services.AddSingleton<WeatherForecastService>();
        services.AddSingleton<ILookupService, DemoLookupService>();
        return services;
    }
}
