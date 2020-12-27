// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// QRCode 组件
    /// </summary>
    public sealed partial class QRCode
    {
        private ElementReference QRCodeElement { get; set; }

        private JSInterop<QRCode>? Interop { get; set; }

        /// <summary>
        /// 获得/设置 二维码生成后回调委托
        /// </summary>
        [Parameter]
        public Func<Task>? OnGenerated { get; set; }

        /// <summary>
        /// 获得/设置 PlaceHolder 文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? PlaceHolder { get; set; }

        /// <summary>
        /// 获得/设置 清除按钮文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? ClearButtonText { get; set; }

        /// <summary>
        /// 获得/设置 生成按钮文字
        /// </summary>
        [Parameter]
        [NotNull]
        public string? GenerateButtonText { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<QRCode>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            PlaceHolder ??= Localizer[nameof(PlaceHolder)];
            ClearButtonText ??= Localizer[nameof(ClearButtonText)];
            GenerateButtonText ??= Localizer[nameof(GenerateButtonText)];
        }

        private async Task Clear()
        {
            await JSRuntime.InvokeVoidAsync(QRCodeElement, "bb_qrcode", "clear");
        }

        private async Task Generate()
        {
            if (Interop == null) Interop = new JSInterop<QRCode>(JSRuntime);
            await Interop.Invoke(this, QRCodeElement, "bb_qrcode", "generate");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [JSInvokable]
        public async Task Generated()
        {
            if (OnGenerated != null) await OnGenerated.Invoke();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Interop?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
