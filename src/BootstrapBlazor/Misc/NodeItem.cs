// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">节点类组件基类</para>
/// <para lang="en">Node component base class</para>
/// </summary>
public abstract class NodeItem
{
    /// <summary>
    /// <para lang="zh">获得/设置 当前节点 Id 默认为 null</para>
    /// <para lang="en">Get/Set current node Id default null</para>
    /// </summary>
    /// <remarks>一般配合数据库使用</remarks>
    public string? Id { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 父级节点 Id 默认为 null</para>
    /// <para lang="en">Get/Set parent node Id default null</para>
    /// </summary>
    /// <remarks>一般配合数据库使用</remarks>
    public string? ParentId { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示文字</para>
    /// <para lang="en">Get/Set display text</para>
    /// </summary>
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 图标</para>
    /// <para lang="en">Get/Set icon</para>
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义样式名</para>
    /// <para lang="en">Get/Set custom css class</para>
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否被禁用 默认 false</para>
    /// <para lang="en">Get/Set whether disabled default false</para>
    /// </summary>
    public bool IsDisabled { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否选中当前节点 默认 false</para>
    /// <para lang="en">Get/Set whether active default false</para>
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否收缩 默认 true 收缩</para>
    /// <para lang="en">Get/Set whether collapsed default true</para>
    /// </summary>
    public bool IsCollapsed { get; set; } = true;

    /// <summary>
    /// <para lang="zh">获得/设置 子组件模板 默认为 null</para>
    /// <para lang="en">Get/Set child component template default null</para>
    /// </summary>
    public RenderFragment? Template { get; set; }
}
