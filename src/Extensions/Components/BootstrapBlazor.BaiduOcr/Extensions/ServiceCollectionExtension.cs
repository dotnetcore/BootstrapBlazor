// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapBlazor 服务扩展类
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    /// 添加百度文字识别服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configOptions"></param>
    /// <returns></returns>
    public static IServiceCollection AddBootstrapBlazorBaiduOcr(this IServiceCollection services, Action<
    BaiduOcrOption>? configOptions = null)
    {
        services.TryAddSingleton<IBaiduOcr, BaiduOcr>();
        services.AddOptionsMonitor<BaiduOcrOption>();
        services.Configure<BaiduOcrOption>(option =>
        {
            configOptions?.Invoke(option);
        });
        return services;
    }
}
