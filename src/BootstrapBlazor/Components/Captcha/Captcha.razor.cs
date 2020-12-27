// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Captcha
    {
        private JSInterop<CaptchaBase>? Interop { get; set; }

        /// <summary>
        /// 获得/设置 Captcha DOM 元素实例
        /// </summary>
        private ElementReference CaptchaElement { get; set; }

        /// <summary>
        /// 获得/设置 Header 显示文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? HeaderText { get; set; }

        /// <summary>
        /// 获得/设置 Bar 显示文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? BarText { get; set; }

        /// <summary>
        /// 获得/设置 Bar 显示文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? FailedText { get; set; }

        /// <summary>
        /// 获得/设置 Bar 显示文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? LoadText { get; set; }

        /// <summary>
        /// 获得/设置 Bar 显示文本
        /// </summary>
        [Parameter]
        public string? TryText { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Captcha>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            HeaderText ??= Localizer[nameof(HeaderText)];
            BarText ??= Localizer[nameof(BarText)];
            FailedText ??= Localizer[nameof(FailedText)];
            LoadText ??= Localizer[nameof(LoadText)];
            TryText ??= Localizer[nameof(TryText)];
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing) Interop?.Dispose();
        }

        /// <summary>
        /// 重置组件方法
        /// </summary>
        protected override void Reset()
        {
            var option = GetCaptchaOption();
            if (Interop == null) Interop = new JSInterop<CaptchaBase>(JSRuntime);
            Interop?.Invoke(this, CaptchaElement, "captcha", nameof(Verify), option);
        }
    }
}
