// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class TableFooterCell
    {
        private string? ClassString => CssBuilder.Default("table-cell")
            .AddClass("justify-content-start", Align == Alignment.Left)
            .AddClass("justify-content-center", Align == Alignment.Center)
            .AddClass("justify-content-end", Align == Alignment.Right)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 单元格内容
        /// </summary>
        [Parameter]
        public string? Text { get; set; }

        /// <summary>
        /// 获得/设置 文字对齐方式 默认为 Alignment.None
        /// </summary>
        [Parameter]
        public Alignment Align { get; set; }

        /// <summary>
        /// 获得/设置 是否为移动端模式
        /// </summary>
        [CascadingParameter(Name = "IsMobileMode")]
        private bool IsMobileMode { get; set; }
    }
}
