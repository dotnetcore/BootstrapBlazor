// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
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
    
    [CascadingParameter]
    private MenuItem? MenuItem { get; set; }

    /// <summary>
    /// 获得/设置 菜单数据集合
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<MenuItem>? Items { get; set; }

    /// <summary>
    /// 获得/设置 组件数据源
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DropdownIcon { get; set; }

    /// <summary>
    /// 获得/设置 菜单箭头图标
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ArrowIcon { get; set; }

    /// <summary>
    /// 获得/设置 菜单项点击回调委托
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<MenuItem, Task>? OnClick { get; set; }

    [CascadingParameter]
    [NotNull]
    private Menu? Parent { get; set; }

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
