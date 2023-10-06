// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para>Marquee组件，文本滚动方向枚举类型</para>
/// <para>Marquee component，Text scrolling direction enumeration</para>
/// </summary>
public enum MarqueeDirecTion1
{
    /// <summary>
    /// <para>Marquee组件，文本从左向右滚动</para>
    /// <para>Marquee component，Text scrolling from left to right</para>
    /// </summary>
    [Description("lefttoright")] LeftToRight,

    /// <summary>
    /// <para>Marquee组件，文本从右向左滚动</para>
    /// <para>Marquee component，Text scrolling from right to left</para>
    /// </summary>
    [Description("righttoleft")] RightToLeft,

    /// <summary>
    ///<para>Marquee组件，从上到下滚动文本</para>
    ///<para>Marquee component，Scrolling text from top to bottom</para> 
    /// </summary>
    [Description("toptobottom")] TopToBottom,

    /// <summary>
    /// <para>Marquee组件，从下至上滚动文本</para>
    /// <para>Marquee component，Scroll text from bottom to top</para>
    /// </summary>
    [Description("bottomtotop")] BottomToTop
}
