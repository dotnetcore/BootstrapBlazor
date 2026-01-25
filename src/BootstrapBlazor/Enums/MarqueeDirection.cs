// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Marquee组件，文本滚动方向枚举类型 <para>Marquee component, Text scrolling direction enumeration</para>
///</para>
/// <para lang="en">Marqueecomponent，文本滚动方向enumtype <para>Marquee component, Text scrolling direction enumeration</para>
///</para>
/// </summary>
public enum MarqueeDirection
{
    /// <summary>
    /// <para lang="zh">文本从左向右滚动 scrolling text from left to right</para>
    /// <para lang="en">文本从左向右滚动 scrolling text from left to right</para>
    /// </summary>
    [Description("LeftToRight")]
    LeftToRight,

    /// <summary>
    /// <para lang="zh">文本从右向左滚动 scrolling text from right to left</para>
    /// <para lang="en">文本从右向左滚动 scrolling text from right to left</para>
    /// </summary>
    [Description("RightToLeft")]
    RightToLeft,

    /// <summary>
    /// <para lang="zh">Marquee组件，从上到下滚动文本 scrolling text from top to bottom</para>
    /// <para lang="en">Marqueecomponent，从上到下滚动文本 scrolling text from top to bottom</para>
    /// </summary>
    [Description("TopToBottom")]
    TopToBottom,

    /// <summary>
    /// <para lang="zh">从下至上滚动文本> Scroll text from bottom to top</para>
    /// <para lang="en">从下至上滚动文本> Scroll text from bottom to top</para>
    /// </summary>
    [Description("BottomToTop")]
    BottomToTop
}
