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
public interface ITableExportContext<TItem>
{
    /// <summary>
    /// <para lang="zh">获得 Table 实例所有列集合</para>
    /// <para lang="en">Gets all columns collection of Table instance</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    IEnumerable<ITableColumn> Columns { get; }

    /// <summary>
    /// <para lang="zh">获得 当前行数据集合</para>
    /// <para lang="en">Gets current row data collection</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    IEnumerable<TItem> Rows { get; }

    /// <summary>
    /// <para lang="zh">获得 当前 Table 内置查询条件方法</para>
    /// <para lang="en">Gets the built-in query condition builder method of current Table</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    QueryPageOptions BuildQueryPageOptions();

    /// <summary>
    /// <para lang="zh">获得 当前 Table 内置 ExportAsync 方法</para>
    /// <para lang="en">Gets the built-in ExportAsync method of current Table</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    Task ExportAsync();

    /// <summary>
    /// <para lang="zh">获得 当前 Table 显示列集合</para>
    /// <para lang="en">Gets display columns collection of current Table</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    IEnumerable<ITableColumn> GetVisibleColumns();
}
