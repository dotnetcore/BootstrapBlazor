// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// BootstrapBlazor 服务扩展类
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 增加 BootstrapBlazor 服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddBootstrapBlazorSpeech(this IServiceCollection services)
    {
        services.TryAddScoped<SpeechService>();
        services.TryAddScoped<ISpeechProvider, AzureSpeechProvider>();
        return services;
    }
}
