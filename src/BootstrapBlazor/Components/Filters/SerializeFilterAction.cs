// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">序列化过滤条件类用于 <see cref="IFilterAction"/> 序列化</para>
/// <para lang="en">Serialize filter condition class for <see cref="IFilterAction"/> serialization</para>
/// </summary>
public sealed class SerializeFilterAction : IFilterAction
{
    /// <summary>
    /// <para lang="zh">获得/设置 过滤条件集合</para>
    /// <para lang="en">Gets or sets filter condition collection</para>
    /// </summary>
    public FilterKeyValueAction? Filter { get; set; }

    /// <summary>
    /// <para lang="zh">重置过滤条件方法</para>
    /// <para lang="en">Resets filter conditions</para>
    /// </summary>
    public void Reset()
    {
        Filter = new();
    }

    /// <summary>
    /// <para lang="zh">设置过滤条件方法</para>
    /// <para lang="en">Sets filter conditions</para>
    /// </summary>
    /// <param name="filter"></param>
    public Task SetFilterConditionsAsync(FilterKeyValueAction filter)
    {
        Filter = filter;
        return Task.CompletedTask;
    }

    /// <summary>
    /// <para lang="zh">获得 所有过滤条件集合</para>
    /// <para lang="en">Gets all filter conditions</para>
    /// </summary>
    public FilterKeyValueAction GetFilterConditions() => Filter ?? new();
}
