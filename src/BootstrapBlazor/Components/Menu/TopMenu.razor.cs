// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">顶栏菜单</para>
/// <para lang="en">Top Menu</para>
/// </summary>
public partial class TopMenu
{
    private string? GetDropdownClassString(MenuItem item, string className = "") => CssBuilder.Default(className)
        .AddClass("dropdown", string.IsNullOrEmpty(className) && !Parent.IsBottom)
        .AddClass("dropup", string.IsNullOrEmpty(className) && Parent.IsBottom)
        .AddClass("disabled", item.IsDisabled)
        .AddClass("active", item.IsActive)
        .Build();

    private static string? GetIconString(string icon) => CssBuilder.Default("menu-icon")
        .AddClass(icon)
        .Build();

    /// <summary>
    /// <para lang="zh">获得/设置 DropdownIcon 图标</para>
    /// <para lang="en">Gets or sets DropdownIcon Icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? DropdownIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 菜单箭头图标</para>
    /// <para lang="en">Gets or sets Menu Arrow Icon</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public string? ArrowIcon { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 菜单数据集合</para>
    /// <para lang="en">Gets or sets Menu Data Collection</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<MenuItem>? Items { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 菜单项点击回调委托</para>
    /// <para lang="en">Gets or sets Menu item click callback delegate</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
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
    /// <para lang="zh">SetParametersAsync 方法</para>
    /// <para lang="en">SetParametersAsync Method</para>
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

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        DropdownIcon ??= IconTheme.GetIconByKey(ComponentIcons.TopMenuDropdownIcon);
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
