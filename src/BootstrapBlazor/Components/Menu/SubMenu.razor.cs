// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
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
        private string? ClassString => CssBuilder.Default("has-leaf")
            .AddClass("active", Item?.IsActive ?? false)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 组件数据源
        /// </summary>
        [Parameter]
        public MenuItem? Item { get; set; }

        /// <summary>
        /// 获得/设置 菜单项点击回调委托
        /// </summary>
        [Parameter]
        public Func<MenuItem, Task> OnClick { get; set; } = _ => Task.CompletedTask;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="className"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private string? GetClassString(string className, MenuItem item) => CssBuilder.Default(className)
            .AddClass("active", item.IsActive && !item.IsDisabled)
            .AddClass("disabled", item.IsDisabled)
            .Build();

    }
}
