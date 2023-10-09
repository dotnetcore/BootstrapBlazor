// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
