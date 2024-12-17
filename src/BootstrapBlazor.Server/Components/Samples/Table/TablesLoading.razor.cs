﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.Table;

/// <summary>
/// 加载示例代码
/// </summary>
public partial class TablesLoading
{
    private static IEnumerable<int> PageItemsSource => new int[] { 4, 10, 20 };

    [NotNull]
    private IEnumerable<SelectedItem>? Hobbies { get; set; }

    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Server/Data/Foo.cs
    /// </summary>
    [NotNull]
    private List<Foo>? Items { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Hobbies = Foo.GenerateHobbies(LocalizerFoo);
        Items = Foo.GenerateFoo(LocalizerFoo);
    }

    private static async Task<Foo> OnAddAsync()
    {
        // 模拟延时
        await Task.Delay(1000);
        return new Foo() { DateTime = DateTime.Now };
    }

    private static async Task<Foo> OnEditAsync(Foo item)
    {
        // 模拟延时
        await Task.Delay(1000);
        return item;
    }

    private async Task<bool> OnSaveAsync(Foo item, ItemChangedType changedType)
    {
        // 模拟延时
        await Task.Delay(1000);
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
        return true;
    }

    private async Task<bool> OnDeleteAsync(IEnumerable<Foo> items)
    {
        // 模拟延时
        await Task.Delay(1000);
        items.ToList().ForEach(i => Items.Remove(i));
        return true;
    }

    private async Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
    {
        // 模拟延时
        await Task.Delay(1000);

        IEnumerable<Foo> items = Items;

        // 处理高级搜索
        var searchModel = options.SearchModel as Foo;
        var isSearch = false;
        if (!string.IsNullOrEmpty(searchModel?.Name))
        {
            items = items.Where(item => item.Name?.Contains(searchModel.Name, StringComparison.OrdinalIgnoreCase) ?? false);
            isSearch = true;
        }

        if (!string.IsNullOrEmpty(searchModel?.Address))
        {
            items = items.Where(item => item.Address?.Contains(searchModel.Address, StringComparison.OrdinalIgnoreCase) ?? false);
            isSearch = true;
        }

        // 处理 Searchable=true 列与 SearchText 模糊搜索
        if (options.Searches.Any())
        {
            items = items.Where(options.Searches.GetFilterFunc<Foo>(FilterLogic.Or));
        }
        else
        {
            // 处理 SearchText 模糊搜索
            if (!string.IsNullOrEmpty(options.SearchText))
            {
                items = items.Where(item => (item.Name?.Contains(options.SearchText) ?? false)
                             || (item.Address?.Contains(options.SearchText) ?? false));
            }
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

        return new QueryData<Foo>()
        {
            Items = items,
            TotalCount = total,
            IsSorted = isSorted,
            IsFiltered = isFiltered,
            IsSearch = isSearch
        };
    }
}
