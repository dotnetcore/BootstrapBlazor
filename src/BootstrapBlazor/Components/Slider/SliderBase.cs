// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Slider 组件
    /// </summary>
    public class SliderBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 组件当前值
        /// </summary>
        [Parameter]
        public int Value { get; set; }

        /// <summary>
        /// ValueChanged 回调方法
        /// </summary>
        [Parameter]
        public EventCallback<int> ValueChanged { get; set; }

        /// <summary>
        /// 获得 按钮 disabled 属性
        /// </summary>
        protected string? Disabled => IsDisabled ? "disabled" : null;

        /// <summary>
        /// 获得/设置 是否禁用
        /// </summary>
        [Parameter]
        public bool IsDisabled { get; set; }
    }
}
