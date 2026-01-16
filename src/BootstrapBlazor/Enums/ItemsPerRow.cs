// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">每行显示多少组件的枚举</para>
/// <para lang="en">每行display多少component的enum</para>
/// </summary>
public enum ItemsPerRow
{
    /// <summary>
    /// <para lang="zh">每行一个</para>
    /// <para lang="en">每行一个</para>
    /// </summary>
    [Description("12")]
    One,

    /// <summary>
    /// <para lang="zh">每行两个</para>
    /// <para lang="en">每行两个</para>
    /// </summary>
    [Description("6")]
    Two,

    /// <summary>
    /// <para lang="zh">每行三个</para>
    /// <para lang="en">每行三个</para>
    /// </summary>
    [Description("4")]
    Three,

    /// <summary>
    /// <para lang="zh">每行四个</para>
    /// <para lang="en">每行四个</para>
    /// </summary>
    [Description("3")]
    Four,

    /// <summary>
    /// <para lang="zh">每行六个</para>
    /// <para lang="en">每行六个</para>
    /// </summary>
    [Description("2")]
    Six,

    /// <summary>
    /// <para lang="zh">每行12个</para>
    /// <para lang="en">每行12个</para>
    /// </summary>
    [Description("1")]
    Twelve
}
