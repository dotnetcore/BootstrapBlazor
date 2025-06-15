// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 指示灯组件
/// </summary>
public partial class Light
{
    /// <summary>
    /// 获得 组件样式
    /// </summary>
    protected string? ClassString => CssBuilder.Default("bb-light")
        .AddClass("is-flat", IsFlat)
        .AddClass("flash", IsFlash && !IsFlat)
        .AddClass("is-flat-flash", IsFlash && IsFlat)
        .AddClass($"light-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 组件是否闪烁 默认为 false 不闪烁
    /// </summary>
    [Parameter]
    public bool IsFlash { get; set; }

    /// <summary>
    /// 获得/设置 是否为平面图形 默认 false
    /// </summary>
    [Parameter]
    public bool IsFlat { get; set; }

    /// <summary>
    /// 获得/设置 指示灯颜色 默认为 Success 绿色
    /// </summary>
    [Parameter]
    public Color Color { get; set; } = Color.Success;
}
