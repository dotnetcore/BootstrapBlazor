// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">RibbonTabItem 实体类</para>
/// <para lang="en">RibbonTabItem Entity</para>
/// </summary>
public class RibbonTabItem
{
    /// <summary>
    /// <para lang="zh">获得/设置 当前节点 Id，默认为 null</para>
    /// <para lang="en">Gets or sets the node Id. Default is null</para>
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 父级节点 Id，默认为 null</para>
    /// <para lang="en">Gets or sets the parent node Id. Default is null</para>
    /// </summary>
    public string? ParentId { get; set; }

    /// <summary>
    /// <para lang="zh">获得 父级菜单，默认为 null</para>
    /// <para lang="en">Gets the parent menu. Default is null</para>
    /// </summary>
    public RibbonTabItem? Parent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 导航菜单链接地址</para>
    /// <para lang="en">Gets or sets the navigation URL</para>
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 A 标签 target 参数，默认为 null</para>
    /// <para lang="en">Gets or sets the target parameter for A tag. Default is null</para>
    /// </summary>
    public string? Target { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图片路径</para>
    /// <para lang="en">Gets or sets the image URL</para>
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 分组名称</para>
    /// <para lang="en">Gets or sets the group name</para>
    /// </summary>
    public string? GroupName { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮标识</para>
    /// <para lang="en">Gets or sets the button identifier</para>
    /// </summary>
    public string? Command { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示文字</para>
    /// <para lang="en">Gets or sets the display text</para>
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图标</para>
    /// <para lang="en">Gets or sets the icon</para>
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义样式名</para>
    /// <para lang="en">Gets or sets the custom CSS class</para>
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否收缩，默认为 true</para>
    /// <para lang="en">Gets or sets whether collapsed. Default is true</para>
    /// </summary>
    public bool IsCollapsed { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否被禁用，默认为 false</para>
    /// <para lang="en">Gets or sets whether disabled. Default is false</para>
    /// </summary>
    public bool IsDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为默认按钮，默认为 false</para>
    /// <para lang="en">Gets or sets whether is default button. Default is false</para>
    /// </summary>
    public bool IsDefault { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否选中当前节点，默认为 false</para>
    /// <para lang="en">Gets or sets whether current node is selected. Default is false</para>
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件数据源</para>
    /// <para lang="en">Gets or sets the component items</para>
    /// </summary>
    public List<RibbonTabItem> Items { get; } = [];

    /// <summary>
    /// <para lang="zh">获得/设置 子组件模板，默认为 null</para>
    /// <para lang="en">Gets or sets the child template. Default is null</para>
    /// </summary>
    public RenderFragment? Template { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 动态组件实例</para>
    /// <para lang="en">Gets or sets the dynamic component instance</para>
    /// </summary>
    public BootstrapDynamicComponent? Component { get; set; }
}
