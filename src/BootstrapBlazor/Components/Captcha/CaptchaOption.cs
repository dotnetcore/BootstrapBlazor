// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Captcha 滑块验证码组件
///</para>
/// <para lang="en">Captcha 滑块验证码component
///</para>
/// </summary>
public class CaptchaOption
{
    /// <summary>
    /// <para lang="zh">获得/设置 验证码图片宽度
    ///</para>
    /// <para lang="en">Gets or sets 验证码图片width
    ///</para>
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 验证码图片高度
    ///</para>
    /// <para lang="en">Gets or sets 验证码图片height
    ///</para>
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 拼图边长 默认 42
    ///</para>
    /// <para lang="en">Gets or sets 拼图边长 Default is 42
    ///</para>
    /// </summary>
    public int SideLength { get; set; } = 42;

    /// <summary>
    /// <para lang="zh">获得/设置 拼图直径 默认 9
    ///</para>
    /// <para lang="en">Gets or sets 拼图直径 Default is 9
    ///</para>
    /// </summary>
    public int Diameter { get; set; } = 9;

    /// <summary>
    /// <para lang="zh">获得/设置 拼图 X 位置
    ///</para>
    /// <para lang="en">Gets or sets 拼图 X 位置
    ///</para>
    /// </summary>
    public int OffsetX { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 拼图 Y 位置
    ///</para>
    /// <para lang="en">Gets or sets 拼图 Y 位置
    ///</para>
    /// </summary>
    public int OffsetY { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 拼图宽度
    ///</para>
    /// <para lang="en">Gets or sets 拼图width
    ///</para>
    /// </summary>
    public int BarWidth { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 拼图背景图片路径
    ///</para>
    /// <para lang="en">Gets or sets 拼图背景图片路径
    ///</para>
    /// </summary>
    public string? ImageUrl { get; set; }
}
