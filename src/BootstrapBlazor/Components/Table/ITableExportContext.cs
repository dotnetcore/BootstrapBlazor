// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Table 导出数据上下文接口</para>
///  <para lang="en">Table 导出data上下文接口</para>
/// </summary>
/// <typeparam name="TItem"></typeparam>
public interface ITableExportContext<TItem>
{
    /// <summary>
    ///  <para lang="zh">获得 Table 实例所有列集合</para>
    ///  <para lang="en">Gets Table instance所有列collection</para>
    /// </summary>
    IEnumerable<ITableColumn> Columns { get; }

    /// <summary>
    ///  <para lang="zh">获得 当前 行数据集合</para>
    ///  <para lang="en">Gets 当前 行datacollection</para>
    /// </summary>
    IEnumerable<TItem> Rows { get; }

    /// <summary>
    ///  <para lang="zh">获得 当前 Table 内置查询条件方法</para>
    ///  <para lang="en">Gets 当前 Table 内置查询条件方法</para>
    /// </summary>
    /// <returns></returns>
    QueryPageOptions BuildQueryPageOptions();

    /// <summary>
    ///  <para lang="zh">获得 当前 Table 内置 ExportAsync 方法</para>
    ///  <para lang="en">Gets 当前 Table 内置 ExportAsync 方法</para>
    /// </summary>
    Task ExportAsync();

    /// <summary>
    ///  <para lang="zh">获得 当前 Table 显示列集合</para>
    ///  <para lang="en">Gets 当前 Table display列collection</para>
    /// </summary>
    /// <returns></returns>
    IEnumerable<ITableColumn> GetVisibleColumns();
}
