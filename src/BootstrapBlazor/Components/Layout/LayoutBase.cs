// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Layout 组件基类
    /// </summary>
    public abstract class LayoutBase : BootstrapComponentBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected bool IsSmallScreen { get; set; }

        /// <summary>
        /// 获得/设置 侧边栏状态
        /// </summary>
        protected bool IsCollapsed { get; set; }

        /// <summary>
        /// 获得/设置 Header 模板
        /// </summary>
        [Parameter]
        public RenderFragment? Header { get; set; }

        /// <summary>
        /// 获得/设置 Footer 模板
        /// </summary>
        [Parameter]
        public RenderFragment? Footer { get; set; }

        /// <summary>
        /// 获得/设置 Side 模板
        /// </summary>
        [Parameter]
        public RenderFragment? Side { get; set; }

        /// <summary>
        /// 获得/设置 NotAuthorized 模板
        /// </summary>
        [Parameter]
        public RenderFragment? NotAuthorized { get; set; }

        /// <summary>
        /// 获得/设置 NotFound 模板
        /// </summary>
        [Parameter]
        public RenderFragment? NotFound { get; set; }

        /// <summary>
        /// 获得/设置 NotFound 标签文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? NotFoundTabText { get; set; }

        /// <summary>
        /// 获得/设置 Footer 高度 支持百分比 默认宽度为 300px
        /// </summary>
        [Parameter]
        public string SideWidth { get; set; } = "300";

        /// <summary>
        /// 获得/设置 Main 模板
        /// </summary>
        [Parameter]
        [NotNull]
        public RenderFragment? Main { get; set; }

        /// <summary>
        /// 获得/设置 侧边栏是否占满整个左侧 默认为 false
        /// </summary>
        [Parameter]
        public bool IsFullSide { get; set; }

        /// <summary>
        /// 获得/设置 是否为正页面布局 默认为 false
        /// </summary>
        [Parameter]
        public bool IsPage { get; set; }

        /// <summary>
        /// 获得/设置 侧边栏菜单集合
        /// </summary>
        [Parameter]
        public IEnumerable<MenuItem>? Menus { get; set; }

        /// <summary>
        /// 获得/设置 是否右侧使用 Tab 组件 默认为 false 不使用
        /// </summary>
        [Parameter]
        public bool UseTabSet { get; set; }

        /// <summary>
        /// 获得/设置 TabItem 显示文本字典 默认 null 未设置时取侧边栏菜单显示文本
        /// </summary>
        [Parameter]
        public Dictionary<string, string>? TabItemTextDictionary { get; set; }

        /// <summary>
        /// 获得/设置 是否固定 Footer 组件
        /// </summary>
        [Parameter]
        public bool IsFixedFooter { get; set; }

        /// <summary>
        /// 获得/设置 是否固定 Header 组件
        /// </summary>
        [Parameter]
        public bool IsFixedHeader { get; set; }

        /// <summary>
        /// 获得/设置 是否显示收缩展开 Bar
        /// </summary>
        [Parameter]
        public bool ShowCollapseBar { get; set; }

        /// <summary>
        /// 获得/设置 是否显示 Footer 模板
        /// </summary>
        [Parameter]
        public bool ShowFooter { get; set; }

        /// <summary>
        /// 获得/设置 是否显示返回顶端按钮
        /// </summary>
        [Parameter]
        public bool ShowGotoTop { get; set; }

        /// <summary>
        /// 获得/设置 点击菜单时回调委托方法 默认为 null
        /// </summary>
        [Parameter]
        public Func<MenuItem, Task>? OnClickMenu { get; set; }

        /// <summary>
        /// 获得/设置 收缩展开回调委托
        /// </summary>
        [Parameter]
        public Func<bool, Task>? OnCollapsed { get; set; }

        /// <summary>
        /// 点击 收缩展开按钮时回调此方法
        /// </summary>
        /// <returns></returns>
        protected async Task CollapseMenu()
        {
            IsCollapsed = !IsCollapsed;
            if (OnCollapsed != null) await OnCollapsed(IsCollapsed);
        }

        /// <summary>
        /// 点击菜单时回调此方法
        /// </summary>
        /// <returns></returns>
        protected Func<MenuItem, Task> ClickMenu() => async item =>
        {
            // 小屏幕时生效
            if (IsSmallScreen && !item.Items.Any()) await CollapseMenu();

            if (OnClickMenu != null) await OnClickMenu(item);
        };
    }
}
