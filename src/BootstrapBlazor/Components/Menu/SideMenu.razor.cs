// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">SideMenu 组件</para>
/// <para lang="en">SideMenu Component</para>
/// </summary>
public partial class SideMenu
{
    private string? GetMenuClassString => CssBuilder.Default("submenu")
        .AddClass("show", MenuItem is { IsCollapsed: false })
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? ParentIdString => Parent.IsAccordion ? $"#{Id}" : null;

    private string? GetTargetIdString(MenuItem item) => $"#{GetTargetId(item)}";

    private string GetTargetId(MenuItem item) => ComponentIdGenerator.Generate(item);

    /// <summary>
    /// <para lang="zh">获得/设置 菜单数据集合</para>
    /// <para lang="en">Get/Set Menu Data Collection</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<MenuItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 DropdownIcon 图标</para>
    /// <para lang="en">Get/Set DropdownIcon Icon</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DropdownIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 菜单箭头图标</para>
    /// <para lang="en">Get/Set Menu Arrow Icon</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ArrowIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 菜单项点击回调委托</para>
    /// <para lang="en">Get/Set Menu item click callback delegate</para>
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<MenuItem, Task>? OnClick { get; set; }

    [CascadingParameter]
    [NotNull]
    private Menu? Parent { get; set; }

    [CascadingParameter]
    private MenuItem? MenuItem { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Menu>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (Parent == null)
        {
            throw new InvalidOperationException(Localizer["InvalidOperationExceptionMessage"]);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        DropdownIcon ??= IconTheme.GetIconByKey(ComponentIcons.SideMenuDropdownIcon);
        ArrowIcon ??= IconTheme.GetIconByKey(ComponentIcons.MenuLinkArrowIcon);
    }

    private async Task OnClickItem(MenuItem item)
    {
        if (OnClick != null)
        {
            await OnClick(item);
        }
    }
}
