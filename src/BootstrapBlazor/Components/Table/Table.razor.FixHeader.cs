// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    public partial class Table<TItem>
    {
        /// <summary>
        /// 获得 wrapper 样式表集合
        /// </summary>
        protected string? FixedHeaderStyleName => CssBuilder.Default()
            .AddClass($"height: {Height}px;", Height.HasValue && !IsPagination)
            .AddClass($"max-height: {Height}px;", Height.HasValue && IsPagination)
            .Build();

        /// <summary>
        /// 获得/设置 Table 组件引用
        /// </summary>
        /// <value></value>
        protected ElementReference TableElement { get; set; }

        /// <summary>
        /// 获得/设置 Table 高度
        /// </summary>
        [Parameter] public int? Height { get; set; }

        /// <summary>
        /// 获得/设置 多表头模板
        /// </summary>
        [Parameter]
        public RenderFragment? MultiHeaderTemplate { get; set; }
    }
}
