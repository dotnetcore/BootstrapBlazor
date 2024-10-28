// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Table 组件 Pdf 导出接口
/// </summary>
[Obsolete("已过期，统一使用 ITableExport 接口")]
public interface ITablePdfExport
{
    /// <summary>
    /// 导出 Pdf 方法
    /// </summary>
    /// <param name="items">导出数据集合</param>
    /// <param name="cols">当前可见列数据集合 默认 null 导出全部列</param>
    /// <param name="fileName">文件名 默认 null ExportData_{DateTime.Now:yyyyMMddHHmmss}.pdf</param>
    Task<bool> ExportAsync<TItem>(IEnumerable<TItem> items, IEnumerable<ITableColumn>? cols, string? fileName = null) where TItem : class;
}
