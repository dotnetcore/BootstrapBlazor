// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class State
    {
        /// <summary>
        /// 获得/设置 是否为新组件 默认为 false
        /// </summary>
        [Parameter]
        public bool IsNew { get; set; }

        /// <summary>
        /// 获得/设置 是否为更新功能 默认为 false
        /// </summary>
        [Parameter]
        public bool IsUpdate { get; set; }

        /// <summary>
        /// 获得/设置 组件数量
        /// </summary>
        [Parameter]
        public int Count { get; set; }
    }
}
