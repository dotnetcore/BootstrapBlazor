// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Maui.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Concurrent;
using Color = BootstrapBlazor.Components.Color;

namespace BootstrapBlazor.Maui.Components.Pages;

/// <summary>
/// 
/// </summary>
public partial class Users
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? Localizer { get; set; }

    /// <summary>
    /// 获得/设置 分页配置数据源
    /// </summary>
    private static IEnumerable<int> PageItemsSource => new int[] { 10, 20, 40 };

    private static string GetAvatarUrl(int id) => $"images/avatars/150-{id}.jpg";

    private static Color GetProgressColor(int count) => count switch
    {
        >= 0 and < 10 => Color.Secondary,
        >= 10 and < 20 => Color.Danger,
        >= 20 and < 40 => Color.Warning,
        >= 40 and < 50 => Color.Info,
        >= 50 and < 70 => Color.Primary,
        _ => Color.Success
    };

    [NotNull]
    private IEnumerable<Foo>? Items { get; set; }

    private static readonly ConcurrentDictionary<Type, Func<IEnumerable<Foo>, string, SortOrder, IEnumerable<Foo>>> SortLambdaCache = new();

    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
    {
        // 此处代码实战中不可用，仅仅为演示而写防止数据全部被删除
        if (Items == null || !Items.Any())
        {
            Items = Foo.GenerateFoo(Localizer, 23).ToList();
        }

        var items = Items;
        var isSearched = false;
        // 处理高级查询
        if (options.SearchModel is Foo model)
        {
            if (!string.IsNullOrEmpty(model.Name))
            {
                items = items.Where(item => item.Name?.Contains(model.Name, StringComparison.OrdinalIgnoreCase) ?? false);
            }

            if (!string.IsNullOrEmpty(model.Address))
            {
                items = items.Where(item => item.Address?.Contains(model.Address, StringComparison.OrdinalIgnoreCase) ?? false);
            }

            isSearched = !string.IsNullOrEmpty(model.Name) || !string.IsNullOrEmpty(model.Address);
        }

        if (options.Searches.Any())
        {
            // 针对 SearchText 进行模糊查询
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
            // 外部未进行排序，内部自动进行排序处理
            var invoker = SortLambdaCache.GetOrAdd(typeof(Foo), key => LambdaExtensions.GetSortLambda<Foo>().Compile());
            items = invoker(items, options.SortName, options.SortOrder);
            isSorted = true;
        }

        var total = items.Count();

        return Task.FromResult(new QueryData<Foo>()
        {
            Items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList(),
            TotalCount = total,
            IsFiltered = isFiltered,
            IsSorted = isSorted,
            IsSearch = isSearched
        });
    }
}
