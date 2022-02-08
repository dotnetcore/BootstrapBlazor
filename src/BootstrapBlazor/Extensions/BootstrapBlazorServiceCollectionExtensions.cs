// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Localization.Json;
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

        services.TryAddSingleton<IConfigureOptions<BootstrapBlazorOptions>, ConfigureOptions<BootstrapBlazorOptions>>();
        services.ConfigureBootstrapBlazorOption(configureOptions);

        services.TryAddSingleton<GeolocationService>();
        services.TryAddSingleton<IIPLocatorProvider, DefaultIPLocatorProvider>();
        services.TryAddSingleton<IConfigureOptions<IPLocatorOption>, ConfigureOptions<IPLocatorOption>>();
        return services;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="services"></param>
    /// <param name="locatorAction"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureIPLocatorOption(this IServiceCollection services, Action<IPLocatorOption> locatorAction)
    {
        services.Configure<IPLocatorOption>(locatorAction);
        return services;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureBootstrapBlazorOption(this IServiceCollection services, Action<BootstrapBlazorOptions>? options = null)
    {
        services.Configure<BootstrapBlazorOptions>(op =>
        {
            options?.Invoke(op);

            // 设置默认文化信息
            if (op.DefaultCultureInfo != null)
            {
                var culture = new CultureInfo(op.DefaultCultureInfo);
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
            }
        });
        return services;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="services"></param>
    /// <param name="localizationAction"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureJsonLocalizationOptions(this IServiceCollection services, Action<JsonLocalizationOptions> localizationAction)
    {
        services.Configure(localizationAction);
        return services;
    }
}
