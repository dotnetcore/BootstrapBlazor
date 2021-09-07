// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Print 配置类
    /// </summary>
    public class PrintOption
    {
        /// <summary>
        /// 获得/设置 打印内容
        /// </summary>
        public RenderFragment? PrintBody { get; set; }

        /// <summary>
        /// 获得/设置 打印组件
        /// </summary>
        public BootstrapDynamicComponent? PrintComponent { get; set; }

        /// <summary>
        /// 获得/设置 是否显示打印预览弹窗
        /// </summary>
        public bool ShowPrintViewDialog { get; set; }
    }
}
