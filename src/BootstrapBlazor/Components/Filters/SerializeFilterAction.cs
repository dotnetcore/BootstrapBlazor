// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">序列化过滤条件类为 <see cref="IFilterAction"/> 序列化使用</para>
/// <para lang="en">Serialize Filter Condition Class for <see cref="IFilterAction"/> Serialization</para>
/// </summary>
public sealed class SerializeFilterAction : IFilterAction
{
    /// <summary>
    /// <para lang="zh">获得/设置 过滤条件集合</para>
    /// <para lang="en">Get/Set Filter Condition Collection</para>
    /// </summary>
    public FilterKeyValueAction? Filter { get; set; }

    /// <summary>
    /// <para lang="zh">重置过滤条件方法</para>
    /// <para lang="en">Reset Filter Conditions Method</para>
    /// </summary>
    public void Reset()
    {
        Filter = new();
    }

    /// <summary>
    /// <para lang="zh">设置过滤条件方法</para>
    /// <para lang="en">Set Filter Conditions Method</para>
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public Task SetFilterConditionsAsync(FilterKeyValueAction filter)
    {
        Filter = filter;
        return Task.CompletedTask;
    }

    /// <summary>
    /// <para lang="zh">获取所有过滤条件集合</para>
    /// <para lang="en">Get All Filter Conditions Collection</para>
    /// </summary>
    /// <returns></returns>
    public FilterKeyValueAction GetFilterConditions() => Filter ?? new();
}
