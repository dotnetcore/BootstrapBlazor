// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Table 视图枚举类型
    /// </summary>
    public enum TableRenderMode
    {
        /// <summary>
        /// 自动
        /// </summary>
        [Description("自动")]
        Auto,

        /// <summary>
        /// Table 布局适用于大屏幕
        /// </summary>
        [Description("表格布局")]
        Table,

        /// <summary>
        /// 卡片式布局适用于小屏幕
        /// </summary>
        [Description("卡片布局")]
        CardView
    }
}
