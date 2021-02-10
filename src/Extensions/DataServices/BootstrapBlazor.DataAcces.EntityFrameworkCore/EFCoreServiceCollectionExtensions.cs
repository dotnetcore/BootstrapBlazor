// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.DataAcces.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// BootstrapBlazor 服务扩展类
    /// </summary>
    public static class EFCoreServiceCollectionExtensions
    {
        /// <summary>
        /// 增加 Entity Framework 数据库操作服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddEntityFrameworkCore<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction) where TContext : DbContext
        {
            services.AddDbContext<TContext>(optionsAction);
            services.AddScoped(typeof(IDataService<>), typeof(DefaultDataService<>));
            services.AddScoped(provider =>
            {
                DbContext DbContextResolve(IEntityFrameworkCoreDataService server)
                {
                    return provider.GetRequiredService<TContext>();
                }
                return (Func<IEntityFrameworkCoreDataService, DbContext>)DbContextResolve;
            });
            return services;
        }

        /// <summary>
        /// 增加配置 DBContext 生命周期参数
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="services"></param>
        /// <param name="optionsAction"></param>
        /// <param name="serviceLifetime"></param>
        /// <returns></returns>
        public static IServiceCollection AddEntityFrameworkCore<TContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction, ServiceLifetime serviceLifetime = default) where TContext : DbContext
        {
            services.AddDbContext<TContext>(optionsAction, serviceLifetime);
            services.AddScoped(typeof(IDataService<>), typeof(DefaultDataService<>));
            services.AddScoped(provider =>
            {
                DbContext DbContextResolve(IEntityFrameworkCoreDataService server)
                {
                    return provider.GetRequiredService<TContext>();
                }
                return (Func<IEntityFrameworkCoreDataService, DbContext>)DbContextResolve;
            });
            return services;
        }
    }
}
