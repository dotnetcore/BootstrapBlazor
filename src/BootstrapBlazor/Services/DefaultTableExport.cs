// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

class DefaultTableExport : ITableExport
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public Task<bool> ExportAsync<TItem>(IEnumerable<TItem> items, IEnumerable<ITableColumn>? cols = null, string? fileName = null, TableExportOptions? options = null)
    {
        return Task.FromResult(false);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    public Task<bool> ExportExcelAsync<TItem>(IEnumerable<TItem> items, IEnumerable<ITableColumn>? cols = null, string? fileName = null, TableExportOptions? options = null)
    {
        return Task.FromResult(false);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="items"></param>
    /// <param name="cols"></param>
    /// <param name="fileName"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public Task<bool> ExportCsvAsync<TItem>(IEnumerable<TItem> items, IEnumerable<ITableColumn>? cols, string? fileName = null, TableExportOptions? options = null)
    {
        return Task.FromResult(false);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="items"></param>
    /// <param name="cols"></param>
    /// <param name="fileName"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public Task<bool> ExportPdfAsync<TItem>(IEnumerable<TItem> items, IEnumerable<ITableColumn>? cols, string? fileName = null, TableExportOptions? options = null)
    {
        return Task.FromResult(false);
    }
}
