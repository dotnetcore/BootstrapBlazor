// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Select 组件基类
    /// </summary>
    public abstract class SelectBase<TValue> : ValidateBase<TValue>
    {
        /// <summary>
        /// 当前选择项实例
        /// </summary>
        protected SelectedItem? SelectedItem { get; set; }

        /// <summary>
        /// 获得/设置 按钮颜色
        /// </summary>
        [Parameter]
        public Color Color { get; set; } = Color.None;

        /// <summary>
        /// 获得/设置 绑定数据集
        /// </summary>
        [Parameter]
        [NotNull]
        public IEnumerable<SelectedItem>? Items { get; set; }

        /// <summary>
        /// 获得/设置 选项模板
        /// </summary>
        [Parameter]
        public RenderFragment<SelectedItem>? ItemTemplate { get; set; }

        /// <summary>
        /// SelectedItemChanged 方法
        /// </summary>
        [Parameter]
        public Func<SelectedItem, Task>? OnSelectedItemChanged { get; set; }

        /// <summary>
        /// 更改组件数据源方法
        /// </summary>
        /// <param name="items"></param>
        [Obsolete("更改数据源 Items 参数即可，下一个版本移除此方法")]
        public void SetItems(IEnumerable<SelectedItem> items)
        {
            Items = items;
            StateHasChanged();
        }
    }
}
