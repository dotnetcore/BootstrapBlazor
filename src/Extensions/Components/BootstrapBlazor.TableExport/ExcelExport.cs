// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using OfficeOpenXml;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
internal class ExcelExport : ITableExcelExport
{
    private DownloadService DownloadService { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="downloadService"></param>
    public ExcelExport(DownloadService downloadService)
    {
        DownloadService = downloadService;
    }

    /// <summary>
    /// 导出 Excel 方法
    /// </summary>
    /// <returns></returns>
    public async Task<bool> ExportAsync<TItem>(IEnumerable<TItem> items, IEnumerable<ITableColumn>? cols = null, string? fileName = null) where TItem : class
    {
        using var excelPackage = new ExcelPackage();
        var worksheet = excelPackage.Workbook.Worksheets.Add("sheet1");

        var rowIndex = 1;
        cols ??= Utility.GetTableColumnsByType(typeof(TItem));
        foreach (var item in items)
        {
            var colIndex = 1;
            foreach (var pi in cols)
            {
                if (rowIndex == 1)
                {
                    if (pi.PropertyType.IsDateTime())
                    {
                        worksheet.Column(colIndex).Width = 18;
                        worksheet.Column(colIndex).Style.Numberformat.Format = "yyyy/mm/dd hh:mm:ss";
                    }
                    else if (pi.PropertyType.IsTimeSpan())
                    {
                        worksheet.Column(colIndex).Width = 10;
                        worksheet.Column(colIndex).Style.Numberformat.Format = "HH:mm:ss";
                    }

                    var thValue = pi.GetDisplayName();
                    worksheet.SetValue(1, colIndex, thValue);
                }
                var value = await FormatValue(pi, Utility.GetPropertyValue(item, pi.GetFieldName()));
                worksheet.SetValue(rowIndex + 1, colIndex, value);
                colIndex++;
            }
            rowIndex++;
        }

        var bytes = excelPackage.GetAsByteArray();
        fileName ??= $"ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
        await DownloadService.DownloadFromByteArrayAsync(fileName, bytes);
        return true;
    }

    private static async Task<object?> FormatValue(ITableColumn col, object? value)
    {
        var ret = value;
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
}
