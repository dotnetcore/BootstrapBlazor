// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Spinner 组件基类
    /// </summary>
    public abstract class SpinnerBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获取Spinner样式
        /// </summary>
        protected string? ClassName => CssBuilder.Default("spinner")
            .AddClass($"spinner-{spinner}")
            .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass($"spinner-border-{Size.ToDescriptionString()}", Size != Size.None)
            .AddClass(Class)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 Spinner样式
        /// </summary>
        [Parameter]
        public string? Class { get; set; }

        /// <summary>
        /// 获得/设置 Spinner颜色
        /// </summary>
        [Parameter]
        public Color Color { get; set; }

        /// <summary>
        /// 获得 / 设置 Spinner大小
        /// </summary>
        [Parameter]
        public Size Size { get; set; }

        /// <summary>
        /// 获得/设置 Spinner类型
        /// </summary>
        [Parameter]
        public SpinnerType SpinnerType { get; set; } = SpinnerType.Border;

        /// <summary>
        /// 获取 Spinner类型
        /// </summary>
        protected virtual string? spinner => SpinnerType switch
        {
            SpinnerType.Border => "border",
            SpinnerType.Grow => "grow",
            _ => ""
        };
    }
}
