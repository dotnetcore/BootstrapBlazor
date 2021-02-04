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
    /// Anchor 组件部分类
    /// </summary>
    public sealed partial class Anchor
    {
        private ElementReference AnchorElement { get; set; }

        /// <summary>
        /// 获得/设置 目标组件 Id
        /// </summary>
        [Parameter]
        public string? Target { get; set; }

        /// <summary>
        /// 获得/设置 滚动组件 Id 默认为 null 使用 window 元素
        /// </summary>
        [Parameter]
        public string? Container { get; set; }

        /// <summary>
        /// 获得/设置 距离顶端偏移量 默认为 0
        /// </summary>
        [Parameter]
        public int Offset { get; set; }

        /// <summary>
        /// 获得/设置 子内容
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && !string.IsNullOrEmpty(Target)) await JSRuntime.InvokeVoidAsync(AnchorElement, "bb_anchor");
        }
    }
}
