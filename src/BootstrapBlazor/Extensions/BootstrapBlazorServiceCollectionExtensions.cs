// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Localization.Json;
using BootstrapBlazor.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
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
    /// <param name="localizationAction"></param>
    /// <returns></returns>
    public static IServiceCollection AddBootstrapBlazor(this IServiceCollection services, Action<BootstrapBlazorOptions>? configureOptions = null, Action<JsonLocalizationOptions>? localizationAction = null)
    {
        services.AddMemoryCache();
        services.AddHttpClient();

        services.AddAuthorizationCore();
        services.AddJsonLocalization(localizationAction);
        services.AddSingleton<ICacheManager, CacheManager>();

        services.TryAddSingleton<IComponentIdGenerator, DefaultIdGenerator>();
        services.TryAddSingleton(typeof(IDispatchService<>), typeof(DefaultDispatchService<>));
        services.TryAddSingleton(typeof(ILookupService), typeof(NullLookupService));

        services.TryAddScoped<ITableExcelExport, DefaultExcelExport>();
        services.TryAddScoped(typeof(IDataService<>), typeof(NullDataService<>));
        services.TryAddScoped<TabItemTextOptions>();

        services.TryAddScoped<DialogService>();
        services.TryAddScoped<MessageService>();
        services.TryAddScoped<PopoverService>();
        services.TryAddScoped<ToastService>();
        services.TryAddScoped<SwalService>();
        services.TryAddScoped<FullScreenService>();
        services.TryAddScoped<PrintService>();
        services.TryAddScoped<TitleService>();
        services.TryAddScoped<DownloadService>();
        services.TryAddScoped<WebClientService>();
        services.TryAddScoped<AjaxService>();
        services.TryAddScoped(typeof(DragDropService<>));
        services.TryAddScoped<ClipboardService>();
        services.TryAddScoped<ResizeNotificationService>();

        services.TryAddScoped<IIPLocatorProvider, DefaultIPLocatorProvider>();
        services.TryAddScoped<IReconnectorProvider, ReconnectorProvider>();

        services.ConfigureBootstrapBlazorOption(configureOptions);
        services.ConfigureIPLocatorOption();
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
            // 设置默认文化信息
            if (op.DefaultCultureInfo != null)
            {
                var culture = new CultureInfo(op.DefaultCultureInfo);
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
            }

            if (string.IsNullOrEmpty(CultureInfo.CurrentUICulture.Name))
            {
                var culture = new CultureInfo(op.FallbackCulture);
                CultureInfo.CurrentUICulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }
            configureOptions?.Invoke(op);
        });
        return services;
    }

    /// <summary>
    /// IPLocatorOption 扩展配置方法
    /// </summary>
    /// <param name="services"></param>
    /// <param name="locatorAction"></param>
    /// <returns></returns>
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
    /// <param name="localizationAction"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureJsonLocalizationOptions(this IServiceCollection services, Action<JsonLocalizationOptions> localizationAction)
    {
        services.Configure(localizationAction);
        return services;
    }

    /// <summary>
    /// 增加支持热更新配置类
    /// </summary>
    /// <typeparam name="TOptions"></typeparam>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddOptionsMonitor<TOptions>(this IServiceCollection services) where TOptions : class
    {
        services.AddOptions();
        services.TryAddSingleton<IOptionsChangeTokenSource<TOptions>, ConfigurationChangeTokenSource<TOptions>>();
        services.TryAddSingleton<IConfigureOptions<TOptions>, ConfigureOptions<TOptions>>();
        return services;
    }
}
