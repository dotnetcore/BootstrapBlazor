// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Badge 徽章组件
/// </summary>
public partial class Badge
{
    /// <summary>
    /// 获得 样式集合
    /// </summary>
    /// <returns></returns>
    protected string? ClassString => CssBuilder.Default("badge")
        .AddClass($"bg-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClass("rounded-pill", IsPill)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 颜色
    /// </summary>
    [Parameter] public Color Color { get; set; } = Color.Primary;

    /// <summary>
    /// 获得/设置 是否显示为胶囊形式
    /// </summary>
    /// <value></value>
    [Parameter] public bool IsPill { get; set; }

    /// <summary>
    /// 子组件
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }
}
