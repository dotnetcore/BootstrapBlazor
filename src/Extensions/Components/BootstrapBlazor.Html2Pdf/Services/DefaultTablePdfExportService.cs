// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

class DefaultTablePdfExportService : ITablePdfExport
{
    private IHtml2Pdf PdfService { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="html2Pdf"></param>
    public DefaultTablePdfExportService(IHtml2Pdf html2Pdf)
    {
        PdfService = html2Pdf;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="items"></param>
    /// <param name="cols"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<bool> ExportAsync<TItem>(IEnumerable<TItem> items, IEnumerable<ITableColumn>? cols, string? fileName = null) where TItem : class => PdfService.ExportAsync("<div>Test</div>", fileName);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<bool> ExportAsync(string id, string? fileName = null) => PdfService.ExportByIdAsync(id, fileName);
}
