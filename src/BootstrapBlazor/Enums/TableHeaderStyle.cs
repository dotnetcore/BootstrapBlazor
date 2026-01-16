// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">表格 thead 样式枚举</para>
///  <para lang="en">表格 thead styleenum</para>
/// </summary>
public enum TableHeaderStyle
{
    /// <summary>
    ///  <para lang="zh">未设置</para>
    ///  <para lang="en">未Sets</para>
    /// </summary>
    None,
    /// <summary>
    ///  <para lang="zh">浅色</para>
    ///  <para lang="en">浅色</para>
    /// </summary>
    [Description("table-light")]
    Light,

    /// <summary>
    ///  <para lang="zh">深色</para>
    ///  <para lang="en">深色</para>
    /// </summary>
    [Description("table-dark")]
    Dark
}
