// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Markdown 基类
    /// </summary>
    public sealed partial class Markdown
    {
        /// <summary>
        /// 获得/设置 DOM 元素实例
        /// </summary>
        private ElementReference MarkdownElement { get; set; }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender) await JSRuntime.InvokeVoidAsync("$.markdown", MarkdownElement);
        }

        /// <summary>
        /// 获得 Markdown 编辑器源码
        /// </summary>
        /// <returns></returns>
        public async ValueTask<string> GetMarkdownString() => await JSRuntime.InvokeAsync<string>("$.markdown", MarkdownElement, "getMarkdown");

        /// <summary>
        /// 获得 Markdown 编辑器 HTML 源码
        /// </summary>
        /// <returns></returns>
        public async ValueTask<string> GetMarkdownHtmlString() => await JSRuntime.InvokeAsync<string>("$.markdown", MarkdownElement, "getHTML");
    }
}
