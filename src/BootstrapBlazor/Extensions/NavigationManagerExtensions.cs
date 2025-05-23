// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.DependencyInjection;

namespace BootstrapBlazor.Components;

/// <summary>
/// NavigationManager 扩展方法
/// </summary>
public static class NavigationManagerExtensions
{
    /// <summary>
    /// 导航并添加 TabItem 方法
    /// </summary>
    /// <param name="navigation"></param>
    /// <param name="provider"></param>
    /// <param name="url"></param>
    /// <param name="text"></param>
    /// <param name="icon"></param>
    /// <param name="closable"></param>
    public static void NavigateTo(this NavigationManager navigation, IServiceProvider provider, string url, string text, string? icon = null, bool closable = true)
    {
        var option = provider.GetRequiredService<TabItemTextOptions>();
        option.Text = text;
        option.Icon = icon;
        option.Closable = closable;
        navigation.NavigateTo(url);
    }

    /// <summary>
    /// 获得当前 Url 的相对路径，不包含 QueryString 和 Fragment（Hash）
    /// </summary>
    /// <param name="navigationManager"></param>
    /// <returns></returns>
    public static string ToBaseRelativePathWithoutQueryAndFragment(this NavigationManager navigationManager)
    {
        var url = navigationManager.ToBaseRelativePath(navigationManager.Uri);

        var index = url.IndexOf('?');
        if (index > -1)
        {
            url = url[..index];
        }

        index = url.IndexOf('#');
        if (index > -1)
        {
            url = url[..index];
        }
        return url;
    }
}
