// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Marquee 字幕滚动组件</para>
///  <para lang="en">Marquee 字幕滚动component</para>
/// </summary>
public partial class Marquee
{
    /// <summary>
    ///  <para lang="zh">获得/设置 组件值 显示文本 默认 Empty</para>
    ///  <para lang="en">Gets or sets component值 display文本 Default is Empty</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 组件值 文本颜色 默认 #000 <para>支持16进制和颜色名称</para>
    ///</para>
    ///  <para lang="en">Gets or sets component值 文本color Default is #000 <para>支持16进制和color名称</para>
    ///</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string Color { get; set; } = "#000";

    /// <summary>
    ///  <para lang="zh">获得/设置 组件值 背景颜色 默认 #fff <para>支持16进制和颜色名称</para>
    ///</para>
    ///  <para lang="en">Gets or sets component值 背景color Default is #fff <para>支持16进制和color名称</para>
    ///</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string BackgroundColor { get; set; } = "#fff";

    /// <summary>
    ///  <para lang="zh">获得/设置 组件值 文本大小 默认 72px</para>
    ///  <para lang="en">Gets or sets component值 文本大小 Default is 72px</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int FontSize { get; set; } = 72;

    /// <summary>
    ///  <para lang="zh">获得/设置 组件值 动画时间 默认 14s <para>值越小滚动越快</para>
    ///</para>
    ///  <para lang="en">Gets or sets component值 动画时间 Default is 14s <para>值越小滚动越快</para>
    ///</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public int Duration { get; set; } = 14;

    /// <summary>
    ///  <para lang="zh">获得/设置 组件值 滚动方向 默认 LeftToRight</para>
    ///  <para lang="en">Gets or sets component值 滚动方向 Default is LeftToRight</para>
    ///  <para><version>10.2.2</version></para>
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
