// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using System;

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
    public static void NavigateTo(this NavigationManager navigation, IServiceProvider provider, string url, string text, string? icon = null, bool? closable = null)
    {
        var option = provider.GetRequiredService<TabItemTextOptions>();
        option.Text = text;
        option.Icon = icon;
        option.IsActive = true;
        option.Closable = closable ?? true;
        navigation.NavigateTo(url);
    }
}
