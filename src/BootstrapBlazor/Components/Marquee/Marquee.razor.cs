// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

public partial class Marquee
{
    /// <summary>
    /// 
    /// </summary>
    [Parameter] public string Text { get; set; } = string.Empty;

    /// <summary>
    /// 
    /// </summary>
    [Parameter] public string Color { get; set; } = "black";

    /// <summary>
    /// 
    /// </summary>
    [Parameter] public string BackgroundColor { get; set; } = "white";

    /// <summary>
    /// 
    /// </summary>
    [Parameter] public int FontSize { get; set; } = 72;

    /// <summary>
    /// 
    /// </summary>
    [Parameter] public int Speed { get; set; } = 14;

    /// <summary>
    /// 
    /// </summary>
    [Parameter] public MarqueeDirecTion DirecTion { get; set; } = MarqueeDirecTion.LeftToRight;
}
