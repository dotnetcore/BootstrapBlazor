// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Table 组件树状结构类</para>
/// <para lang="en">Table component tree structure class</para>
/// </summary>
public class TableTreeNode<TItem> : NodeBase<TItem>, IExpandableNode<TItem>
{
    /// <summary>
    /// <para lang="zh">获得/设置 子节点集合</para>
    /// <para lang="en">Get/Set child node collection</para>
    /// </summary>
    [DisallowNull]
    [NotNull]
    public IEnumerable<TableTreeNode<TItem>>? Items { get; set; } = Enumerable.Empty<TableTreeNode<TItem>>();

    /// <summary>
    /// <para lang="zh">获得/设置 子节点集合</para>
    /// <para lang="en">Get/Set child node collection</para>
    /// </summary>
    IEnumerable<IExpandableNode<TItem>> IExpandableNode<TItem>.Items { get => Items; set => Items = value.OfType<TableTreeNode<TItem>>(); }

    /// <summary>
    /// <para lang="zh">获得/设置 父级节点</para>
    /// <para lang="en">Get/Set parent node</para>
    /// </summary>
    public TableTreeNode<TItem>? Parent { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 父级节点</para>
    /// <para lang="en">Get/Set parent node</para>
    /// </summary>
    IExpandableNode<TItem>? IExpandableNode<TItem>.Parent
    {
        get => Parent;
        set
        {
            Parent = null;
            if (value is TableTreeNode<TItem> item)
            {
                Parent = item;
            }
        }
    }

    /// <summary>
    /// <para lang="zh">构造函数</para>
    /// <para lang="en">Constructor</para>
    /// </summary>
    public TableTreeNode([DisallowNull] TItem item)
    {
        Value = item;
    }
}
