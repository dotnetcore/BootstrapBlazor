// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ScrollIntoViewOptions 配置类
/// </summary>
public class ScrollIntoViewOptions
{
    /// <summary>
    /// Determines whether scrolling is instant or animates smoothly
    /// </summary>
    public ScrollIntoViewBehavior Behavior { get; set; }

    /// <summary>
    /// Defines the vertical alignment of the element within the scrollable ancestor container
    /// </summary>
    public ScrollIntoViewBlock Block { get; set; }

    /// <summary>
    /// Defines the horizontal alignment of the element within the scrollable ancestor container
    /// </summary>
    public ScrollIntoViewInline Inline { get; set; }
}

/// <summary>
/// Determines whether scrolling is instant or animates smoothly. This option is a string which must take one of the following values
/// </summary>
[JsonEnumConverter(true)]
public enum ScrollIntoViewBehavior
{
    /// <summary>
    /// scrolling should animate smoothly
    /// </summary>
    Smooth,

    /// <summary>
    /// scrolling should happen instantly in a single jump
    /// </summary>
    Instant,

    /// <summary>
    /// scroll behavior is determined by the computed value of scroll-behavior
    /// </summary>
    Auto
}

/// <summary>
/// Defines vertical alignment
/// </summary>
[JsonEnumConverter(true)]
public enum ScrollIntoViewBlock
{
    /// <summary>
    /// Start
    /// </summary>
    Start,

    /// <summary>
    /// Center
    /// </summary>
    Center,

    /// <summary>
    /// End
    /// </summary>
    End,

    /// <summary>
    /// Nearest
    /// </summary>
    Nearest
}

/// <summary>
/// Defines horizontal alignment
/// </summary>
[JsonEnumConverter(true)]
public enum ScrollIntoViewInline
{
    /// <summary>
    /// Start
    /// </summary>
    Start,

    /// <summary>
    /// Center
    /// </summary>
    Center,

    /// <summary>
    /// End
    /// </summary>
    End,

    /// <summary>
    /// Nearest
    /// </summary>
    Nearest
}
