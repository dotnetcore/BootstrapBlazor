// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Server.Services;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 后台任务扩展方法
/// </summary>
internal static class ServicesExtensions
{
    /// <summary>
    /// 添加示例后台任务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="themes"></param>
    public static IServiceCollection AddBootstrapBlazorServices(this IServiceCollection services, IEnumerable<KeyValuePair<string, string>> themes)
    {
        // 增加错误日志
        services.AddLogging(logging => logging.AddFileLogger());

        // 增加后台任务服务
        services.AddTaskServices();
        services.AddHostedService<ClearUploadFilesService>();

        // 增加演示网站服务
        services.AddWebSiteServices();

        // 增加 BootstrapBlazor 组件
        services.AddBootstrapBlazor(options =>
        {
            // 统一设置 Toast 组件自动消失时间
            options.ToastDelay = 4000;
            options.Themes.AddRange(themes);
        }, options =>
        {
            // 附加自己的 json 多语言文化资源文件 如 zh-TW.json
            options.AdditionalJsonAssemblies = new Assembly[]
            {
                    typeof(BootstrapBlazor.Shared.App).Assembly,
                    typeof(BootstrapBlazor.Components.Chart).Assembly
            };
        });

        // 增加多语言支持配置信息
        services.AddRequestLocalization<IOptions<BootstrapBlazorOptions>>((localizerOption, blazorOption) =>
        {
            var supportedCultures = blazorOption.Value.GetSupportedCultures();

            localizerOption.SupportedCultures = supportedCultures;
            localizerOption.SupportedUICultures = supportedCultures;
        });

        // 增加 PetaPoco ORM 数据服务操作类
        // 需要时打开下面代码
        //services.AddPetaPoco(option =>
        //{
        //    // 配置数据信息
        //    // 使用 SQLite 数据以及从配置文件中获取数据库连接字符串
        //    // 需要引用 Microsoft.Data.Sqlite 包，操作 SQLite 数据库
        //    // 需要引用 PetaPoco.Extensions 包，PetaPoco 包扩展批量插入与删除
        //    option.UsingProvider<SQLiteDatabaseProvider>()
        //          .UsingConnectionString(Configuration.GetConnectionString("bb"));
        //});

        // 增加 FreeSql ORM 数据服务操作类
        // 需要时打开下面代码
        // 需要引入 FreeSql 对 SQLite 的扩展包 FreeSql.Provider.Sqlite
        //services.AddFreeSql(option =>
        //{
        //    option.UseConnectionString(FreeSql.DataType.Sqlite, Configuration.GetConnectionString("bb"))
#if DEBUG
        //         //开发环境:自动同步实体
        //         .UseAutoSyncStructure(true)
        //         //调试sql语句输出
        //         .UseMonitorCommand(cmd => System.Console.WriteLine(cmd.CommandText))
#endif
        //        ;
        //});

        // 增加 EFCore ORM 数据服务操作类
        // 需要时打开下面代码
        //services.AddEntityFrameworkCore<Shared.Pages.FooDbContext>(option =>
        //{
        //    // 需要引用 Microsoft.EntityFrameworkCore.Sqlite 包，操作 SQLite 数据库
        //    option.UseSqlite(Configuration.GetConnectionString("bb"));
        //});
        return services;
    }
}
