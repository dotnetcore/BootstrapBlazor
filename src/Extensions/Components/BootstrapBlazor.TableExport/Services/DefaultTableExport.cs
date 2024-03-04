// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MiniExcelLibs;
using System.Text;

namespace BootstrapBlazor.Components;

/// <summary>
/// 构造函数
/// </summary>
/// <param name="serviceProvider"></param>
class DefaultTableExport(IServiceProvider serviceProvider) : ITableExport
{
    private IServiceProvider ServiceProvider { get; set; } = serviceProvider;

    /// <summary>
    /// 导出 方法
    /// </summary>
    /// <param name="items">导出数据集合</param>
    /// <param name="cols">导出列集合 默认 null 全部导出</param>
    /// <param name="fileName">导出后下载文件名</param>
    /// <returns></returns>
    public Task<bool> ExportAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols = null, string? fileName = null) => InternalExportAsync(items, cols, ExcelType.XLSX, fileName);

    /// <summary>
    /// 导出 Excel 方法
    /// </summary>
    /// <param name="items">导出数据集合</param>
    /// <param name="cols">导出列集合 默认 null 全部导出</param>
    /// <param name="fileName">导出后下载文件名</param>
    /// <returns></returns>
    public Task<bool> ExportExcelAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols = null, string? fileName = null) => InternalExportAsync(items, cols, ExcelType.XLSX, fileName);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="items"></param>
    /// <param name="cols"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public Task<bool> ExportCsvAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, string? fileName = null) => InternalExportAsync(items, cols, ExcelType.CSV, fileName);

    private async Task<bool> InternalExportAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, ExcelType excelType, string? fileName = null)
    {
        var value = new List<Dictionary<string, object?>>();
        cols ??= Utility.GetTableColumns<TModel>();
        foreach (var item in items)
        {
            if (item != null)
            {
                var row = new Dictionary<string, object?>();
                foreach (var pi in cols)
                {
                    var val = await FormatValue(pi, Utility.GetPropertyValue(item, pi.GetFieldName()));
                    row.Add(pi.GetDisplayName(), val);
                }
                value.Add(row);
            }
        }
        using var stream = new MemoryStream();
        await MiniExcel.SaveAsAsync(stream, value, excelType: excelType);

        fileName ??= $"ExportData_{DateTime.Now:yyyyMMddHHmmss}.{GetExtension()}";
        stream.Position = 0;
        var downloadService = ServiceProvider.GetRequiredService<DownloadService>();
        await downloadService.DownloadFromStreamAsync(fileName, stream);
        return true;

        string GetExtension() => excelType == ExcelType.XLSX ? "xlsx" : "csv";
    }

    private static async Task<object?> FormatValue(ITableColumn col, object? value)
    {
        var ret = value;
        if (col.Lookup != null)
        {
            ret = col.Lookup.FirstOrDefault(i => i.Value.Equals(value?.ToString(), col.LookupStringComparison))?.Text;
        }
        if (col.Formatter != null)
        {
            // 格式化回调委托
            ret = await col.Formatter(value);
        }
        else if (!string.IsNullOrEmpty(col.FormatString))
        {
            // 格式化字符串
            ret = Utility.Format(value, col.FormatString);
        }
        else if (col.PropertyType.IsEnum())
        {
            ret = col.PropertyType.ToDescriptionString(value?.ToString());
        }
        else if (value is IEnumerable<object> v)
        {
            ret = string.Join(",", v);
        }
        return ret;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="items"></param>
    /// <param name="cols"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public async Task<bool> ExportPdfAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols, string? fileName = null)
    {
        var ret = false;
        var logger = ServiceProvider.GetRequiredService<ILogger<DefaultTableExport>>();

        try
        {
            // 生成表格
            var html = await GenerateTableHtmlAsync(items, cols);

            // 得到 Pdf 文件数据
            var pdfService = ServiceProvider.GetRequiredService<IExportPdf>();
            var stream = await pdfService.PdfStreamAsync(html);

            // 下载 Pdf 文件
            var downloadService = ServiceProvider.GetRequiredService<DownloadService>();
            fileName ??= $"ExportData_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            await downloadService.DownloadFromStreamAsync(fileName, stream);
            ret = true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "ExportPdfAsync execute failed");
        }
        return ret;
    }

    private static async Task<string> GenerateTableHtmlAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols)
    {
        var builder = new StringBuilder();
        cols ??= Utility.GetTableColumns<TModel>();
        builder.AppendLine("<table class=\"table table-bordered table-striped\">");

        // 生成表头
        builder.AppendLine("<thead><tr>");
        foreach (var pi in cols)
        {
            builder.AppendLine($"<th><div class=\"table-cell\">{pi.GetDisplayName()}</div></th>");
        }
        builder.AppendLine("</tr></thead>");

        builder.AppendLine("<tbody>");
        foreach (var item in items)
        {
            if (item != null)
            {
                builder.AppendLine("<tr>");
                foreach (var pi in cols)
                {
                    var val = await FormatValue(pi, Utility.GetPropertyValue(item, pi.GetFieldName()));
                    builder.AppendLine($"<td><div class=\"table-cell\">{val}</div></td>");
                }
                builder.AppendLine("</tr>");
            }
        }
        builder.AppendLine("</tbody>");
        builder.AppendLine("</table>");
        return builder.ToString();
    }
}
