// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// MenuLink 组件内部封装 NavLink 组件
/// </summary>
public sealed partial class MenuLink
{
    private string? ClassString => CssBuilder.Default("nav-link")
        .AddClass("active", Parent.DisableNavigation && Item.IsActive && !Item.IsDisabled)
        .AddClass("disabled", Item.IsDisabled)
        .AddClass("expand", Parent.IsVertical && !Item.IsCollapsed)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? MenuArrowClassString => CssBuilder.Default("arrow")
        .AddClass("fa fa-fw", Parent.IsVertical)
        .AddClass("fa-angle-left", Item.Items.Any())
        .Build();

    private string? HrefString => (Parent.DisableNavigation || Item.IsDisabled || Item.Items.Any() || string.IsNullOrEmpty(Item.Url)) ? "#" : Item.Url.TrimStart('/');

    private string? TargetString => string.IsNullOrEmpty(Item.Target) ? null : Item.Target;

    /// <summary>
    /// 获得/设置 MenuItem 实例 不可为空
    /// </summary>
    [Parameter]
    [NotNull]
    public MenuItem? Item { get; set; }

    [CascadingParameter]
    [NotNull]
    private Menu? Parent { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Menu>? Localizer { get; set; }

    private NavLinkMatch ItemMatch => string.IsNullOrEmpty(Item.Url) ? NavLinkMatch.All : Item.Match;

    private string? IconString => string.IsNullOrEmpty(Item.Icon)
        ? (Parent.IsVertical
            ? (Parent.IsCollapsed
                ? "fa-none"
                : "fa fa-fw")
            : null)
        : Item.Icon.Contains("fa-fw", StringComparison.OrdinalIgnoreCase)
            ? Item.Icon
            : $"{Item.Icon} fa-fw";

    private string? StyleClassString => Parent.IsVertical
        ? (Item.Indent == 0
            ? null
            : $"padding-left: {Item.Indent * Parent.IndentSize}px;")
        : null;

    /// <summary>
    /// SetParametersAsync 方法
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public override Task SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);

        if (Parent == null)
        {
            throw new InvalidOperationException(Localizer["InvalidOperationExceptionMessage"]);
        }

        // For derived components, retain the usual lifecycle with OnInit/OnParametersSet/etc.
        return base.SetParametersAsync(ParameterView.Empty);
    }
}
