// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public sealed partial class SubMenu
{
    /// <summary>
    /// 获得 组件样式
    /// </summary>
    private string? ClassString => CssBuilder.Default("has-leaf nav-link")
        .AddClass("active", Item.IsActive)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? GetIconString => string.IsNullOrEmpty(Item.Icon)
        ? null
        : Item.Icon.Contains("fa-fw", StringComparison.OrdinalIgnoreCase)
            ? Item.Icon
            : $"{Item.Icon} fa-fw";

    /// <summary>
    /// 获得/设置 组件数据源
    /// </summary>
    [Parameter]
    [NotNull]
    public MenuItem? Item { get; set; }

    /// <summary>
    /// 获得/设置 菜单项点击回调委托
    /// </summary>
    [Parameter]
    public Func<MenuItem, Task>? OnClick { get; set; }

    [CascadingParameter]
    [NotNull]
    private Menu? Parent { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Menu>? Localizer { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    private static string? GetClassString(MenuItem item) => CssBuilder.Default()
        .AddClass("active", item.IsActive && !item.IsDisabled)
        .AddClass("disabled", item.IsDisabled)
        .Build();

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

    private async Task OnClickItem(MenuItem item)
    {
        if (OnClick != null)
        {
            await OnClick(item);
        }
    }
}
