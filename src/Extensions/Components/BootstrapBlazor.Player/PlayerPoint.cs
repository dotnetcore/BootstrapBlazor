// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Player 进度条标记点类
/// </summary>
public class PlayerPoint
{
    /// <summary>
    /// get or set represents the marker position in seconds
    /// </summary>
    public int? Time { get; set; }

    /// <summary>
    /// get or set the HTML string to be displayed
    /// </summary>
    public string? Label { get; set; }
}
