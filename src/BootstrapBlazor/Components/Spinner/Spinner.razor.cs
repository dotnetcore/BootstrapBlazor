// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Spinner 组件基类
/// </summary>
public partial class Spinner
{
    /// <summary>
    /// 获取Spinner样式
    /// </summary>
    protected string? ClassString => CssBuilder.Default("spinner")
        .AddClass($"spinner-{SpinnerType.ToDescriptionString()}")
        .AddClass($"text-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass($"spinner-border-{Size.ToDescriptionString()}", Size != Size.None)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 Spinner 颜色 默认 None 无设置
    /// </summary>
    [Parameter]
    public Color Color { get; set; }

    /// <summary>
    /// 获得 / 设置 Spinner 大小 默认 None 无设置
    /// </summary>
    [Parameter]
    public Size Size { get; set; }

    /// <summary>
    /// 获得/设置 Spinner 类型 默认为 Border
    /// </summary>
    [Parameter]
    public SpinnerType SpinnerType { get; set; }
}
