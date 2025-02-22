﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Html2ImageOptions 选项类
/// </summary>
public class Html2ImageOptions
{
    /// <summary>
    /// Width in pixels to be applied to node before rendering.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Width { get; set; }

    /// <summary>
    /// Height in pixels to be applied to node before rendering.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Height { get; set; }

    /// <summary>
    /// A string value for the background color, any valid CSS color value.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? BackgroundColor { get; set; }

    /// <summary>
    /// Width in pixels to be applied to canvas on export.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? CanvasWidth { get; set; }

    /// <summary>
    /// Height in pixels to be applied to canvas on export.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? CanvasHeight { get; set; }

    /// <summary>
    /// An array of style properties to be copied to node's style before rendering.
    /// For performance-critical scenarios, users may want to specify only the required properties instead of all styles.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string[]? IncludeStyleProperties { get; set; }

    /// <summary>
    /// A number between `0` and `1` indicating image quality (e.g. 0.92 => 92%)
    /// of the JPEG image.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? Quality { get; set; }

    /// <summary>
    /// The pixel ratio of captured image. Default is the actual pixel ratio of
    /// the device. Set 1 to use as initial-scale 1 for the image
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public double? PixelRatio { get; set; }

    /// <summary>
    /// A string indicating the image format. The default type is image/png; that type is also used if the given type isn't supported.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Type { get; set; }
}
