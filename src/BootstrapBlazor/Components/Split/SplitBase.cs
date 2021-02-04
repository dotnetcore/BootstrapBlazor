// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Split 组件基类
    /// </summary>
    public abstract class SplitBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 是否垂直分割
        /// </summary>
        [Parameter]
        public bool IsVertical { get; set; }

        /// <summary>
        /// 获得/设置 第一个窗格初始化位置占比 默认为 50%
        /// </summary>
        [Parameter]
        public string Basis { get; set; } = "50%";

        /// <summary>
        /// 获得/设置 第一个窗格模板
        /// </summary>
        [Parameter]
        public RenderFragment? FirstPaneTemplate { get; set; }

        /// <summary>
        /// 获得/设置 第二个窗格模板
        /// </summary>
        [Parameter]
        public RenderFragment? SecondPaneTemplate { get; set; }
    }
}
