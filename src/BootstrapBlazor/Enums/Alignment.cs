// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 对齐方式枚举类型
/// </summary>
public enum Alignment
{
    /// <summary>
    /// 未设置
    /// </summary>
    None,

    /// <summary>
    /// 左对齐
    /// </summary>
    [Description("start")]
    Left,

    /// <summary>
    /// 居中对齐
    /// </summary>
    [Description("center")]
    Center,

    /// <summary>
    /// 右对齐
    /// </summary>
    [Description("end")]
    Right
}
