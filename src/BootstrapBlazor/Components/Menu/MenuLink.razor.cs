// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Routing;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">MenuLink 组件内部封装 NavLink 组件</para>
///  <para lang="en">MenuLink Component internally encapsulates NavLink Component</para>
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
    ///  <para lang="zh">获得/设置 MenuItem 实例 不可为空</para>
    ///  <para lang="en">Get/Set MenuItem Instance. Cannot be null</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public MenuItem? Item { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 ArrowIcon 图标</para>
    ///  <para lang="en">Get/Set ArrowIcon Icon</para>
    ///  <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? ArrowIcon { get; set; }

    [CascadingParameter]
    [NotNull]
    private Menu? Parent { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Menu>? Localizer { get; set; }

    private string? IconString => CssBuilder.Default("menu-icon")
        .AddClass(Item.Icon)
        .Build();

    private string? StyleClassString => Parent.IsVertical
        ? (Item.Indent == 0
            ? null
            : $"padding-left: {Item.Indent * Parent.IndentSize}px;")
        : null;

    /// <summary>
    ///  <para lang="zh">SetParametersAsync 方法</para>
    ///  <para lang="en">SetParametersAsync Method</para>
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
