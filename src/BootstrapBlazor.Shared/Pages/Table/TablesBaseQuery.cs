using BootstrapBlazor.Components;
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
        private static readonly ConcurrentDictionary<Type, Func<IEnumerable<BindItem>, string, SortOrder, IEnumerable<BindItem>>> SortLambdaCache = new ConcurrentDictionary<Type, Func<IEnumerable<BindItem>, string, SortOrder, IEnumerable<BindItem>>>();

        /// <summary>
        /// 
        /// </summary>
        protected BindItem SearchModel { get; set; } = new BindItem();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected Task OnResetSearchAsync(BindItem item)
        {
            item.Name = "";
            item.Address = "";
            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        protected IEnumerable<int> PageItemsSource => new int[] { 2, 4, 10, 20 };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        protected Task<QueryData<BindItem>> OnQueryAsync(QueryPageOptions options) => BindItemQueryAsync(Items, options);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        protected Task<QueryData<BindItem>> BindItemQueryAsync(IEnumerable<BindItem> items, QueryPageOptions options)
        {
            //TODO: 此处代码后期精简
            if (!string.IsNullOrEmpty(SearchModel.Name)) items = items.Where(item => item.Name?.Contains(SearchModel.Name, StringComparison.OrdinalIgnoreCase) ?? false);
            if (!string.IsNullOrEmpty(SearchModel.Address)) items = items.Where(item => item.Address?.Contains(SearchModel.Address, StringComparison.OrdinalIgnoreCase) ?? false);
            if (!string.IsNullOrEmpty(options.SearchText)) items = items.Where(item => (item.Name?.Contains(options.SearchText) ?? false)
                 || (item.Address?.Contains(options.SearchText) ?? false));

            // 过滤
            var isFiltered = false;
            if (options.Filters.Any())
            {
                items = items.Where(options.Filters.GetFilterFunc<BindItem>());

                // 通知内部已经过滤数据了
                isFiltered = true;
            }

            // 排序
            var isSorted = false;
            if (!string.IsNullOrEmpty(options.SortName))
            {
                // 外部未进行排序，内部自动进行排序处理
                var invoker = SortLambdaCache.GetOrAdd(typeof(BindItem), key => items.GetSortLambda().Compile());
                items = invoker(items, options.SortName, options.SortOrder);

                // 通知内部已经过滤数据了
                isSorted = true;
            }

            // 设置记录总数
            var total = items.Count();

            // 内存分页
            items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList();

            return Task.FromResult(new QueryData<BindItem>()
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
