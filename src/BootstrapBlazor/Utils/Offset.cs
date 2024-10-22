// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 物体偏移量类
/// </summary>
public class Offset
{
    /// <summary>
    /// 获得/设置 X 轴偏移量
    /// </summary>
    public int Top { get; set; }

    /// <summary>
    /// 获得/设置 Y 轴偏移量
    /// </summary>
    public int Left { get; set; }

    /// <summary>
    /// 获得/设置 宽度
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// 获得/设置 高度
    /// </summary>
    public int Height { get; set; }
}
