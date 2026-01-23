// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">FontAwesome 图标库扩展方法</para>
/// <para lang="en">FontAwesome Icon Theme Extensions</para>
/// </summary>
public static class IIconThemeExtensions
{
    /// <summary>
    /// <para lang="zh">通过指定 Key 获得 Icon 字符串方法</para>
    /// <para lang="en">Get Icon string by key</para>
    /// </summary>
    /// <param name="iconTheme"></param>
    /// <param name="key"></param>
    /// <param name="defaultIcon"></param>
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
