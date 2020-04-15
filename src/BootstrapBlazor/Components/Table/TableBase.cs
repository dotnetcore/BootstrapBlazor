using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Table 组件基类
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public abstract partial class TableBase<TItem> : BootstrapComponentBase where TItem : class, new()
    {
        /// <summary>
        /// 获得 wrapper 样式表集合
        /// </summary>
        protected string? WrapperClassName => CssBuilder.Default("table-wrapper")
            .AddClass("table-bordered", IsBordered)
            .Build();

        /// <summary>
        /// 获得 class 样式表集合
        /// </summary>
        protected string? ClassName => CssBuilder.Default("table")
            .AddClass("table-striped", IsStriped)
            .AddClass("table-hover", IsStriped)
            .AddClass("table-fixed", Height.HasValue)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 表头 Model 实例
        /// </summary>
        protected TItem HeaderModel => Items?.FirstOrDefault() ?? new TItem();

        /// <summary>
        /// 获得/设置 TableHeader 实例
        /// </summary>
        [Parameter]
        public RenderFragment<TItem>? HeaderTemplate { get; set; }

        /// <summary>
        /// 获得/设置 RowTemplate 实例
        /// </summary>
        [Parameter]
        public RenderFragment<TItem>? RowTemplate { get; set; }

        /// <summary>
        /// 获得/设置 TableFooter 实例
        /// </summary>
        [Parameter]
        public RenderFragment? TableFooter { get; set; }

        /// <summary>
        /// 获得/设置 数据集合
        /// </summary>
        [Parameter]
        public IEnumerable<TItem>? Items { get; set; }

        /// <summary>
        /// 获得/设置 是否显示表脚 默认为 false
        /// </summary>
        [Parameter]
        public bool ShowFooter { get; set; }

        /// <summary>
        /// 获得/设置 是否斑马线样式
        /// </summary>
        [Parameter] public bool IsStriped { get; set; }

        /// <summary>
        /// 获得/设置 是否带边框样式
        /// </summary>
        [Parameter] public bool IsBordered { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // 初始化每页显示数量
            if (IsPagination)
            {
                PageItems = PageItemsSource?.FirstOrDefault() ?? QueryPageOptions.DefaultPageItems;

                if (Items != null) throw new InvalidOperationException($"Please set {nameof(OnQuery)} instead set {nameof(Items)} property when {nameof(IsPagination)} be set True.");
            }

            // 初始化 EditModel
            if (EditModel == null) EditModel = OnAdd?.Invoke() ?? new TItem();

            // 设置 OnSort 回调方法
            OnSort = new Action<string, SortOrder>((sortName, sortOrder) =>
            {
                (SortName, SortOrder) = (sortName, sortOrder);
                Query();
            });

            // 如果未设置 Items 数据源 自动执行查询方法
            if (Items == null)
            {
                QueryData();
                if (Items == null) Items = new TItem[0];
            }
        }
    }
}
