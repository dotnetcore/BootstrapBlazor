// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">颜色枚举类型</para>
/// <para lang="en">Color Enum</para>
/// </summary>
public enum Color
{
    /// <summary>
    /// <para lang="zh">无颜色</para>
    /// <para lang="en">None</para>
    /// </summary>
    [Description("none")]
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
