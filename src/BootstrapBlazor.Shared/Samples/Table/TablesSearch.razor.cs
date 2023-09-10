// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples.Table;

/// <summary>
/// 搜索示例代码
/// </summary>
public partial class TablesSearch
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Shared/Data/Foo.cs
    /// </summary>
    [NotNull]
    private List<Foo>? Items { get; set; }

    private Foo SearchModel { get; set; } = new Foo();

    private static IEnumerable<int> PageItemsSource => new int[] { 4, 10, 20 };

    private IEnumerable<SelectedItem>? SearchItems { get; set; }

    private ITableSearchModel CustomerSearchModel { get; set; } = new FooSearchModel();

    private bool ShowResetButton { get; set; } = true;

    private bool ShowSearchButton { get; set; } = true;

    private bool ShowSearchText { get; set; }

    private SearchMode SearchModeValue { get; set; }

    private bool SearchModeFlag
    {
        get => SearchModeValue == SearchMode.Popup;
        set => SearchModeValue = value ? SearchMode.Popup : SearchMode.Top;
    }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();
        Items = Foo.GenerateFoo(FooLocalizer);
        SearchItems = new List<SelectedItem>()
        {
            new SelectedItem
            {
                Text = Localizer["SelectedItemText"].Value,
                Value = ""
            },
            new SelectedItem
            {
                Text = Localizer["SelectedItemText1"].Value,
                Value = Localizer["SelectedItemValue1"].Value
            },
            new SelectedItem
            {
                Text = Localizer["SelectedItemText2"].Value,
                Value = Localizer["SelectedItemValue2"].Value
            },
        };
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
        if (!string.IsNullOrEmpty(options.SearchText))
        {
            items = items.Where(i => (i.Name?.Contains(options.SearchText, StringComparison.OrdinalIgnoreCase) ?? false) || (i.Address?.Contains(options.SearchText, StringComparison.OrdinalIgnoreCase) ?? false));
        }
        else if (!string.IsNullOrEmpty(SearchModel.Name))
        {
            items = items.Where(i => i.Name == SearchModel.Name);
        }
        else if (!string.IsNullOrEmpty(SearchModel.Address))
        {
            items = items.Where(i => i.Address == SearchModel.Address);
        }

        // 设置记录总数
        var total = items.Count();
        // 内存分页
        items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();
        return Task.FromResult(new QueryData<Foo>() { Items = items, TotalCount = total, IsSorted = true, IsFiltered = true, IsSearch = true, IsAdvanceSearch = true });
    }

    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
    {
        IEnumerable<Foo> items = Items;
        var isAdvanceSearch = false;
        // 处理高级搜索
        if (options.AdvanceSearches.Any())
        {
            items = items.Where(options.AdvanceSearches.GetFilterFunc<Foo>());
            isAdvanceSearch = true;
        }

        // 处理 自定义 高级搜索 CustomerSearchModel 过滤条件
        if (options.CustomerSearches.Any())
        {
            items = items.Where(options.CustomerSearches.GetFilterFunc<Foo>());
            isAdvanceSearch = true;
        }

        // 处理 Searchable=true 列与 SearchText 模糊搜索
        if (options.Searches.Any())
        {
            items = items.Where(options.Searches.GetFilterFunc<Foo>(FilterLogic.Or));
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
        return Task.FromResult(new QueryData<Foo>() { Items = items, TotalCount = total, IsSorted = isSorted, IsFiltered = isFiltered, IsSearch = options.CustomerSearches.Any() || !string.IsNullOrEmpty(options.SearchText), IsAdvanceSearch = isAdvanceSearch });
    }
}
