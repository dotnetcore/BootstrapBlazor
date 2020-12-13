// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using BootstrapBlazor.Components;
using BootstrapBlazor.DataAcces.FreeSql;
using FreeSql;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// BootstrapBlazor 服务扩展类
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
            services.AddSingleton<IFreeSql>(sp =>
            {
                var builder = new FreeSqlBuilder();
                optionsAction(builder);
                var instance = builder.Build();
                configureAction?.Invoke(instance);
                return instance;
            });

            services.AddSingleton(typeof(IDataService<>), typeof(DefaultDataService<>));
            return services;
        }
    }
}
