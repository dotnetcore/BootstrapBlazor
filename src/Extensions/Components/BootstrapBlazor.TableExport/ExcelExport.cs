// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.JSInterop;
using OfficeOpenXml;
using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
internal class ExcelExport : ITableExcelExport
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<bool> ExportAsync<TItem>(IEnumerable<TItem> items, IEnumerable<ITableColumn> cols, IJSRuntime jsRuntime) where TItem : class
    {
        using var excelPackage = new ExcelPackage();
        var worksheet = excelPackage.Workbook.Worksheets.Add("sheet1");

        var y = 1;
        foreach (var item in items)
        {
            var x = 1;
            foreach (var pi in item.GetType().GetProperties())
            {
                if (!cols.Any(col => col.GetFieldName() == pi.Name)) continue;

                if (y == 1)
                {
                    if (pi.Name != null)
                    {
                        if (pi.PropertyType == typeof(DateTime) || pi.PropertyType == typeof(DateTime?)
                            || pi.PropertyType == typeof(TimeSpan) || pi.PropertyType == typeof(TimeSpan?))
                        {
                            worksheet.Column(x).Width = 18;
                            worksheet.Column(x).Style.Numberformat.Format = "yyyy/m/d h:mm:ss";
                        }

                        var th_value = cols.FirstOrDefault(x => x.GetFieldName() == pi.Name)?.Text
                            ?? Utility.GetDisplayName(items.First(), pi.Name);
                        worksheet.SetValue(1, x, th_value);
                    }
                }
                var value = await FormatValue(cols.First(col => col.GetFieldName() == pi.Name), pi.GetValue(item, null));
                worksheet.SetValue(y + 1, x, value);
                x++;
            }
            y++;
        }

        var bytes = excelPackage.GetAsByteArray();
        var fileName = DateTime.Now.Ticks;
        var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        var excelName = $"{fileName}.xlsx";
        var bytesBase64 = Convert.ToBase64String(bytes);
        await jsRuntime.InvokeVoidAsync(identifier: "$.generatefile", excelName, bytesBase64, contentType);
        return true;
    }

    private static async Task<string> FormatValue(ITableColumn col, object? value)
    {
        var ret = "";
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
        else if (col.PropertyType.IsDateTime())
        {
            ret = Utility.Format(value, CultureInfo.CurrentUICulture.DateTimeFormat);
        }
        else if (value is IEnumerable<object> v)
        {
            ret = string.Join(",", v);
        }
        else
        {
            ret = value?.ToString() ?? "";
        }
        return ret;
    }
}
