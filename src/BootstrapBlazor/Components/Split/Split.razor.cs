// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Split
    {
        /// <summary>
        /// 获得 组件 DOM 实例
        /// </summary>
        private ElementReference SplitElement { get; set; }

        /// <summary>
        /// 获得 组件样式
        /// </summary>
        private string? ClassString => CssBuilder.Default("split")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 组件 Wrapper 样式
        /// </summary>
        private string? WrapperClassString => CssBuilder.Default("split-wrapper")
            .AddClass("is-horizontal", !IsVertical)
            .Build();

        /// <summary>
        /// 获得 第一个窗格 Style
        /// </summary>
        private string? StyleString => CssBuilder.Default()
            .AddClass($"flex-basis: {Basis.ConvertToPercentString()};")
            .Build();

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
                await JSRuntime.InvokeVoidAsync(SplitElement, "bb_split");
            }
        }
    }
}
