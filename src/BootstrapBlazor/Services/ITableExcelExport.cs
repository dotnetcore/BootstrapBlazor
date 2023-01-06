// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public interface ITableExcelExport
{
    /// <summary>
    /// 导出 Excel 方法
    /// </summary>
    /// <param name="items">当前行数据</param>
    /// <param name="cols">当前可见列数据集合</param>
    /// <param name="fileName">文件名</param>
    Task<bool> ExportAsync<TItem>(IEnumerable<TItem> items, IEnumerable<ITableColumn> cols, string? fileName = null) where TItem : class;
}
