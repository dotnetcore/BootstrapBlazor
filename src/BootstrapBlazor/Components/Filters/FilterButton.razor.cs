// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class FilterButton<TValue>
    {
        /// <summary>
        /// 获得/设置 清除过滤条件时的回调方法
        /// </summary>
        [Parameter]
        public Func<Task>? OnClearFilter { get; set; }

        private async Task ClearFilter()
        {
            if (OnClearFilter != null)
            {
                await OnClearFilter();
            }
        }
    }
}
