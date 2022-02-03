// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class AnchorLink
{
    /// <summary>
    /// 获得/设置 组件 Id 属性 要求页面内唯一
    /// </summary>
    [Parameter]
    public string? Id { get; set; }

    /// <summary>
    /// 获得/设置 组件 Text 显示文字
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 组件 拷贝成功后 显示文字
    /// </summary>
    [Parameter]
    public string? TooltipText { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Inject]
    [NotNull]
    private IStringLocalizer<AnchorLink>? Localizer { get; set; }

    private string? ClassString => CssBuilder.Default("anchor-link")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        TooltipText ??= Localizer[nameof(TooltipText)];
    }
}
