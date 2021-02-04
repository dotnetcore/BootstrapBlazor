// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Modal
    {
        /// <summary>
        /// 获得/设置 DOM 元素实例
        /// </summary>
        private ElementReference ModalElement { get; set; }

        /// <summary>
        /// 获得 样式字符串
        /// </summary>
        private string? ClassString => CssBuilder.Default("modal")
            .AddClass("fade", IsFade)
            .Build();

        /// <summary>
        /// 获得 后台关闭弹窗设置
        /// </summary>
        private string? Backdrop => IsBackdrop ? null : "static";

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync(ModalElement, "bb_modal", "init");
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        /// <summary>
        /// 弹窗状态切换方法
        /// </summary>
        public override async ValueTask Toggle()
        {
            Dialogs.ForEach(d => d.IsShown = Dialogs.IndexOf(d) == 0);
            await JSRuntime.InvokeVoidAsync(ModalElement, "bb_modal", "toggle");
        }

        /// <summary>
        /// 显示弹窗方法
        /// </summary>
        /// <returns></returns>
        public override async ValueTask Show()
        {
            Dialogs.ForEach(d => d.IsShown = Dialogs.IndexOf(d) == 0);
            await JSRuntime.InvokeVoidAsync(ModalElement, "bb_modal", "show");
        }

        /// <summary>
        /// 关闭弹窗方法
        /// </summary>
        /// <returns></returns>
        public override async ValueTask Close()
        {
            Dialogs.ForEach(d => d.IsShown = Dialogs.IndexOf(d) == 0);
            await JSRuntime.InvokeVoidAsync(ModalElement, "bb_modal", "hide");
        }
    }
}
