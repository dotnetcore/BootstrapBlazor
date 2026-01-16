// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">GoTop 组件</para>
/// <para lang="en">GoTop Component</para>
/// </summary>
public partial class GoTop
{
    /// <summary>
    /// <para lang="zh">获得/设置 返回顶端 Icon 属性</para>
    /// <para lang="en">Get/Set Back to Top Icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Icon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 滚动条所在组件</para>
    /// <para lang="en">Get/Set Scroll Container Component</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Target { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 滚动行为 默认 ScrollIntoViewBehavior.Smooth</para>
    /// <para lang="en">Get/Set Scroll Behavior Default ScrollIntoViewBehavior.Smooth</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public ScrollIntoViewBehavior ScrollBehavior { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 鼠标悬停提示文字信息</para>
    /// <para lang="en">Get/Set Tooltip Text</para>
    /// <para><version>10.2.2</version></para>
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

    private string? ScrollBehaviorString => ScrollBehavior == ScrollIntoViewBehavior.Smooth ? null : ScrollBehavior.ToString().ToLowerInvariant();

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        TooltipText ??= Localizer[nameof(TooltipText)];
        Icon ??= IconTheme.GetIconByKey(ComponentIcons.GoTopIcon);
    }
}
