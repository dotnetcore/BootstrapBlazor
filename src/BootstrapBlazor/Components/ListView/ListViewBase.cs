// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// ListView 组件基类
    /// </summary>
    public class ListViewBase<TItem> : BootstrapComponentBase where TItem : class, new()
    {
        /// <summary>
        ///  Card组件样式
        /// </summary>
        protected virtual string? ClassName => CssBuilder.Default("listview")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 CardHeard
        /// </summary>
        [Parameter]
        public RenderFragment? HeaderTemplate { get; set; }

        /// <summary>
        /// 获得/设置 CardBody
        /// </summary>
        [Parameter]
        public RenderFragment<TItem>? BodyTemplate { get; set; }

        /// <summary>
        /// 获得/设置 CardFooter
        /// </summary>
        [Parameter]
        public RenderFragment? FooterTemplate { get; set; }

        /// <summary>
        /// 获得/设置 数据源
        /// </summary>
        [Parameter]
        public IEnumerable<TItem>? Items { get; set; }

        /// <summary>
        /// 获得/设置 是否分页 默认为 false 不分页
        /// </summary>
        [Parameter]
        public bool Pageable { get; set; }

        /// <summary>
        /// 获得/设置 每页显示数据数量的外部数据源
        /// </summary>
        [Parameter]
        public IEnumerable<int>? PageItemsSource { get; set; }

        /// <summary>
        /// 获得/设置 分组名称
        /// </summary>
        [Parameter]
        public Func<TItem, object>? GroupName { get; set; }

        /// <summary>
        /// 异步查询回调方法
        /// </summary>
        [Parameter]
        public Func<QueryPageOptions, Task<QueryData<TItem>>>? OnQueryAsync { get; set; }
        /// <summary>
        /// 获得/设置 ListView组件元素点击时回调委托
        /// </summary>
        [Parameter]
        public Func<TItem, Task> OnListViewItemClick { get; set; } = item => Task.CompletedTask;

        /// <summary>
        /// 获得/设置 数据总条目
        /// </summary>
        protected int TotalCount { get; set; }

        /// <summary>
        /// 获得/设置 当前页码
        /// </summary>
        protected int PageIndex { get; set; } = 1;

        /// <summary>
        /// 获得/设置 每页数据数量
        /// </summary>
        protected int PageItems { get; set; } = QueryPageOptions.DefaultPageItems;

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            // 初始化每页显示数量
            if (Pageable)
            {
                PageItems = PageItemsSource?.FirstOrDefault() ?? QueryPageOptions.DefaultPageItems;

                if (Items != null) throw new InvalidOperationException($"Please set {nameof(OnQueryAsync)} instead set {nameof(Items)} property when {nameof(Pageable)} be set True.");
            }

            // 如果未设置 Items 数据源 自动执行查询方法
            if (Items == null)
            {
                await QueryData();
                if (Items == null) Items = new TItem[0];
            }
        }

        /// <summary>
        /// 点击页码调用此方法
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageItems"></param>
        protected async Task OnPageClick(int pageIndex, int pageItems)
        {
            if (pageIndex != PageIndex)
            {
                PageIndex = pageIndex;
                PageItems = pageItems;
                await QueryAsync();
            }
        }

        /// <summary>
        /// 每页记录条数变化是调用此方法
        /// </summary>
        protected async Task OnPageItemsChanged(int pageItems)
        {
            PageIndex = 1;
            PageItems = pageItems;
            await QueryAsync();
        }

        /// <summary>
        /// 查询按钮调用此方法
        /// </summary>
        /// <returns></returns>
        public async Task QueryAsync()
        {
            await QueryData();
            StateHasChanged();
        }

        /// <summary>
        /// 调用 OnQuery 回调方法获得数据源
        /// </summary>
        protected async Task QueryData()
        {
            QueryData<TItem>? queryData = null;
            if (OnQueryAsync != null)
            {
                queryData = await OnQueryAsync(new QueryPageOptions()
                {
                    PageIndex = PageIndex,
                    PageItems = PageItems,
                });
            }
            if (queryData != null)
            {
                Items = queryData.Items;
                TotalCount = queryData.TotalCount;
            }
        }
        /// <summary>
        /// 点击元素事件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected async Task OnClick(TItem item)
        {
            if (OnListViewItemClick != null) await OnListViewItemClick.Invoke(item);
        }
    }
}
