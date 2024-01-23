// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
