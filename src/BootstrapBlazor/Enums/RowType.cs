// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 行内格式枚举
    /// </summary>
    public enum RowType
    {
        /// <summary>
        /// 默认格式
        /// </summary>
        [Description("row")]
        Normal,

        /// <summary>
        /// 表单中使用 label 在左，控件不充满
        /// </summary>
        [Description("inline")]
        Inline
    }
}
