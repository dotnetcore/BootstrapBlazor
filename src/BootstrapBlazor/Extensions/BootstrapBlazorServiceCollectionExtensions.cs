// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Globalization;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// <para lang="zh">BootstrapBlazor 服务扩展类</para>
/// <para lang="en">BootstrapBlazor Service Extensions</para>
/// </summary>
public static class BootstrapBlazorServiceCollectionExtensions
{
    /// <summary>
    /// <para lang="zh">增加 BootstrapBlazor 服务</para>
    /// <para lang="en">Add BootstrapBlazor Service</para>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configureOptions"></param>
    /// <param name="localizationConfigure"></param>
    /// <returns></returns>
    public static IServiceCollection AddBootstrapBlazor(this IServiceCollection services, Action<BootstrapBlazorOptions>? configureOptions = null, Action<JsonLocalizationOptions>? localizationConfigure = null)
    {
        services.AddMemoryCache();
        services.AddHttpClient();

        services.AddJsonLocalization(localizationConfigure);

        services.AddConfiguration();
        services.TryAddSingleton<ICacheManager, CacheManager>();
        services.TryAddSingleton<IComponentIdGenerator, DefaultIdGenerator>();
        services.TryAddSingleton<ILookupService, NullLookupService>();
        services.TryAddSingleton<IVersionService, DefaultJSVersionService>();
        services.TryAddSingleton<IZipArchiveService, DefaultZipArchiveService>();
        services.TryAddSingleton(typeof(IDispatchService<>), typeof(DefaultDispatchService<>));

        // <para lang="zh">增加 OtpOptions 配置支持</para>
        // <para lang="en">Add OtpOptions support</para>
        services.AddOptionsMonitor<OtpOptions>();

        // <para lang="zh">增加 ITotpService</para>
        // <para lang="en">Add ITotpService</para>
        services.TryAddSingleton<ITotpService, DefaultTotpService>();

        // <para lang="zh">BootstrapBlazorRootRegisterService 服务</para>
        // <para lang="en">BootstrapBlazorRootRegisterService Service</para>
        services.AddScoped<BootstrapBlazorRootRegisterService>();

        // <para lang="zh">Html2Pdf 服务</para>
        // <para lang="en">Html2Pdf Service</para>
        services.TryAddSingleton<IHtml2Pdf, DefaultHtml2PdfService>();

        // <para lang="zh">Html2Image 服务</para>
        // <para lang="en">Html2Image Service</para>
        services.TryAddScoped<IHtml2Image, DefaultHtml2ImageService>();

        // <para lang="zh">Table 导出服务</para>
        // <para lang="en">Table Export Service</para>
        services.TryAddScoped<ITableExport, DefaultTableExport>();

        // <para lang="zh">主题服务</para>
        // <para lang="en">Theme Service</para>
        services.TryAddScoped<IThemeProvider, DefaultThemeProvider>();

        // <para lang="zh">IP 地理位置定位服务</para>
        // <para lang="en">IP Locator Service</para>
        services.TryAddSingleton<IIpLocatorFactory, DefaultIpLocatorFactory>();
        services.AddSingleton<IIpLocatorProvider, BaiduIpLocatorProvider>();
        services.AddSingleton<IIpLocatorProvider, BaiduIpLocatorProviderV2>();

#if NET8_0_OR_GREATER
        services.AddKeyedSingleton<IIpLocatorProvider, BaiduIpLocatorProvider>(nameof(BaiduIpLocatorProvider));
        services.AddKeyedSingleton<IIpLocatorProvider, BaiduIpLocatorProviderV2>(nameof(BaiduIpLocatorProviderV2));
#endif

        // <para lang="zh">节日服务</para>
        // <para lang="en">Festival Service</para>
        services.TryAddSingleton<ICalendarFestivals, DefaultCalendarFestivals>();

        // <para lang="zh">假日服务</para>
        // <para lang="en">Holiday Service</para>
        services.TryAddSingleton<ICalendarHolidays, DefaultCalendarHolidays>();

        // <para lang="zh">在线连接服务</para>
        // <para lang="en">Connection Service</para>
        services.TryAddSingleton<IConnectionService, DefaultConnectionService>();

        // <para lang="zh">限流器服务</para>
        // <para lang="en">Throttle Dispatcher Service</para>
        services.TryAddSingleton<IThrottleDispatcherFactory, DefaultThrottleDispatcherFactory>();

        // <para lang="zh">汉字拼音服务</para>
        // <para lang="en">Pinyin Service</para>
        services.TryAddSingleton<IPinyinService, DefaultPinyinService>();

        services.TryAddScoped(typeof(IDataService<>), typeof(NullDataService<>));
        services.TryAddScoped<IReconnectorProvider, ReconnectorProvider>();
        services.TryAddScoped<IGeoLocationService, DefaultGeoLocationService>();
        services.TryAddScoped<IComponentHtmlRenderer, ComponentHtmlRenderer>();
        services.TryAddScoped<IBrowserFingerService, DefaultBrowserFingerService>();
        services.TryAddScoped<ISerialService, DefaultSerialService>();
        services.TryAddScoped<IBluetooth, DefaultBluetooth>();
        services.TryAddScoped<IMediaDevices, DefaultMediaDevices>();
        services.TryAddScoped<IVideoDevice, DefaultVideoDevice>();
        services.TryAddScoped<IAudioDevice, DefaultAudioDevice>();
        services.TryAddScoped<INetworkMonitorService, DefaultNetowrkMonitorService>();
        services.AddScoped<TabItemTextOptions>();
        services.AddScoped<DialogService>();
        services.AddScoped<MaskService>();
        services.AddScoped<MessageService>();
        services.AddScoped<ToastService>();
        services.AddScoped<DrawerService>();
        services.AddScoped<SwalService>();
        services.AddScoped<FullScreenService>();
        services.AddScoped<PrintService>();
        services.AddScoped<TitleService>();
        services.AddScoped<DownloadService>();
        services.AddScoped<WebClientService>();
        services.AddScoped<AjaxService>();
        services.AddScoped(typeof(DragDropService<>));
        services.AddScoped<ClipboardService>();
        services.AddScoped<NotificationService>();
        services.AddScoped<EyeDropperService>();
        services.AddScoped<WebSpeechService>();

        services.ConfigureBootstrapBlazorOption(configureOptions);

        services.AddTabItemBindOptions();
        services.AddIconTheme();
        return services;
    }

