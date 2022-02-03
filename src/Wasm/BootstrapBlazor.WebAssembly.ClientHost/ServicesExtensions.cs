// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// 
/// </summary>
public static class ServicesExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddWasmServices(this IServiceCollection services)
    {
        // 增加演示网站服务
        services.AddWebSiteServices();

        // 增加 BootstrapBlazor 组件
        services.AddBootstrapBlazor(options =>
        {
                // 统一设置 Toast 组件自动消失时间
                options.ToastDelay = 4000;
            options.Themes.AddRange(new KeyValuePair<string, string>[]
            {
                    new("Ant Design (完善中)", "ant"),
                    new("LayUI (完善中)", "layui")
            });
        }, options =>
        {
                // 附加自己的 json 多语言文化资源文件 如 zh-TW.json
                options.AdditionalJsonAssemblies = new Assembly[]
            {
                    typeof(BootstrapBlazor.Shared.App).Assembly,
                    typeof(BootstrapBlazor.Components.Chart).Assembly
            };
        });

        services.Configure<WebsiteOptions>(options =>
        {
            options.RepositoryUrl = "https://www.blazor.zone/api/docs/";
        });

        services.Configure<BootstrapBlazorOptions>(op =>
        {
            op.ToastDelay = 4000;
            op.FallbackCulture = "en";
            op.SupportedCultures = new List<string> { "zh-CN", "en-US" };
        });

        services.AddLocalization();
        return services;
    }
}
