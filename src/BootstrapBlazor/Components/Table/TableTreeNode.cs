// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Table 组件树状结构类
/// </summary>
/// <typeparam name="TItem"></typeparam>
public class TableTreeNode<TItem>
{
    /// <summary>
    /// 获得/设置 当前节点值
    /// </summary>
    public TItem Value { get; }

    /// <summary>
    /// 获得/设置 子节点集合
    /// </summary>
    public List<TableTreeNode<TItem>>? Items { get; set; }

    /// <summary>
    /// 获得/设置 是否展开 默认 false
    /// </summary>
    public bool IsExpand { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    public TableTreeNode(TItem item)
    {
        Value = item;
    }
}
