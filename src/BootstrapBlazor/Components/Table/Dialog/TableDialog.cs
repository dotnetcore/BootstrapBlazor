// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class TableDialog<TModel> : ComponentBase
    {
#nullable disable
        /// <summary>
        /// 获得/设置 EditModel 实例
        /// </summary>
        [Parameter]
        public TModel Model { get; set; }
#nullable restore

        /// <summary>
        /// 获得/设置 BodyTemplate 实例
        /// </summary>
        [Parameter]
        public RenderFragment<TModel>? BodyTemplate { get; set; }

        /// <summary>
        /// 获得 表头集合
        /// </summary>
        [Parameter]
        public IEnumerable<ITableColumn>? Columns { get; set; }

        /// <summary>
        /// 获得/设置 是否显示标签
        /// </summary>
        [Parameter]
        public bool ShowLabel { get; set; }
    }
}
