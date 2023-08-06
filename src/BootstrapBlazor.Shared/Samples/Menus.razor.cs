// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Menus
/// </summary>
public sealed partial class Menus
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    [NotNull]
    private IEnumerable<MenuItem>? Items { get; set; }

    private string? ClickedMenuItemText { get; set; }

    [NotNull]
    private IEnumerable<MenuItem>? BottomItems { get; set; }

    [NotNull]
    private IEnumerable<MenuItem>? IconItems { get; set; }

    [NotNull]
    private ConsoleLogger? SideLogger { get; set; }

    [NotNull]
    private IEnumerable<MenuItem>? SideMenuItems { get; set; }

    [NotNull]
    private IEnumerable<MenuItem>? IconSideMenuItems { get; set; }

    [NotNull]
    private IEnumerable<MenuItem>? WidgetIconSideMenuItems { get; set; }

    [NotNull]
    private IEnumerable<MenuItem>? CollapsedIconSideMenuItems { get; set; }

    [NotNull]
    private IEnumerable<MenuItem>? DynamicSideMenuItems { get; set; }

    [NotNull]
    private ConsoleLogger? DisabledLogger { get; set; }

    [NotNull]
    private IEnumerable<MenuItem>? DisabledMenuItems { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        Items = await MenusDataGerator.GetTopItemsAsync(Localizer);
        BottomItems = await MenusDataGerator.GetBottomMenuItemsAsync(Localizer);
        IconItems = await MenusDataGerator.GetTopIconItemsAsync(Localizer);
        SideMenuItems = await MenusDataGerator.GetSideMenuItemsAsync(Localizer);
        IconSideMenuItems = await MenusDataGerator.GetIconSideMenuItemsAsync(Localizer);
        WidgetIconSideMenuItems = await MenusDataGerator.GetWidgetIconSideMenuItemsAsync(Localizer);
        CollapsedIconSideMenuItems = await MenusDataGerator.GetCollapsedIconSideMenuItemsAsync(Localizer);
        DynamicSideMenuItems = await MenusDataGerator.GetSideMenuItemsAsync(Localizer);
        DisabledMenuItems = await MenusDataGerator.GetDisabledMenuItemsAsync(Localizer);
    }

    private Task OnClickBottomMenu(MenuItem item)
    {
        ClickedMenuItemText = item.Text;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private Task OnClickMenu(MenuItem item)
    {
        Logger.Log($"菜单点击项: {item.Text}");
        return Task.CompletedTask;
    }

    private Task OnClickSideMenu(MenuItem item)
    {
        SideLogger.Log($"菜单点击项: {item.Text}");
        return Task.CompletedTask;
    }

    private async Task UpdateMenu()
    {
        DynamicSideMenuItems = await MenusDataGerator.GetIconSideMenuItemsAsync(Localizer);
    }

    private async Task ResetMenu()
    {
        DynamicSideMenuItems = await MenusDataGerator.GetSideMenuItemsAsync(Localizer);
    }

    private Task OnClick2(MenuItem item)
    {
        DisabledLogger.Log($"菜单点击项: {item.Text}");
        return Task.CompletedTask;
    }

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "Items",
            Description = Localizer["MenusAttr_Items"],
            Type = "IEnumerable<MenuItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsVertical",
            Description = Localizer["MenusAttr_IsVertical"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsBottom",
            Description = Localizer["MenusAttr_IsBottom"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "IsAccordion",
            Description = Localizer["MenusAttr_IsAccordion"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "IsScrollIntoView",
            Description = Localizer["MenusAttr_IsScrollIntoView"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new() {
            Name = "DisableNavigation",
            Description = Localizer["MenusAttr_DisableNavigation"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "OnClick",
            Description = Localizer["MenusAttr_OnClick"],
            Type = "Func<MenuItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
