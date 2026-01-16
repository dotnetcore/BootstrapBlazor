// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Table 组件 Pdf 导出接口</para>
/// <para lang="en">Table Component PDF Export Interface</para>
/// </summary>
[Obsolete("已过期，统一使用 ITableExport 接口")]
public interface ITablePdfExport
{
    /// <summary>
    /// <para lang="zh">导出 Pdf 方法</para>
    /// <para lang="en">Export PDF Method</para>
    /// </summary>
    /// <param name="items"><para lang="zh">导出数据集合</para><para lang="en">Export Data Collection</para></param>
    /// <param name="cols"><para lang="zh">当前可见列数据集合 默认 null 导出全部列</para><para lang="en">Current visible column data collection, default null to export all columns</para></param>
    /// <param name="fileName"><para lang="zh">文件名 默认 null ExportData_{DateTime.Now:yyyyMMddHHmmss}.pdf</para><para lang="en">File name, default null ExportData_{DateTime.Now:yyyyMMddHHmmss}.pdf</para></param>
    Task<bool> ExportAsync<TItem>(IEnumerable<TItem> items, IEnumerable<ITableColumn>? cols, string? fileName = null) where TItem : class;
}
