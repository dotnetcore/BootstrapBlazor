// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 设置列宽示例代码
/// </summary>
public partial class TablesColumnResizing
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
        5,
        10,
        20
    };

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        Items = Foo.GenerateFoo(FooLocalizer);
    }

    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
    {
        IEnumerable<Foo> items = Items;
        // 过滤
        var isFiltered = false;
        if (options.Filters.Any())
        {
            items = items.Where(options.Filters.GetFilterFunc<Foo>());
            isFiltered = true;
        }

        // 排序
        var isSorted = false;
        if (!string.IsNullOrEmpty(options.SortName))
        {
            var invoker = Foo.GetNameSortFunc();
            items = invoker(items, options.SortName, options.SortOrder);
            isSorted = true;
        }

        // 设置记录总数
        var total = items.Count();
        // 内存分页
        items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();
        return Task.FromResult(new QueryData<Foo>() { Items = items, TotalCount = total, IsSorted = isSorted, IsFiltered = isFiltered, IsSearch = true });
    }

    /// <summary>
    /// CustomerButton
    /// </summary>
    /// <param name = "items"></param>
    private async Task CustomerButton(IEnumerable<Foo> items)
    {
        var cate = ToastCategory.Information;
        var title = Localizer["CustomerButtonTitle"];
        var content = Localizer["CustomerButtonContent", items.Count()];
        await ToastService.Show(new ToastOption() { Category = cate, Title = title, Content = content });
    }

    [NotNull]
    private Table<Foo>? TableRows { get; set; }

    private async Task OnRowButtonClick(Foo item, string text)
    {
        var cate = ToastCategory.Success;
        var title = $"{text} {item.Name}";
        var content = Localizer["OnRowButtonClickContent"];
        await ToastService.Show(new ToastOption() { Category = cate, Title = title, Content = content });
        await TableRows.QueryAsync();
    }
}
