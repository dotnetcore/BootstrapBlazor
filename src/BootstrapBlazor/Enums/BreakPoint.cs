// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">BreakPoint 枚举</para>
/// <para lang="en">BreakPoint Enum</para>
/// </summary>
[JsonEnumConverter(true)]
public enum BreakPoint
{
    /// <summary>
    /// <para lang="zh">未设置</para>
    /// <para lang="en">Not Set</para>
    /// </summary>
    None,

    /// <summary>
    /// <para lang="zh">超小屏幕 小于 375px</para>
    /// <para lang="en">Extra extra small screen &lt; 375px</para>
    /// </summary>
    ExtraExtraSmall,

    /// <summary>
    /// <para lang="zh">超小屏幕 大于等于 375px</para>
    /// <para lang="en">Extra small screen &gt;= 375px</para>
    /// </summary>
    ExtraSmall,

    /// <summary>
    /// <para lang="zh">小屏幕 大于等于 576px</para>
    /// <para lang="en">Small screen &gt;= 576px</para>
    /// </summary>
    Small,

    /// <summary>
    /// <para lang="zh">中屏幕 大于等于 768px</para>
    /// <para lang="en">Medium screen &gt;= 768px</para>
    /// </summary>
    Medium,

    /// <summary>
    /// <para lang="zh">大屏幕 大于等于 992px</para>
    /// <para lang="en">Large screen &gt;= 992px</para>
    /// </summary>
    Large,

    /// <summary>
    /// <para lang="zh">超大屏幕 大于等于 1200px</para>
    /// <para lang="en">Extra large screen &gt;= 1200px</para>
    /// </summary>
    ExtraLarge,

    /// <summary>
    /// <para lang="zh">超大屏幕 大于等于 1400px</para>
    /// <para lang="en">Extra extra large screen &gt;= 1400px</para>
    /// </summary>
    ExtraExtraLarge
}
