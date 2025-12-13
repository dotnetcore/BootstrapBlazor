// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 序列化过滤条件类为 <see cref="IFilterAction"/> 序列化使用
/// </summary>
public sealed class SerializeFilterAction : IFilterAction
{
    /// <summary>
    /// 获得/设置 过滤条件集合
    /// </summary>
    public FilterKeyValueAction? Filter { get; set; }

    /// <summary>
    /// 重置过滤条件方法
    /// </summary>
    public void Reset()
    {
        Filter = new();
    }

    /// <summary>
    /// 设置过滤条件方法
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public Task SetFilterConditionsAsync(FilterKeyValueAction filter)
    {
        Filter = filter;
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获取所有过滤条件集合
    /// </summary>
    /// <returns></returns>
    public FilterKeyValueAction GetFilterConditions() => Filter ?? new();
}
