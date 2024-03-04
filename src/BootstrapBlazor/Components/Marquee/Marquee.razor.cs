// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Marquee 字幕滚动组件
/// </summary>
public partial class Marquee
{
    /// <summary>
    /// 获得/设置 组件值 显示文本 默认 Empty
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 组件值 文本颜色 默认 #000
    /// <para>支持16进制和颜色名称</para>
    /// </summary>
    [Parameter]
    public string Color { get; set; } = "#000";

    /// <summary>
    /// 获得/设置 组件值 背景颜色 默认 #fff
    /// <para>支持16进制和颜色名称</para>
    /// </summary>
    [Parameter]
    public string BackgroundColor { get; set; } = "#fff";

    /// <summary>
    /// 获得/设置 组件值 文本大小 默认 72px
    /// </summary>
    [Parameter]
    public int FontSize { get; set; } = 72;

    /// <summary>
    /// 获得/设置 组件值 动画时间 默认 14s
    /// <para>值越小滚动越快</para>
    /// </summary>
    [Parameter]
    public int Duration { get; set; } = 14;

    /// <summary>
    /// 获得/设置 组件值 滚动方向 默认 LeftToRight
    /// </summary>
    [Parameter]
    public MarqueeDirection Direction { get; set; }

    private string? ClassString => CssBuilder.Default("marquee")
        .AddClass("marquee-vertical", Direction == MarqueeDirection.TopToBottom || Direction == MarqueeDirection.BottomToTop)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default()
        .AddClass($"background-color: {BackgroundColor};")
        .AddClass($"color: {Color};")
        .AddClass($"font-size: {FontSize}px;")
        .Build();

    private string? TextClassString => CssBuilder.Default("marquee-text")
        .AddClass($"marquee-text-left", Direction == MarqueeDirection.LeftToRight || Direction == MarqueeDirection.RightToLeft)
        .AddClass($"marquee-text-top", Direction == MarqueeDirection.TopToBottom || Direction == MarqueeDirection.BottomToTop)
        .Build();

    private string? TextStyleString => CssBuilder.Default()
        .AddClass($"animation-duration: {Duration}s;")
        .AddClass($"animation-name: {Direction.ToDescriptionString()};")
        .Build();
}
