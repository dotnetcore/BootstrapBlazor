// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.DataAccess.FreeSql;
using FreeSql;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// BootstrapBlazor FreeSql 服务扩展类
/// </summary>
public static class FreeSqlServiceCollectionExtensions
{
    /// <summary>
    /// 增加 FreeSql 数据库操作服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="optionsAction"></param>
    /// <param name="configureAction"></param>
    /// <returns></returns>
    public static IServiceCollection AddFreeSql(this IServiceCollection services, Action<FreeSqlBuilder> optionsAction, Action<IFreeSql>? configureAction = null)
    {
        services.AddSingleton(sp =>
        {
            var builder = new FreeSqlBuilder();
            optionsAction(builder);
            var instance = builder.Build();
            configureAction?.Invoke(instance);
            return instance;
        });

        services.AddScoped(typeof(IDataService<>), typeof(DefaultDataService<>));
        return services;
    }
}
