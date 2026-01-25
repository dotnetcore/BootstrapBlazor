// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IFilterAction 接口</para>
/// <para lang="en">IFilterAction interface</para>
/// </summary>
public interface IFilterAction
{
    /// <summary>
    /// <para lang="zh">重置过滤条件</para>
    /// <para lang="en">Resets filter conditions</para>
    /// </summary>
    void Reset();

    /// <summary>
    /// <para lang="zh">获得 过滤条件集合</para>
    /// <para lang="en">Gets filter condition collection</para>
    /// </summary>
    FilterKeyValueAction GetFilterConditions();

    /// <summary>
    /// <para lang="zh">设置现有过滤条件</para>
    /// <para lang="en">Sets existing filter conditions</para>
    /// </summary>
    Task SetFilterConditionsAsync(FilterKeyValueAction conditions);
}
