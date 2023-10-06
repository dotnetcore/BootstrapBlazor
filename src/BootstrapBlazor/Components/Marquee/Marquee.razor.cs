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
    [Parameter] public string Text { get; set; } = string.Empty;

    /// <summary>
    /// 获得/设置 组件值 文本颜色 默认 black
    /// <para>支持16进制和颜色名称</para>
    /// </summary>
    [Parameter] public string Color { get; set; } = "black";

    /// <summary>
    /// 获得/设置 组件值 背景颜色 默认 white
    /// <para>支持16进制和颜色名称</para>
    /// </summary>
    [Parameter] public string BackgroundColor { get; set; } = "white";

    /// <summary>
    /// 获得/设置 组件值 文本大小 默认 72
    /// </summary>
    [Parameter] public int FontSize { get; set; } = 72;

    /// <summary>
    /// 获得/设置 组件值 动画时间 默认 14s
    /// <para>值越小滚动越快</para>
    /// </summary>
    [Parameter] public int Duration { get; set; } = 14;

    /// <summary>
    /// 获得/设置 组件值 滚动方向 默认 LeftToRight
    /// </summary>
    [Parameter] public MarqueeDirecTion DirecTion { get; set; } = MarqueeDirecTion.LeftToRight;
}
