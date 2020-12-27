// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// IToolbarButton 接口
    /// </summary>
    public interface IToolbarButton<TItem>
    {
        /// <summary>
        /// 
        /// </summary>
        Func<IEnumerable<TItem>, Task>? OnClickCallback { get; set; }
    }
}
