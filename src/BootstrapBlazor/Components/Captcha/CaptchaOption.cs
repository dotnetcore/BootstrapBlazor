// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Captcha 滑块验证码组件
/// </summary>
public class CaptchaOption
{
    /// <summary>
    /// 获得/设置 验证码图片宽度
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// 获得/设置 验证码图片高度
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// 获得/设置 拼图边长 默认 42
    /// </summary>
    public int SideLength { get; set; } = 42;

    /// <summary>
    /// 获得/设置 拼图直径 默认 9
    /// </summary>
    public int Diameter { get; set; } = 9;

    /// <summary>
    /// 获得/设置 拼图 X 位置
    /// </summary>
    public int OffsetX { get; set; }

    /// <summary>
    /// 获得/设置 拼图 Y 位置
    /// </summary>
    public int OffsetY { get; set; }

    /// <summary>
    /// 获得/设置 拼图宽度
    /// </summary>
    public int BarWidth { get; set; }

    /// <summary>
    /// 获得/设置 拼图背景图片路径
    /// </summary>
    public string? ImageUrl { get; set; }
}
