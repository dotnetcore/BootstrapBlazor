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
    public partial class Empty
    {

        /// <summary>
        ///  获得或设置图片路径
        /// </summary>
        [Parameter]
        public string? Image { get; set; } = "_content/BootstrapBlazor/images/empty.svg";

        /// <summary>
        /// 获得或设置空状态描述
        /// </summary>
        [Parameter]
        public string? Description { get; set; }

        /// <summary>
        /// 获得或设置svg宽度
        /// </summary>
        [Parameter]
        public string? Width { get; set; } = "100";

        /// <summary>
        /// 获得或设置svg高度
        /// </summary>
        [Parameter]
        public string? Height { get; set; } = "100";

        /// <summary>
        /// 获得或设置自定义模板
        /// </summary>
        [Parameter]
        public RenderFragment? Telemplate { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Empty>? Localizer { get; set; }

        /// <summary>
        /// 组件初始化设置
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();
            Description = Description ?? Localizer[nameof(Description)];
        }

    }
}
