// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// NavbarDropdownItem 组件用于在导航栏下拉菜单中创建菜单项
/// </summary>
public partial class NavbarDropdownItem
{
    /// <summary>
    /// 获得/设置 子组件模板
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 菜单项文本
    /// </summary>
    [Parameter]
    public string? Url { get; set; }

    /// <summary>
    /// 获得/设置 A 标签 target 参数 默认 null
    /// </summary>
    [Parameter]
    public string? Target { get; set; }
}
