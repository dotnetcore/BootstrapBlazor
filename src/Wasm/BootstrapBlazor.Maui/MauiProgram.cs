// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/
using BootstrapBlazor.Components;
using BootstrapBlazor.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.X509.Qualified;
using System.Reflection;

namespace BootstrapBlazor.Maui;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddMauiBlazorWebView();

        // 增加错误日志
        //builder.Services.AddLogging(logging => logging.AddFileLogger());

        // 增加后台任务服务
        //builder.Services.AddTaskServices();
        //builder.Services.AddHostedService<ClearUploadFilesService>();

        // 增加演示网站服务
        builder.Services.AddWebSiteServices();

        // 配置网站路由表
        builder.Services.Configure<WebsiteOptions>(op =>
        {
            op.AdditionalAssemblies = new Assembly[] { typeof(Shared.App).Assembly };
        });

        // 增加 BootstrapBlazor 组件
        builder.Services.AddBootstrapBlazor();

        // 增加 Azure 语音服务
        //services.AddBootstrapBlazorAzureSpeech();

        // 增加 Baidu 语音服务
        builder.Services.AddBootstrapBlazorBaiduSpeech();

        builder.Services.ConfigureJsonLocalizationOptions(op =>
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
        //builder.Services.ConfigureIPLocatorOption(op => op.LocatorFactory = sp => new BaiDuIPLocator());

        // 增加 Baidu ORC 服务
        builder.Services.AddBootstrapBlazorBaiduOcr();

        // 增加多语言支持配置信息
        //builder.Services.AddRequestLocalization<IOptionsMonitor<BootstrapBlazorOptions>>((localizerOption, blazorOption) =>
        //{
        //    blazorOption.OnChange(op => Invoke(op));
        //    Invoke(blazorOption.CurrentValue);

        //    void Invoke(BootstrapBlazorOptions option)
        //    {
        //        var supportedCultures = option.GetSupportedCultures();
        //        localizerOption.SupportedCultures = supportedCultures;
        //        localizerOption.SupportedUICultures = supportedCultures;
        //    }
        //});

        // 增加 AzureOpenAI 服务
        builder.Services.AddBootstrapBlazorAzureOpenAIService();

        // 增加 Pdf 导出服务
        builder.Services.AddBootstrapBlazorHtml2PdfService();

        //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        //    .AddCookie()
        //    .AddGitee(OAuthHelper.Configure)
        //    .AddGitHub(OAuthHelper.Configure);

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

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
