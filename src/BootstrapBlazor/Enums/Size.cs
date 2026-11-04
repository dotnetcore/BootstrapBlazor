// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">用于指定组件尺寸或响应式断点的枚举类型</para>
/// <para lang="en">Specifies a component size or responsive breakpoint.</para>
/// </summary>
public enum Size
{
    /// <summary>
    /// <para lang="zh">未指定尺寸</para>
    /// <para lang="en">No size specified.</para>
    /// </summary>
    None,

    /// <summary>
    /// <para lang="zh">超小尺寸；作为断点时适用于小于 576px 的视口</para>
    /// <para lang="en">Extra-small size; as a breakpoint, applies to viewports narrower than 576px.</para>
    /// </summary>
    [Description("xs")]
    ExtraSmall,

    /// <summary>
    /// <para lang="zh">小尺寸；作为断点时适用于宽度大于等于 576px 的视口</para>
    /// <para lang="en">Small size; as a breakpoint, applies to viewports 576px wide and above.</para>
    /// </summary>
    [Description("sm")]
    Small,

    /// <summary>
    /// <para lang="zh">中等尺寸；作为断点时适用于宽度大于等于 768px 的视口</para>
    /// <para lang="en">Medium size; as a breakpoint, applies to viewports 768px wide and above.</para>
    /// </summary>
    [Description("md")]
    Medium,

    /// <summary>
    /// <para lang="zh">大尺寸；作为断点时适用于宽度大于等于 992px 的视口</para>
    /// <para lang="en">Large size; as a breakpoint, applies to viewports 992px wide and above.</para>
    /// </summary>
    [Description("lg")]
    Large,

    /// <summary>
    /// <para lang="zh">超大尺寸；作为断点时适用于宽度大于等于 1200px 的视口</para>
    /// <para lang="en">Extra-large size; as a breakpoint, applies to viewports 1200px wide and above.</para>
    /// </summary>
    [Description("xl")]
    ExtraLarge,

    /// <summary>
    /// <para lang="zh">超超大尺寸；作为断点时适用于宽度大于等于 1400px 的视口</para>
    /// <para lang="en">Extra-extra-large size; as a breakpoint, applies to viewports 1400px wide and above.</para>
    /// </summary>
    [Description("xxl")]
    ExtraExtraLarge
}
