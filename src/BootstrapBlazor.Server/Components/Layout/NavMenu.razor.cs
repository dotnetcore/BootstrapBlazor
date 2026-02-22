// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Layout;

/// <summary>
///
/// </summary>
public partial class NavMenu
{
    private bool IsAccordion { get; set; }

    private bool IsExpandAll { get; set; }

    [NotNull]
    private string? AccordionText { get; set; }

    [NotNull]
    private string? ExpandAllText { get; set; }

    [Inject]
    [NotNull]
    private TitleService? TitleService { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<NavMenu>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private MenuService? MenuService { get; set; }

    [NotNull]
    private List<MenuItem>? Menus { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Menus = MenuService.GetMenus();
        Menus.ForEach(m => m.IsCollapsed = true);
        AccordionText ??= Localizer["MenuAccordion"];
        ExpandAllText ??= Localizer["MenuExpandAll"];
    }

    private Task OnValueChanged(bool accordion)
    {
        if (accordion)
        {
            IsExpandAll = false;
        }
        return Task.CompletedTask;
    }

    private async Task OnClickMenu(MenuItem item)
    {
        if (!item.Items.Any() && !string.IsNullOrEmpty(item.Text))
        {
            await TitleService.SetTitle($"{item.Text} - {Localizer["SiteTitle"]}");
        }
    }
}
