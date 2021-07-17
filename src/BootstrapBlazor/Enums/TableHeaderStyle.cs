// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 表格 thead 样式枚举
    /// </summary>
    public enum TableHeaderStyle
    {
        /// <summary>
        /// 未设置
        /// </summary>
        None,
        /// <summary>
        /// 浅色
        /// </summary>
        [Description("table-light")]
        Light,

        /// <summary>
        /// 深色
        /// </summary>
        [Description("table-dark")]
        Dark
    }
}
