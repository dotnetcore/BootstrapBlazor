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
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "IsBorderCard",
            Description = Localizer["TabsAtt1IsBorderCard"].Value,
            Type = "boolean",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsCard",
            Description = Localizer["TabAtt2IsCard"].Value,
            Type = "boolean",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsOnlyRenderActiveTab",
            Description = Localizer["TabAtt3IsOnlyRenderActiveTab"].Value,
            Type = "boolean",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsLazyLoadTabItem",
            Description = Localizer["TabAttIsLazyLoadTabItem"].Value,
            Type = "boolean",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowClose",
            Description = Localizer["TabAtt4ShowClose"].Value,
            Type = "boolean",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowExtendButtons",
            Description = Localizer["TabAtt5ShowExtendButtons"].Value,
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowNavigatorButtons",
            Description = Localizer["TabAttShowNavigatorButtons"].Value,
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowActiveBar",
            Description = Localizer["TabAttShowActiveBar"].Value,
            Type = "boolean",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ClickTabToNavigation",
            Description = Localizer["TabAtt6ClickTabToNavigation"].Value,
            Type = "boolean",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "TabStyle",
            Description = Localizer["TabAtt2TabStyle"].Value,
            Type = "TabStyle",
            ValueList = "Default|Chrome",
            DefaultValue = "Default"
        },
        new()
        {
            Name = "Placement",
            Description = Localizer["TabAtt7Placement"].Value,
            Type = "Placement",
            ValueList = "Top|Right|Bottom|Left",
            DefaultValue = "Top"
        },
        new()
        {
            Name = "Height",
            Description = Localizer["TabAtt8Height"].Value,
            Type = "int",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new()
        {
            Name = "Items",
            Description = Localizer["TabAtt9Items"].Value,
            Type = "IEnumerable<TabItemBase>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "DefaultUrl",
            Description = Localizer["TabDefaultUrl"].Value,
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "HeaderTemplate",
            Description = Localizer["TabAttHeaderTemplate"].Value,
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ChildContent",
            Description = Localizer["TabAtt10ChildContent"].Value,
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "AdditionalAssemblies",
            Description = Localizer["TabAtt11AdditionalAssemblies"].Value,
            Type = "IEnumerable<Assembly>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnClickTab",
            Description = Localizer["TabAtt12OnClickTab"].Value,
            Type = "Func<TabItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnCloseTabItemAsync",
            Description = Localizer["AttributeOnCloseTabItemAsync"].Value,
            Type = "Func<TabItem, Task<bool>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnClickTabItemAsync",
            Description = Localizer["AttributeOnClickTabItemAsync"].Value,
            Type = "Func<TabItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "AllowDrag",
            Description = Localizer["TabAttAllowDrag"].Value,
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "OnDragItemEndAsync",
            Description = Localizer["TabAttOnDragItemEndAsync"].Value,
            Type = "Func<TabItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "NotAuthorized",
            Description = Localizer["AttributeNotAuthorized"].Value,
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "NotFound",
            Description = Localizer["AttributeNotFound"].Value,
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ExcludeUrls",
            Description = Localizer["AttributeExcludeUrls"].Value,
            Type = "IEnumerable<string>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ButtonTemplate",
            Description = Localizer["AttributeButtonTemplate"].Value,
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Tab.ShowToolbar),
            Description = Localizer["AttributeShowToolbar"].Value,
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(Tab.ToolbarTemplate),
            Description = Localizer["AttributeToolbarTemplate"].Value,
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Tab.ShowRefreshToolbarButton),
            Description = Localizer["AttributeShowRefreshToolbarButton"].Value,
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(Tab.ShowFullscreenToolbarButton),
            Description = Localizer["AttributeShowFullscreenToolbarButton"].Value,
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(Tab.RefreshToolbarTooltipText),
            Description = Localizer["AttributeRefreshToolbarTooltipText"].Value,
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Tab.FullscreenToolbarTooltipText),
            Description = Localizer["AttributeFullscreenToolbarTooltipText"].Value,
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Tab.RefreshToolbarButtonIcon),
            Description = Localizer["AttributeRefreshToolbarButtonIcon"].Value,
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Tab.OnToolbarRefreshCallback),
            Description = Localizer["AttributeOnToolbarRefreshCallback"].Value,
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Tab.FullscreenToolbarButtonIcon),
            Description = Localizer["AttributeFullscreenToolbarButtonIcon"].Value,
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Tab.ShowContextMenu),
            Description = Localizer["AttributeShowContextMenu"].Value,
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(Tab.ContextMenuRefreshIcon),
            Description = Localizer["AttributeContextMenuRefreshIcon"].Value,
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Tab.ContextMenuCloseIcon),
            Description = Localizer["AttributeContextMenuCloseIcon"].Value,
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Tab.ContextMenuCloseOtherIcon),
            Description = Localizer["AttributeContextMenuCloseOtherIcon"].Value,
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Tab.ContextMenuCloseAllIcon),
            Description = Localizer["AttributeContextMenuCloseAllIcon"].Value,
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Tab.ContextMenuFullScreenIcon),
            Description = Localizer["AttributeContextMenuFullScreenIcon"].Value,
            Type = "string?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Tab.OnBeforeShowContextMenu),
            Description = Localizer["AttributeOnBeforeShowContextMenu"].Value,
            Type = "Func<TabItem, Task<bool>>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];

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
