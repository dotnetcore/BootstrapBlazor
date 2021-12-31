// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Localization.Json;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;
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
    /// <param name="locatorAction"></param>
    /// <returns></returns>
    public static IServiceCollection AddBootstrapBlazor(this IServiceCollection services, Action<BootstrapBlazorOptions>? configureOptions = null, Action<JsonLocalizationOptions>? localizationAction = null, Action<IPLocatorOption>? locatorAction = null)
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

        services.TryAddSingleton<IConfigureOptions<BootstrapBlazorOptions>, ConfigureOptions<BootstrapBlazorOptions>>();
        services.Configure<BootstrapBlazorOptions>(options =>
        {
            configureOptions?.Invoke(options);

                // 设置默认文化信息
                if (options.DefaultCultureInfo != null)
            {
                var culture = new CultureInfo(options.DefaultCultureInfo);
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;
            }
        });

        services.TryAddSingleton<IIPLocatorProvider, DefaultIPLocatorProvider>();
        services.Configure<IPLocatorOption>(options =>
        {
            locatorAction?.Invoke(options);
        });
        return services;
    }
}
