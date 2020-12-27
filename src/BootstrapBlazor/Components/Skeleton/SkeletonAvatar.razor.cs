// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class SkeletonAvatar
    {
        private string? AvatarClassString => CssBuilder.Default("skeleton-avatar")
            .AddClass("circle", Circle)
            .Build();

        /// <summary>
        /// 获得/设置 是否为圆形 默认为 false
        /// </summary>
        [Parameter]
        public bool Circle { get; set; }
    }
}
