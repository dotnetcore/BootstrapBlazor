// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    public partial class Table<TItem>
    {
        /// <summary>
        /// 获得/设置 是否分页 默认为 false
        /// </summary>
        [Parameter] public bool IsPagination { get; set; }

        /// <summary>
        /// 获得/设置 是否显示行号列 默认为 false
        /// </summary>
        [Parameter]
        public bool ShowLineNo { get; set; }

        /// <summary>
        /// 获得/设置 行号列标题文字 默认为 行号
        /// </summary>
        [Parameter]
        public string LineNoText { get; set; } = "行号";

        /// <summary>
        /// 获得/设置 每页显示数据数量的外部数据源
        /// </summary>
        [Parameter]
        public IEnumerable<int> PageItemsSource { get; set; } = new int[] { 20, 50, 100, 200, 500, 1000 };

        /// <summary>
        /// 异步查询回调方法
        /// </summary>
        [Parameter]
        public Func<QueryPageOptions, Task<QueryData<TItem>>>? OnQueryAsync { get; set; }

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
            if (PageItems != pageItems)
            {
                PageIndex = 1;
                PageItems = pageItems;
                await QueryAsync();
            }
        }
    }
}
