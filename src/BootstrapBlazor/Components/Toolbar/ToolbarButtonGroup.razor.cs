// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">ToolbarGroup 组件用于在工具栏中添加一组按钮</para>
///  <para lang="en">ToolbarGroup component用于在工具栏中添加一组button</para>
/// </summary>
public partial class ToolbarButtonGroup
{
    /// <summary>
    ///  <para lang="zh">获得/设置 子组件模板</para>
    ///  <para lang="en">Gets or sets 子componenttemplate</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string? ClassString => CssBuilder.Default("bb-toolbar-group btn-group")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
