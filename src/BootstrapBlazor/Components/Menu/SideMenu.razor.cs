// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public partial class SideMenu
{
    private string? GetMenuClassString => CssBuilder.Default("submenu")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 菜单数据集合
    /// </summary>
    [Parameter]
    public IEnumerable<MenuItem> Items { get; set; } = Array.Empty<MenuItem>();

    /// <summary>
    /// 获得/设置 菜单项点击回调委托
    /// </summary>
    [Parameter]
    [NotNull]
    public Func<MenuItem, Task>? OnClick { get; set; }

    [CascadingParameter]
    private Menu? Parent { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Menu>? Localizer { get; set; }

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
