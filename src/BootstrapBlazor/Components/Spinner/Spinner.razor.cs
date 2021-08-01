// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Spinner 组件基类
    /// </summary>
    public partial class Spinner
    {
        /// <summary>
        /// 获取Spinner样式
        /// </summary>
        protected string? ClassString => CssBuilder.Default("spinner")
            .AddClass($"spinner-{SpinnerType.ToDescriptionString()}")
            .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass($"spinner-border-{Size.ToDescriptionString()}", Size != Size.None)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 Spinner 颜色 默认 None 无设置
        /// </summary>
        [Parameter]
        public Color Color { get; set; }

        /// <summary>
        /// 获得 / 设置 Spinner 大小 默认 None 无设置
        /// </summary>
        [Parameter]
        public Size Size { get; set; }

        /// <summary>
        /// 获得/设置 Spinner 类型 默认为 Border
        /// </summary>
        [Parameter]
        public SpinnerType SpinnerType { get; set; }
    }
}
