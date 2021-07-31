// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Row
    {
        /// <summary>
        /// 获得/设置 设置一行显示多少个子组件
        /// </summary>
        [Parameter]
        public ItemsPerRow ItemsPerRow { get; set; }

        /// <summary>
        /// 获得/设置 设置行格式 默认 Row 布局
        /// </summary>
        [Parameter]
        public RowType RowType { get; set; }

        /// <summary>
        /// 获得/设置 子 Row 跨父 Row 列数 默认为 null
        /// </summary>
        [Parameter]
        public int? ColSpan { get; set; }

        /// <summary>
        /// 获得/设置 子组件
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        private ElementReference RowElement { get; set; }

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
                await JSRuntime.InvokeVoidAsync(RowElement, "bb_row");
            }
        }
    }
}
