// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

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

        Items = await MenusDataGenerator.GetTopItemsAsync(Localizer);
        BottomItems = await MenusDataGenerator.GetBottomMenuItemsAsync(Localizer);
        IconItems = await MenusDataGenerator.GetTopIconItemsAsync(Localizer);
        SideMenuItems = await MenusDataGenerator.GetSideMenuItemsAsync(Localizer);
        IconSideMenuItems = await MenusDataGenerator.GetIconSideMenuItemsAsync(Localizer);
        WidgetIconSideMenuItems = await MenusDataGenerator.GetWidgetIconSideMenuItemsAsync(Localizer);
        CollapsedIconSideMenuItems = await MenusDataGenerator.GetCollapsedIconSideMenuItemsAsync(Localizer);
        DynamicSideMenuItems = await MenusDataGenerator.GetSideMenuItemsAsync(Localizer);
        DisabledMenuItems = await MenusDataGenerator.GetDisabledMenuItemsAsync(Localizer);
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
        DynamicSideMenuItems = await MenusDataGenerator.GetIconSideMenuItemsAsync(Localizer);
    }

    private async Task ResetMenu()
    {
        DynamicSideMenuItems = await MenusDataGenerator.GetSideMenuItemsAsync(Localizer);
    }

    private AttributeItem[] GetAttributes() =>
    [
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
    ];
}
