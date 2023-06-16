// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

namespace BootstrapBlazor.Components;

/// <summary>
/// 图标数据点样式枚举
/// </summary>
public enum ChartPointStyle
{
    /// <summary>
    /// 圆圈
    /// </summary>
    [Description("circle")]
    Circle,
    /// <summary>
    /// 交叉
    /// </summary>
    [Description("cross")]
    Cross,
    /// <summary>
    /// 交叉旋转
    /// </summary>
    [Description("crossRot")]
    CrossRot,
    /// <summary>
    /// 点
    /// </summary>
    [Description("dash")]
    Dash,
    /// <summary>
    /// 线
    /// </summary>
    [Description("line")]
    Line,
    /// <summary>
    /// 矩形
    /// </summary>
    [Description("rect")]
    Rect,
    /// <summary>
    /// 矩形圆角
    /// </summary>
    [Description("rectRounded")]
    RectRounded,
    /// <summary>
    /// 矩形旋转
    /// </summary>
    [Description("rectRot")]
    RectRot,
    /// <summary>
    /// 星号
    /// </summary>
    [Description("star")]
    Star,
    /// <summary>
    /// 三角形
    /// </summary>
    [Description("triangle")]
    Triangle
}
