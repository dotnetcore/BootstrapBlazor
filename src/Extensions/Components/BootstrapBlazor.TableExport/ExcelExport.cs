// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using MiniExcelLibs;

namespace BootstrapBlazor.Components;

class ExcelExport : ITableExcelExport
{
    private IServiceProvider ServiceProvider { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="serviceProvider"></param>
    public ExcelExport(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    /// <summary>
    /// 导出 Excel 方法
    /// </summary>
    /// <param name="items">导出数据集合</param>
    /// <param name="cols">导出列集合 默认 null 全部导出</param>
    /// <param name="fileName">导出后下载文件名</param>
    /// <returns></returns>
    public async Task<bool> ExportAsync<TModel>(IEnumerable<TModel> items, IEnumerable<ITableColumn>? cols = null, string? fileName = null) where TModel : class
    {
        var value = new List<Dictionary<string, object?>>();
        cols ??= Utility.GetTableColumns<TModel>();
        foreach (var item in items)
        {
            var row = new Dictionary<string, object?>();
            foreach (var pi in cols)
            {
                var val = await FormatValue(pi, Utility.GetPropertyValue(item, pi.GetFieldName()));
                row.Add(pi.GetDisplayName(), val);
            }
            value.Add(row);
        }
        using var stream = new MemoryStream();
        await MiniExcel.SaveAsAsync(stream, value);

        fileName ??= $"ExportData_{DateTime.Now:yyyyMMddHHmmss}.xlsx";
        stream.Position = 0;
        var downloadService = ServiceProvider.GetRequiredService<DownloadService>();
        await downloadService.DownloadFromStreamAsync(fileName, stream);
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
