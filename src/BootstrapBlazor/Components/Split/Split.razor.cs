// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// Split 组件
/// </summary>
public sealed partial class Split
{
    /// <summary>
    /// 获得 组件样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("split")
        .AddClass("is-vertical", IsVertical)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 第一个窗格 Style
    /// </summary>
    private string? StyleString => CssBuilder.Default()
        .AddClass($"flex-basis: {Basis.ConvertToPercentString()};")
        .Build();

    /// <summary>
    /// 获取 是否开启折叠功能 默认 false
    /// </summary>
    [Parameter]
    public bool IsCollapsible { get; set; }

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

    /// <summary>
    /// 获得/设置 窗格折叠时回调方法 参数 bool 值为 true 是表示已折叠 值为 false 表示第二个已折叠
    /// </summary>
    [Parameter]
    public Func<bool, Task>? OnCollapsedAsync { get; set; }
}
