// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// BootstrapBlazor 服务扩展类
/// </summary>
public static class ServiceCollectionExtension
{
    /// <summary>
    /// 增加 语音识别服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configOptions"></param>
    /// <returns></returns>
    public static IServiceCollection AddBootstrapBlazorBaiduSpeech(this IServiceCollection services, Action<
        BaiduSpeechOption>? configOptions = null)
    {
        services.AddHttpClient();
        services.AddMemoryCache();

        services.TryAddScoped<RecognizerService>();
        services.AddScoped<IRecognizerProvider, BaiduRecognizerProvider>();

        services.TryAddScoped<SynthesizerService>();
        services.AddScoped<ISynthesizerProvider, BaiduSynthesizerProvider>();

        services.TryAddSingleton<IConfigureOptions<BaiduSpeechOption>, ConfigureOptions<BaiduSpeechOption>>();

        services.Configure<BaiduSpeechOption>(option =>
        {
            configOptions?.Invoke(option);
        });
        return services;
    }
}
