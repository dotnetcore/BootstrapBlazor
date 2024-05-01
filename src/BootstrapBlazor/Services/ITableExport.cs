// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Table 组件 Excel 导出接口
/// </summary>
public interface ITableExport
{
    /// <summary>
    /// 导出 方法
    /// </summary>
    /// <param name="items">导出数据集合</param>
    /// <param name="cols">当前可见列数据集合 默认 null 导出全部列</param>
    /// <param name="fileName">文件名 默认 null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</param>
    Task<bool> ExportAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, string? fileName = null);

    /// <summary>
    /// 导出 方法
    /// </summary>
    /// <param name="items">导出数据集合</param>
    /// <param name="cols">当前可见列数据集合 默认 null 导出全部列</param>
    /// <param name="options">导出配置实例</param>
    /// <param name="fileName">文件名 默认 null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</param>
    Task<bool> ExportAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, TableExportOptions options, string? fileName = null) => ExportAsync(items, cols, fileName);

    /// <summary>
    /// 导出 Excel 方法
    /// </summary>
    /// <param name="items">导出数据集合</param>
    /// <param name="cols">当前可见列数据集合 默认 null 导出全部列</param>
    /// <param name="fileName">文件名 默认 null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</param>
    Task<bool> ExportExcelAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, string? fileName = null);

    /// <summary>
    /// 导出 Excel 方法
    /// </summary>
    /// <param name="items">导出数据集合</param>
    /// <param name="cols">当前可见列数据集合 默认 null 导出全部列</param>
    /// <param name="options">导出配置实例</param>
    /// <param name="fileName">文件名 默认 null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</param>
    Task<bool> ExportExcelAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, TableExportOptions options, string? fileName = null) => ExportExcelAsync(items, cols, fileName);

    /// <summary>
    /// 导出 Csv 方法
    /// </summary>
    /// <param name="items">导出数据集合</param>
    /// <param name="cols">当前可见列数据集合 默认 null 导出全部列</param>
    /// <param name="fileName">文件名 默认 null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</param>
    Task<bool> ExportCsvAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, string? fileName = null);

    /// <summary>
    /// 导出 Csv 方法
    /// </summary>
    /// <param name="items">导出数据集合</param>
    /// <param name="cols">当前可见列数据集合 默认 null 导出全部列</param>
    /// <param name="options">导出配置实例</param>
    /// <param name="fileName">文件名 默认 null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</param>
    Task<bool> ExportCsvAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, TableExportOptions options, string? fileName = null) => ExportCsvAsync(items, cols, fileName);

    /// <summary>
    /// 导出 Pdf 方法
    /// </summary>
    /// <param name="items">导出数据集合</param>
    /// <param name="cols">当前可见列数据集合 默认 null 导出全部列</param>
    /// <param name="fileName">文件名 默认 null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</param>
    Task<bool> ExportPdfAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, string? fileName = null);

    /// <summary>
    /// 导出 Pdf 方法
    /// </summary>
    /// <param name="items">导出数据集合</param>
    /// <param name="cols">当前可见列数据集合 默认 null 导出全部列</param>
    /// <param name="options">导出配置实例</param>
    /// <param name="fileName">文件名 默认 null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</param>
    Task<bool> ExportPdfAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, TableExportOptions options, string? fileName = null) => ExportPdfAsync(items, cols, fileName);

    /// <summary>
    /// 导出 Pdf 方法
    /// </summary>
    /// <param name="items">导出数据集合</param>
    /// <param name="cols">当前可见列数据集合 默认 null 导出全部列</param>
    /// <param name="options">导出配置实例</param>
    /// <param name="fileName">文件名 默认 null ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx</param>
    /// <param name="links">样式表集合</param>
    Task<bool> ExportPdfAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, TableExportOptions options, string? fileName = null, IEnumerable<string>? links = null) => ExportPdfAsync(items, cols, fileName);
}
