﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Divider 组件
/// </summary>
public partial class Divider
{
    /// <summary>
    /// 获得 class 样式集合
    /// </summary>
    protected virtual string? ClassString => CssBuilder.Default("divider")
        .AddClass("divider-vertical", IsVertical)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得 class 样式集合
    /// </summary>
    protected virtual string? TextClassString => CssBuilder.Default("divider-text")
        .AddClass("is-left", Alignment.Left == Alignment)
        .AddClass("is-center", Alignment.Center == Alignment)
        .AddClass("is-right", Alignment.Right == Alignment)
        .Build();

    /// <summary>
    /// 获得/设置 是否为垂直显示 默认为 false 
    /// </summary>
    [Parameter]
    public bool IsVertical { get; set; }

    /// <summary>
    /// 获得/设置 组件对齐方式 默认为居中
    /// </summary>
    [Parameter]
    public Alignment Alignment { get; set; } = Alignment.Center;

    /// <summary>
    /// 获得/设置 文案显示文字
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 文案显示图标
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 子内容
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }
}
