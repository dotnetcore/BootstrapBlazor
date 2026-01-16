// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IHtml2ImageOptions 选项类接口</para>
/// <para lang="en">IHtml2ImageOptions Class Interface</para>
/// </summary>
public interface IHtml2ImageOptions
{
    /// <summary>
    /// <para lang="zh">Width in pixels to be applied to node before rendering.
    ///</para>
    /// <para lang="en">Width in pixels to be applied to node before rendering.
    ///</para>
    /// </summary>
    int? Width { get; set; }

    /// <summary>
    /// <para lang="zh">Height in pixels to be applied to node before rendering.
    ///</para>
    /// <para lang="en">Height in pixels to be applied to node before rendering.
    ///</para>
    /// </summary>
    int? Height { get; set; }

    /// <summary>
    /// <para lang="zh">A string value for the background 颜色, any valid CSS 颜色 value.
    ///</para>
    /// <para lang="en">A string value for the background color, any valid CSS color value.
    ///</para>
    /// </summary>
    string? BackgroundColor { get; set; }

    /// <summary>
    /// <para lang="zh">Width in pixels to be applied to canvas on export.
    ///</para>
    /// <para lang="en">Width in pixels to be applied to canvas on export.
    ///</para>
    /// </summary>
    int? CanvasWidth { get; set; }

    /// <summary>
    /// <para lang="zh">Height in pixels to be applied to canvas on export.
    ///</para>
    /// <para lang="en">Height in pixels to be applied to canvas on export.
    ///</para>
    /// </summary>
    int? CanvasHeight { get; set; }

    /// <summary>
    /// <para lang="zh">An array of 样式 properties to be copied to node's 样式 before rendering.
    /// For performance-critical scenarios, users may want to specify only the required properties instead of all 样式s.
    ///</para>
    /// <para lang="en">An array of style properties to be copied to node's style before rendering.
    /// For performance-critical scenarios, users may want to specify only the required properties instead of all styles.
    ///</para>
    /// </summary>
    string[]? IncludeStyleProperties { get; set; }

    /// <summary>
    /// <para lang="zh">A number between `0` and `1` indicating image quality (e.g. 0.92 => 92%)
    /// of the JPEG image.
    ///</para>
    /// <para lang="en">A number between `0` and `1` indicating image quality (e.g. 0.92 => 92%)
    /// of the JPEG image.
    ///</para>
    /// </summary>
    double? Quality { get; set; }

    /// <summary>
    /// <para lang="zh">pixel ratio of captured image. 默认为 the actual pixel ratio of
    /// the device. Set 1 to use as initial-scale 1 for the image
    ///</para>
    /// <para lang="en">The pixel ratio of captured image. Default is the actual pixel ratio of
    /// the device. Set 1 to use as initial-scale 1 for the image
    ///</para>
    /// </summary>
    double? PixelRatio { get; set; }

    /// <summary>
    /// <para lang="zh">A string indicating the image format. default 类型 is image/png; that 类型 is also used if the given 类型 isn't supported.
    ///</para>
    /// <para lang="en">A string indicating the image format. The default type is image/png; that type is also used if the given type isn't supported.
    ///</para>
    /// </summary>
    string? Type { get; set; }
}
