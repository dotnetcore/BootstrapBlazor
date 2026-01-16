// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">指示灯组件</para>
/// <para lang="en">Indicator Light Component</para>
/// </summary>
public partial class Light
{
    /// <summary>
    /// <para lang="zh">获得 组件样式</para>
    /// <para lang="en">Get Component Style</para>
    /// </summary>
    protected string? ClassString => CssBuilder.Default("bb-light")
        .AddClass("is-flat", IsFlat)
        .AddClass("flash", IsFlash && !IsFlat)
        .AddClass("is-flat-flash", IsFlash && IsFlat)
        .AddClass($"light-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 组件是否闪烁 默认为 false 不闪烁</para>
    /// <para lang="en">Get/Set Whether the component is flashing. Default is false (No flash)</para>
    /// </summary>
    [Parameter]
    public bool IsFlash { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为平面图形 默认 false</para>
    /// <para lang="en">Get/Set Whether it is a flat graphic. Default false</para>
    /// </summary>
    [Parameter]
    public bool IsFlat { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 指示灯颜色 默认为 Success 绿色</para>
    /// <para lang="en">Get/Set Indicator Color. Default Success (Green)</para>
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.Success;
}
