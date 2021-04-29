// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class SubMenu
    {
        /// <summary>
        /// 获得 组件样式
        /// </summary>
        private string? ClassString => CssBuilder.Default("has-leaf nav-link")
            .AddClass("active", Item.IsActive)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        private string? GetIconString => string.IsNullOrEmpty(Item.Icon)
            ? (Parent != null && Parent.IsVertical
                ? "fa fa-fw"
                : null)
            : Item.Icon.Contains("fa-fw", StringComparison.OrdinalIgnoreCase)
                ? Item.Icon
                : $"{Item.Icon} fa-fw";

        /// <summary>
        /// 获得/设置 组件数据源
        /// </summary>
        [Parameter]
        [NotNull]
        public MenuItem? Item { get; set; }

        /// <summary>
        /// 获得/设置 菜单项点击回调委托
        /// </summary>
        [Parameter]
        public Func<MenuItem, Task> OnClick { get; set; } = _ => Task.CompletedTask;

        [CascadingParameter]
        private Menu? Parent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private static string? GetClassString(MenuItem item) => CssBuilder.Default()
            .AddClass("active", item.IsActive && !item.IsDisabled)
            .AddClass("disabled", item.IsDisabled)
            .Build();
    }
}
