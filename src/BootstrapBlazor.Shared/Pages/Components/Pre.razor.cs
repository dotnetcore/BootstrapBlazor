// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages.Components
{
    /// <summary>
    /// Pre 组件
    /// </summary>
    public sealed partial class Pre
    {
        private ElementReference PreElement { get; set; }

        private bool Loaded { get; set; }

        private bool CanCopy { get; set; }

        /// <summary>
        /// 获得 样式集合
        /// </summary>
        /// <returns></returns>
        private string? ClassName => CssBuilder.Default()
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        [Inject]
        [NotNull]
        private ExampleService? Example { get; set; }

        /// <summary>
        /// 获得/设置 IJSRuntime 实例
        /// </summary>
        [Inject]
        [NotNull]
        private IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        /// 获得/设置 子组件 CodeFile 为空时生效
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 获得/设置 用户自定义属性
        /// </summary>
        /// <returns></returns>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object>? AdditionalAttributes { get; set; }

        /// <summary>
        /// 获得/设置 示例文档名称
        /// </summary>
        [Parameter]
        public string? CodeFile { get; set; }

        /// <summary>
        /// OnInitializedAsync 方法
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            if (ChildContent == null)
            {
                await ReloadExampleCodeAsync();
            }
            else
            {
                Loaded = true;
                CanCopy = true;
            }
        }

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (Loaded)
            {
                await JSRuntime.InvokeVoidAsync("$.highlight", PreElement);
            }
        }

        private async Task ReloadExampleCodeAsync()
        {
            if (!string.IsNullOrEmpty(CodeFile))
            {
                var code = await Example.GetCodeAsync(CodeFile);
                if (!string.IsNullOrEmpty(code))
                {
                    ChildContent = builder =>
                    {
                        builder.AddContent(0, code);
                    };
                }
                CanCopy = !string.IsNullOrEmpty(code) && code != "无";
                Loaded = true;
            }
            else
            {
                Loaded = true;
            }
        }
    }
}
