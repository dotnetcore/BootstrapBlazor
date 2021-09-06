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
    /// Handwritten 手写签名
    /// </summary>
    public sealed partial class Handwritten
    {
        /// <summary>
        /// 清除按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? ClearButtonText { get; set; }

        /// <summary>
        /// 保存按钮文本
        /// </summary>
        [Parameter]
        [NotNull]
        public string? SaveButtonText { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Handwritten>? Localizer { get; set; }

        /// <summary>
        /// 手写结果回调方法
        /// </summary>
        [Parameter]
        public EventCallback<string> HandwrittenBase64 { get; set; }

        /// <summary>
        /// 手写签名imgBase64字符串
        /// </summary>
        [Parameter]
        public string? Result { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            ClearButtonText ??= Localizer[nameof(ClearButtonText)];
            SaveButtonText ??= Localizer[nameof(SaveButtonText)]; 
        } 

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (!firstRender) return;
            await JsRuntime.InvokeAsync<string>("handwrittenv2.start", true, DotNetObjectReference.Create(this));
        }

        /// <summary>
        /// 保存结果
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        [JSInvokable("invokeFromJS")]
        public async Task ChangeValue(string val)
        {
            Result = val;
            StateHasChanged();
            await HandwrittenBase64.InvokeAsync(val);
            //return Task.CompletedTask;
        }
         
    }

}
