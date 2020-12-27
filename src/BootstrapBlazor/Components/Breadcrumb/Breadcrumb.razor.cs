// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace BootstrapBlazor.Components
{

    /// <summary>
    /// Breadcrumb 组件
    /// </summary>
    public sealed partial class Breadcrumb
    {
        /// <summary>
        /// 获得/设置 数据集
        /// </summary>
        [Parameter]
        public IEnumerable<BreadcrumbItem> Value { get; set; } = Enumerable.Empty<BreadcrumbItem>();

        /// <summary>
        /// 获得/设置 面包屑渲染模式是否进行导航 默认 false 不进行导航
        /// </summary>
        [Parameter]
        public bool ActiveLink { get; set; }

        private string? GetItemClassName(BreadcrumbItem item) => CssBuilder.Default("breadcrumb-item")
            .Build();

        private string? CurrentPage(BreadcrumbItem item) => CssBuilder.Default()
            .Build();
    }
}
