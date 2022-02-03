// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
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
    /// 获得/设置 空状态描述 默认为 null
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
    /// 组件初始化设置
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Text ??= Localizer[nameof(Text)];
    }
}
