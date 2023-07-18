// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// MenuLink 组件内部封装 NavLink 组件
/// </summary>
public sealed partial class MenuLink
{
    private string? ClassString => CssBuilder.Default("nav-link")
        .AddClass(Item.CssClass, !string.IsNullOrEmpty(Item.CssClass))
        .AddClass("active", Parent.DisableNavigation && Item.IsActive && !Item.IsDisabled)
        .AddClass("disabled", Item.IsDisabled)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? MenuArrowClassString => CssBuilder.Default("arrow")
        .AddClass(ArrowIcon, Item.Items.Any())
        .Build();

    private string? HrefString => (Parent.DisableNavigation || Item.IsDisabled || Item.Items.Any() || string.IsNullOrEmpty(Item.Url)) ? "#" : Item.Url.TrimStart('/');

    private string? TargetString => string.IsNullOrEmpty(Item.Target) ? null : Item.Target;

    private bool PreventDefault => HrefString == "#";

    private string? AriaExpandedString => (Parent.IsVertical && !Item.IsCollapsed ? "true" : "false");

    /// <summary>
    /// 获得/设置 MenuItem 实例 不可为空
    /// </summary>
    [Parameter]
    [NotNull]
    public MenuItem? Item { get; set; }

    /// <summary>
    /// 获得/设置 ArrowIcon 图标
    /// </summary>
    [Parameter]
    public string? ArrowIcon { get; set; }

    [CascadingParameter]
    [NotNull]
    private Menu? Parent { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Menu>? Localizer { get; set; }

    private NavLinkMatch ItemMatch => string.IsNullOrEmpty(Item.Url) ? NavLinkMatch.All : Item.Match;

    private string? IconString => CssBuilder.Default("menu-icon")
        .AddClass(Item.Icon)
        .Build();

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
