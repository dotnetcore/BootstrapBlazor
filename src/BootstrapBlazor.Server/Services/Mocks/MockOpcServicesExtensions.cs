// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.OpcDa;

namespace Microsoft.Extensions.DependencyInjection;

static class MockOpcServicesExtensions
{
    /// <summary>
    /// 增加模拟 OpcDaServer 操作服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddMockOpcDaServer(this IServiceCollection services)
    {
        services.AddSingleton<IOpcDaServer, MockOpcDaServer>();
        return services;
    }
}
