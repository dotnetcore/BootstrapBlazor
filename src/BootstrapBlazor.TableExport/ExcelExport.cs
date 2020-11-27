// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
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

                            var th_value = items.FirstOrDefault()?.GetDisplayName(pi.Name);

                            worksheet.SetValue(1, x, th_value);
                        }
                    }
                    var value = pi.GetValue(item, null);
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
    }
}
