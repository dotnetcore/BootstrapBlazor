// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Menus
{
    [NotNull]
    private BlockLogger? Trace { get; set; }

    [NotNull]
    private BlockLogger? Trace2 { get; set; }

    [NotNull]
    private BlockLogger? TraceSideMenu { get; set; }

    [NotNull]
    private IEnumerable<MenuItem>? Items { get; set; }

    [NotNull]
    private IEnumerable<MenuItem>? BottomItems { get; set; }

    [NotNull]
    private IEnumerable<MenuItem>? IconItems { get; set; }

    [NotNull]
    private IEnumerable<MenuItem>? SideMenuItems { get; set; }

    [NotNull]
    private IEnumerable<MenuItem>? IconSideMenuItems { get; set; }

    [NotNull]
    private IEnumerable<MenuItem>? WidgetIconSideMenuItems { get; set; }

    [NotNull]
    private IEnumerable<MenuItem>? CollapsedIconSideMenuItems { get; set; }

    [NotNull]
    private IEnumerable<MenuItem>? DisabledMenuItems { get; set; }

    [NotNull]
    private IEnumerable<MenuItem>? DynamicSideMenuItems { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Menus>? Localizer { get; set; }

    private Task OnClickMenu(MenuItem item)
    {
        Trace.Log($"菜单点击项: {item.Text}");
        return Task.CompletedTask;
    }

    private string? ClickedMenuItemText { get; set; }

    private Task OnClickBottomMenu(MenuItem item)
    {
        ClickedMenuItemText = item.Text;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnClick2(MenuItem item)
    {
        Trace2.Log($"菜单点击项: {item.Text}");
        return Task.CompletedTask;
    }

    private Task OnClickSideMenu(MenuItem item)
    {
        TraceSideMenu?.Log($"菜单点击项: {item.Text}");
        return Task.CompletedTask;
    }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        Items = await MenusDataGerator.GetTopItemsAsync(Localizer);
        IconItems = await MenusDataGerator.GetTopIconItemsAsync(Localizer);
        SideMenuItems = await MenusDataGerator.GetSideMenuItemsAsync(Localizer);
        IconSideMenuItems = await MenusDataGerator.GetIconSideMenuItemsAsync(Localizer);
        WidgetIconSideMenuItems = await MenusDataGerator.GetWidgetIconSideMenuItemsAsync(Localizer);
        CollapsedIconSideMenuItems = await MenusDataGerator.GetCollapsedIconSideMenuItemsAsync(Localizer);
        DisabledMenuItems = await MenusDataGerator.GetDisabledMenuItemsAsync(Localizer);
        DynamicSideMenuItems = await MenusDataGerator.GetSideMenuItemsAsync(Localizer);
        BottomItems = await MenusDataGerator.GetBottomMenuItemsAsync(Localizer);
    }

    private async Task UpdateMenu()
    {
        DynamicSideMenuItems = await MenusDataGerator.GetIconSideMenuItemsAsync(Localizer);
    }

    private async Task ResetMenu()
    {
        DynamicSideMenuItems = await MenusDataGerator.GetSideMenuItemsAsync(Localizer);
    }

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            new AttributeItem()
            {
                Name = "Items",
                Description = Localizer["Desc1"],
                Type = "IEnumerable<MenuItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem()
            {
                Name = "IsVertical",
                Description = Localizer["Desc2"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem()
            {
                Name = "IsBottom",
                Description = Localizer["Desc3"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsAccordion",
                Description = Localizer["Desc4"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "DisableNavigation",
                Description = Localizer["Desc5"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "OnClick",
                Description = Localizer["Desc6"],
                Type = "Func<MenuItem, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            }
    };
}
