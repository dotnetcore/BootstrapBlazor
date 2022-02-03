// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace BootstrapBlazor.Components;

/// <summary>
/// NavMenu 组件基类
/// </summary>
public abstract class NavBase : BootstrapComponentBase
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
    /// 获得 NavLink 组件渲染内容
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    protected RenderFragment Render(NavLink item) => new RenderFragment(builder =>
    {
        var index = 0;
        builder.OpenComponent<NavLink>(index++);
        builder.AddMultipleAttributes(index++, item.AdditionalAttributes);
        builder.AddAttribute(index++, nameof(NavLink.ActiveClass), item.ActiveClass);
        builder.AddAttribute(index++, nameof(NavLink.Match), item.Match);
        builder.AddAttribute(index++, nameof(NavLink.ChildContent), item.ChildContent);
        builder.CloseComponent();
    });
}
