// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

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
    /// 获得/设置 是否有子节点 默认 false 用于判断是否有子节点
    /// </summary>
    public bool HasChildren { get; set; }

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
