// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 导出示例代码
/// </summary>
public partial class TablesExport
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Shared/Data/Foo.cs
    /// </summary>
    [NotNull]
    private List<Foo>? Items { get; set; }
    private static IEnumerable<int> PageItemsSource => new int[]
    {
        4,
        10,
        20
    };

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        Items = Foo.GenerateFoo(LocalizerFoo);
    }

    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
    {
        IEnumerable<Foo> items = Items;

        // 设置记录总数
        var total = items.Count();
        // 内存分页
        items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();
        return Task.FromResult(new QueryData<Foo>() { Items = items, TotalCount = total });
    }

    private Task CsvExportAsync()
    {
        //your code ...

        return ToastService.Success("CSV export", "Export CSV data successfully");
    }

    [Inject]
    [NotNull]
    private ITableExcelExport? Exporter { get; set; }

    private async Task<bool> OnExportAsync(ITableExportDataContext<Foo> context)
    {
        // your code ...
        var ret = await Exporter.ExportAsync(context.Rows, context.Columns, "Test.xlsx");

        // 返回 true 时自动弹出提示框
        return ret;
    }
}
