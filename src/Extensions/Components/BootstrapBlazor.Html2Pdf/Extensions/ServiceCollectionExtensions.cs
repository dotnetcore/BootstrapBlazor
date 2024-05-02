// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// BootstrapBlazor 服务扩展类
/// </summary>
public static class BootstrapBlazorHtml2PdfServiceExtensions
{
    /// <summary>
    /// 添加 AzureOpenAIService 服务
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddBootstrapBlazorHtml2PdfService(this IServiceCollection services)
    {
        services.AddSingleton<IHtml2Pdf, DefaultPdfService>();

        return services;
    }
}
