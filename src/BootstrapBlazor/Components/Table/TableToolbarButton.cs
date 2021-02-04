// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 表格 Toolbar 按钮组件
    /// </summary>
    public class TableToolbarButton<TItem> : BootstrapComponentBase, IToolbarButton<TItem>
    {
        /// <summary>
        /// 获得/设置 按钮颜色
        /// </summary>
        [Parameter]
        public Color Color { get; set; } = Color.Primary;

        /// <summary>
        /// 获得/设置 显示图标
        /// </summary>
        [Parameter]
        public string? Icon { get; set; }

        /// <summary>
        /// 获得/设置 显示文字
        /// </summary>
        [Parameter]
        public string? Text { get; set; }

        /// <summary>
        /// 获得/设置 是否禁用
        /// </summary>
        [Parameter]
        public bool IsDisabled { get; set; }

        /// <summary>
        /// 获得/设置 按钮点击后回调委托
        /// </summary>
        [Parameter]
        public Func<IEnumerable<TItem>, Task>? OnClickCallback { get; set; }

        /// <summary>
        /// 获得/设置 按钮点击后回调委托
        /// </summary>
        [Parameter]
        public Func<Task>? OnClick { get; set; }

        /// <summary>
        /// 获得/设置 Table Toolbar 实例
        /// </summary>
        [CascadingParameter]
        protected TableToolbar<TItem>? Toolbar { get; set; }

        /// <summary>
        /// 组件初始化方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Toolbar?.AddButton(this);
        }
    }
}
