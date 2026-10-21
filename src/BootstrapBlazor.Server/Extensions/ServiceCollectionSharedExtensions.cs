// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 注册服务扩展方法
/// </summary>
public static class ServiceCollectionSharedExtensions
{
    /// <summary>
    /// 添加示例后台任务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configureOptions"></param>
    public static IServiceCollection AddBootstrapBlazorServices(this IServiceCollection services, Action<BootstrapBlazorOptions>? configureOptions = null)
    {
        services.AddSingleton<CodeSnippetService>();
        services.AddSingleton<SiteNotificationService>();
        services.AddHostedService(provider => provider.GetRequiredService<SiteNotificationService>());
        services.AddSingleton<ISampleDocumentationService>(provider => provider.GetRequiredService<CodeSnippetService>());
        services.AddSingleton<MockDataTableDynamicService>();

        // 增加示例网站配置
        services.AddBootstrapBlazorLayoutServices(configureOptions);

        // 增加 Azure 语音服务
        //services.AddBootstrapBlazorAzureSpeech();

        // 增加 Baidu 语音服务
        services.AddBootstrapBlazorBaiduSpeech();

        // 增加 Baidu ORC 服务
        services.AddBootstrapBlazorBaiduOcr();

        // 增加 AzureOpenAI 服务
        services.AddBootstrapBlazorAzureOpenAIService();

        // 增加 AzureTranslator 服务
        services.AddBootstrapBlazorAzureTranslator();

        // 增加 Html2Pdf 导出服务
        services.AddBootstrapBlazorHtml2PdfService();

        // 增加 BootstrapBlazor 假日服务

        // 增加 Table Excel 导出服务
        services.AddBootstrapBlazorTableExportService();

        // 增加离线 IP 定位服务
        services.AddBootstrapBlazorIP2RegionService();

        // 增加 JuHe 定位服务
        services.AddBootstrapBlazorJuHeIpLocatorService();

        // 增加密钥存储配置
        services.AddDataProtection();

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
        //         //调试 sql 语句输出
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
