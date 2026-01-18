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
    /// <para lang="zh">获得/设置 当前节点 Id 默认为 null</para>
    /// <para lang="en">Gets or sets Node Id. Default null</para>
    /// </summary>
    /// <remarks>一般配合数据库使用</remarks>
    public string? Id { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 父级节点 Id 默认为 null</para>
    /// <para lang="en">Gets or sets Parent Node Id. Default null</para>
    /// </summary>
    /// <remarks>一般配合数据库使用</remarks>
    public string? ParentId { get; set; }

    /// <summary>
    /// <para lang="zh">获得 父级菜单 默认为 null</para>
    /// <para lang="en">Get Parent Menu. Default null</para>
    /// </summary>
    public RibbonTabItem? Parent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 导航菜单链接地址</para>
    /// <para lang="en">Gets or sets Navigation URL</para>
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 A 标签 target 参数 默认 null</para>
    /// <para lang="en">Gets or sets Target parameter for A tag. Default null</para>
    /// </summary>
    public string? Target { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图片路径</para>
    /// <para lang="en">Gets or sets Image URL</para>
    /// </summary>
    public string? ImageUrl { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 分组名称</para>
    /// <para lang="en">Gets or sets Group Name</para>
    /// </summary>
    public string? GroupName { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 按钮标识</para>
    /// <para lang="en">Gets or sets Button Identifier</para>
    /// </summary>
    public string? Command { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示文字</para>
    /// <para lang="en">Gets or sets Display Text</para>
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图标</para>
    /// <para lang="en">Gets or sets Icon</para>
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义样式名</para>
    /// <para lang="en">Gets or sets Custom CSS Class</para>
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否收缩 默认 true 收缩</para>
    /// <para lang="en">Gets or sets Whether collapsed. Default true</para>
    /// </summary>
    public bool IsCollapsed { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 是否被禁用 默认 false</para>
    /// <para lang="en">Gets or sets Whether disabled. Default false</para>
    /// </summary>
    public bool IsDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为默认按钮 默认 false</para>
    /// <para lang="en">Gets or sets Whether Default Button. Default false</para>
    /// </summary>
    public bool IsDefault { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否选中当前节点 默认 false</para>
    /// <para lang="en">Gets or sets Whether current node is selected. Default false</para>
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 组件数据源</para>
    /// <para lang="en">Gets or sets Component Items</para>
    /// </summary>
    public List<RibbonTabItem> Items { get; } = [];

    /// <summary>
    /// <para lang="zh">获得/设置 子组件模板 默认为 null</para>
    /// <para lang="en">Gets or sets Child Template. Default null</para>
    /// </summary>
    public RenderFragment? Template { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 动态组件实例</para>
    /// <para lang="en">Gets or sets Dynamic Component Instance</para>
    /// </summary>
    public BootstrapDynamicComponent? Component { get; set; }
}
