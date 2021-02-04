// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Block
    {
        /// <summary>
        /// 获得/设置 组件 Title 属性
        /// </summary>
        [Parameter]
        [NotNull]
        public string? Title { get; set; }

        /// <summary>
        /// 获得/设置 组件说明信息
        /// </summary>
        [Parameter]
        public string Introduction { get; set; } = "未设置";

        /// <summary>
        /// 获得/设置 组件内容
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [NotNull]
        private string? SubTitle { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Block>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Title ??= Localizer[nameof(Title)];
            SubTitle ??= Localizer[nameof(SubTitle)];
        }
    }
}
