// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// Display 显示
/// </summary>
public class L2DwidgetDisplay
{
    /// <summary>
    /// rate for super sampling rate 超采样等级
    /// </summary>
    public double? SuperSample { get; set; }

    /// <summary>
    /// Width to the canvas which shows the model canvas的长度
    /// </summary>
    public double? Width { get; set; }

    /// <summary>
    /// Height to the canvas which shows the model canvas的高度
    /// </summary>
    public double? Height { get; set; }

    /// <summary>
    /// Left of right side to show 显示位置：左或右
    /// </summary>
    [JsonConverter(typeof(EnumDescriptionConverter<L2DwidgetDisplayPosition>))]
    public L2DwidgetDisplayPosition Position { get; set; } = L2DwidgetDisplayPosition.RightBottom;

    /// <summary>
    /// Horizontal offset of the canvas canvas水平偏移
    /// </summary>
    public double HOffset { get; set; } = 0;

    /// <summary>
    /// Vertical offset of the canvas canvas垂直偏移
    /// </summary>
    public double VOffset { get; set; } = -20;


}
