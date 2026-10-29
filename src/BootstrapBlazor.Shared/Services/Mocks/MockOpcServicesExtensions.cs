// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.OpcDa;
using BootstrapBlazor.OpcUa;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// <para lang="zh">注册浏览器与非 Windows 示例使用的 OPC 模拟服务。</para>
/// <para lang="en">Registers mock OPC services for browser and non-Windows samples.</para>
/// </summary>
public static class MockOpcServicesExtensions
{
    /// <summary>
    /// <para lang="zh">增加模拟 OpcDaClient 操作服务。</para>
    /// <para lang="en">Adds the mock OpcDa client service.</para>
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddMockOpcDaClient(this IServiceCollection services)
    {
        services.AddSingleton<IOpcDaClient, MockOpcDaClient>();
        return services;
    }

    /// <summary>
    /// <para lang="zh">增加模拟 OpcUaClient 操作服务。</para>
    /// <para lang="en">Adds the mock OpcUa client service.</para>
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddMockOpcUaClient(this IServiceCollection services)
    {
        services.AddScoped<IOpcUaClient, MockOpcUaClient>();
        return services;
    }
}
