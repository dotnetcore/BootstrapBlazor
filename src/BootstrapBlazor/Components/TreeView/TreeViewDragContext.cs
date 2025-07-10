// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="TreeView{TItem}"/> 组件拖动上下文类
/// </summary>
public class TreeViewDragContext<TItem>(TreeViewItem<TItem> source, TreeViewItem<TItem> target, bool children = false)
{
    /// <summary>
    /// 获得 源节点
    /// </summary>
    public TreeViewItem<TItem> Source => source;

    /// <summary>
    /// 获得 目标节点
    /// </summary>
    public TreeViewItem<TItem> Target => target;

    /// <summary>
    /// 获得 是否未目标节点的子节点
    /// </summary>
    public bool IsChildren => children;
}
