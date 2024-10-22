// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 节点类基类
/// </summary>
public abstract class NodeBase
{
    /// <summary>
    /// 获得/设置 是否展开 默认 false
    /// </summary>
    public bool IsExpand { get; set; }

    /// <summary>
    /// 获得/设置 是否有子节点 默认 false 用于判断是否有子节点
    /// </summary>
    public bool HasChildren { get; set; }
}

/// <summary>
/// NodeBase 泛型基类
/// </summary>
/// <typeparam name="TItem"></typeparam>
public abstract class NodeBase<TItem> : NodeBase
{
    /// <summary>
    /// 获得/设置 当前节点值
    /// </summary>
    [NotNull]
    public TItem? Value { get; set; }
}
