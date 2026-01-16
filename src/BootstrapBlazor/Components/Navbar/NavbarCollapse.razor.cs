// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">NavbarCollapse 组件用于在导航栏中适配响应式布局</para>
///  <para lang="en">NavbarCollapse component用于在导航栏中适配响应式布局</para>
/// </summary>
public partial class NavbarCollapse
{
    /// <summary>
    ///  <para lang="zh">获得/设置 子组件模板</para>
    ///  <para lang="en">Gets or sets 子componenttemplate</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string? ClassString => CssBuilder.Default("collapse navbar-collapse")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
