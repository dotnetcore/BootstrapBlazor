// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

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
