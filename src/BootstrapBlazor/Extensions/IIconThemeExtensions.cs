// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
