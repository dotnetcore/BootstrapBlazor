// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Carousel
    {
        private ElementReference CarouselElement { get; set; }

        /// <summary>
        /// 获得 class 样式集合
        /// </summary>
        private string? ClassName => CssBuilder.Default("carousel slide")
            .AddClass("carousel-fade", IsFade)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 data-target 属性值
        /// </summary>
        /// <value></value>
        private string? TargetId => $"#{Id}";

        /// <summary>
        /// 获得 Style 样式
        /// </summary>
        private string? StyleName => CssBuilder.Default()
            .AddClass($"width: {Width}px;", Width.HasValue)
            .Build();

        /// <summary>
        /// 检查是否 active
        /// </summary>
        /// <param name="index"></param>
        /// <param name="css"></param>
        /// <returns></returns>
        private string? CheckActive(int index, string? css = null) => CssBuilder.Default(css)
            .AddClass("active", index == 0)
            .Build();

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender) await JSRuntime.InvokeVoidAsync(CarouselElement, "bb_carousel");
        }
    }
}
