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
    /// <summary>
    /// <para lang="zh">获得/设置桌面端侧栏是否收起</para>
    /// <para lang="en">Gets or sets whether the desktop sidebar is collapsed.</para>
    /// <para><version>v11.1.2</version></para>
    /// </summary>
    [Parameter]
    public bool IsCollapsed { get; set; }

    /// <summary>
    /// <para lang="zh">侧栏收起状态变更时触发</para>
    /// <para lang="en">Callback invoked when the sidebar collapsed state changes.</para>
    /// <para><version>v11.1.2</version></para>
    /// </summary>
    [Parameter]
    public EventCallback<bool> IsCollapsedChanged { get; set; }

    private bool _isSmallScreen;

    private bool IsCollapsedPopup => IsCollapsed && !_isSmallScreen;

    private string SidebarToggleText => Localizer[IsCollapsedPopup ? "ExpandSidebar" : "CollapseSidebar"];

    private Task ToggleSidebarAsync() => IsCollapsedChanged.InvokeAsync(!IsCollapsed);

    private Placement Placement => IsCollapsedPopup ? Placement.Right : Placement.Left;

    private string? NavString => CssBuilder.Default("sidebar-content collapse d-md-block")
        .AddClass("is-collapsed", IsCollapsedPopup)
        .Build();

    private string? CollapseIconString => CssBuilder.Default("fa-solid")
        .AddClass("fa-angles-right", IsCollapsedPopup)
        .AddClass("fa-angles-left", !IsCollapsedPopup)
        .Build();

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

    private Task OnBreakPointChangedAsync(BreakPoint size)
    {
        var isSmallScreen = size < BreakPoint.Medium;
        if (_isSmallScreen != isSmallScreen)
        {
            _isSmallScreen = isSmallScreen;
            StateHasChanged();
        }
        return Task.CompletedTask;
    }

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
