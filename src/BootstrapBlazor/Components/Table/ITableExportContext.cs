// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Table 导出数据上下文接口
/// </summary>
/// <typeparam name="TItem"></typeparam>
public interface ITableExportContext<TItem>
{
    /// <summary>
    /// 获得 Table 实例所有列集合
    /// </summary>
    IEnumerable<ITableColumn> Columns { get; }

    /// <summary>
    /// 获得 当前 行数据集合
    /// </summary>
    IEnumerable<TItem> Rows { get; }

    /// <summary>
    /// 获得 当前 Table 内置查询条件方法
    /// </summary>
    /// <returns></returns>
    QueryPageOptions BuildQueryPageOptions();

    /// <summary>
    /// 获得 当前 Table 内置 ExportAsync 方法
    /// </summary>
    Task ExportAsync();

    /// <summary>
    /// 获得 当前 Table 显示列集合
    /// </summary>
    /// <returns></returns>
    IEnumerable<ITableColumn> GetVisibleColumns();
}
