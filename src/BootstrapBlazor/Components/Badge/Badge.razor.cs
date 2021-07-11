// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Badge 徽章组件
    /// </summary>
    public partial class Badge
    {
        /// <summary>
        /// 获得 样式集合
        /// </summary>
        /// <returns></returns>
        protected string? ClassName => CssBuilder.Default("badge")
            .AddClass($"bg-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass("rounded-pill", IsPill)
            .AddClass("text-dark", Color == Color.Warning || Color == Color.Info)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 颜色
        /// </summary>
        [Parameter] public Color Color { get; set; } = Color.Primary;

        /// <summary>
        /// 获得/设置 是否显示为胶囊形式
        /// </summary>
        /// <value></value>
        [Parameter] public bool IsPill { get; set; }

        /// <summary>
        /// 子组件
        /// </summary>
        [Parameter] public RenderFragment? ChildContent { get; set; }
    }
}
