// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
