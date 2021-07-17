// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class ComponentCard
    {
        private string ImageUrl => $"_content/BootstrapBlazor.Shared/images/{Image}";

        private string? ClassString => CssBuilder.Default("col-12 col-sm-6 col-md-4 col-lg-3")
            .AddClass("d-none", !string.IsNullOrEmpty(SearchText) && !Text.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase))
            .Build();

        /// <summary>
        /// 获得/设置 Header 文字
        /// </summary>
        [Parameter]
        public string Text { get; set; } = "未设置";

        /// <summary>
        /// 获得/设置 组件图片
        /// </summary>
        [Parameter]
        public string Image { get; set; } = "Divider.svg";

        /// <summary>
        /// 获得/设置 链接地址
        /// </summary>
        [Parameter]
        public string? Url { get; set; }

        [CascadingParameter]
        private List<string>? ComponentNames { get; set; }

        [CascadingParameter]
        private ComponentCategory? Parent { get; set; }

        [CascadingParameter]
        private string? SearchText { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            ComponentNames?.Add(Text);
            Parent?.Add(this);
        }
    }
}
