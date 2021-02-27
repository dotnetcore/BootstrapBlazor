// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages.Table
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class TablesToolbar
    {
        [Inject]
        [NotNull]
        private IStringLocalizer<Foo>? Localizer { get; set; }

        private static IEnumerable<int> PageItemsSource => new int[] { 2, 4, 10, 20 };

        [NotNull]
        private List<Foo>? Items { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Items = Foo.GenerateFoo(Localizer);
        }

        private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
        {
            // 设置记录总数
            var total = Items.Count;

            // 内存分页
            var items = Items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();

            return Task.FromResult(new QueryData<Foo>()
            {
                Items = items,
                TotalCount = total,
                IsSorted = true,
                IsFiltered = true,
                IsSearch = true
            });
        }

        private Task<QueryData<Foo>> OnSearchQueryAsync(QueryPageOptions options)
        {
            var items = Items.AsEnumerable();
            if (!string.IsNullOrEmpty(options.SearchText))
            {
                // 针对 SearchText 进行模糊查询
                items = items.Where(i => (i.Address ?? "").Contains(options.SearchText)
                        || (i.Name ?? "").Contains(options.SearchText));
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
                IsFiltered = true,
                IsSearch = true
            });
        }

        private Task<bool> OnDeleteAsync(IEnumerable<Foo> items)
        {
            items.ToList().ForEach(foo => Items.Remove(foo));
            return Task.FromResult(true);
        }

        private async Task DownloadAsync(IEnumerable<Foo> items)
        {
            var cate = ToastCategory.Information;
            var title = "自定义下载示例";
            var content = "请先选择数据，然后点击下载按钮";
            if (items.Any())
            {
                cate = ToastCategory.Success;
                content = $"开始下载选中的 {items.Count()} 条数据";
            }
            await ToastService.Show(new ToastOption()
            {
                Category = cate,
                Title = title,
                Content = content
            });
        }
    }
}
