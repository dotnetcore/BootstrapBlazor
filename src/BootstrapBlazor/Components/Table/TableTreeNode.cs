// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TItem"></typeparam>
public class TableTreeNode<TItem> where TItem : class
{
    /// <summary>
    /// 获得/设置 当前节点值
    /// </summary>
    public TItem Value { get; }

    /// <summary>
    /// 获得/设置 当前节点唯一标识
    /// </summary>
    public object? Key { get; }

    /// <summary>
    /// 获得/设置 是否展开
    /// </summary>
    public bool IsExpand { get; set; }

    /// <summary>
    /// 获得/设置 子节点集合
    /// </summary>
    public List<TableTreeNode<TItem>> Children { get; } = new();

    /// <summary>
    /// 获得/设置 是否有子节点
    /// </summary>
    public bool HasChildren { get; set; }

    /// <summary>
    /// 获得/设置 父级节点对象实例
    /// </summary>
    public TableTreeNode<TItem>? Parent { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    public TableTreeNode(TItem item)
    {
        Value = item;
        Key = Utility.GetKeyValue<TItem, object?>(item);
    }
}
