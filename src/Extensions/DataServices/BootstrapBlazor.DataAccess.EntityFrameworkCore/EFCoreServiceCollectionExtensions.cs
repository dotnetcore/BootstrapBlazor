// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.DataAccess.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// BootstrapBlazor EFCore 服务扩展类
/// </summary>
public static class EFCoreServiceCollectionExtensions
{
    /// <summary>
    /// 增加 Entity Framework 数据库操作服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="optionsAction"></param>
    /// <param name="contextLifetime"></param>
    /// <param name="optionsLifetime"></param>
    /// <returns></returns>
    public static IServiceCollection AddEntityFrameworkCore<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder>? optionsAction = null, ServiceLifetime contextLifetime = ServiceLifetime.Scoped, ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
        where TContext : DbContext
    {
        services.AddDbContext<TContext>(optionsAction, contextLifetime, optionsLifetime);
        services.Add(new ServiceDescriptor(typeof(IDataService<>), typeof(DefaultDataService<>), contextLifetime));
        services.Add(new ServiceDescriptor(typeof(Func<IEntityFrameworkCoreDataService, DbContext>), provider =>
        {
            DbContext DbContextResolve(IEntityFrameworkCoreDataService server)
            {
                return provider.GetRequiredService<TContext>();
            }
            return (Func<IEntityFrameworkCoreDataService, DbContext>)DbContextResolve;
        }, contextLifetime));
        return services;
    }
}
