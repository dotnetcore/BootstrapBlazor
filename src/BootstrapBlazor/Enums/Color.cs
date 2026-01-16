// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">颜色枚举类型</para>
///  <para lang="en">Color Enum</para>
/// </summary>
public enum Color
{
    /// <summary>
    ///  <para lang="zh">无颜色</para>
    ///  <para lang="en">None</para>
    /// </summary>
    [Description("none")]
    None,

    /// <summary>
    ///  <para lang="zh">active</para>
    ///  <para lang="en">active</para>
    /// </summary>
    [Description("active")]
    Active,

    /// <summary>
    ///  <para lang="zh">primary</para>
    ///  <para lang="en">primary</para>
    /// </summary>
    [Description("primary")]
    Primary,

    /// <summary>
    ///  <para lang="zh">secondary</para>
    ///  <para lang="en">secondary</para>
    /// </summary>
    [Description("secondary")]
    Secondary,

    /// <summary>
    ///  <para lang="zh">success</para>
    ///  <para lang="en">success</para>
    /// </summary>
    [Description("success")]
    Success,

    /// <summary>
    ///  <para lang="zh">danger</para>
    ///  <para lang="en">danger</para>
    /// </summary>
    [Description("danger")]
    Danger,

    /// <summary>
    ///  <para lang="zh">warning</para>
    ///  <para lang="en">warning</para>
    /// </summary>
    [Description("warning")]
    Warning,

    /// <summary>
    ///  <para lang="zh">info</para>
    ///  <para lang="en">info</para>
    /// </summary>
    [Description("info")]
    Info,

    /// <summary>
    ///  <para lang="zh">light</para>
    ///  <para lang="en">light</para>
    /// </summary>
    [Description("light")]
    Light,

    /// <summary>
    ///  <para lang="zh">dark</para>
    ///  <para lang="en">dark</para>
    /// </summary>
    [Description("dark")]
    Dark,

    /// <summary>
    ///  <para lang="zh">link</para>
    ///  <para lang="en">link</para>
    /// </summary>
    [Description("link")]
    Link
}
