// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Size 枚举类型</para>
///  <para lang="en">Size enumtype</para>
/// </summary>
public enum FullScreenSize
{
    /// <summary>
    ///  <para lang="zh">无设置</para>
    ///  <para lang="en">无Sets</para>
    /// </summary>
    None,

    /// <summary>
    ///  <para lang="zh">始终全屏</para>
    ///  <para lang="en">始终全屏</para>
    /// </summary>
    [Description("fullscreen")]
    Always,

    /// <summary>
    ///  <para lang="zh">sm 小设置小于 576px</para>
    ///  <para lang="en">sm 小Sets小于 576px</para>
    /// </summary>
    [Description("fullscreen-sm-down")]
    Small,

    /// <summary>
    ///  <para lang="zh">md 中等设置小于 768px</para>
    ///  <para lang="en">md 中等Sets小于 768px</para>
    /// </summary>
    [Description("fullscreen-md-down")]
    Medium,

    /// <summary>
    ///  <para lang="zh">lg 大设置小于 992px</para>
    ///  <para lang="en">lg 大Sets小于 992px</para>
    /// </summary>
    [Description("fullscreen-lg-down")]
    Large,

    /// <summary>
    ///  <para lang="zh">xl 超大设置小于 1200px</para>
    ///  <para lang="en">xl 超大Sets小于 1200px</para>
    /// </summary>
    [Description("fullscreen-xl-down")]
    ExtraLarge,

    /// <summary>
    ///  <para lang="zh">xxl 超大设置小于 1400px</para>
    ///  <para lang="en">xxl 超大Sets小于 1400px</para>
    /// </summary>
    [Description("fullscreen-xxl-down")]
    ExtraExtraLarge
}
