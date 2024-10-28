// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

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
