// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public interface IExpandableNode<TItem>
{
    /// <summary>
    /// 获得/设置 是否展开 默认 false
    /// </summary>
    public bool IsExpand { get; set; }

    /// <summary>
    /// 获得/设置 子节点
    /// </summary>
    [DisallowNull]
    [NotNull]
    IEnumerable<IExpandableNode<TItem>>? Items { get; set; }

    /// <summary>
    /// 获得/设置 TItem 泛型值
    /// </summary>
    [DisallowNull]
    [NotNull]
    TItem? Value { get; set; }

    /// <summary>
    /// 获得/设置 父级节点
    /// </summary>
    IExpandableNode<TItem>? Parent { get; set; }
}
