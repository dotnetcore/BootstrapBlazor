// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// IFilterAction 接口
/// </summary>
[JsonDerivedType(typeof(SearchFilterAction))]
public interface IFilterAction
{
    /// <summary>
    /// 获得 IFilter 实例中的过滤条件集合
    /// </summary>
    /// <returns></returns>
    FilterKeyValueAction GetFilterConditions();

    /// <summary>
    /// 重置过滤条件方法
    /// </summary>
    void Reset();

    /// <summary>
    /// Override existing filter conditions
    /// </summary>
    Task SetFilterConditionsAsync(FilterKeyValueAction conditions);
}
