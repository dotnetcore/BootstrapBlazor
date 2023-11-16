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
            new()
            {
                Text = Localizer["SelectedItemText"].Value,
                Value = ""
            },
            new()
            {
                Text = Localizer["SelectedItemText1"].Value,
                Value = Localizer["SelectedItemValue1"].Value
            },
            new()
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
            var oldItem = Items.Find(i => i.Id == item.Id);
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
        // 由于使用 SearchTemplate 自定义处理了。无法使用内置的 ToFilter 获得过滤条件

        // 处理模糊搜索
        IEnumerable<Foo> items = Items;
        if (!string.IsNullOrEmpty(options.SearchText))
        {
            items = items.Where(i => (i.Name?.Contains(options.SearchText, StringComparison.OrdinalIgnoreCase) ?? false) || (i.Address?.Contains(options.SearchText, StringComparison.OrdinalIgnoreCase) ?? false));
        }

        // 处理自定义搜索条件
        if (!string.IsNullOrEmpty(SearchModel.Name))
        {
            items = items.Where(i => i.Name == SearchModel.Name);
        }
        if (!string.IsNullOrEmpty(SearchModel.Address))
        {
            items = items.Where(i => i.Address == SearchModel.Address);
        }

        // 设置记录总数
        var total = items.Count();
        // 内存分页
        items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();
        return Task.FromResult(new QueryData<Foo>()
        {
            Items = items,
            TotalCount = total,
            IsSorted = true,
            IsFiltered = options.Filters.Any(),
            IsSearch = options.Searches.Any(),
            IsAdvanceSearch = options.AdvanceSearches.Any()
        });
    }

    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
    {
        // 使用内置扩展方法 ToFilter 获得过滤条件
        var items = Items.Where(options.ToFilterFunc<Foo>());

        // 使用 Sort 扩展排序方法进行排序
        var isSorted = false;
        if (!string.IsNullOrEmpty(options.SortName))
        {
            items = items.Sort(options.SortName, options.SortOrder);
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
            IsFiltered = options.Filters.Any(),
            IsSearch = options.CustomerSearches.Any() || !string.IsNullOrEmpty(options.SearchText),
            IsAdvanceSearch = options.AdvanceSearches.Any()
        });
    }
}
