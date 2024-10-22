// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Table 导出数据上下文接口
/// </summary>
/// <typeparam name="TItem"></typeparam>
public interface ITableExportDataContext<out TItem>
{
    /// <summary>
    /// 获得 导出类型 <see cref="TableExportType"/> 枚举类型
    /// </summary>
    TableExportType ExportType { get; }

    /// <summary>
    /// 获得 当前 行数据集合
    /// </summary>
    IEnumerable<TItem> Rows { get; }

    /// <summary>
    /// 获得 当前表格可见列集合
    /// </summary>
    IEnumerable<ITableColumn> Columns { get; }

    /// <summary>
    /// 获得 当前 Table 内置查询条件方法
    /// </summary>
    QueryPageOptions Options { get; }
}
