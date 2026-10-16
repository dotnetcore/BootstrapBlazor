using System.Globalization;

namespace BootstrapBlazor.Server.Components.Layout;

/// <summary>
/// Client-side site navigation.
/// </summary>
public partial class NavMenu
{
    private bool IsAccordion { get; set; }

    private bool IsExpandAll { get; set; }

    private bool IsMenuOpen { get; set; }

    private string AccordionText { get; set; } = "";

    private string ExpandAllText { get; set; } = "";

    private List<MenuItem> Menus { get; set; } = [];

    private string? SidebarMenuClass => CssBuilder.Default("sidebar-content collapse d-md-block")
        .AddClass("show", IsMenuOpen)
        .Build();

    [Inject]
    private TitleService TitleService { get; set; } = default!;

    [Inject]
    private IStringLocalizer<NavMenu> Localizer { get; set; } = default!;

    [Inject]
    private MenuService MenuService { get; set; } = default!;

    /// <inheritdoc />
    protected override void OnInitialized()
    {
        Menus = MenuService.GetMenus();
        Menus.ForEach(menu => menu.IsCollapsed = true);
        AccordionText = Localizer["MenuAccordion"];
        ExpandAllText = Localizer["MenuExpandAll"];
    }

    private void ToggleMenu() => IsMenuOpen = !IsMenuOpen;

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
        IsMenuOpen = false;
        if (!item.Items.Any() && !string.IsNullOrEmpty(item.Text))
        {
            await TitleService.SetTitle($"{item.Text} - {Localizer["SiteTitle"]}");
        }
    }
}
