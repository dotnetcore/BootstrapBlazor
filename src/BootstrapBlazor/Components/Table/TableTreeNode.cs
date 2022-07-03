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
    public IEnumerable<TableTreeNode<TItem>> Items { get; set; } = Enumerable.Empty<TableTreeNode<TItem>>();

    /// <summary>
    /// 获得/设置 是否展开 默认 false
    /// </summary>
    /// <remarks>设置 值为 true 时，并且 <see cref="Items"/> 不为空时首次加载自动展开</remarks>
    public bool IsExpand { get; set; }

    /// <summary>
    /// 获得/设置 是否有子节点 默认 false 用于控制是否显示行首小箭头
    /// </summary>
    /// <remarks>有子节点时如果 <see cref="Items"/> 为空时，展开行时会回调 <see cref="Table{TItem}.OnTreeExpand"/> 方法</remarks>
    public bool HasChildren { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    public TableTreeNode(TItem item)
    {
        Value = item;
    }
}
