// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 搜索组件上下文类 
/// </summary>
public class SearchContext<TValue>(Search<TValue> search, Func<Task> onSearchAsync, Func<Task> onClearAsync)
{
    /// <summary>
    /// 获得/设置 搜索组件实例
    /// </summary>
    public Search<TValue> Search { get; } = search;

    /// <summary>
    /// 获得/设置 清空按钮回调方法
    /// </summary>
    public Func<Task> OnClearAsync { get; } = onClearAsync;

    /// <summary>
    /// 获得/设置 搜索按钮回调方法
    /// </summary>
    public Func<Task> OnSearchAsync { get; } = onSearchAsync;
}
