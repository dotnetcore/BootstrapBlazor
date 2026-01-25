// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Size 枚举类型</para>
/// <para lang="en">Size enumtype</para>
/// </summary>
public enum Size
{
    /// <summary>
    /// <para lang="zh">无设置</para>
    /// <para lang="en">无Sets</para>
    /// </summary>
    None,

    /// <summary>
    /// <para lang="zh">xs 超小设置小于 576px</para>
    /// <para lang="en">xs 超小Sets小于 576px</para>
    /// </summary>
    [Description("xs")]
    ExtraSmall,

    /// <summary>
    /// <para lang="zh">sm 小设置大于等于 576px</para>
    /// <para lang="en">sm 小Sets大于等于 576px</para>
    /// </summary>
    [Description("sm")]
    Small,

    /// <summary>
    /// <para lang="zh">md 中等设置大于等于 768px</para>
    /// <para lang="en">md 中等Sets大于等于 768px</para>
    /// </summary>
    [Description("md")]
    Medium,

    /// <summary>
    /// <para lang="zh">lg 大设置大于等于 992px</para>
    /// <para lang="en">lg 大Sets大于等于 992px</para>
    /// </summary>
    [Description("lg")]
    Large,

    /// <summary>
    /// <para lang="zh">xl 超大设置大于等于 1200px</para>
    /// <para lang="en">xl 超大Sets大于等于 1200px</para>
    /// </summary>
    [Description("xl")]
    ExtraLarge,

    /// <summary>
    /// <para lang="zh">xl 超大设置大于等于 1400px</para>
    /// <para lang="en">xl 超大Sets大于等于 1400px</para>
    /// </summary>
    [Description("xxl")]
    ExtraExtraLarge
}
