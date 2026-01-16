// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">搜索组件上下文类</para>
/// <para lang="en">Search Component Context Class</para>
/// </summary>
public class SearchContext<TValue>(Search<TValue> search, Func<Task> onSearchAsync, Func<Task> onClearAsync)
{
    /// <summary>
    /// <para lang="zh">获得/设置 搜索组件实例</para>
    /// <para lang="en">Get/Set Search Component Instance</para>
    /// </summary>
    public Search<TValue> Search { get; } = search;

    /// <summary>
    /// <para lang="zh">获得/设置 清空按钮回调方法</para>
    /// <para lang="en">Get/Set Clear Button Callback Method</para>
    /// </summary>
    public Func<Task> OnClearAsync { get; } = onClearAsync;

    /// <summary>
    /// <para lang="zh">获得/设置 搜索按钮回调方法</para>
    /// <para lang="en">Get/Set Search Button Callback Method</para>
    /// </summary>
    public Func<Task> OnSearchAsync { get; } = onSearchAsync;
}
