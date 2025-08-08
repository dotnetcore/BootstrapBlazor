// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ToolbarItem 组件用于在工具栏中添加子组件
/// </summary>
public partial class ToolbarItem
{
    /// <summary>
    /// 获得/设置 子组件模板
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    private string? ClassString => CssBuilder.Default("bb-toolbar-item")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();
}
