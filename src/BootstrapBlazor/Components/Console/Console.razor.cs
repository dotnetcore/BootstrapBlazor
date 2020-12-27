// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Console
    {
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

            await JSRuntime.InvokeVoidAsync(ConsoleElement, "bb_console_log");
        }
    }
}
