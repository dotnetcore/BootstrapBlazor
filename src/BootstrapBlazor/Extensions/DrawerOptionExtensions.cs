// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh"><see cref="DrawerOption"/> 扩展方法</para>
/// <para lang="en"><see cref="DrawerOption"/> Extensions</para>
/// </summary>
public static class DrawerOptionExtensions
{
    /// <summary>
    /// <para lang="zh">获得 组件渲染块</para>
    /// <para lang="en">Get component render fragment</para>
    /// </summary>
    /// <param name="drawerOption"></param>
    /// <returns></returns>
    public static RenderFragment? GetContent(this DrawerOption drawerOption) => drawerOption.ChildContent ?? drawerOption.Component?.Render();
}
