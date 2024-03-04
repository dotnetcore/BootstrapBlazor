// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 指示灯组件
/// </summary>
public partial class Light
{
    /// <summary>
    /// 获得 组件样式
    /// </summary>
    protected string? ClassString => CssBuilder.Default("light")
        .AddClass("is-flat", IsFlat)
        .AddClass("flash", IsFlash && !IsFlat)
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
