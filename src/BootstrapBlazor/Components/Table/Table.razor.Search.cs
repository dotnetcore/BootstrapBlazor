// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    public partial class Table<TItem>
    {
        /// <summary>
        /// 获得 高级搜索样式
        /// </summary>
        protected string? AdvanceSearchClass => CssBuilder.Default("btn btn-secondary")
            .AddClass("btn-info", IsSearch)
            .Build();

        /// <summary>
        /// 获得/设置 是否搜索
        /// </summary>
        protected bool IsSearch { get; set; }

        /// <summary>
        /// 获得/设置 是否数据过滤
        /// </summary>
        protected bool IsFiltered { get; set; }

        /// <summary>
        /// 获得/设置 是否数据排序
        /// </summary>
        protected bool IsSorted { get; set; }

        /// <summary>
        /// 获得/设置 SearchTemplate 实例
        /// </summary>
        [Parameter]
        public RenderFragment<TItem>? SearchTemplate { get; set; }

        /// <summary>
        /// 获得/设置 SearchModel 实例
        /// </summary>
        [Parameter]
        public TItem SearchModel { get; set; } = new TItem();

        /// <summary>
        /// 获得/设置 是否显示搜索框 默认为 false 不显示搜索框
        /// </summary>
        [Parameter]
        public bool ShowSearch { get; set; }

        /// <summary>
        /// 获得/设置 是否显示搜索框 默认为 true 显示搜索文本框  <see cref="ShowSearch" />
        /// </summary>
        [Parameter]
        public bool ShowSearchText { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否显示清空搜索按钮 默认显示 <see cref="ShowSearch" />
        /// </summary>
        [Parameter]
        public bool ShowResetButton { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否显示搜索按钮 默认显示 <see cref="ShowSearch" />
        /// </summary>
        [Parameter]
        public bool ShowSearchButton { get; set; } = true;

        /// <summary>
        /// 获得/设置 是否显示高级搜索按钮 默认显示 <see cref="ShowSearch" />
        /// </summary>
        [Parameter]
        public bool ShowAdvancedSearch { get; set; } = true;

        /// <summary>
        /// 获得/设置 搜索关键字 通过列设置的 Searchable 自动生成搜索拉姆达表达式
        /// </summary>
        [Parameter]
        public string? SearchText { get; set; }

        /// <summary>
        /// 获得/设置 搜索栏渲染方式 默认 Popup 弹窗模式
        /// </summary>
        [Parameter]
        public SearchMode SearchMode { get; set; }

        /// <summary>
        /// 获得/设置 每行显示组件数量 默认为 2
        /// </summary>
        [Parameter]
        public int SearchDialogItemsPerRow { get; set; } = 2;

        /// <summary>
        /// 获得/设置 设置行内组件布局格式 默认 Inline 布局
        /// </summary>
        [Parameter]
        public RowType SearchDialogRowType { get; set; } = RowType.Inline;

        /// <summary>
        /// 获得/设置 设置 <see cref="SearchDialogRowType" /> Inline 模式下标签对齐方式 默认 None 等效于 Left 左对齐
        /// </summary>
        [Parameter]
        public Alignment SearchDialogLabelAlign { get; set; }

        /// <summary>
        /// 重置搜索按钮异步回调方法
        /// </summary>
        [Parameter]
        public Func<TItem, Task>? OnResetSearchAsync { get; set; }

        /// <summary>
        /// 重置查询方法
        /// </summary>
        protected async Task ResetSearchClick()
        {
            await ToggleLoading(true);
            if (OnResetSearchAsync != null)
            {
                await OnResetSearchAsync(SearchModel);
            }
            else if (SearchTemplate == null)
            {
                Utility.Reset(SearchModel);
            }

            PageIndex = 1;
            await QueryAsync();
            await ToggleLoading(false);
        }

        /// <summary>
        /// 查询方法
        /// </summary>
        protected async Task SearchClick()
        {
            PageIndex = 1;
            await QueryAsync();
        }

        /// <summary>
        /// 高级查询按钮点击时调用此方法
        /// </summary>
        protected async Task ShowSearchDialog()
        {
            var option = new SearchDialogOption<TItem>()
            {
                IsScrolling = ScrollingDialogContent,
                Title = SearchModalTitle,
                Model = SearchModel,
                DialogBodyTemplate = SearchTemplate,
                OnResetSearchClick = ResetSearchClick,
                OnSearchClick = SearchClick,
                RowType = SearchDialogRowType,
                ItemsPerRow = SearchDialogItemsPerRow,
                LabelAlign = SearchDialogLabelAlign
            };

            var columns = Columns.Where(i => i.Searchable).ToList();
            columns.ForEach(col => col.EditTemplate = col.SearchTemplate);

            if (columns.Any())
            {
                option.Items = columns;
            }

            await DialogService.ShowSearchDialog(option);
        }

        /// <summary>
        /// 通过列集合中的 Searchable 列与 SearchText 拼装 IFilterAction 集合 
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<IFilterAction> GetSearchs()
        {
            var columns = Columns.Where(col => col.Searchable);

            // 处理 SearchText
            var searchs = new List<InternalSearchAction>();
            if (!string.IsNullOrEmpty(SearchText))
            {
                searchs.AddRange(columns.Where(col => col.PropertyType == typeof(string)).Select(col => new InternalSearchAction() { FieldKey = col.GetFieldName(), Value = SearchText }));
            }
            return searchs;
        }

        /// <summary>
        /// 重置搜索按钮调用此方法
        /// </summary>
        protected async Task ClearSearchClick()
        {
            SearchText = null;
            await ResetSearchClick();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected List<ITableColumn> GetSearchColumns()
        {
            var cols = Columns.Where(c => c.Searchable).ToList();
            cols.ForEach(col => col.EditTemplate = col.SearchTemplate);
            return cols;
        }

        /// <summary>
        /// 客户端 SearchTextbox 文本框内按回车时调用此方法
        /// </summary>
        /// <returns></returns>
        [JSInvokable]
        public async Task OnSearch() => await SearchClick();

        /// <summary>
        /// 客户端 SearchTextbox 文本框内按 ESC 时调用此方法
        /// </summary>
        /// <returns></returns>
        [JSInvokable]
        public async Task OnClearSearch() => await ClearSearchClick();
    }
}
