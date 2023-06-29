// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Mobile 移动设备
/// </summary>
public class L2DwidgetMobile
{
    /// <summary>
    /// Whether to show on mobile device 是否在移动设备上显示
    /// </summary>
    public bool Show { get; set; } = true;

    /// <summary>
    /// Scale on mobile device 移动设备上的缩放
    /// </summary>
    public double Scale { get; set; } = 0.5;
}
