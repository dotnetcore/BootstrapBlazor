// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Layout
    {
        /// <summary>
        /// 获得/设置 鼠标悬停提示文字信息
        /// </summary>
        [Parameter]
        [NotNull]
        public string? TooltipText { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Layout>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            TooltipText ??= Localizer[nameof(TooltipText)];
        }
    }
}
