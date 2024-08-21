﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// WinBox 弹窗配置类
/// </summary>
public class WinBoxOption
{
    /// <summary>
    /// Set the initial z-index of the window to this value (will be increased automatically when unfocused/focused).
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Index { get; set; }

    /// <summary>
    /// Set a unique id to the window. Used to define custom styles in css, query elements by context or just to identify the corresponding window instance. If no ID was set it will automatically create one for you.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Root { get; set; }

    /// <summary>
    /// Add one or more class names to the window (multiple class names as array or separated with whitespaces e.g. "class-a class-b"). Used to define custom styles in css, query elements by context (also within CSS) or just to tag the corresponding window instance.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Class { get; set; }

    /// <summary>
    /// The window title.
    /// </summary>
    // appearance:
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Title { get; set; }

    /// <summary>
    /// Set the background of the window (supports all CSS styles which are also supported by the style-attribute "background", e.g. colors, transparent colors, hsl, gradients, background images)
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Background { get; set; }

    /// <summary>
    /// Set the border width of the window (supports all the browsers css units).
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Border { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Header { get; set; }

    /// <summary>
    /// Make the title bar icon visible and set the image source to this url.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Icon { get; set; }

    /// <summary>
    /// Shows the window as modal.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Modal { get; set; }

    /// <summary>
    /// Automatically toggles the window into maximized state when created.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Max { get; set; }

    /// <summary>
    /// Automatically toggles the window into minimized state when created.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Min { get; set; }

    /// <summary>
    /// Automatically toggles the window into hidden state when created.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public bool? Hidden { get; set; }

    /// <summary>
    /// Set the initial width/height of the window (supports units "px" and "%").
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Width { get; set; }

    /// <summary>
    /// Set the initial width/height of the window (supports units "px" and "%").
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Height { get; set; }

    /// <summary>
    /// Set the minimal width/height of the window (supports units "px" and "%"). Should be at least the height of the window header title bar.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("minheight")]
    public object? MinHeight { get; set; }

    /// <summary>
    /// Set the minimal width/height of the window (supports units "px" and "%"). Should be at least the height of the window header title bar.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("minwidth")]
    public object? MinWidth { get; set; }

    /// <summary>
    /// Set the maximum width/height of the window (supports units "px" and "%").
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("maxheight")]
    public object? MaxHeight { get; set; }

    /// <summary>
    /// Set the maximum width/height of the window (supports units "px" and "%").
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("maxwidth")]
    public object? MaxWidth { get; set; }

    /// <summary>
    /// Automatically size the window to fit the window contents.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("autosize")]
    public bool? AutoSize { get; set; }

    /// <summary>
    /// Set the initial position of the window (supports: "right" for x-axis, "bottom" for y-axis, "center" for both, units "px" and "%" for both).
    /// </summary>
    // position:
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? X { get; set; }

    /// <summary>
    /// Set the initial position of the window (supports: "right" for x-axis, "bottom" for y-axis, "center" for both, units "px" and "%" for both).
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Y { get; set; }

    /// <summary>
    /// Set or limit the viewport of the window's available area (supports units "px" and "%"). Also used for custom splitscreen configurations.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Top { get; set; }

    /// <summary>
    /// Set or limit the viewport of the window's available area (supports units "px" and "%"). Also used for custom splitscreen configurations.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Right { get; set; }

    /// <summary>
    /// Set or limit the viewport of the window's available area (supports units "px" and "%"). Also used for custom splitscreen configurations.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Bottom { get; set; }

    /// <summary>
    /// Set or limit the viewport of the window's available area (supports units "px" and "%"). Also used for custom splitscreen configurations.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Left { get; set; }

    /// <summary>
    /// Open URL inside the window (loaded via iframe).
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public object? Url { get; set; }

    /// <summary>
    /// Mount an element (widget, template, etc.) to the window body.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Mount { get; set; }

    /// <summary>
    /// Set the innerHTML of the window body.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Html { get; set; }
}
