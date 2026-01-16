// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">可扩展节点接口</para>
/// <para lang="en">Expandable node interface</para>
/// </summary>
public interface IExpandableNode<TItem>
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否展开 默认 false</para>
    /// <para lang="en">Get/Set whether expand default false</para>
    /// </summary>
    public bool IsExpand { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否有子节点 默认 false 用于判断是否有子节点</para>
    /// <para lang="en">Get/Set whether has children default false used to determine whether there are children</para>
    /// </summary>
    public bool HasChildren { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 子节点</para>
    /// <para lang="en">Get/Set children</para>
    /// </summary>
    [DisallowNull]
    [NotNull]
    IEnumerable<IExpandableNode<TItem>>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 TItem 泛型值</para>
    /// <para lang="en">Get/Set TItem generic value</para>
    /// </summary>
    [DisallowNull]
    [NotNull]
    TItem? Value { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 父级节点</para>
    /// <para lang="en">Get/Set parent node</para>
    /// </summary>
    IExpandableNode<TItem>? Parent { get; set; }
}
