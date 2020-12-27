// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Table<TItem>
    {
        /// <summary>
        /// 获得 过滤小图标样式
        /// </summary>
        protected string? GetFilterClassString(string fieldName) => CssBuilder.Default("fa fa-fw fa-filter")
            .AddClass("active", Filters.ContainsKey(fieldName))
            .Build();

        /// <summary>
        /// 获得/设置 表头过滤时回调方法
        /// </summary>
        public Func<Task>? OnFilterAsync { get; private set; }

        /// <summary>
        /// 获得 过滤集合
        /// </summary>
        public Dictionary<string, IFilterAction> Filters { get; } = new Dictionary<string, IFilterAction>();

        /// <summary>
        /// 点击 过滤小图标方法
        /// </summary>
        /// <param name="col"></param>
        protected EventCallback<MouseEventArgs> OnFilterClick(ITableColumn col) => EventCallback.Factory.Create<MouseEventArgs>(this, () =>
        {
            col.Filter?.Show();
        });
    }
}
