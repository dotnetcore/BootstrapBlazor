// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Routing;

namespace BootstrapBlazor.Components;

/// <summary>
/// NavMenu 组件基类
/// </summary>
public partial class Nav
{
    /// <summary>
    /// 获得 组件样式
    /// </summary>
    protected string? ClassString => CssBuilder.Default("nav")
        .AddClass("justify-content-center", Alignment == Alignment.Center && !IsVertical)
        .AddClass("justify-content-end", Alignment == Alignment.Right && !IsVertical)
        .AddClass("flex-column", IsVertical)
        .AddClass("nav-pills", IsPills)
        .AddClass("nav-fill", IsFill)
        .AddClass("nav-justified", IsJustified)
        .AddClass("text-end", Alignment == Alignment.Right && IsVertical)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 组件数据源
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<NavLink>? Items { get; set; }

    /// <summary>
    /// 获得/设置 组件对齐方式
    /// </summary>
    [Parameter]
    public Alignment Alignment { get; set; } = Alignment.Left;

    /// <summary>
    /// 获得/设置 是否垂直分布
    /// </summary>
    [Parameter]
    public bool IsVertical { get; set; }

    /// <summary>
    /// 获得/设置 是否为胶囊
    /// </summary>
    [Parameter]
    public bool IsPills { get; set; }

    /// <summary>
    /// 获得/设置 是否填充
    /// </summary>
    [Parameter]
    public bool IsFill { get; set; }

    /// <summary>
    /// 获得/设置 是否等宽
    /// </summary>
    [Parameter]
    public bool IsJustified { get; set; }

    /// <summary>
    /// 获得/设置 组件内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Items ??= Enumerable.Empty<NavLink>();
    }
}
