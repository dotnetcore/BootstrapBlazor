// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// Marquee组件，文本滚动方向枚举类型
/// <para>Marquee component, Text scrolling direction enumeration</para>
/// </summary>
public enum MarqueeDirection
{
    /// <summary>
    /// 文本从左向右滚动
    /// scrolling text from left to right
    /// </summary>
    [Description("LeftToRight")]
    LeftToRight,

    /// <summary>
    /// 文本从右向左滚动
    /// scrolling text from right to left
    /// </summary>
    [Description("RightToLeft")]
    RightToLeft,

    /// <summary>
    /// Marquee组件，从上到下滚动文本
    /// scrolling text from top to bottom
    /// </summary>
    [Description("TopToBottom")]
    TopToBottom,

    /// <summary>
    /// 从下至上滚动文本>
    /// Scroll text from bottom to top
    /// </summary>
    [Description("BottomToTop")]
    BottomToTop
}
