// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Table 组件 Excel 导出接口</para>
/// <para lang="en">Table Component Excel Export Interface</para>
/// </summary>
public interface ITableExport
{
    /// <summary>
    /// <para lang="zh">导出 方法</para>
    /// <para lang="en">Export Method</para>
    /// </summary>
    /// <param name="items"><para lang="zh">导出数据集合</para><para lang="en">Export Data Collection</para></param>
    /// <param name="cols"><para lang="zh">当前可见列数据集合 默认 null 导出全部列</para><para lang="en">Current visible column data collection, default null to export all columns</para></param>
    /// <param name="fileName"><para lang="zh">文件名 默认 null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</para><para lang="en">File name, default null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</para></param>
    Task<bool> ExportAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, string? fileName = null);

    /// <summary>
    /// <para lang="zh">导出 方法</para>
    /// <para lang="en">Export Method</para>
    /// </summary>
    /// <param name="items">导出数据集合</param>
    /// <param name="cols">当前可见列数据集合 默认 null 导出全部列</param>
    /// <param name="options">导出配置实例</param>
    /// <param name="fileName">文件名 默认 null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</param>
    Task<bool> ExportAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, TableExportOptions options, string? fileName = null) => ExportAsync(items, cols, fileName);

    /// <summary>
    /// <para lang="zh">导出 Excel 方法</para>
    /// <para lang="en">Export Excel Method</para>
    /// </summary>
    /// <param name="items">导出数据集合</param>
    /// <param name="cols">当前可见列数据集合 默认 null 导出全部列</param>
    /// <param name="fileName">文件名 默认 null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</param>
    Task<bool> ExportExcelAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, string? fileName = null);

    /// <summary>
    /// <para lang="zh">导出 Excel 方法</para>
    /// <para lang="en">Export Excel Method</para>
    /// </summary>
    /// <param name="items"><para lang="zh">导出数据集合</para><para lang="en">Export Data Collection</para></param>
    /// <param name="cols"><para lang="zh">当前可见列数据集合 默认 null 导出全部列</para><para lang="en">Current visible column data collection, default null to export all columns</para></param>
    /// <param name="options"><para lang="zh">导出配置实例</para><para lang="en">Export Options Instance</para></param>
    /// <param name="fileName"><para lang="zh">文件名 默认 null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</para><para lang="en">File name, default null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</para></param>
    Task<bool> ExportExcelAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, TableExportOptions options, string? fileName = null) => ExportExcelAsync(items, cols, fileName);

    /// <summary>
    /// <para lang="zh">导出 Csv 方法</para>
    /// <para lang="en">Export CSV Method</para>
    /// </summary>
    /// <param name="items">导出数据集合</param>
    /// <param name="cols">当前可见列数据集合 默认 null 导出全部列</param>
    /// <param name="fileName">文件名 默认 null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</param>
    Task<bool> ExportCsvAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, string? fileName = null);

    /// <summary>
    /// <para lang="zh">导出 Csv 方法</para>
    /// <para lang="en">Export CSV Method</para>
    /// </summary>
    /// <param name="items">导出数据集合</param>
    /// <param name="cols">当前可见列数据集合 默认 null 导出全部列</param>
    /// <param name="options">导出配置实例</param>
    /// <param name="fileName">文件名 默认 null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</param>
    Task<bool> ExportCsvAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, TableExportOptions options, string? fileName = null) => ExportCsvAsync(items, cols, fileName);

    /// <summary>
    /// <para lang="zh">导出 Pdf 方法</para>
    /// <para lang="en">Export PDF Method</para>
    /// </summary>
    /// <param name="items">导出数据集合</param>
    /// <param name="cols">当前可见列数据集合 默认 null 导出全部列</param>
    /// <param name="fileName">文件名 默认 null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</param>
    Task<bool> ExportPdfAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, string? fileName = null);

    /// <summary>
    /// <para lang="zh">导出 Pdf 方法</para>
    /// <para lang="en">Export PDF Method</para>
    /// </summary>
    /// <param name="items">导出数据集合</param>
    /// <param name="cols">当前可见列数据集合 默认 null 导出全部列</param>
    /// <param name="options">导出配置实例</param>
    /// <param name="fileName">文件名 默认 null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</param>
    Task<bool> ExportPdfAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, TableExportOptions options, string? fileName = null) => ExportPdfAsync(items, cols, fileName);

    /// <summary>
    /// <para lang="zh">导出 Pdf 方法</para>
    /// <para lang="en">Export PDF Method</para>
    /// </summary>
    /// <param name="items"><para lang="zh">导出数据集合</para><para lang="en">Export Data Collection</para></param>
    /// <param name="cols"><para lang="zh">当前可见列数据集合 默认 null 导出全部列</para><para lang="en">Current visible column data collection, default null to export all columns</para></param>
    /// <param name="options"><para lang="zh">导出配置实例</para><para lang="en">Export Options Instance</para></param>
    /// <param name="fileName"><para lang="zh">文件名 默认 null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</para><para lang="en">File name, default null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</para></param>
    /// <param name="links"><para lang="zh">样式表集合</para><para lang="en">Style Sheet Collection</para></param>
    Task<bool> ExportPdfAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, TableExportOptions options, string? fileName = null, IEnumerable<string>? links = null) => ExportPdfAsync(items, cols, fileName);
}
