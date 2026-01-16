// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">对齐方式枚举类型</para>
/// <para lang="en">Alignment Enum</para>
/// </summary>
public enum Alignment
{
    /// <summary>
    /// <para lang="zh">未设置</para>
    /// <para lang="en">Not Set</para>
    /// </summary>
    None,

    /// <summary>
    /// <para lang="zh">左对齐</para>
    /// <para lang="en">Left Align</para>
    /// </summary>
    [Description("start")]
    Left,

    /// <summary>
    /// <para lang="zh">居中对齐</para>
    /// <para lang="en">Center Align</para>
    /// </summary>
    [Description("center")]
    Center,

    /// <summary>
    /// <para lang="zh">右对齐</para>
    /// <para lang="en">Right Align</para>
    /// </summary>
    [Description("end")]
    Right
}
