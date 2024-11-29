// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples;

/// <summary>
/// Marquee 组件
/// </summary>
public partial class Marquees
{
    private string BackgroundColor { get; set; } = "#000000";

    private string TextColor { get; set; } = "#ff0000";

    private string Text { get; set; } = "BootstrapBlazor 组件库，基于 Bootstrap 样式库精心打造，为您快速开发项目带来飞一般的感觉";

    private int FontSize { get; set; } = 72;

    private int Duration { get; set; } = 20;

    private MarqueeDirection Direction { get; set; }
}
