// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Layout
    {
        private JSInterop<Layout>? Interop { get; set; }

        /// <summary>
        /// 获得 组件样式
        /// </summary>
        private string? ClassString => CssBuilder.Default("layout")
            .AddClass("has-sidebar", Side != null && IsFullSide)
            .AddClass("is-page", IsPage)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 页脚样式
        /// </summary>
        private string? FooterClassString => CssBuilder.Default("layout-footer")
            .AddClass("is-fixed", IsFixedFooter)
            .AddClass("is-collapsed", IsCollapsed)
            .Build();

        /// <summary>
        /// 获得 页头样式
        /// </summary>
        private string? HeaderClassString => CssBuilder.Default("layout-header")
            .AddClass("is-fixed", IsFixedHeader)
            .Build();

        /// <summary>
        /// 获得 侧边栏样式
        /// </summary>
        private string? SideClassString => CssBuilder.Default("layout-side")
            .AddClass("is-collapsed", IsCollapsed)
            .AddClass("is-fixed-header", IsFixedHeader)
            .AddClass("is-fixed-footer", IsFixedFooter)
            .Build();

        /// <summary>
        /// 获得 侧边栏 Style 字符串
        /// </summary>
        private string? SideStyleString => CssBuilder.Default()
            .AddClass($"width: {SideWidth.ConvertToPercentString()}", !string.IsNullOrEmpty(SideWidth) && SideWidth != "0")
            .Build();

        /// <summary>
        /// 获得 Main 样式
        /// </summary>
        private string? MainClassString => CssBuilder.Default("layout-main")
            .AddClass("is-collapsed", IsCollapsed)
            .Build();

        /// <summary>
        /// 获得 展开收缩 Bar 样式
        /// </summary>
        private string? CollapseBarClassString => CssBuilder.Default("layout-header-bar")
            .AddClass("is-collapsed", IsCollapsed)
            .Build();

        /// <summary>
        /// 获得/设置 鼠标悬停提示文字信息
        /// </summary>
        [Parameter]
        [NotNull]
        public string? TooltipText { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Layout>? Localizer { get; set; }

        /// <summary>
        /// 获得 Tab 组件实例
        /// </summary>
        public Tab? TabSet { get; private set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            TooltipText ??= Localizer[nameof(TooltipText)];
        }

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
                Interop = new JSInterop<Layout>(JSRuntime);
                await Interop.Invoke(this, null, "bb_layout", nameof(SetCollapsed));
            }
        }

        /// <summary>
        /// 设置侧边栏收缩方法 客户端监控 window.onresize 事件回调此方法
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
