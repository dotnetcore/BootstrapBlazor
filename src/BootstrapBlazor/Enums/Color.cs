// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 颜色枚举类型
    /// </summary>
    public enum Color
    {
        /// <summary>
        /// 无颜色
        /// </summary>
        None,

        /// <summary>
        /// active
        /// </summary>
        [Description("active")]
        Active,

        /// <summary>
        /// primary
        /// </summary>
        [Description("primary")]
        Primary,

        /// <summary>
        /// secondary
        /// </summary>
        [Description("secondary")]
        Secondary,

        /// <summary>
        /// success
        /// </summary>
        [Description("success")]
        Success,

        /// <summary>
        /// danger
        /// </summary>
        [Description("danger")]
        Danger,

        /// <summary>
        /// warning
        /// </summary>
        [Description("warning")]
        Warning,

        /// <summary>
        /// info
        /// </summary>
        [Description("info")]
        Info,

        /// <summary>
        /// light
        /// </summary>
        [Description("light")]
        Light,

        /// <summary>
        /// dark
        /// </summary>
        [Description("dark")]
        Dark,

        /// <summary>
        /// link
        /// </summary>
        [Description("link")]
        Link
    }
}
