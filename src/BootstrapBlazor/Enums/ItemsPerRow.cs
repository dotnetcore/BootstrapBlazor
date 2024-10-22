// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 每行显示多少组件的枚举
/// </summary>
public enum ItemsPerRow
{
    /// <summary>
    /// 每行一个
    /// </summary>
    [Description("12")]
    One,

    /// <summary>
    /// 每行两个
    /// </summary>
    [Description("6")]
    Two,

    /// <summary>
    /// 每行三个
    /// </summary>
    [Description("4")]
    Three,

    /// <summary>
    /// 每行四个
    /// </summary>
    [Description("3")]
    Four,

    /// <summary>
    /// 每行六个
    /// </summary>
    [Description("2")]
    Six,

    /// <summary>
    /// 每行12个
    /// </summary>
    [Description("1")]
    Twelve
}
