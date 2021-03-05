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
    /// Markdown 组件
    /// </summary>
    public sealed partial class Markdown
    {
        /// <summary>
        /// 获得/设置 DOM 元素实例
        /// </summary>
        private ElementReference MarkdownElement { get; set; }

        /// <summary>
        /// 获得/设置 控件高度，默认300px
        /// </summary>
        [Parameter]
        public int Height { get; set; } = 300;

        /// <summary>
        /// 获得/设置 控件最小高度，默认200px
        /// </summary>
        [Parameter]
        public int MinHeight { get; set; } = 200;

        /// <summary>
        /// 获得/设置 初始化时显示的界面，markdown 界面，所见即所得界面
        /// </summary>
        [Parameter]
        public InitialEditType InitialEditType { get; set; }

        /// <summary>
        /// 获得/设置 预览模式，Tab 页预览，分栏预览 默认分栏预览 Vertical
        /// </summary>
        [Parameter]
        public PreviewStyle PreviewStyle { get; set; }

        /// <summary>
        /// 获得/设置 语言，默认为简体中文，如果改变，需要自行引入语言包
        /// </summary>
        [Parameter]
        public string? Language { get; set; }

        /// <summary>
        /// 获得/设置 提示信息
        /// </summary>
        [Parameter]
        public string? Placeholder { get; set; }

        private readonly MarkdownOption _markdownOption = new();

        private bool IsRendered { get; set; }

        private Command? ActionCommand { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            _markdownOption.PreviewStyle = PreviewStyle.ToDescriptionString();
            _markdownOption.InitialEditType = InitialEditType.ToDescriptionString();
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

            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("$.bb_markdown", MarkdownElement, "init", _markdownOption);
                IsRendered = true;

                if (ActionCommand.HasValue)
                {
                    await RunCommand(ActionCommand.Value);
                }
            }
        }

        /// <summary>
        /// 设置Html内容（会覆盖原有内容，请谨慎使用）
        /// </summary>
        /// <returns></returns>
        public ValueTask SetHtml(string html) => RunCommand(new Command()
        {
            Type = "set",
            Method = "setHtml",
            Value = html
        });

        /// <summary>
        /// 设置 Html 内容（会覆盖原有内容，请谨慎使用）
        /// </summary>
        /// <returns></returns>
        public ValueTask SetMarkdown(string markdown) => RunCommand(new Command()
        {
            Type = "set",
            Method = "setMarkdown",
            Value = markdown
        });

        /// <summary>
        /// 插入文本到光标处
        /// </summary>
        /// <returns></returns>
        public ValueTask InsertText(string text) => RunCommand(new Command()
        {
            Type = "set",
            Method = "insertText",
            Value = text
        });

        /// <summary>
        /// 隐藏 Editor
        /// </summary>
        /// <returns></returns>
        public ValueTask Hide() => RunCommand(new Command()
        {
            Type = "get",
            Method = "hide"
        });

        /// <summary>
        /// 显示 Editor
        /// </summary>
        /// <returns></returns>
        public ValueTask Show() => RunCommand(new Command()
        {
            Type = "get",
            Method = "show"
        });

        /// <summary>
        /// 获得 Markdown 编辑器 已选中的文本内容
        /// </summary>
        /// <returns></returns>
        public ValueTask<string> GetSelectedText() => JSRuntime.InvokeAsync<string>("$.bb_markdown", MarkdownElement, "get", "getSelectedText");

        /// <summary>
        /// 获得 Markdown 编辑器源码
        /// </summary>
        /// <returns></returns>
        public ValueTask<string> GetMarkdownString() => JSRuntime.InvokeAsync<string>("$.bb_markdown", MarkdownElement, "get", "getMarkdown");

        /// <summary>
        /// 获得 Markdown 编辑器 HTML 源码
        /// </summary>
        /// <returns></returns>
        public ValueTask<string> GetMarkdownHtmlString() => JSRuntime.InvokeAsync<string>("$.bb_markdown", MarkdownElement, "get", "getHtml");

        private async ValueTask RunCommand(Command cmd)
        {
            if (IsRendered)
            {
                await JSRuntime.InvokeVoidAsync("$.bb_markdown", MarkdownElement, cmd.Type, cmd.Method, cmd.Value);
            }
            else
            {
                ActionCommand = cmd;
            }
        }

        private struct Command
        {
            public string Method { get; set; }

            public string Type { get; set; }

            public string Value { get; set; }
        }
    }
}
