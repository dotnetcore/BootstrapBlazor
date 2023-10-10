// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Stack
{
    private string? ClassValue => CssBuilder.Default()
        .AddClass("stack-vertical", Orientation == Orientation.Vertical)
        .AddClass("stack-horizontal", Orientation == Orientation.Horizontal)
        .Build();

    private string? StyleValue => new StyleBuilder()
        .AddStyle("align-items", GetHorizontalAlignment(), () => Orientation == Orientation.Vertical)
        .AddStyle("justify-content", GetVerticalAlignment(), () => Orientation == Orientation.Vertical)

        .AddStyle("justify-content", GetHorizontalAlignment(), () => Orientation == Orientation.Horizontal)
        .AddStyle("align-items", GetVerticalAlignment(), () => Orientation == Orientation.Horizontal)

        .AddStyle("column-gap", $"{HorizontalGap}px", () => HorizontalGap.HasValue)
        .AddStyle("row-gap", $"{VerticalGap}px", () => VerticalGap.HasValue)
        .AddStyle("width", Width, () => !string.IsNullOrEmpty(Width))
        .AddStyle("flex-wrap", "wrap", () => Wrap)

        .Build();


    /// <summary>
    /// Gets or set the orientation of the stacked components. 
    /// </summary>
    [Parameter]
    public Orientation Orientation { get; set; } = Orientation.Horizontal;

    /// <summary>
    /// The horizontal alignment of the components in the stack. 
    /// </summary>
    [Parameter]
    public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Left;

    /// <summary>
    /// The vertical alignment of the components in the stack.
    /// </summary>
    [Parameter]
    public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.Top;

    /// <summary>
    /// The width of the stack as a percentage string (default = 100%).
    /// </summary>
    [Parameter]
    public string? Width { get; set; } = "100%";

    /// <summary>
    /// Gets or sets if the stack wraps.
    /// </summary>
    [Parameter]
    public bool Wrap { get; set; } = false;

    /// <summary>
    /// Gets or sets the gap between horizontally stacked components (in pixels).
    /// Default is 10 pixels.
    /// </summary>
    [Parameter]
    public int? HorizontalGap { get; set; } = 10;

    /// <summary>
    /// Gets or sets the gap between vertically stacked components (in pixels).
    /// Default is 10 pixels
    /// </summary>
    [Parameter]
    public int? VerticalGap { get; set; } = 10;

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string GetHorizontalAlignment()
    {
        return HorizontalAlignment switch
        {
            HorizontalAlignment.Left => "start",
            HorizontalAlignment.Center => "center",
            HorizontalAlignment.Right => "end",
            _ => "start",
        };
    }

    private string GetVerticalAlignment()
    {
        return VerticalAlignment switch
        {
            VerticalAlignment.Top => "start",
            VerticalAlignment.Center => "center",
            VerticalAlignment.Bottom => "end",
            _ => "start",
        };
    }
}
