// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.DataAccess.SqlSugar;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// BootstrapBlazor SqlSugar 服务扩展类
/// </summary>
public static class SqlSugarServiceCollectionExtensions
{
    /// <summary>
    /// 增加 SqlSugar 数据库操作服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="optionsAction"></param>
    /// <param name="configureAction"></param>
    /// <returns></returns>
    public static IServiceCollection AddSqlSugar(this IServiceCollection services,
        Action<ConnectionConfig> optionsAction,
        Action<SqlSugarClient>? configureAction = null)
    {
        services.AddSingleton<ISqlSugarClient>(sp =>
        {
            var config = new ConnectionConfig();
            optionsAction(config);
            return new SqlSugarScope(config, configureAction);
        });
        services.AddScoped(typeof(IDataService<>), typeof(DefaultDataService<>));
        return services;
    }
}
