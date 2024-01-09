// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// GoTop 组件
/// </summary>
public partial class GoTop
{
    /// <summary>
    /// 获得/设置 返回顶端 Icon 属性
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// 获得/设置 滚动条所在组件
    /// </summary>
    [Parameter]
    public string? Target { get; set; }

    /// <summary>
    /// 获得/设置 鼠标悬停提示文字信息
    /// </summary>
    [Parameter]
    [NotNull]
    public string? TooltipText { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<GoTop>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        TooltipText ??= Localizer[nameof(TooltipText)];
        Icon ??= IconTheme.GetIconByKey(ComponentIcons.GoTopIcon);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Target);

}
