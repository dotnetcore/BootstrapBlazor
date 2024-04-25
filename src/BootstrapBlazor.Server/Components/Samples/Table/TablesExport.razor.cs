// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text;

namespace BootstrapBlazor.Server.Components.Samples.Table;

/// <summary>
/// 导出示例代码
/// </summary>
public partial class TablesExport
{
    /// <summary>
    /// ToastService 服务实例
    /// </summary>
    [Inject]
    [NotNull]
    private ToastService? Toast { get; set; }

    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Server/Data/Foo.cs
    /// </summary>
    [NotNull]
    private List<Foo>? Items { get; set; }

    private static IEnumerable<int> PageItemsSource => new int[] { 4, 10, 20 };

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

    private async Task<bool> OnExportAsync(ITableExportDataContext<Foo> context)
    {
        // 自定义导出方法
        // 通过 context 参数可以自己查询数据进行导出操作
        // 本例使用 context 传递来的 Rows/Columns 自定义文件名为 Test.xlsx
        var ret = await TableExport.ExportExcelAsync(context.Rows, context.Columns, "Test.xlsx");

        // 返回 true 时自动弹出提示框
        return ret;
    }

    [Inject]
    [NotNull]
    private ITableExport? TableExport { get; set; }

    [Inject]
    [NotNull]
    private ClipboardService? ClipboardService { get; init; }

    private async Task ClipBoardExportAsync(ITableExportContext<Foo> context)
    {
        // 自定义导出当前页面数据到剪切板,可以直接粘贴到 Excel 中
        // 使用 BootstrapBlazor 内置服务 ClipboardService 实例方法 Copy 进行导出操作
        // 导出数据使用 context 传递来的 Rows/Columns 即为当前页数据
        await ExportToClipBoard(context.Columns, context.Rows);

        // 返回 true 时自动弹出提示框
        await ShowToast(true);
    }

    private async Task ClipBoardExportAllAsync(ITableExportContext<Foo> context)
    {
        // 自定义导出当前页面数据到剪切板,可以直接粘贴到 Excel 中
        // 使用 BootstrapBlazor 内置服务 ClipboardService 实例方法 Copy 进行导出操作
        // 通过 context 参数的查询条件
        var option = context.BuildQueryPageOptions();

        // 通过内置扩展方法 GetFilterFunc 过滤数据
        // EFCore 可使用 GetFilterLambda 获得表达式直接给 Where 方法使用
        var data = Items.Where(option.ToFilterFunc<Foo>());

        // 导出符合条件的所有数据 data
        await ExportToClipBoard(context.Columns, data);

        // 返回 true 时自动弹出提示框
        await ShowToast(true);
    }

    private async Task ExportToClipBoard(IEnumerable<ITableColumn> columns, IEnumerable<Foo> rows)
    {
        var sb = new StringBuilder();

        // 导出表格 Header
        var titles = columns.Select(x => x.GetDisplayName());
        sb.AppendJoin('\t', titles).AppendLine();

        // 导出表格 Row
        var fieldNames = columns.Select(x => x.GetFieldName());
        foreach (var row in rows)
        {
            var values = fieldNames.Select(x => row.GetType().GetProperty(x)?.GetValue(row)).ToArray();
            sb.AppendJoin('\t', values).AppendLine();
        }

        var result = sb.ToString();
        await ClipboardService.Copy(result);
    }

    private async Task ExcelExportAsync(ITableExportContext<Foo> context)
    {
        // 自定义导出模板导出当前页面数据为 Excel 方法
        // 使用 BootstrapBlazor 内置服务 ITableExcelExport 实例方法 ExportAsync 进行导出操作
        // 导出数据使用 context 传递来的 Rows/Columns 即为当前页数据
        var ret = await TableExport.ExportExcelAsync(context.Rows, context.Columns, $"Test_{DateTime.Now:yyyyMMddHHmmss}.xlsx");

        // 返回 true 时自动弹出提示框
        await ShowToast(ret);
    }

    private async Task ExcelExportAllAsync(ITableExportContext<Foo> context)
    {
        // 自定义导出模板导出当前页面数据为 Excel 方法
        // 使用 BootstrapBlazor 内置服务 ITableExport 实例方法 ExportExcelAsync 进行导出操作
        // 通过 context 参数的查询条件
        var option = context.BuildQueryPageOptions();

        // 通过内置扩展方法 GetFilterFunc 过滤数据
        // EFCore 可使用 GetFilterLambda 获得表达式直接给 Where 方法使用
        var data = Items.Where(option.ToFilterFunc<Foo>());

        // 导出符合条件的所有数据 data
        var ret = await TableExport.ExportExcelAsync(data, context.Columns, $"Test_{DateTime.Now:yyyyMMddHHmmss}.xlsx");

        // 返回 true 时自动弹出提示框
        await ShowToast(ret);
    }

    private async Task CsvExportAsync(ITableExportContext<Foo> context)
    {
        // 自定义导出模板导出当前页面数据为 Csv 方法
        // 使用 BootstrapBlazor 内置服务 ITableExcelExport 实例方法 ExportCsvAsync 进行导出操作
        // 导出数据使用 context 传递来的 Rows/Columns 即为当前页数据
        var ret = await TableExport.ExportCsvAsync(context.Rows, context.Columns, $"Test_{DateTime.Now:yyyyMMddHHmmss}.csv");

        // 返回 true 时自动弹出提示框
        await ShowToast(ret);
    }

    private async Task ShowToast(bool result)
    {
        if (result)
        {
            await Toast.Success(Localizer["TablesExportToastTitle"], Localizer["TablesExportToastSuccessContent"]);
        }
        else
        {
            await Toast.Error(Localizer["TablesExportToastTitle"], Localizer["TablesExportToastFailedContent"]);
        }
    }
}
