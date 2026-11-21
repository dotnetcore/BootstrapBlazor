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

    private bool _popupMenuCollapsed = true;

    [NotNull]
    private IEnumerable<MenuItem>? PopupMenuItems { get; set; }

    [NotNull]
    private IEnumerable<MenuItem>? PopupLayoutMenuItems { get; set; }

    [NotNull]
    private ConsoleLogger? PopupLogger { get; set; }

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
        PopupMenuItems = CreatePopupMenuItems();
        PopupLayoutMenuItems = CreatePopupMenuItems();
    }

    private IEnumerable<MenuItem> CreatePopupMenuItems() =>
    [
        new(Localizer["System"].Value, icon: "fa-solid fa-gears")
        {
            IsCollapsed = false,
            Items =
            [
                new(Localizer["Website"].Value, icon: "fa-solid fa-font-awesome") { IsActive = true },
                new(Localizer["Task"].Value, icon: "fa-solid fa-bars-progress")
                {
                    Items =
                    [
                        new(Localizer["User"].Value, icon: "fa-solid fa-user"),
                        new(Localizer["Role"].Value, icon: "fa-solid fa-sitemap") { IsDisabled = true }
                    ]
                }
            ]
        },
        new(Localizer["Log"].Value, icon: "fa-solid fa-database")
        {
            Items =
            [
                new(Localizer["Access"].Value, icon: "fa-solid fa-bars"),
                new(Localizer["Operation"].Value, icon: "fa-solid fa-pen") { IsDisabled = true }
            ]
        },
        new(Localizer["Menu"].Value, icon: "fa-solid fa-gauge-high"),
        new(Localizer["Login"].Value),
        new(Localizer["Authorize"].Value, icon: "fa-solid fa-users")
        {
            IsDisabled = true,
            Items = [new(Localizer["User"].Value)]
        }
    ];

    private Task OnClickPopupMenuAsync(MenuItem item)
    {
        PopupLogger.Log(item.Text ?? string.Empty);
        return Task.CompletedTask;
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
}
