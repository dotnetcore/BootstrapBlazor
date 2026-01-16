// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">按钮类型枚举</para>
///  <para lang="en">Button Type Enum</para>
/// </summary>
public enum ButtonType
{
    /// <summary>
    ///  <para lang="zh">正常按钮</para>
    ///  <para lang="en">Button</para>
    /// </summary>
    [Description("button")]
    Button,

    /// <summary>
    ///  <para lang="zh">提交按钮</para>
    ///  <para lang="en">Submit</para>
    /// </summary>
    [Description("submit")]
    Submit,

    /// <summary>
    ///  <para lang="zh">重置按钮</para>
    ///  <para lang="en">Reset</para>
    /// </summary>
    [Description("reset")]
    Reset
}
