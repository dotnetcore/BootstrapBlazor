// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ScrollIntoViewOptions 配置类</para>
/// <para lang="en">ScrollIntoViewOptions configuration class</para>
/// </summary>
public class ScrollIntoViewOptions
{
    /// <summary>
    /// <para lang="zh">Determines 是否 scrolling is instant or animates smoothly
    ///</para>
    /// <para lang="en">Determines whether scrolling is instant or animates smoothly
    ///</para>
    /// </summary>
    public ScrollIntoViewBehavior Behavior { get; set; }

    /// <summary>
    /// <para lang="zh">Defines the vertical alignment of the element within the scrollable ancestor container
    ///</para>
    /// <para lang="en">Defines the vertical alignment of the element within the scrollable ancestor container
    ///</para>
    /// </summary>
    public ScrollIntoViewBlock Block { get; set; }

    /// <summary>
    /// <para lang="zh">Defines the horizontal alignment of the element within the scrollable ancestor container
    ///</para>
    /// <para lang="en">Defines the horizontal alignment of the element within the scrollable ancestor container
    ///</para>
    /// </summary>
    public ScrollIntoViewInline Inline { get; set; }
}

/// <summary>
/// <para lang="zh">Determines 是否 scrolling is instant or animates smoothly. This option is a string which must take one of the following values
///</para>
/// <para lang="en">Determines whether scrolling is instant or animates smoothly. This option is a string which must take one of the following values
///</para>
/// </summary>
[JsonEnumConverter(true)]
public enum ScrollIntoViewBehavior
{
    /// <summary>
    /// <para lang="zh">scrolling should animate smoothly
    ///</para>
    /// <para lang="en">scrolling should animate smoothly
    ///</para>
    /// </summary>
    [Description("smooth")]
    Smooth,

    /// <summary>
    /// <para lang="zh">scrolling should happen instantly in a single jump
    ///</para>
    /// <para lang="en">scrolling should happen instantly in a single jump
    ///</para>
    /// </summary>
    [Description("instant")]
    Instant,

    /// <summary>
    /// <para lang="zh">scroll behavior is determined by the computed value of scroll-behavior
    ///</para>
    /// <para lang="en">scroll behavior is determined by the computed value of scroll-behavior
    ///</para>
    /// </summary>
    [Description("auto")]
    Auto
}

/// <summary>
/// <para lang="zh">Defines vertical alignment
///</para>
/// <para lang="en">Defines vertical alignment
///</para>
/// </summary>
[JsonEnumConverter(true)]
public enum ScrollIntoViewBlock
{
    /// <summary>
    /// <para lang="zh">Start
    ///</para>
    /// <para lang="en">Start
    ///</para>
    /// </summary>
    [Description("start")]
    Start,

    /// <summary>
    /// <para lang="zh">Center
    ///</para>
    /// <para lang="en">Center
    ///</para>
    /// </summary>
    [Description("center")]
    Center,

    /// <summary>
    /// <para lang="zh">End
    ///</para>
    /// <para lang="en">End
    ///</para>
    /// </summary>
    [Description("end")]
    End,

    /// <summary>
    /// <para lang="zh">Nearest
    ///</para>
    /// <para lang="en">Nearest
    ///</para>
    /// </summary>
    [Description("nearest")]
    Nearest
}

/// <summary>
/// <para lang="zh">Defines horizontal alignment
///</para>
/// <para lang="en">Defines horizontal alignment
///</para>
/// </summary>
[JsonEnumConverter(true)]
public enum ScrollIntoViewInline
{
    /// <summary>
    /// <para lang="zh">Start
    ///</para>
    /// <para lang="en">Start
    ///</para>
    /// </summary>
    [Description("start")]
    Start,

    /// <summary>
    /// <para lang="zh">Center
    ///</para>
    /// <para lang="en">Center
    ///</para>
    /// </summary>
    [Description("center")]
    Center,

    /// <summary>
    /// <para lang="zh">End
    ///</para>
    /// <para lang="en">End
    ///</para>
    /// </summary>
    [Description("end")]
    End,

    /// <summary>
    /// <para lang="zh">Nearest
    ///</para>
    /// <para lang="en">Nearest
    ///</para>
    /// </summary>
    [Description("nearest")]
    Nearest
}
