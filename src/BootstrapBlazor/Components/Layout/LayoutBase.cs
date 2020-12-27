// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Layout 组件基类
    /// </summary>
    public abstract class LayoutBase : BootstrapComponentBase
    {
        private JSInterop<LayoutBase>? Interop { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected bool IsSmallScreen { get; set; }

        /// <summary>
        /// 获得 组件样式
        /// </summary>
        protected string? ClassString => CssBuilder.Default("layout")
            .AddClass("has-sidebar", Side != null && IsFullSide)
            .AddClass("is-page", IsPage)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 页脚样式
        /// </summary>
        protected string? FooterClassString => CssBuilder.Default("layout-footer")
            .AddClass("is-fixed", IsFixedFooter)
            .AddClass("is-collapsed", IsCollapsed)
            .Build();

        /// <summary>
        /// 获得 页头样式
        /// </summary>
        protected string? HeaderClassString => CssBuilder.Default("layout-header")
            .AddClass("is-fixed", IsFixedHeader)
            .Build();

        /// <summary>
        /// 获得 侧边栏样式
        /// </summary>
        protected string? SideClassString => CssBuilder.Default("layout-side")
            .AddClass("is-collapsed", IsCollapsed)
            .AddClass("is-fixed-header", IsFixedHeader)
            .AddClass("is-fixed-footer", IsFixedFooter)
            .Build();

        /// <summary>
        /// 获得 侧边栏 Style 字符串
        /// </summary>
        protected string? SideStyleString => CssBuilder.Default()
            .AddClass($"width: {SideWidth.ConvertToPercentString()}", !string.IsNullOrEmpty(SideWidth) && SideWidth != "0")
            .Build();

        /// <summary>
        /// 获得 Main 样式
        /// </summary>
        protected string? MainClassString => CssBuilder.Default("layout-main")
            .AddClass("is-collapsed", IsCollapsed)
            .Build();

        /// <summary>
        /// 获得 展开收缩 Bar 样式
        /// </summary>
        protected string? CollapseBarClassString => CssBuilder.Default("layout-header-bar")
            .AddClass("is-collapsed", IsCollapsed)
            .Build();

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
        /// 获得/设置 Footer 高度 支持百分比 默认宽度为 300px
        /// </summary>
        [Parameter]
        public string SideWidth { get; set; } = "300";

        /// <summary>
        /// 获得/设置 Main 模板
        /// </summary>
        [Parameter]
        public RenderFragment? Main { get; set; }

        /// <summary>
        /// 获得/设置 是否为暗黑模式
        /// </summary>
        [Parameter]
        public bool IsDark { get; set; }

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
        /// 获得/设置 Gets or sets a collection of additional assemblies that should be searched for components that can match URIs.
        /// </summary>
        [Parameter]
        public IEnumerable<Assembly>? AdditionalAssemblies { get; set; }

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
        public Func<bool, Task> OnCollapsed { get; set; } = b => Task.CompletedTask;

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                Interop = new JSInterop<LayoutBase>(JSRuntime);
                await Interop.Invoke(this, null, "bb_layout", nameof(SetCollapsed));
            }
        }

        /// <summary>
        /// 点击 收缩展开按钮时回调此方法
        /// </summary>
        /// <returns></returns>
        protected async Task CollapseMenu()
        {
            IsCollapsed = !IsCollapsed;
            await OnCollapsed.Invoke(IsCollapsed);
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

        /// <summary>
        /// 设置 请求头方法
        /// </summary>
        /// <returns></returns>
        [JSInvokable]
        public void SetCollapsed(int width)
        {
            IsSmallScreen = width < 768;
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing) Interop?.Dispose();
        }
    }
}
