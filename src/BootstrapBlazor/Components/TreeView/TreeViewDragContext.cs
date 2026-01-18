// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TreeView 组件拖动上下文类</para>
/// <para lang="en">TreeView Component Drag Context Class</para>
/// </summary>
public class TreeViewDragContext<TItem>(TreeViewItem<TItem> source, TreeViewItem<TItem> target, bool children = false)
{
    /// <summary>
    /// <para lang="zh">获得 源节点</para>
    /// <para lang="en">Gets the source node</para>
    /// </summary>
    public TreeViewItem<TItem> Source => source;

    /// <summary>
    /// <para lang="zh">获得 目标节点</para>
    /// <para lang="en">Gets the target node</para>
    /// </summary>
    public TreeViewItem<TItem> Target => target;

    /// <summary>
    /// <para lang="zh">获得 是否为目标节点的子节点</para>
    /// <para lang="en">Gets whether it is a child node of the target node</para>
    /// </summary>
    public bool IsChildren => children;
}
