// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ITable 接口
/// </summary>
public interface ITable : IColumnCollection
{
    /// <summary>
    /// 获得 ITable 实例配置的可见列集合
    /// </summary>
    IEnumerable<ITableColumn> GetVisibleColumns();

    /// <summary>
    /// 获得 过滤条件集合
    /// </summary>
    Dictionary<string, IFilterAction> Filters { get; }

    /// <summary>
    /// 获得 过滤异步回调方法
    /// </summary>
    Func<Task>? OnFilterAsync { get; }
}