    /// <summary>
    /// <para lang="zh">BootstrapBlazorOptions 扩展配置方法</para>
    /// <para lang="en">BootstrapBlazorOptions Configuration Extension</para>
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

            // <para lang="zh">设置默认文化信息</para>
            // <para lang="en">Set default culture info</para>
            if (op.DefaultCultureInfo != null)
            {
                var culture = new CultureInfo(op.DefaultCultureInfo);
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
            }

            // <para lang="zh">设置 FallbackCulture</para>
            // <para lang="en">Set FallbackCulture</para>
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
    /// <para lang="zh">IPLocatorOption 扩展配置方法</para>
    /// <para lang="en">IPLocatorOption Configuration Extension</para>
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
    /// <para lang="zh">JsonLocalizationOptions 扩展配置方法</para>
    /// <para lang="en">JsonLocalizationOptions Configuration Extension</para>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="localizationConfigure"></param>
    public static IServiceCollection ConfigureJsonLocalizationOptions(this IServiceCollection services, Action<JsonLocalizationOptions> localizationConfigure)
    {
        services.Configure(localizationConfigure);
        return services;
    }

    /// <summary>
    /// <para lang="zh">增加支持热更新配置类</para>
    /// <para lang="en">Add Options Monitor Support</para>
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
    /// <para lang="zh">增加 菜单与标签捆绑类配置项服务</para>
    /// <para lang="en">Add TabItem Bind Options Service</para>
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    static IServiceCollection AddTabItemBindOptions(this IServiceCollection services)
    {
        services.AddOptionsMonitor<TabItemBindOptions>();
        return services;
    }

    /// <summary>
    /// <para lang="zh">配置第三方菜单路由与 Tab 标签页捆绑字典扩展方法</para>
    /// <para lang="en">Configure TabItem Menu Bind Options Extension</para>
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
    /// <para lang="zh">增加 图标映射配置项服务</para>
    /// <para lang="en">Add Icon Theme Options Service</para>
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
    /// <para lang="zh">配置 <see cref="IconThemeOptions"/> 扩展方法</para>
    /// <para lang="en">Configure IconThemeOptions Extension</para>
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configureOptions"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureIconThemeOptions(this IServiceCollection services, Action<IconThemeOptions>? configureOptions = null)
    {
        if (configureOptions != null)
        {
            services.Configure(configureOptions);
        }
        return services;
    }
}
