﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class Empty
{
    private string? ClassString => CssBuilder.Default("empty")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    ///  获得/设置 图片路径 默认为 null
    /// </summary>
    [Parameter]
    public string? Image { get; set; }

    /// <summary>
    /// 获得/设置 空状态描述 默认为 无数据
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 自定义模板
    /// </summary>
    [Parameter]
    public RenderFragment? Template { get; set; }

    /// <summary>
    /// 获得/设置 子组件
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Empty>? Localizer { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Text ??= Localizer[nameof(Text)];
    }
}
