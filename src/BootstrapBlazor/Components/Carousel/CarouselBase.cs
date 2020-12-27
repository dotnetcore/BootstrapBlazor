// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// CarouselBase 组件
    /// </summary>
    public abstract class CarouselBase : IdComponentBase
    {
        /// <summary>
        /// 获得 class 样式集合
        /// </summary>
        protected virtual string? ClassName => CssBuilder.Default("carousel slide")
            .AddClass("carousel-fade", IsFade)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 data-target 属性值
        /// </summary>
        /// <value></value>
        protected virtual string? TargetId => $"#{Id}";

        /// <summary>
        /// 获得 Style 样式
        /// </summary>
        protected virtual string? StyleName => CssBuilder.Default()
            .AddClass($"width: {Width}px;", Width.HasValue)
            .Build();

        /// <summary>
        /// 检查是否 active
        /// </summary>
        /// <param name="index"></param>
        /// <param name="css"></param>
        /// <returns></returns>
        protected string? CheckActive(int index, string? css = null) => CssBuilder.Default(css)
            .AddClass("active", index == 0)
            .Build();

        /// <summary>
        /// 获得 Images 集合
        /// </summary>
        [Parameter]
        public IEnumerable<string> Images { get; set; } = Enumerable.Empty<string>();

        /// <summary>
        /// 获得/设置 内部图片的宽度
        /// </summary>
        [Parameter]
        public int? Width { get; set; }

        /// <summary>
        /// 获得/设置 是否采用淡入淡出效果 默认为 false
        /// </summary>
        [Parameter]
        public bool IsFade { get; set; }

        /// <summary>
        /// 获得/设置 点击 Image 回调委托
        /// </summary>
        [Parameter]
        public Func<string, Task>? OnClick { get; set; }

        /// <summary>
        /// 点击 Image 是触发此方法
        /// </summary>
        /// <returns></returns>
        protected async Task OnClickImage(string imageUrl)
        {
            if (OnClick != null) await OnClick(imageUrl);
        }
    }
}
