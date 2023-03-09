// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// FontAwesome 图标库扩展方法
/// </summary>
public static class IconMapperOptionsExtensions
{
    /// <summary>
    /// 添加 FontAwesome 图标到系统
    /// </summary>
    /// <param name="options"></param>
    public static void AddFontAwesomeIconMapper(this IconMapperOptions options)
    {
        options.Items = new()
        {
            { BootstrapIcons.AnchorLinkIcon, "fa-solid fa-link" }
        };
    }

    /// <summary>
    /// 通过指定 Key 获得 Icon 字符串方法
    /// </summary>
    /// <param name="options"></param>
    /// <param name="key"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static string? GetIcon(this IconMapperOptions options, BootstrapIcons key, string? defaultValue = null)
    {
        string? icon = null;
        if (options.Items.TryGetValue(key, out var v))
        {
            icon = v;
        }
        return icon ?? defaultValue;
    }
}
