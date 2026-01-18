// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TreeNodeBase 抽象基类</para>
/// <para lang="en">TreeNodeBase abstract base class</para>
/// </summary>
public abstract class TreeNodeBase<TItem> : NodeBase<TItem>
{
    /// <summary>
    /// <para lang="zh">获得/设置 显示文字</para>
    /// <para lang="en">Gets or sets display text</para>
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图标</para>
    /// <para lang="en">Gets or sets icon</para>
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 展开节点状态下的 Icon 图标</para>
    /// <para lang="en">Gets or sets Icon when node is expanded</para>
    /// </summary>
    public string? ExpandIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义样式名</para>
    /// <para lang="en">Gets or sets custom css class</para>
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否被禁用 默认 false</para>
    /// <para lang="en">Gets or sets whether disabled default false</para>
    /// </summary>
    public bool IsDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否选中当前节点 默认 false</para>
    /// <para lang="en">Gets or sets whether active default false</para>
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子组件模板 默认为 null</para>
    /// <para lang="en">Gets or sets child component template default null</para>
    /// </summary>
    public RenderFragment<TItem>? Template { get; set; }
}
