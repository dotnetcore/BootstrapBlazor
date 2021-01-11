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
