// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Tabs
/// </summary>
public sealed partial class Tabs
{

    [NotNull]
    private Tab? TabSet { get; set; }

    private Placement BindPlacement = Placement.Top;

    private bool RemoveEndable => (TabSet?.Items.Count() ?? 4) < 4;

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

    private void SetPlacement(Placement placement)
    {
        BindPlacement = placement;
    }

    private Task AddTab(Tab tabset)
    {
        var text = $"Tab {tabset.Items.Count() + 1}";
        tabset.AddTab(new Dictionary<string, object?>
        {
            [nameof(TabItem.Text)] = text,
            [nameof(TabItem.IsActive)] = true,
            [nameof(TabItem.ChildContent)] = new RenderFragment(builder =>
            {
                var index = 0;
                builder.OpenElement(index++, "div");
                builder.AddContent(index++, Localizer["BackAddTabText", text]);
                builder.CloseElement();
            })
        });
        return Task.CompletedTask;
    }

    private static Task Active(Tab tabset)
    {
        tabset.ActiveTab(0);
        return Task.CompletedTask;
    }

    private static async Task RemoveTab(Tab tabset)
    {
        if (tabset.Items.Count() > 4)
        {
            var item = tabset.Items.Last();
            await tabset.RemoveTab(item);
        }
    }

    /// <summary>
    /// OnAfterRenderAsync
    /// </summary>
    /// <param name="firstRender"></param>
    /// <returns></returns>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var menuItem = TabMenu?.Items.FirstOrDefault();
            if (menuItem != null)
            {
                await InvokeAsync(() =>
                {
                    var _ = TabMenu?.OnClick?.Invoke(menuItem);
                });
            }
        }
    }

    private IEnumerable<MenuItem> GetSideMenuItems()
    {
        return new List<MenuItem>
        {
            new MenuItem() { Text = Localizer["BackText1"]  },
            new MenuItem() { Text = Localizer["BackText2"] }
        };
    }

    private Task OnClickMenuItem(MenuItem item)
    {
        var text = item.Text;
        var tabItem = TabSetMenu.Items.FirstOrDefault(i => i.Text == text);
        if (tabItem == null) AddTabItem(text ?? "");
        else TabSetMenu.ActiveTab(tabItem);
        return Task.CompletedTask;
    }

    private void AddTabItem(string text) => TabSetMenu.AddTab(new Dictionary<string, object?>
    {
        [nameof(TabItem.Text)] = text,
        [nameof(TabItem.IsActive)] = true,
        [nameof(TabItem.ChildContent)] = text == Localizer["BackText1"] ? BootstrapDynamicComponent.CreateComponent<Counter>().Render() : BootstrapDynamicComponent.CreateComponent<FetchData>().Render()
    });

    private void OnClick()
    {
        ShowButtons = !ShowButtons;
    }

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
                var index = 0;
                builder.OpenElement(index++, "div");
                builder.AddContent(index++, Localizer["BackAddTabText", text]);
                builder.CloseElement();
            })
        });
        return Task.CompletedTask;
    }

    private static string? GetClassString(TabItem tabItem) => CssBuilder.Default("tabs-item")
        .AddClass("active", tabItem.IsActive)
        .Build();

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
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {

        new() {
            Name = "IsBorderCard",
            Description = Localizer["TabsAtt1IsBorderCard"].Value,
            Type = "boolean",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new() {
            Name = "IsCard",
            Description = Localizer["TabAtt2IsCard"].Value,
            Type = "boolean",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new() {
            Name = "IsOnlyRenderActiveTab",
            Description = Localizer["TabAtt3IsOnlyRenderActiveTab"].Value,
            Type = "boolean",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new() {
            Name = "ShowClose",
            Description = Localizer["TabAtt4ShowClose"].Value,
            Type = "boolean",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new() {
            Name = "ShowExtendButtons",
            Description = Localizer["TabAtt5ShowExtendButtons"].Value,
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new() {
            Name = "ClickTabToNavigation",
            Description = Localizer["TabAtt6ClickTabToNavigation"].Value,
            Type = "boolean",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new() {
            Name = "Placement",
            Description = Localizer["TabAtt7Placement"].Value,
            Type = "Placement",
            ValueList = "Top|Right|Bottom|Left",
            DefaultValue = "Top"
        },
        new() {
            Name = "Height",
            Description = Localizer["TabAtt8Height"].Value,
            Type = "int",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new() {
            Name = "Items",
            Description = Localizer["TabAtt9Items"].Value,
            Type = "IEnumerable<TabItemBase>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "DefaultUrl",
            Description = Localizer["TabDefaultUrl"].Value,
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "HeaderTemplate",
            Description = Localizer["TabAttHeaderTemplate"].Value,
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "ChildContent",
            Description = Localizer["TabAtt10ChildContent"].Value,
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "AdditionalAssemblies",
            Description = Localizer["TabAtt11AdditionalAssemblies"].Value,
            Type = "IEnumerable<Assembly>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "OnClickTab",
            Description = Localizer["TabAtt12OnClickTab"].Value,
            Type = "Func<TabItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };

    /// <summary>
    /// 获得方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<MethodItem> GetMethods() => new MethodItem[]
    {

        new MethodItem() {
            Name = "AddTab",
            Description = Localizer["TabMethod1AddTab"].Value,
            Parameters = "TabItem, int? Index = null",
            ReturnValue = " — "
        },
        new MethodItem() {
            Name = "RemoveTab",
            Description = Localizer["TabMethod2RemoveTab"].Value,
            Parameters = "TabItem",
            ReturnValue = " — "
        },
        new MethodItem() {
            Name = "ActiveTab",
            Description = Localizer["TabMethod3ActiveTab"].Value,
            Parameters = "TabItem",
            ReturnValue = " — "
        },
        new MethodItem() {
            Name = "ClickPrevTab",
            Description = Localizer["TabMethod4ClickPrevTab"].Value,
            Parameters = "",
            ReturnValue = "Task"
        },
        new MethodItem() {
            Name = "ClickNextTab",
            Description = Localizer["TabMethod5ClickNextTab"].Value,
            Parameters = "",
            ReturnValue = "Task"
        },
        new MethodItem() {
            Name = "CloseCurrentTab",
            Description = Localizer["TabMethod6CloseCurrentTab"].Value,
            Parameters = "",
            ReturnValue = "Task"
        },
        new MethodItem() {
            Name = "CloseOtherTabs",
            Description = Localizer["TabMethod7CloseOtherTabs"].Value,
            Parameters = "",
            ReturnValue = "Task"
        },
        new MethodItem() {
            Name = "CloseAllTabs",
            Description = Localizer["TabMethod8CloseAllTabs"].Value,
            Parameters = "",
            ReturnValue = "Task"
        },
        new MethodItem() {
            Name = nameof(Tab.GetActiveTab),
            Description = Localizer["TabMethod9GetActiveTab"].Value,
            Parameters = "",
            ReturnValue = "Tabitem"
        }
    };
}
