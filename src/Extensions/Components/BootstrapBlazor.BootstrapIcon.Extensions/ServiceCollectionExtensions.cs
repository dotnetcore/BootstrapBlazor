// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// BootstrapBlazor 服务扩展类
/// </summary>
public static class IconMapperOptionsExtensions
{
    /// <summary>
    /// 添加 Bootstrap 图标主题服务
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection ConfigureBootstrapIconTheme(this IServiceCollection services)
    {
        services.ConfigureIconThemeOptions(options =>
        {
            options.ThemeKey = "bootstrap";
            options.Icons["bootstrap"] = DefaultIcon.Icons;
        });
        return services;
    }
}
