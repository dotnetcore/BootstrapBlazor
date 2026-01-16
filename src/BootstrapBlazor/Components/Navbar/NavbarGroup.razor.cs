// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">NavbarGroup 组件用于在导航栏中分组</para>
///  <para lang="en">NavbarGroup component用于在导航栏中分组</para>
/// </summary>
public partial class NavbarGroup
{
    /// <summary>
    ///  <para lang="zh">获得/设置 子组件模板</para>
    ///  <para lang="en">Gets or sets 子componenttemplate</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否启用滚动</para>
    ///  <para lang="en">Gets or sets whether启用滚动</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsScrolling { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 高度值 默认 200px;</para>
    ///  <para lang="en">Gets or sets height值 Default is 200px;</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Height { get; set; }

    private string? ClassString => CssBuilder.Default("navbar-nav")
        .AddClass("navbar-nav-scroll", IsScrolling)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default()
        .AddClass($"--bs-scroll-height: {Height};", !string.IsNullOrEmpty(Height))
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    ///  <para lang="zh"><inheritdoc/></para>
    ///  <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Height ??= "200px";
    }
}
