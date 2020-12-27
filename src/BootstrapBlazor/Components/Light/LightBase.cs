// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Light 组件基类
    /// </summary>
    public abstract class LightBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 组件样式
        /// </summary>
        protected string? ClassString => CssBuilder.Default("light")
            .AddClass("flash", IsFlash)
            .AddClass($"light-{Color.ToDescriptionString()}", Color != Color.None)
            .Build();

        /// <summary>
        /// 获得/设置 组件是否闪烁 默认为 false 不闪烁
        /// </summary>
        [Parameter]
        public bool IsFlash { get; set; }

        /// <summary>
        /// 获得/设置 指示灯 Tooltip 显示文字
        /// </summary>
        [Parameter]
        public string? Title { get; set; }

        /// <summary>
        /// 获得/设置 指示灯颜色 默认为 Success 绿色
        /// </summary>
        [Parameter]
        public Color Color { get; set; } = Color.Success;
    }
}
