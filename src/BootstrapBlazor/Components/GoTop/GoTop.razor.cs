// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
    /// 获得/设置 滚动行为 默认 ScrollIntoViewBehavior.Smooth
    /// </summary>
    [Parameter]
    public ScrollIntoViewBehavior Behavior { get; set; }

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

    private string? BehaviorString => Behavior == ScrollIntoViewBehavior.Smooth ? null : Behavior.ToString().ToLowerInvariant();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        TooltipText ??= Localizer[nameof(TooltipText)];
        Icon ??= IconTheme.GetIconByKey(ComponentIcons.GoTopIcon);
    }
}
