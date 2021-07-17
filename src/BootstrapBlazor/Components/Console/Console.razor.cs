// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Console
    {
        /// <summary>
        /// 获得 组件样式
        /// </summary>
        private string? ClassString => CssBuilder.Default("card console")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 Console Body Style 字符串
        /// </summary>
        private string? BodyStyleString => CssBuilder.Default()
            .AddClass($"height: {Height}px;", Height > 0)
            .Build();

        /// <summary>
        /// 获得 Footer 样式
        /// </summary>
        private string? FooterClassString => CssBuilder.Default("card-footer text-end")
            .AddClass("d-none", OnClear == null && !ShowAutoScroll)
            .Build();

        /// <summary>
        /// 获得 按钮样式
        /// </summary>
        private string? ClearButtonClassString => CssBuilder.Default("btn btn-secondary")
            .AddClass($"btn-{ClearButtonColor.ToDescriptionString()}", ClearButtonColor != Color.None)
            .Build();

        /// <summary>
        /// 获得 客户端是否自动滚屏样式字符串
        /// </summary>
        private string? AutoScrollClassString => CssBuilder.Default("fa text-start")
            .AddClass("fa-check-square-o", IsAutoScroll)
            .AddClass("fa-square-o", !IsAutoScroll)
            .Build();

        /// <summary>
        /// 获得 客户端是否自动滚屏标识
        /// </summary>
        private string? AutoScrollString => (IsAutoScroll && ShowAutoScroll) ? "auto" : null;

        /// <summary>
        /// 获得 Console 组件客户端引用实例
        /// </summary>
        private ElementReference ConsoleElement { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Console>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            HeaderText ??= Localizer[nameof(HeaderText)];
            LightTitle ??= Localizer[nameof(LightTitle)];
            ClearButtonText ??= Localizer[nameof(ClearButtonText)];
            AutoScrollText ??= Localizer[nameof(AutoScrollText)];
        }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (IsAutoScroll && ShowAutoScroll)
            {
                await JSRuntime.InvokeVoidAsync(ConsoleElement, "bb_console");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void ClickAutoScroll()
        {
            IsAutoScroll = !IsAutoScroll;
        }

        /// <summary>
        /// 获取消息样式
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string? GetClassString(ConsoleMessageItem item) => CssBuilder.Default()
            .AddClass($"text-{item.Color.ToDescriptionString()}", item.Color != Color.None)
            .Build();

        /// <summary>
        /// 清空控制台消息方法
        /// </summary>
        public void ClearConsole()
        {
            OnClear?.Invoke();
        }
    }
}
