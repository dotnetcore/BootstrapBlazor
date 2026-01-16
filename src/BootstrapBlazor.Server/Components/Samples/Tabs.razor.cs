// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Tabs
/// </summary>
public sealed partial class Tabs
{
    [NotNull]
    private Tab? TabSet { get; set; }

    private Placement _bindPlacement = Placement.Top;

    private bool RemoveEnabled => (TabSet?.Items.Count() ?? 4) < 4;

    [NotNull]
    private Menu? TabMenu { get; set; }

    [NotNull]
    private Tab? TabSetMenu { get; set; }

    [NotNull]
    private Tab? TabSetApp { get; set; }

    private bool ShowButtons { get; set; } = true;

    [NotNull]
    private Tab? TabSetTemplate { get; set; }

    private string TabItemText { get; set; } = "Test";

    private bool Disabled { get; set; } = true;

    private string DisableText { get; set; } = "Enable";

    private void SetPlacement(Placement placement)
    {
        _bindPlacement = placement;
    }

    private Task AddTab(Tab tab)
    {
        var text = $"Tab {tab.Items.Count() + 1}";
        tab.AddTab(new Dictionary<string, object?>
        {
            [nameof(TabItem.Text)] = text,
            [nameof(TabItem.IsActive)] = true,
            [nameof(TabItem.ChildContent)] = new RenderFragment(builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddContent(1, Localizer["BackAddTabText", text]);
                builder.CloseElement();
            })
        });
        return Task.CompletedTask;
    }

    private static Task Active(Tab tab)
    {
        tab.ActiveTab(0);
        return Task.CompletedTask;
    }

    private static async Task RemoveTab(Tab tab)
    {
        if (tab.Items.Count() > 4)
        {
            var item = tab.Items.Last();
            await tab.RemoveTab(item);
        }
    }

    private void OnToggleDisable()
    {
        Disabled = !Disabled;
        DisableText = Disabled ? "Enable" : "Disable";
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            var menuItem = TabMenu.Items.FirstOrDefault();
            if (menuItem != null && TabMenu.OnClick is not null)
            {
                TabMenu.OnClick(menuItem);
            }
        }
    }

    private List<MenuItem> GetSideMenuItems() =>
    [
        new() { Text = Localizer["BackText1"] },
        new() { Text = Localizer["BackText2"] }
    ];

    private Task OnClickMenuItem(MenuItem item)
    {
        var text = item.Text;
        var tabItem = TabSetMenu.Items.FirstOrDefault(i => i.Text == text);
        if (tabItem == null) AddTabItem(text ?? "");
        else TabSetMenu.ActiveTab(tabItem);
        return Task.CompletedTask;
    }

    private async Task<bool> OnBeforeShowContextMenu(TabItem item)
    {
        await Task.Yield();
        return item.IsDisabled == false;
    }

    private void AddTabItem(string text) => TabSetMenu.AddTab(new Dictionary<string, object?>
    {
        [nameof(TabItem.Text)] = text,
        [nameof(TabItem.IsActive)] = true,
        [nameof(TabItem.ChildContent)] = text == Localizer["BackText1"] ? BootstrapDynamicComponent.CreateComponent<Counter>().Render() : BootstrapDynamicComponent.CreateComponent<FetchData>().Render()
    });

    private async Task RemoveTab()
    {
        if (TabSetApp.Items.Count() > 4)
        {
            var item = TabSet.Items.Last();
            await TabSet.RemoveTab(item);
        }
    }

    private Task AddTab()
    {
        var text = $"Tab {TabSetApp.Items.Count() + 1}";
        TabSet.AddTab(new Dictionary<string, object?>
        {
            [nameof(TabItem.Text)] = text,
            [nameof(TabItem.IsActive)] = true,
            [nameof(TabItem.ChildContent)] = new RenderFragment(builder =>
            {
                builder.OpenElement(0, "div");
                builder.AddContent(1, Localizer["BackAddTabText", text]);
                builder.CloseElement();
            })
        });
        return Task.CompletedTask;
    }

    private async Task OnClickTabItem(TabItem tabItem)
    {
        TabSetTemplate.ActiveTab(tabItem);
        await ToastService.Information("Click TabItem", $"{tabItem.Text} clicked");
    }

    private Task OnSetTitle(string text)
    {
        TabItemText = text;
        StateHasChanged();
        return Task.CompletedTask;
    }
    /// <summary>
    /// 获得方法
    /// </summary>
    /// <returns></returns>
    private MethodItem[] GetMethods() =>
    [
        new MethodItem()
        {
            Name = "AddTab",
            Description = Localizer["TabMethod1AddTab"].Value,
            Parameters = "TabItem, int? Index = null",
            ReturnValue = " — "
        },
        new MethodItem()
        {
            Name = "RemoveTab",
            Description = Localizer["TabMethod2RemoveTab"].Value,
            Parameters = "TabItem",
            ReturnValue = " — "
        },
        new MethodItem()
        {
            Name = "ActiveTab",
            Description = Localizer["TabMethod3ActiveTab"].Value,
            Parameters = "TabItem",
            ReturnValue = " — "
        },
        new MethodItem()
        {
            Name = "ClickPrevTab",
            Description = Localizer["TabMethod4ClickPrevTab"].Value,
            Parameters = "",
            ReturnValue = "Task"
        },
        new MethodItem()
        {
            Name = "ClickNextTab",
            Description = Localizer["TabMethod5ClickNextTab"].Value,
            Parameters = "",
            ReturnValue = "Task"
        },
        new MethodItem()
        {
            Name = "CloseCurrentTab",
            Description = Localizer["TabMethod6CloseCurrentTab"].Value,
            Parameters = "",
            ReturnValue = "Task"
        },
        new MethodItem()
        {
            Name = "CloseOtherTabs",
            Description = Localizer["TabMethod7CloseOtherTabs"].Value,
            Parameters = "",
            ReturnValue = "Task"
        },
        new MethodItem()
        {
            Name = "CloseAllTabs",
            Description = Localizer["TabMethod8CloseAllTabs"].Value,
            Parameters = "",
            ReturnValue = "Task"
        },
        new MethodItem()
        {
            Name = nameof(Tab.GetActiveTab),
            Description = Localizer["TabMethod9GetActiveTab"].Value,
            Parameters = "",
            ReturnValue = "TabItem"
        }
    ];
}
