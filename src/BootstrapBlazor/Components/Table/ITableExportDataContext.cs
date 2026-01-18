// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Table 导出数据上下文接口</para>
/// <para lang="en">Table export data context interface</para>
/// </summary>
/// <typeparam name="TItem"></typeparam>
public interface ITableExportDataContext<out TItem>
{
    /// <summary>
    /// <para lang="zh">获得 导出类型 TableExportType 枚举类型</para>
    /// <para lang="en">Gets export type TableExportType enumeration type</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    TableExportType ExportType { get; }

    /// <summary>
    /// <para lang="zh">获得 当前行数据集合</para>
    /// <para lang="en">Gets current row data collection</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    IEnumerable<TItem> Rows { get; }

    /// <summary>
    /// <para lang="zh">获得 当前表格可见列集合</para>
    /// <para lang="en">Gets current table visible columns collection</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    IEnumerable<ITableColumn> Columns { get; }

    /// <summary>
    /// <para lang="zh">获得 当前 Table 内置查询条件</para>
    /// <para lang="en">Gets the built-in query condition of current Table</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    QueryPageOptions Options { get; }
}
