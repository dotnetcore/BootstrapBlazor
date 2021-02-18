// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Pages.Components;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    ///
    /// </summary>
    public abstract class TablesBaseQuery : TablesBase
    {
        private static readonly ConcurrentDictionary<Type, Func<IEnumerable<Foo>, string, SortOrder, IEnumerable<Foo>>> SortLambdaCache = new ConcurrentDictionary<Type, Func<IEnumerable<Foo>, string, SortOrder, IEnumerable<Foo>>>();

        /// <summary>
        ///
        /// </summary>
        protected Foo SearchModel { get; set; } = new Foo();

        /// <summary>
        ///
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected static Task OnResetSearchAsync(Foo item)
        {
            item.Name = "";
            item.Address = "";
            return Task.CompletedTask;
        }

        /// <summary>
        ///
        /// </summary>
        protected static IEnumerable<int> PageItemsSource => new int[] { 2, 4, 10, 20 };

        /// <summary>
        ///
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        protected Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options) => FooQueryAsync(Items, options);

        /// <summary>
        ///
        /// </summary>
        /// <param name="items"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        protected Task<QueryData<Foo>> FooQueryAsync(IEnumerable<Foo> items, QueryPageOptions options)
        {
            if (!string.IsNullOrEmpty(SearchModel.Name)) items = items.Where(item => item.Name?.Contains(SearchModel.Name, StringComparison.OrdinalIgnoreCase) ?? false);
            if (!string.IsNullOrEmpty(SearchModel.Address)) items = items.Where(item => item.Address?.Contains(SearchModel.Address, StringComparison.OrdinalIgnoreCase) ?? false);

            if (options.Searchs.Any())
            {
                // 针对 SearchText 进行模糊查询
                items = items.Where(options.Searchs.GetFilterFunc<Foo>(FilterLogic.Or));
            }
            else
            {
                if (!string.IsNullOrEmpty(options.SearchText))
                    items = items.Where(item => (item.Name?.Contains(options.SearchText) ?? false)
                                 || (item.Address?.Contains(options.SearchText) ?? false));
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
                var invoker = SortLambdaCache.GetOrAdd(typeof(Foo), key => items.GetSortLambda().Compile());
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
                IsSearch = !string.IsNullOrEmpty(SearchModel.Name) || !string.IsNullOrEmpty(SearchModel.Address)
            });
        }
    }
}
