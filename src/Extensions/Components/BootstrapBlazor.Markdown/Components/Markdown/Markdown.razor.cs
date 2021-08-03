// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Markdown 组件
    /// </summary>
    public partial class Markdown : IDisposable
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

        /// <summary>
        /// 获得/设置 组件值
        /// </summary>
        [Parameter]
        public string? Value { get; set; }

        /// <summary>
        /// 获得/设置 组件值回调
        /// </summary>
        [Parameter]
        public EventCallback<string?> ValueChanged { get; set; }

        /// <summary>
        /// 获得/设置 组件 Html 代码
        /// </summary>
        [Parameter]
        public string? Html { get; set; }

        /// <summary>
        /// 获得/设置 组件 Html 代码回调
        /// </summary>
        [Parameter]
        public EventCallback<string?> HtmlChanged { get; set; }

        /// <summary>
        /// 获取/设置 组件是否为浏览器模式
        /// </summary>
        [Parameter]
        public bool? IsViewer { get; set; }

        private JSInterop<Markdown>? Interop { get; set; }

        private readonly MarkdownOption _markdownOption = new();

        private bool IsRender { get; set; }

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
            _markdownOption.initialValue = Value;
            _markdownOption.Viewer = IsViewer;
        }

        /// <summary>
        /// OnParametersSetAsync 方法
        /// </summary>
        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            IsRender = true;
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
                IsRender = false;
                if (Interop == null)
                {
                    Interop = new JSInterop<Markdown>(JSRuntime);
                }

                await Interop.InvokeVoidAsync(this, MarkdownElement, "bb_markdown", _markdownOption, nameof(Update));
            }

            if (IsRender)
            {
                if (Interop == null)
                {
                    Interop = new JSInterop<Markdown>(JSRuntime);
                }

                await Interop.InvokeVoidAsync(this, MarkdownElement, "bb_markdown", Value ?? "", "setMarkdown");
            }
        }

        /// <summary>
        /// 更新组件值方法
        /// </summary>
        /// <param name="vals"></param>
        /// <returns></returns>
        [JSInvokable]
        public async Task Update(string[] vals)
        {
            if (vals.Length == 2)
            {
                var hasChanged = !EqualityComparer<string>.Default.Equals(vals[0], Value);
                if (hasChanged)
                {
                    Value = vals[0];
                    if (ValueChanged.HasDelegate)
                    {
                        await ValueChanged.InvokeAsync(Value);
                    }
                }

                hasChanged = !EqualityComparer<string>.Default.Equals(vals[1], Html);
                if (hasChanged)
                {
                    Html = vals[1];
                    if (HtmlChanged.HasDelegate)
                    {
                        await HtmlChanged.InvokeAsync(Html);
                    }
                }
            }
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Interop?.Dispose();
                Interop = null;
            }
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
