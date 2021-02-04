// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Scroll 组件基类
    /// </summary>
    public abstract class ScrollBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 子组件
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 获得/设置 组件高度
        /// </summary>
        [Parameter]
        public string Height { get; set; } = "auto";

        /// <summary>
        /// 获得/设置 组件宽度
        /// </summary>
        [Parameter]
        public string Width { get; set; } = "auto";

        /// <summary>
        /// 获得/设置 是否自动隐藏 默认为 true
        /// </summary>
        [Parameter]
        public bool IsAutoHide { get; set; } = true;
    }
}
