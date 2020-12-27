// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Tooltip 组件
    /// </summary>
    public class Tooltip : BootstrapComponentBase, ITooltip
    {
        /// <summary>
        /// 获得/设置 弹出框类型
        /// </summary>
        public PopoverType PopoverType { get; set; }

        /// <summary>
        /// 获得/设置 显示内容
        /// </summary>
        [Parameter]
        public string? Content { get; set; }

        /// <summary>
        /// 获得/设置 显示文字是否为 Html 默认为 false
        /// </summary>
        [Parameter]
        public bool IsHtml { get; set; }

        /// <summary>
        /// 获得/设置 位置 默认为 Placement.Auto
        /// </summary>
        [Parameter]
        public Placement Placement { get; set; }

        /// <summary>
        /// 获得/设置 显示文字
        /// </summary>
        [Parameter]
        public string? Title { get; set; }

        /// <summary>
        /// 获得/设置 触发方式 可组合 click focus hover 默认为 focus hover
        /// </summary>
        [Parameter]
        public string Trigger { get; set; } = "focus hover";

        /// <summary>
        /// 获得/设置 ITooltip 实例
        /// </summary>
        [CascadingParameter]
        public ITooltipHost? TooltipHost { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (TooltipHost != null) TooltipHost.Tooltip = this;
        }
    }
}
