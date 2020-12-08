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
using PetaPoco;
using System;
using System.Linq;

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
        /// <returns></returns>
        public static IServiceCollection AddFreeSql(this IServiceCollection services, Action<FreeSqlBuilder> optionsAction)
        {

            string connstr = $"data source=test.db;Pooling=true;Max Pool Size=10";

            IFreeSql fsql  = new FreeSql.FreeSqlBuilder()
                .UseConnectionString(FreeSql.DataType.Sqlite, connstr,
                typeof(FreeSql.Sqlite.SqliteProvider<>))
                .UseAutoSyncStructure(true)
                .UseMonitorCommand(cmd => System.Console.WriteLine(cmd.CommandText))
                .UseNoneCommandParameter(true)
                .Build();

            fsql.Aop.ConfigEntity += (s, e) => {
                var attr = e.EntityType.GetCustomAttributes(typeof(TableNameAttribute), false).FirstOrDefault() as TableNameAttribute;
                if (attr != null)
                    e.ModifyResult.Name = attr.Value; //表名
            };
            fsql.Aop.ConfigEntityProperty += (s, e) => {
                var attr = e.Property.DeclaringType.GetCustomAttributes(typeof(PrimaryKeyAttribute), false).FirstOrDefault() as PrimaryKeyAttribute;
                if (attr != null && attr.Value == e.Property.Name)
                {
                    e.ModifyResult.IsPrimary = true;
                    if (attr.AutoIncrement)
                        e.ModifyResult.IsIdentity = attr.AutoIncrement;
                }
            };

 
            services.AddSingleton<IFreeSql>(fsql);

            //services.AddSingleton<IFreeSql>(sp =>
            //{
            //    var builder = new FreeSqlBuilder();
            //    optionsAction(builder);
            //    return builder.Build();
            //});

            services.AddSingleton(typeof(IDataService<>), typeof(DefaultDataService<>));
            return services;
        }
    }
}
