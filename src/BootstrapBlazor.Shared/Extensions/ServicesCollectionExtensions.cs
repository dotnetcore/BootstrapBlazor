﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 注册服务扩展方法
/// </summary>
public static class ServicesCollectionExtensions
{
    /// <summary>
    /// 添加示例后台任务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configureOptions"></param>
    public static IServiceCollection AddBootstrapBlazorServices(this IServiceCollection services, Action<BootstrapBlazorOptions>? configureOptions = null)
    {
        // 增加演示网站服务
        services.AddWebSiteServices();

        // 增加 BootstrapBlazor 组件
        services.AddBootstrapBlazor(configureOptions);

        // 增加 Azure 语音服务
        //services.AddBootstrapBlazorAzureSpeech();

        // 增加 Baidu 语音服务
        services.AddBootstrapBlazorBaiduSpeech();

        // 增加 Baidu ORC 服务
        services.AddBootstrapBlazorBaiduOcr();

        // 增加多语言支持配置信息
        services.AddRequestLocalization<IOptionsMonitor<BootstrapBlazorOptions>>((localizerOption, blazorOption) =>
        {
            blazorOption.OnChange(Invoke);
            Invoke(blazorOption.CurrentValue);
            return;

            void Invoke(BootstrapBlazorOptions option)
            {
                var supportedCultures = option.GetSupportedCultures();
                localizerOption.SupportedCultures = supportedCultures;
                localizerOption.SupportedUICultures = supportedCultures;
            }
        });

        // 增加 AzureOpenAI 服务
        services.AddBootstrapBlazorAzureOpenAIService();

        // 增加 AzureTranslator 服务
        services.AddBootstrapBlazorAzureTranslator();

        // 增加 Html2Pdf 导出服务
        services.AddBootstrapBlazorHtml2PdfService();

        // 增加 WinBox 弹窗服务
        services.AddBootstrapBlazorWinBoxService();

        // 配置 Tab 与 Menu 联动字典
        services.ConfigureTabItemMenuBindOptions(options =>
        {
            options.Binders.Add("layout-demo/text=Parameter1", new() { Text = "示例网页" });
            options.Binders.Add("layout-demo", new() { Text = "Text 1" });
            options.Binders.Add("layout-demo?text=Parameter", new() { Text = "Text 2" });
            options.Binders.Add("layout-demo/text=Parameter", new() { Text = "Text 3" });
        });

        // 增加 MaterialDesign 图标主题
        services.ConfigureMaterialDesignIconTheme();

        // 增加 Bootstrap 图标主题
        services.ConfigureBootstrapIconTheme();

        // 增加 FontAwesome 图标主题
        services.ConfigureIconThemeOptions(options => options.ThemeKey = "fa");

        // 增加 BootstrapBlazor 假日服务
        services.AddBootstrapHolidayService();

        // 增加 Table Excel 导出服务
        services.AddBootstrapBlazorTableExportService();

        // 增加 脚本版本服务
        services.AddBootstrapAppendVersionService();

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

    /// <summary>
    /// 添加 Server Side 演示网站服务
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddWebSiteServices(this IServiceCollection services)
    {
        services.AddSingleton<WeatherForecastService>();
        services.AddSingleton<PackageVersionService>();
        services.AddSingleton<CodeSnippetService>();
        services.AddSingleton<DashboardService>();
        services.AddSingleton(typeof(IDataService<>), typeof(TableDemoDataService<>));
        services.AddSingleton(typeof(ILookupService), typeof(DemoLookupService));
        services.AddSingleton<MockDataTableDynamicService>();

        services.AddSingleton<MenuService>();
        services.AddScoped<FanControllerDataService>();

        // 增加示例网站配置
        services.AddOptionsMonitor<WebsiteOptions>();

        // 增加模拟登录服务
        services.AddAuthorization();
        services.AddCascadingAuthenticationState();
        services.AddScoped<AuthenticationStateProvider, MockAuthenticationStateProvider>();

        // 增加 MeiliSearch 服务
        services.AddBootstrapBlazorMeiliSearch();

        return services;
    }
}
