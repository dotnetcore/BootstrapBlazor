// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.Core.Converter;

namespace BootstrapBlazor.Components;

/// <summary>
/// ScrollIntoViewOptions 配置类
/// </summary>
public class ScrollIntoViewOptions
{
    /// <summary>
    /// 获得/设置 滚动条宽度 默认 5px
    /// </summary>
    public ScrollIntoViewBehavior Behavior { get; set; }

    /// <summary>
    /// 获得/设置 滚动条鼠标悬浮宽度 默认 5px
    /// </summary>
    public ScrollIntoViewBlock Block { get; set; }

    /// <summary>
    /// 获得/设置 滚动条鼠标悬浮宽度 默认 5px
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
