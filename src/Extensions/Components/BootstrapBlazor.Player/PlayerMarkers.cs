// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Player 组件预览图类
/// </summary>
public class PlayerMarkers
{
    /// <summary>
    /// get or set Whether to enable markers
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// 获得 标记点集合
    /// </summary>
    public List<PlayerPoint> Points { get; } = [];
}
