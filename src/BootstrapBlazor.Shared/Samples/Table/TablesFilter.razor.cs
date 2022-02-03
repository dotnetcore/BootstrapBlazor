// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 过滤示例代码
/// </summary>
public partial class TablesFilter
{
    private static IEnumerable<int> PageItemsSource => new int[] { 4, 10, 20 };

    [NotNull]
    private List<Foo>? Items { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? Localizer { get; set; }

    private string SortString { get; set; } = "DateTime desc, Address";

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Foo.GenerateFoo(Localizer);
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
        if (options.SortName == nameof(Foo.DateTime) && options.SortList != null)
        {
            var sortInvoker = Utility.GetSortListFunc<Foo>();
            items = sortInvoker(items, options.SortList);
            isSorted = true;
        }
        else if (!string.IsNullOrEmpty(options.SortName))
        {
            // 外部未进行排序，内部自动进行排序处理
            var invoker = Utility.GetSortFunc<Foo>();
            items = invoker(items, options.SortName, options.SortOrder);
            isSorted = true;
        }

        // 设置记录总数
        var total = items.Count();

        // 内存分页
        items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();

        return Task.FromResult(new QueryData<Foo>()
        {
            Items = items,
            TotalCount = total,
            IsSorted = isSorted,
            IsFiltered = isFiltered
        });
    }

    private static string OnSort(string sortName, SortOrder sortOrder)
    {
        string sortString = "";
        if (sortName == nameof(Foo.DateTime))
        {
            if (sortOrder == SortOrder.Asc)
            {
                sortString = "DateTime, Count";
            }
            else if (sortOrder == SortOrder.Desc)
            {
                sortString = "DateTime desc, Count desc";
            }
            else
            {
                sortString = "DateTime desc, Count";
            }
        }
        return sortString;
    }
}
