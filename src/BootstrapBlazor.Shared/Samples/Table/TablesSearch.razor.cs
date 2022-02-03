// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// Table 组件搜索示例代码
/// </summary>
public sealed partial class TablesSearch
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? Localizer { get; set; }

    private Foo SearchModel { get; set; } = new Foo();

    private static IEnumerable<int> PageItemsSource => new int[] { 4, 10, 20 };

    [NotNull]
    private List<Foo>? Items { get; set; }

    private bool SearchModeFlag
    {
        get
        {
            return SearchModeValue == SearchMode.Popup;
        }
        set
        {
            SearchModeValue = value ? SearchMode.Popup : SearchMode.Top;
        }
    }

    private bool ShowResetButton { get; set; } = true;

    private bool ShowSearchButton { get; set; } = true;

    private bool ShowSearchText { get; set; }

    private SearchMode SearchModeValue { get; set; }

    [NotNull]
    private ITableSearchModel CustomerSearchModel { get; set; } = new FooSearchModel();

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Foo.GenerateFoo(Localizer);
    }

    private static Task<Foo> OnAddAsync() => Task.FromResult(new Foo() { DateTime = DateTime.Now });

    private Task<bool> OnSaveAsync(Foo item, ItemChangedType changedType)
    {
        // 增加数据演示代码
        if (changedType == ItemChangedType.Add)
        {
            item.Id = Items.Max(i => i.Id) + 1;
            Items.Add(item);
        }
        else
        {
            var oldItem = Items.FirstOrDefault(i => i.Id == item.Id);
            if (oldItem != null)
            {
                oldItem.Name = item.Name;
                oldItem.Address = item.Address;
                oldItem.DateTime = item.DateTime;
                oldItem.Count = item.Count;
                oldItem.Complete = item.Complete;
                oldItem.Education = item.Education;
            }
        }
        return Task.FromResult(true);
    }

    private Task<bool> OnDeleteAsync(IEnumerable<Foo> items)
    {
        items.ToList().ForEach(i => Items.Remove(i));
        return Task.FromResult(true);
    }

    private static Task OnResetSearchAsync(Foo item)
    {
        item.Name = "";
        item.Address = "";
        return Task.CompletedTask;
    }

    private Task<QueryData<Foo>> OnSearchModelQueryAsync(QueryPageOptions options)
    {
        // 自定义了 SearchModel
        IEnumerable<Foo> items = Items;

        // 设置记录总数
        var total = items.Count();

        if (!string.IsNullOrEmpty(SearchModel.Name))
        {
            items = items.Where(i => i.Name == SearchModel.Name);
        }

        if (!string.IsNullOrEmpty(SearchModel.Address))
        {
            items = items.Where(i => i.Address == SearchModel.Address);
        }

        // 内存分页
        items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();

        return Task.FromResult(new QueryData<Foo>()
        {
            Items = items,
            TotalCount = total,
            IsSorted = true,
            IsFiltered = true,
            IsSearch = true,
            IsAdvanceSearch = true
        });
    }


    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
    {
        IEnumerable<Foo> items = Items;

        var isAdvanceSearch = false;
        // 处理高级搜索
        if (options.AdvanceSearchs.Any())
        {
            items = items.Where(options.AdvanceSearchs.GetFilterFunc<Foo>());
            isAdvanceSearch = true;
        }

        // 处理 自定义 高级搜索 CustomerSearchModel 过滤条件
        if (options.CustomerSearchs.Any())
        {
            items = items.Where(options.CustomerSearchs.GetFilterFunc<Foo>());
            isAdvanceSearch = true;
        }

        // 处理 Searchable=true 列与 SeachText 模糊搜索
        if (options.Searchs.Any())
        {
            items = items.Where(options.Searchs.GetFilterFunc<Foo>(FilterLogic.Or));
        }

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

        return Task.FromResult(new QueryData<Foo>()
        {
            Items = items,
            TotalCount = total,
            IsSorted = isSorted,
            IsFiltered = isFiltered,
            IsSearch = options.CustomerSearchs.Any(),
            IsAdvanceSearch = isAdvanceSearch
        });
    }
}
