// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">节点类基类</para>
///  <para lang="en">Node base class</para>
/// </summary>
public abstract class NodeBase
{
    /// <summary>
    ///  <para lang="zh">获得/设置 是否展开 默认 false</para>
    ///  <para lang="en">Get/Set whether expand default false</para>
    /// </summary>
    public bool IsExpand { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否有子节点 默认 false 用于判断是否有子节点</para>
    ///  <para lang="en">Get/Set whether has children default false used to determine whether there are children</para>
    /// </summary>
    public bool HasChildren { get; set; }
}

/// <summary>
///  <para lang="zh">NodeBase 泛型基类</para>
///  <para lang="en">NodeBase generic base class</para>
/// </summary>
/// <typeparam name="TItem"></typeparam>
public abstract class NodeBase<TItem> : NodeBase
{
    /// <summary>
    ///  <para lang="zh">获得/设置 当前节点值</para>
    ///  <para lang="en">Get/Set current node value</para>
    /// </summary>
    [NotNull]
    public TItem? Value { get; set; }
}
