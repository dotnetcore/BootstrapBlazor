// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Maui.Data;
using Microsoft.Extensions.Logging;

namespace BootstrapBlazor.Maui;

/// <summary>
/// 
/// </summary>
public static class MauiProgram
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
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

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<WeatherForecastService>();

        // 增加 BootstrapBlazor 组件服务
        builder.Services.AddBootstrapBlazor();

        // 增加 Table 数据服务操作类
        builder.Services.AddTableDemoDataService();

        return builder.Build();
    }
}
