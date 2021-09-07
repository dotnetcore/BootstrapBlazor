// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class PrintButton
    {
        /// <summary>
        /// 获得/设置 预览模板地址 必填项 默认为空
        /// </summary>
        [Parameter]
        public string? PreviewUrl { get; set; }

        [Inject]
        [NotNull]
        private PrintService? PrintService { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            // 不需要走 base.OnInitialized 方法

            ButtonIcon = Icon;
        }
    }
}
