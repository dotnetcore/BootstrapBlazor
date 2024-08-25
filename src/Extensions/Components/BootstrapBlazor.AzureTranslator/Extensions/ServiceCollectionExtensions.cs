// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// BootstrapBlazor 服务扩展类
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 增加 语音识别服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configOptions"></param>
    /// <returns></returns>
    public static IServiceCollection AddBootstrapBlazorAzureTranslator(this IServiceCollection services, Action<
        AzureTranslatorOption>? configOptions = null)
    {
        services.AddHttpClient();

        services.AddSingleton<IAzureTranslatorService, AzureTranslatorService>();
        services.AddOptionsMonitor<AzureTranslatorOption>();

        services.Configure<AzureTranslatorOption>(option =>
        {
            configOptions?.Invoke(option);
        });
        return services;
    }
}
