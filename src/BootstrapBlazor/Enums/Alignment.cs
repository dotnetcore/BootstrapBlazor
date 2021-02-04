// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 对齐方式枚举类型
    /// </summary>
    public enum Alignment
    {
        /// <summary>
        /// 
        /// </summary>
        None,

        /// <summary>
        /// 
        /// </summary>
        [Description("left")]
        Left,

        /// <summary>
        /// 
        /// </summary>
        [Description("center")]
        Center,

        /// <summary>
        /// 
        /// </summary>
        [Description("right")]
        Right
    }
}
