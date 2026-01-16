// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Table 组件 Excel 导出接口</para>
///  <para lang="en">Table Component Excel Export Interface</para>
/// </summary>
[Obsolete("已过期，请使用 ITableExport 代替 Please use ITableExport instead")]
public interface ITableExcelExport
{
    /// <summary>
    ///  <para lang="zh">导出 Excel 方法</para>
    ///  <para lang="en">Export Excel Data Method</para>
    /// </summary>
    /// <param name="items"><para lang="zh">导出数据集合</para><para lang="en">Export Data Collection</para></param>
    /// <param name="cols"><para lang="zh">当前可见列数据集合 默认 null 导出全部列</para><para lang="en">Current visible column data collection, default null to export all columns</para></param>
    /// <param name="fileName"><para lang="zh">文件名 默认 null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</para><para lang="en">File name, default null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</para></param>
    Task<bool> ExportAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, string? fileName = null);

    /// <summary>
    ///  <para lang="zh">导出 Csv 方法</para>
    ///  <para lang="en">Export CSV Data Method</para>
    /// </summary>
    /// <param name="items"><para lang="zh">导出数据集合</para><para lang="en">导出datacollection</para></param>
    /// <param name="cols"><para lang="zh">当前可见列数据集合 默认 null 导出全部列</para><para lang="en">当前可见列datacollection default is null 导出全部列</para></param>
    /// <param name="fileName"><para lang="zh">文件名 默认 null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</para><para lang="en">文件名 default is null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</para></param>
    Task<bool> ExportCsvAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, string? fileName = null);

    /// <summary>
    ///  <para lang="zh">导出 Pdf 方法</para>
    ///  <para lang="en">Export PDF Data Method</para>
    /// </summary>
    /// <param name="items"><para lang="zh">导出数据集合</para><para lang="en">导出datacollection</para></param>
    /// <param name="cols"><para lang="zh">当前可见列数据集合 默认 null 导出全部列</para><para lang="en">当前可见列datacollection default is null 导出全部列</para></param>
    /// <param name="fileName"><para lang="zh">文件名 默认 null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</para><para lang="en">文件名 default is null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</para></param>
    Task<bool> ExportPdfAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, string? fileName = null);
}
