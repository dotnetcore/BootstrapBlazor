// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// FontAwesome 图标库扩展方法
/// </summary>
public static class IIconThemeExtensions
{
    /// <summary>
    /// 通过指定 Key 获得 Icon 字符串方法
    /// </summary>
    /// <param name="iconTheme"></param>
    /// <param name="key"></param>
    /// <param name="defaultIcon"></param>
    /// <returns></returns>
    public static string? GetIconByKey(this IIconTheme iconTheme, ComponentIcons key, string? defaultIcon = null)
    {
        string? icon = null;
        var icons = iconTheme.GetIcons();
        if (icons.TryGetValue(key, out var v))
        {
            icon = v;
        }
        return icon ?? defaultIcon;
    }
}
