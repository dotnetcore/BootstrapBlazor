// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Reflection.Metadata;
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
        /// 控件高度，默认300px
        /// </summary>
        [Parameter]
        public int Height { get; set; } = 300;

        /// <summary>
        /// 控件最小高度，默认200px
        /// </summary>
        [Parameter]
        public int MinHeight { get; set; } = 200;

        /// <summary>
        /// 初始化时显示的界面，markdown界面，所见即所得界面
        /// </summary>
        [Parameter]
        public InitialEditType InitialEditType { get; set; } = InitialEditType.Markdown;

        /// <summary>
        /// 预览模式，Tab页预览，分栏预览
        /// </summary>
        [Parameter]
        public PreviewStyle PreviewStyle { get; set; } = PreviewStyle.Vertical;

        /// <summary>
        /// 语言，默认为简体中文，如果改变，需要自行引入语言包
        /// </summary>
        [Parameter]
        public string Language { get; set; } = "zh-CN";

        /// <summary>
        /// 提示信息
        /// </summary>
        [Parameter]
        public string Placeholder { get; set; } = "";

        private MarkdownOption _markdownOption = new MarkdownOption();

        /// <summary>
        ///
        /// </summary>
        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            if (InitialEditType == InitialEditType.Wysiwyg)
            {
                _markdownOption.InitialEditType = "wysiwyg";
            }

            if (PreviewStyle == PreviewStyle.Tab)
            {
                _markdownOption.PreviewStyle = "tab";
            }

            _markdownOption.Language = Language;
            _markdownOption.Placeholder = Placeholder;
            _markdownOption.Height = $"{Height}px";
            _markdownOption.MinHeight = $"{MinHeight}px";
        }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender) await JSRuntime.InvokeVoidAsync("$.bb_markdown", MarkdownElement, 0, _markdownOption);
        }

        /// <summary>
        /// 获得 Markdown 编辑器源码
        /// </summary>
        /// <returns></returns>
        public async ValueTask<string> GetMarkdownString() => await JSRuntime.InvokeAsync<string>("$.bb_markdown", MarkdownElement, 1, "getMarkdown");

        /// <summary>
        /// 获得 Markdown 编辑器 HTML 源码
        /// </summary>
        /// <returns></returns>
        public async ValueTask<string> GetMarkdownHtmlString() => await JSRuntime.InvokeAsync<string>("$.bb_markdown", MarkdownElement, 1, "getHtml");

        /// <summary>
        /// 设置Html内容（会覆盖原有内容，请谨慎使用）
        /// </summary>
        /// <returns></returns>
        public async Task SetHtml(string html) => await JSRuntime.InvokeVoidAsync("$.bb_markdown", MarkdownElement, 2, "setHtml", html);

        /// <summary>
        /// 设置Html内容（会覆盖原有内容，请谨慎使用）
        /// </summary>
        /// <returns></returns>
        public async Task SetMarkdown(string markdown) => await JSRuntime.InvokeVoidAsync("$.bb_markdown", MarkdownElement, 2, "setMarkdown", markdown);

        /// <summary>
        /// 插入文本到光标处
        /// </summary>
        /// <returns></returns>
        public async Task InsertText(string text) => await JSRuntime.InvokeVoidAsync("$.bb_markdown", MarkdownElement, 2, "insertText", text);

        /// <summary>
        /// 隐藏Editor
        /// </summary>
        /// <returns></returns>
        public async Task Hide() => await JSRuntime.InvokeVoidAsync("$.bb_markdown", MarkdownElement, 1, "hide");

        /// <summary>
        /// 显示Editor
        /// </summary>
        /// <returns></returns>
        public async Task Show() => await JSRuntime.InvokeVoidAsync("$.bb_markdown", MarkdownElement, 1, "show");

        /// <summary>
        /// 获得 Markdown 编辑器 已选中的文本内容
        /// </summary>
        /// <returns></returns>
        public async ValueTask<string> GetSelectedText() => await JSRuntime.InvokeAsync<string>("$.bb_markdown", MarkdownElement, 1, "getSelectedText");
    }
}
