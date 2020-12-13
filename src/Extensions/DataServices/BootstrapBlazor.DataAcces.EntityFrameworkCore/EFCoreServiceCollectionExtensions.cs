// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

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
    }
}
