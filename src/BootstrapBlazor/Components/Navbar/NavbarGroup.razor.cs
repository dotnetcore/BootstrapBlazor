// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// NavbarGroup 组件用于在导航栏中分组
/// </summary>
public partial class NavbarGroup
{
    /// <summary>
    /// 获得/设置 子组件模板
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 是否启用滚动
    /// </summary>
    [Parameter]
    public bool IsScrolling { get; set; }

    /// <summary>
    /// 获得/设置 高度值 默认 200px;
    /// </summary>
    [Parameter]
    public string? Height { get; set; }

    private string? ClassString => CssBuilder.Default("navbar-nav")
        .AddClass("navbar-nav-scroll", IsScrolling)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default()
        .AddStyle("--bs-scroll-height", Height, IsScrolling)
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Height ??= "200px";
    }
}
