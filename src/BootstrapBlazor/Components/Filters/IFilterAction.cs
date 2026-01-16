// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IFilterAction 接口</para>
/// <para lang="en">IFilterAction Interface</para>
/// </summary>
public interface IFilterAction
{
    /// <summary>
    /// <para lang="zh">重置过滤条件方法</para>
    /// <para lang="en">Reset Filter Conditions Method</para>
    /// </summary>
    void Reset();

    /// <summary>
    /// <para lang="zh">获得 IFilter 实例中的过滤条件集合</para>
    /// <para lang="en">Get Filter Condition Collection in IFilter Instance</para>
    /// </summary>
    /// <returns></returns>
    FilterKeyValueAction GetFilterConditions();

    /// <summary>
    /// Override existing filter conditions
    /// </summary>
    Task SetFilterConditionsAsync(FilterKeyValueAction conditions);
}
