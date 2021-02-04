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
    /// 
    /// </summary>
    public sealed partial class SideMenu
    {
        /// <summary>
        /// 获得 MenuItemLink 样式
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string? GetMenuItemLinkClassString(MenuItem item) => CssBuilder.Default("nav-link show collapse")
            .AddClass("collapsed", !item.IsActive || item.IsCollapsed)
            .Build();

        /// <summary>
        /// 获得 MenuItem 样式
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string? GetMenuItemClassString(MenuItem item) => CssBuilder.Default("collapse-item collapse")
            .AddClass("show", item.IsActive || !item.IsCollapsed)
            .AddClass("collapsed", item.IsCollapsed)
            .Build();

        private string? GetMenuWrapperClassString(MenuItem item) => CssBuilder.Default("card-header-wrapper")
            .AddClass("disabled", item.IsDisabled)
            .Build();

        /// <summary>
        /// 获得 是否展开字符串
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string GetExpandedString(MenuItem item) => item.IsActive || !item.IsCollapsed ? "true" : "false";

        /// <summary>
        /// 获得/设置 是否禁止导航 默认为 false 允许导航
        /// </summary>
        [Parameter]
        public bool DisableNavigation { get; set; }

        /// <summary>
        /// 获得/设置 菜单数据集合
        /// </summary>
        [Parameter]
        public new IEnumerable<MenuItem> Items { get; set; } = new MenuItem[0];

        /// <summary>
        /// 获得/设置 菜单项点击回调委托
        /// </summary>
        [Parameter]
        public Func<MenuItem, Task> OnClick { get; set; } = _ => Task.CompletedTask;
    }
}
