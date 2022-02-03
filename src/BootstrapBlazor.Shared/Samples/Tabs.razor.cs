// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Tabs
{
    [NotNull]
    private Tab? TabSet { get; set; }

    [NotNull]
    private Tab? TabSet2 { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

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

    private bool RemoveEndable => (TabSet?.Items.Count() ?? 4) < 4;

    private static Task RemoveTab(Tab tabset)
    {
        if (tabset.Items.Count() > 4)
        {
            var item = tabset.Items.Last();
            tabset.RemoveTab(item);
        }
        return Task.CompletedTask;
    }

    private Placement BindPlacement = Placement.Top;

    private void SetPlacement(Placement placement)
    {
        BindPlacement = placement;
    }

    private IEnumerable<MenuItem> GetSideMenuItems()
    {
        return new List<MenuItem>
            {
                new MenuItem() { Text = Localizer["BackText1"]  },
                new MenuItem() { Text = Localizer["BackText2"] }
            };
    }

    [NotNull]
    private Tab? TabSetMenu { get; set; }

    [NotNull]
    private Menu? TabMenu { get; set; }

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

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "IsBorderCard",
            Description = Localizer["Att1"].Value,
            Type = "boolean",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsCard",
            Description = Localizer["Att2"].Value,
            Type = "boolean",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsOnlyRenderActiveTab",
            Description = Localizer["Att3"].Value,
            Type = "boolean",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowClose",
            Description = Localizer["Att4"].Value,
            Type = "boolean",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ShowExtendButtons",
            Description = Localizer["Att5"].Value,
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "ClickTabToNavigation",
            Description = Localizer["Att6"].Value,
            Type = "boolean",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "Placement",
            Description = Localizer["Att7"].Value,
            Type = "Placement",
            ValueList = "Top|Right|Bottom|Left",
            DefaultValue = "Top"
        },
        new AttributeItem() {
            Name = "Height",
            Description = Localizer["Att8"].Value,
            Type = "int",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new AttributeItem() {
            Name = "Items",
            Description = Localizer["Att9"].Value,
            Type = "IEnumerable<TabItemBase>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "DefaultUrl",
            Description = Localizer["DefaultUrl"].Value,
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "ChildContent",
            Description = Localizer["Att10"].Value,
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "AdditionalAssemblies",
            Description = Localizer["Att11"].Value,
            Type = "IEnumerable<Assembly>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnClickTab",
            Description = Localizer["Att12"].Value,
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
        // TODO: 移动到数据库中
        new MethodItem() {
            Name = "AddTab",
            Description = Localizer["Method1"].Value,
            Parameters = "TabItem",
            ReturnValue = " — "
        },
        new MethodItem() {
            Name = "RemoveTab",
            Description = Localizer["Method2"].Value,
            Parameters = "TabItem",
            ReturnValue = " — "
        },
        new MethodItem() {
            Name = "ActiveTab",
            Description = Localizer["Method3"].Value,
            Parameters = "TabItem",
            ReturnValue = " — "
        },
        new MethodItem() {
            Name = "ClickPrevTab",
            Description = Localizer["Method4"].Value,
            Parameters = "",
            ReturnValue = "Task"
        },
        new MethodItem() {
            Name = "ClickNextTab",
            Description = Localizer["Method5"].Value,
            Parameters = "",
            ReturnValue = "Task"
        },
        new MethodItem() {
            Name = "CloseCurrentTab",
            Description = Localizer["Method6"].Value,
            Parameters = "",
            ReturnValue = "Task"
        },
        new MethodItem() {
            Name = "CloseOtherTabs",
            Description = Localizer["Method7"].Value,
            Parameters = "",
            ReturnValue = "Task"
        },
        new MethodItem() {
            Name = "CloseAllTabs",
            Description = Localizer["Method8"].Value,
            Parameters = "",
            ReturnValue = "Task"
        },
        new MethodItem() {
            Name = nameof(Tab.GetActiveTab),
            Description = Localizer["Method9"].Value,
            Parameters = "",
            ReturnValue = "Tabitem"
        }
    };
}
