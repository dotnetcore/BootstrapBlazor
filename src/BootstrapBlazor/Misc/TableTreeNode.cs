// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Table 组件树状结构类
/// </summary>
public class TableTreeNode<TItem> : NodeBase<TItem>, IExpandableNode<TItem>
{
    /// <summary>
    /// 获得/设置 子节点集合
    /// </summary>
    [DisallowNull]
    [NotNull]
    public IEnumerable<TableTreeNode<TItem>>? Items { get; set; } = Enumerable.Empty<TableTreeNode<TItem>>();

    /// <summary>
    /// 获得/设置 子节点集合
    /// </summary>
    IEnumerable<IExpandableNode<TItem>> IExpandableNode<TItem>.Items { get => Items; set => Items = value.OfType<TableTreeNode<TItem>>(); }

    /// <summary>
    /// 获得/设置 父级节点
    /// </summary>
    public TableTreeNode<TItem>? Parent { get; set; }

    /// <summary>
    /// 获得/设置 父级节点
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
    /// 构造函数
    /// </summary>
    public TableTreeNode([DisallowNull] TItem item)
    {
        Value = item;
    }
}
