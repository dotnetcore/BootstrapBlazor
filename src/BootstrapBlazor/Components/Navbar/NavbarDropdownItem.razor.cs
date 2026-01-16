// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">NavbarDropdownItem 组件用于在导航栏下拉菜单中创建菜单项</para>
///  <para lang="en">NavbarDropdownItem component用于在导航栏下拉菜单中创建菜单项</para>
/// </summary>
public partial class NavbarDropdownItem
{
    /// <summary>
    ///  <para lang="zh">获得/设置 子组件模板</para>
    ///  <para lang="en">Gets or sets 子componenttemplate</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 菜单项文本</para>
    ///  <para lang="en">Gets or sets 菜单项文本</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Url { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 A 标签 target 参数 默认 null</para>
    ///  <para lang="en">Gets or sets A 标签 target 参数 Default is null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Target { get; set; }
}
