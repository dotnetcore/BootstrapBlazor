// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Toast 弹出窗组件
    /// </summary>
    public partial class Toast
    {
        private string? ClassString => CssBuilder.Default("toast-container")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 Toast 组件样式设置
        /// </summary>
        private string? StyleString => CssBuilder.Default()
            .AddClass("top: 1rem; left: 1rem;", Placement == Placement.TopStart)
            .AddClass("top: 1rem; right: 1rem;", Placement == Placement.TopEnd)
            .AddClass("bottom: 1rem; left: 1rem;", Placement == Placement.BottomStart)
            .AddClass("bottom: 1rem; right: 1rem;", Placement == Placement.BottomEnd)
            .Build();

        private string? ToastBoxClassString => CssBuilder.Default()
            .AddClass("left", Placement == Placement.TopStart || Placement == Placement.BottomStart)
            .Build();

        /// <summary>
        /// 获得 弹出窗集合
        /// </summary>
        private List<ToastOption> Toasts { get; } = new List<ToastOption>();

        /// <summary>
        /// 获得/设置 显示文字
        /// </summary>
        [Parameter]
        public Placement Placement { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Placement = Placement.BottomEnd;

            // 注册 Toast 弹窗事件
            if (ToastService != null)
            {
                ToastService.Register(this, Show);
            }
        }

        private async Task Show(ToastOption option)
        {
            Toasts.Add(option);
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 清除 ToastBox 方法
        /// </summary>
        [JSInvokable]
        public async Task Clear()
        {
            Toasts.Clear();
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 设置 Toast 容器位置方法
        /// </summary>
        /// <param name="placement"></param>
        public void SetPlacement(Placement placement)
        {
            Placement = placement;
            StateHasChanged();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                ToastService.UnRegister(this);
            }
        }
    }
}
