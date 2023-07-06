// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Bootstrap.Shared.OAuth;
using BootstrapBlazor.Components;
using BootstrapBlazor.Server.Services;
using BootstrapBlazor.Shared;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
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
    /// <param name="configureOptions"></param>
    public static IServiceCollection AddBootstrapBlazorServices(this IServiceCollection services, Action<BootstrapBlazorOptions>? configureOptions = null)
    {
        // 增加错误日志
        services.AddLogging(logging => logging.AddFileLogger());

        // 增加后台任务服务
        services.AddTaskServices();
        services.AddHostedService<ClearUploadFilesService>();

        // 增加演示网站服务
        services.AddWebSiteServices();

        // 配置网站路由表
        services.Configure<WebsiteOptions>(op =>
        {
            op.AdditionalAssemblies = new Assembly[] { typeof(BootstrapBlazor.Shared.OAuth.AzureOpenAIUser).Assembly };
        });

        // 增加 BootstrapBlazor 组件
        services.AddBootstrapBlazor(configureOptions);

        // 增加 Azure 语音服务
        //services.AddBootstrapBlazorAzureSpeech();

        // 增加 Baidu 语音服务
        services.AddBootstrapBlazorBaiduSpeech();

        services.ConfigureJsonLocalizationOptions(op =>
        {
            // 附加自己的 json 多语言文化资源文件 如 zh-TW.json
            op.AdditionalJsonAssemblies = new Assembly[]
            {
                typeof(BootstrapBlazor.Shared.App).Assembly,
                typeof(BootstrapBlazor.Components.BarcodeReader).Assembly,
                typeof(BootstrapBlazor.Components.Chart).Assembly,
                typeof(BootstrapBlazor.Components.SignaturePad).Assembly
            };
        });

        // 设置地理位置定位器
        services.ConfigureIPLocatorOption(op => op.LocatorFactory = sp => new BaiDuIPLocator());

        // 增加 Baidu ORC 服务
        services.AddBootstrapBlazorBaiduOcr();

        // 增加多语言支持配置信息
        services.AddRequestLocalization<IOptionsMonitor<BootstrapBlazorOptions>>((localizerOption, blazorOption) =>
        {
            blazorOption.OnChange(op => Invoke(op));
            Invoke(blazorOption.CurrentValue);

            void Invoke(BootstrapBlazorOptions option)
            {
                var supportedCultures = option.GetSupportedCultures();
                localizerOption.SupportedCultures = supportedCultures;
                localizerOption.SupportedUICultures = supportedCultures;
            }
        });

        // 增加 AzureOpenAI 服务
        services.AddBootstrapBlazorAzureOpenAIService();

        // 增加 Pdf 导出服务
        services.AddBootstrapBlazorHtml2PdfService();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie()
            .AddGitee(OAuthHelper.Configure)
            .AddGitHub(OAuthHelper.Configure);

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
