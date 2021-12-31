// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Scroll
{
    private string? ClassString => CssBuilder.Default("scroll")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? StyleString => CssBuilder.Default()
        .AddClass($"height: {Height};", !string.IsNullOrEmpty(Height))
        .AddStyleFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 组件高度
    /// </summary>
    [Parameter]
    public string? Height { get; set; }
}
