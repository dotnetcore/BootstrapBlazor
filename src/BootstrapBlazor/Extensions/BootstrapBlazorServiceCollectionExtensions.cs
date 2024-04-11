// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Globalization;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// BootstrapBlazor 服务扩展类
/// </summary>
public static class BootstrapBlazorServiceCollectionExtensions
{
    /// <summary>
    /// 增加 BootstrapBlazor 服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configureOptions"></param>
    /// <param name="localizationConfigure"></param>
    /// <returns></returns>
    public static IServiceCollection AddBootstrapBlazor(this IServiceCollection services, Action<BootstrapBlazorOptions>? configureOptions = null, Action<JsonLocalizationOptions>? localizationConfigure = null)
    {
        services.AddMemoryCache();
        services.AddHttpClient();

        services.AddAuthorizationCore();
        services.AddJsonLocalization(localizationConfigure);

        services.AddConfiguration();
        services.TryAddSingleton<ICacheManager, CacheManager>();
        services.TryAddSingleton<IComponentIdGenerator, DefaultIdGenerator>();
        services.TryAddSingleton<ILookupService, NullLookupService>();
        services.TryAddSingleton<IVersionService, DefaultJSVersionService>();
        services.TryAddSingleton<IZipArchiveService, DefaultZipArchiveService>();
        services.TryAddSingleton(typeof(IDispatchService<>), typeof(DefaultDispatchService<>));
        services.TryAddSingleton<ITableExport, DefaultTableExport>();
        services.TryAddSingleton<IExportPdf, DefaultExportPdf>();

        // IP 地理位置定位服务
        services.TryAddSingleton<IIpLocatorFactory, DefaultIpLocatorFactory>();
        services.AddSingleton<IIpLocatorProvider, JuHeIpLocatorProvider>();
        services.AddSingleton<IIpLocatorProvider, BaiduIpLocatorProvider>();
        services.AddSingleton<IIpLocatorProvider, BaiduIpLocatorProviderV2>();

        // 节日服务
        services.TryAddSingleton<ICalendarFestivals, DefaultCalendarFestivals>();

        // 假日服务
        services.TryAddSingleton<ICalendarHolidays, DefaultCalendarHolidays>();

        // 在线连接服务
        services.TryAddSingleton<IConnectionService, DefaultConnectionService>();

        // 限流器服务
        services.TryAddSingleton<IThrottleDispatcherFactory, DefaultThrottleDispatcherFactory>();

        services.TryAddScoped(typeof(IDataService<>), typeof(NullDataService<>));
        services.TryAddScoped<IReconnectorProvider, ReconnectorProvider>();
        services.TryAddScoped<IGeoLocationService, DefaultGeoLocationService>();
        services.TryAddScoped<IComponentHtmlRenderer, ComponentHtmlRenderer>();
        services.TryAddScoped<IBrowserFingerService, DefaultBrowserFingerService>();

        services.AddScoped<TabItemTextOptions>();
        services.AddScoped<DialogService>();
        services.AddScoped<MessageService>();
        services.AddScoped<ToastService>();
        services.AddScoped<SwalService>();
        services.AddScoped<FullScreenService>();
        services.AddScoped<PrintService>();
        services.AddScoped<TitleService>();
        services.AddScoped<DownloadService>();
        services.AddScoped<WebClientService>();
        services.AddScoped<AjaxService>();
        services.AddScoped(typeof(DragDropService<>));
        services.AddScoped<ClipboardService>();
        services.AddScoped<ResizeNotificationService>();
        services.AddScoped<NotificationService>();
        services.AddScoped<EyeDropperService>();

        services.ConfigureBootstrapBlazorOption(configureOptions);

        services.AddTabItemBindOptions();
        services.AddIconTheme();
        return services;
    }

    /// <summary>
    /// BootstrapBlazorOptions 扩展配置方法
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configureOptions"></param>
    /// <returns></returns>
    private static IServiceCollection ConfigureBootstrapBlazorOption(this IServiceCollection services, Action<BootstrapBlazorOptions>? configureOptions = null)
    {
        services.AddOptionsMonitor<BootstrapBlazorOptions>();
        services.Configure<BootstrapBlazorOptions>(op =>
        {
            configureOptions?.Invoke(op);

            // 设置默认文化信息
            if (op.DefaultCultureInfo != null)
            {
                var culture = new CultureInfo(op.DefaultCultureInfo);
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
            }

            // 设置 FallbackCulture
            SetFallbackCulture();

            [ExcludeFromCodeCoverage]
            void SetFallbackCulture()
            {
                if (string.IsNullOrEmpty(CultureInfo.CurrentUICulture.Name))
                {
                    var culture = new CultureInfo(op.FallbackCulture);
                    CultureInfo.CurrentCulture = culture;
                    CultureInfo.CurrentUICulture = culture;
                }
            }
        });
        return services;
    }

    /// <summary>
    /// IPLocatorOption 扩展配置方法
    /// </summary>
    /// <param name="services"></param>
    /// <param name="locatorAction"></param>
    /// <returns></returns>
    [Obsolete("已弃用 请删除即可")]
    [ExcludeFromCodeCoverage]
    public static IServiceCollection ConfigureIPLocatorOption(this IServiceCollection services, Action<IPLocatorOption>? locatorAction = null)
    {
        services.AddOptionsMonitor<IPLocatorOption>();
        if (locatorAction != null)
        {
            services.Configure(locatorAction);
        }
        return services;
    }

    /// <summary>
    /// JsonLocalizationOptions 扩展配置方法
    /// </summary>
    /// <param name="services"></param>
    /// <param name="localizationConfigure"></param>
    public static IServiceCollection ConfigureJsonLocalizationOptions(this IServiceCollection services, Action<JsonLocalizationOptions> localizationConfigure)
    {
        services.Configure(localizationConfigure);
        return services;
    }

    /// <summary>
    /// 增加支持热更新配置类
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <param name="services"></param>
    public static IServiceCollection AddOptionsMonitor<TOptions>(this IServiceCollection services) where TOptions : class
    {
        services.AddOptions();
        services.TryAddSingleton<IOptionsChangeTokenSource<TOptions>, ConfigurationChangeTokenSource<TOptions>>();
        services.TryAddSingleton<IConfigureOptions<TOptions>, ConfigureOptions<TOptions>>();
        return services;
    }

    /// <summary>
    /// 增加 菜单与标签捆绑类配置项服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    static IServiceCollection AddTabItemBindOptions(this IServiceCollection services)
    {
        services.AddOptionsMonitor<TabItemBindOptions>();
        return services;
    }

    /// <summary>
    /// 增加第三方菜单路由与 Tab 捆绑字典配置
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configureOptions"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureTabItemMenuBindOptions(this IServiceCollection services, Action<TabItemBindOptions> configureOptions)
    {
        services.Configure(configureOptions);
        return services;
    }

    /// <summary>
    /// 增加 图标映射配置项服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    static IServiceCollection AddIconTheme(this IServiceCollection services)
    {
        services.TryAddSingleton<IIconTheme, DefaultIconTheme>();
        services.AddOptionsMonitor<IconThemeOptions>();
        return services;
    }

    /// <summary>
    /// IconThemeOptions 扩展配置方法
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configureOptions"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureIconThemeOptions(this IServiceCollection services, Action<IconThemeOptions> configureOptions)
    {
        services.Configure(configureOptions);
        return services;
    }
}
