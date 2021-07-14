// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages.Table
{
    /// <summary>
    /// Table 组件列属性示例代码
    /// </summary>
    public partial class TablesColumn
    {
        private static readonly ConcurrentDictionary<Type, Func<IEnumerable<Foo>, string, SortOrder, IEnumerable<Foo>>> SortLambdaCache = new();

        [NotNull]
        private List<Foo>? Items { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Foo>? Localizer { get; set; }

        [Inject]
        [NotNull]
        private ToastService? ToastService { get; set; }

        private static IEnumerable<int> PageItemsSource => new int[] { 4, 10, 20 };

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Items = Foo.GenerateFoo(Localizer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static Task<string> IntFormatter(object? d)
        {
            var ret = "";
            var data = d as TableColumnContext<Foo, object?>;
            if (data != null && data.Value != null)
            {
                var val = (int)data.Value;
                ret = val.ToString("0.00");
            }
            return Task.FromResult(ret);
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
                var invoker = SortLambdaCache.GetOrAdd(typeof(Foo), key => LambdaExtensions.GetSortLambda<Foo>().Compile());
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
                IsSearch = true
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        private async Task CustomerButton(IEnumerable<Foo> items)
        {
            var cate = ToastCategory.Information;
            var title = "自定义按钮处理方法";
            var content = $"通过不同的函数区分按钮处理逻辑，参数 Items 为 Table 组件中选中的行数据集合，当前选择数据 {items.Count()} 条";
            await ToastService.Show(new ToastOption()
            {
                Category = cate,
                Title = title,
                Content = content
            });
        }

        private List<Foo> SelectedRows { get; set; } = new();

        [NotNull]
        private Table<Foo>? TableRows { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        private async Task OnRowButtonClick(Foo item)
        {
            var cate = ToastCategory.Success;
            var title = "行内按钮处理方法";
            var content = "通过不同的函数区分按钮处理逻辑，参数 Item 为当前行数据";
            await ToastService.Show(new ToastOption()
            {
                Category = cate,
                Title = title,
                Content = content
            });

            SelectedRows.Clear();
            SelectedRows.Add(item);
            await TableRows.QueryAsync();
        }
    }
}
