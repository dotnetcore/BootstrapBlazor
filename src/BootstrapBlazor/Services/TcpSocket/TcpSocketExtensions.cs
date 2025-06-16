// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Runtime.Versioning;

namespace BootstrapBlazor.Components;

/// <summary>
/// TcpSocket 扩展方法
/// </summary>
[UnsupportedOSPlatform("browser")]
public static class TcpSocketExtensions
{
    /// <summary>
    /// 增加
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddBootstrapBlazorTcpSocketFactory(this IServiceCollection services)
    {
        // 添加 ITcpSocket 实现
        services.TryAddSingleton<ITcpSocketFactory, DefaultTcpSocketFactory>();

        // 添加 ITcpSocketClient 实现
        services.TryAddTransient<ITcpSocketClient, DefaultTcpSocketClient>();
        return services;
    }
}
