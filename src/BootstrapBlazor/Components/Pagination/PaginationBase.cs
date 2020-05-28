using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Pagination 分页组件
    /// </summary>
    public abstract class PaginationBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 页码总数
        /// </summary>
        public int PageCount => (int)Math.Ceiling(TotalCount * 1.0 / PageItems);

        /// <summary>
        /// 获得 Pagination 样式
        /// </summary>
        /// <returns></returns>
        protected string? PaginationClass => CssBuilder.Default("pagination")
            .AddClass("d-none", PageCount == 1)
            .Build();

        /// <summary>
        /// PaginationBar 样式
        /// </summary>
        /// <returns></returns>
        protected string? PaginationBarClass => CssBuilder.Default("pagination-bar")
            .AddClass("d-none", !ShowPaginationInfo)
            .Build();

        /// <summary>
        /// 获得 PageItems 下拉框显示文字
        /// </summary>
        /// <value></value>
        protected string? PageItemsString => $"{PageItems} 条/页";

        /// <summary>
        /// 获得/设置 开始页码
        /// </summary>
        protected int StartPageIndex => Math.Max(1, Math.Min(PageCount - 4, Math.Max(1, PageIndex - 2)));

        /// <summary>
        /// 获得/设置 结束页码
        /// </summary>
        protected int EndPageIndex => Math.Min(PageCount, Math.Max(5, PageIndex + 2));

        /// <summary>
        /// 获得/设置 数据总数
        /// </summary>
        [Parameter]
        public int TotalCount { get; set; }

        /// <summary>
        /// 获得/设置 当前页码
        /// </summary>
        [Parameter]
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 获得/设置 每页显示数据数量
        /// </summary>
        [Parameter]
        public int PageItems { get; set; }

        /// <summary>
        /// 获得/设置 是否显示分页数据汇总信息 默认为 true 显示
        /// </summary>
        /// <value></value>
        [Parameter] public bool ShowPaginationInfo { get; set; } = true;

        /// <summary>
        /// 获得/设置 每页显示数据数量的外部数据源
        /// </summary>
        /// <value></value>
        [Parameter] public IEnumerable<int>? PageItemsSource { get; set; }

        /// <summary>
        /// 点击页码时回调方法
        /// </summary>
        /// <return>第一个参数是当前页码，第二个参数是当前每页设置显示的数据项数量</return>
        [Parameter]
        public Func<int, int, Task>? OnPageClick { get; set; }

        /// <summary>
        /// 点击设置每页显示数据数量时回调方法
        /// </summary>
        [Parameter]
        public Func<int, Task>? OnPageItemsChanged { get; set; }

        /// <summary>
        /// 上一页方法
        /// </summary>
        protected void MovePrev(int index)
        {
            var pageIndex = PageIndex > 1 ? Math.Max(1, PageIndex - index) : PageCount;
            OnPageItemClick(pageIndex);
        }

        /// <summary>
        /// 下一页方法
        /// </summary>
        protected void MoveNext(int index)
        {
            var pageIndex = PageIndex < PageCount ? Math.Min(PageCount, PageIndex + index) : 1;
            OnPageItemClick(pageIndex);
        }

        /// <summary>
        /// 获得页码设置集合
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<SelectedItem> GetPageItems()
        {
            var pages = PageItemsSource ?? new List<int>() { 20, 40, 80, 100, 200 };
            var ret = new List<SelectedItem>();
            for (int i = 0; i < pages.Count(); i++)
            {
                var item = new SelectedItem(pages.ElementAt(i).ToString(), $"{pages.ElementAt(i)} 条/页");
                ret.Add(item);
                if (pages.ElementAt(i) >= TotalCount) break;
            }
            return ret;
        }

        /// <summary>
        /// 点击页码时回调方法
        /// </summary>
        /// <param name="pageIndex"></param>
        protected void OnPageItemClick(int pageIndex)
        {
            PageIndex = pageIndex;
            OnPageClick?.Invoke(pageIndex, PageItems);
        }

        /// <summary>
        /// 每页显示数据项数量选项更改时回调方法
        /// </summary>
        protected void OnPageItemsSelectItemChanged(SelectedItem item)
        {
            if (int.TryParse(item.Value, out var pageItems))
            {
                PageItems = pageItems;
                PageIndex = 1;
                OnPageItemsChanged?.Invoke(PageItems);
            }
        }
    }
}
