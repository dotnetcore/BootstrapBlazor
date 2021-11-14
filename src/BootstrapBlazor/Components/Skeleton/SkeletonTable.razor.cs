// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class SkeletonTable
    {
        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public int Rows { get; set; } = 7;

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public int Columns { get; set; } = 3;
    }
}
