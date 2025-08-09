// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// NavbarDropdown 组件用于在导航栏中创建下拉菜单
/// </summary>
public partial class NavbarDropdown
{
    /// <summary>
    /// 获得/设置 子组件模板
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 Dropdown 菜单标题文本
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 获取菜单对齐方式 默认 none 未设置
    /// </summary>
    [Parameter]
    public Alignment MenuAlignment { get; set; }

    /// <summary>
    /// 获得/设置 下拉选项方向 默认 Dropdown 向下
    /// </summary>
    [Parameter]
    public Direction Direction { get; set; }

    private string? ClassString => CssBuilder.Default("nav-item")
        .AddClass(Direction.ToDescriptionString())
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 菜单对齐方式样式
    /// </summary>
    private string? MenuAlignmentClass => CssBuilder.Default("dropdown-menu shadow")
        .AddClass($"dropdown-menu-{MenuAlignment.ToDescriptionString()}", MenuAlignment == Alignment.Right)
        .Build();
}
