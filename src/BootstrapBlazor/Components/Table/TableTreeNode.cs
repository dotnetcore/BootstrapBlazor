// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TItem"></typeparam>
public class TableTreeNode<TItem> : ITableTreeItem<TItem> where TItem : class
{
    /// <summary>
    /// 获得/设置 当前节点值
    /// </summary>
    public TItem Value { get; }

    /// <summary>
    /// 获得/设置 是否展开
    /// </summary>
    public bool IsExpand { get; set; }

    /// <summary>
    /// 获得/设置 子节点集合
    /// </summary>
    public List<TableTreeNode<TItem>>? Children { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    public TableTreeNode(TItem item)
    {
        Value = item;
    }

    IEnumerable<ITableTreeItem<TItem>>? ITableTreeItem<TItem>.Children => Children;

    /// <inheritdoc/>
    public void SetChildren(IEnumerable<TItem> items)
    {
        Children = items.Select(item => new TableTreeNode<TItem>(item)).ToList();
    }
}
