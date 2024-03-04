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
    /// 添加 Meterial 图标主题服务
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection ConfigureMaterialDesignIconTheme(this IServiceCollection services)
    {
        services.ConfigureIconThemeOptions(options =>
        {
            options.ThemeKey = "mdi";
            options.Icons["mdi"] = DefaultIcon.Icons;
        });
        return services;
    }
}
