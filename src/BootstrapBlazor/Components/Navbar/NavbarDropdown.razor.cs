// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">NavbarDropdown 组件用于在导航栏中创建下拉菜单</para>
/// <para lang="en">NavbarDropdown component for creating dropdown menu in navigation bar</para>
/// </summary>
public partial class NavbarDropdown
{
    /// <summary>
    /// <para lang="zh">获得/设置 子组件模板</para>
    /// <para lang="en">Gets or sets the child component template</para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 Dropdown 菜单标题文本</para>
    /// <para lang="en">Gets or sets the dropdown menu title text</para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 菜单对齐方式，默认为 none 未设置</para>
    /// <para lang="en">Gets or sets the menu alignment. Default is none</para>
    /// </summary>
    [Parameter]
    public Alignment MenuAlignment { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 下拉选项方向，默认为 Dropdown 向下</para>
    /// <para lang="en">Gets or sets the dropdown direction. Default is Dropdown</para>
    /// </summary>
    [Parameter]
    public Direction Direction { get; set; }

    private string? ClassString => CssBuilder.Default("nav-item")
        .AddClass(Direction.ToDescriptionString())
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">菜单对齐方式样式</para>
    /// <para lang="en">Menu alignment style</para>
    /// </summary>
    private string? MenuAlignmentClass => CssBuilder.Default("dropdown-menu shadow")
        .AddClass($"dropdown-menu-{MenuAlignment.ToDescriptionString()}", MenuAlignment == Alignment.Right)
        .Build();
}
