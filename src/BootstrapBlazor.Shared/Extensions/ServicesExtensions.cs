// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared;
using BootstrapBlazor.Shared.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 添加演示网站服务
/// </summary>
public static class ServicesExtensions
{
    /// <summary>
    /// 添加 Server Side 演示网站服务
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddWebSiteServices(this IServiceCollection services)
    {
        services.AddSingleton<WeatherForecastService>();
        services.AddSingleton<VersionService>();
        services.AddSingleton<CodeSnippetService>();
        services.AddSingleton<IConfigureOptions<WebsiteOptions>, ConfigureOptions<WebsiteOptions>>();
        services.AddSingleton(typeof(IDataService<>), typeof(TableDemoDataService<>));

        // 增加模拟登录服务
        services.AddScoped<AuthenticationStateProvider, MockAuthenticationStateProvider>();

        // 增加 Table Excel 导出服务
        services.AddBootstrapBlazorTableExcelExport();

        return services;
    }
}
