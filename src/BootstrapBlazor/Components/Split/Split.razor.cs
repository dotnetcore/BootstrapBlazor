// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public sealed partial class Split
{
    /// <summary>
    /// 获得 组件样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("split")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 组件 Wrapper 样式
    /// </summary>
    private string? WrapperClassString => CssBuilder.Default("split-wrapper")
        .AddClass("is-horizontal", !IsVertical)
        .Build();

    /// <summary>
    /// 获得 第一个窗格 Style
    /// </summary>
    private string? StyleString => CssBuilder.Default()
        .AddClass($"flex-basis: {Basis.ConvertToPercentString()};")
        .Build();

    /// <summary>
    /// 获得/设置 是否垂直分割
    /// </summary>
    [Parameter]
    public bool IsVertical { get; set; }

    /// <summary>
    /// 获得/设置 第一个窗格初始化位置占比 默认为 50%
    /// </summary>
    [Parameter]
    public string Basis { get; set; } = "50%";

    /// <summary>
    /// 获得/设置 第一个窗格模板
    /// </summary>
    [Parameter]
    public RenderFragment? FirstPaneTemplate { get; set; }

    /// <summary>
    /// 获得/设置 第二个窗格模板
    /// </summary>
    [Parameter]
    public RenderFragment? SecondPaneTemplate { get; set; }
}
