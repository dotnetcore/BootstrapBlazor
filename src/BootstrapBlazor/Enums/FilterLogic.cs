// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 逻辑运算符
    /// </summary>
    public enum FilterLogic
    {
        /// <summary>
        /// 并且
        /// </summary>
        [Description("并且")]
        And,

        /// <summary>
        /// 或者
        /// </summary>
        [Description("或者")]
        Or
    }
}
