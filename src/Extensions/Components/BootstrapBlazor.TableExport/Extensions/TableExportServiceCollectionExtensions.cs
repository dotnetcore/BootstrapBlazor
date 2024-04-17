// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// BootstrapBlazor 服务扩展类
/// </summary>
public static class TableExportServiceCollectionExtensions
{
    /// <summary>
    /// 增加 Table 数据导出服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    [Obsolete("已过期，请使用 AddBootstrapBlazorTableExportService 代替 Please use AddBootstrapBlazorTableExportService")]
    public static IServiceCollection AddBootstrapBlazorTableExcelExport(this IServiceCollection services) => services.AddBootstrapBlazorTableExportService();

    /// <summary>
    /// 增加 Table 数据导出服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddBootstrapBlazorTableExportService(this IServiceCollection services)
    {
        services.AddScoped<ITableExport, DefaultTableExport>();
        return services;
    }
}
