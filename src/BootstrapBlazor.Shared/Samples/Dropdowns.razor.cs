// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
///
/// </summary>
public sealed partial class Dropdowns
{
    private List<SelectedItem> Items { get; set; } = new List<SelectedItem>
        {
            new SelectedItem{ Text="北京",Value="0"},
            new SelectedItem{ Text="上海",Value="1"},
            new SelectedItem{ Text="广州",Value="2"},
        };

    private static List<SelectedItem> EmptyList => new();

    private List<SelectedItem> Foos { get; set; } = new List<SelectedItem>
        {
            new SelectedItem{ Text="北京",Value="0"},
            new SelectedItem{ Text="上海",Value="1"},
            new SelectedItem{ Text="广州",Value="2"},
        };

    private List<SelectedItem> RadioItems { get; set; } = new List<SelectedItem>
        {
            new SelectedItem("1", "北京") { Active = true },
            new SelectedItem("2", "上海")
        };

    private List<SelectedItem> RadioDropDownItems { get; set; } = new List<SelectedItem>
        {
            new SelectedItem("1", "北京") { Active = true },
            new SelectedItem("2", "上海"),
            new SelectedItem("3", "广州")
        };

    private IEnumerable<SelectedItem>? Items2 { get; set; }

    private readonly IEnumerable<SelectedItem> Items3 = new SelectedItem[]
    {
            new SelectedItem ("", "请选择 ..."),
            new SelectedItem ("Beijing", "北京") { Active = true },
            new SelectedItem ("Shanghai", "上海"),
            new SelectedItem ("Hangzhou", "杭州")
    };

    /// <summary>
    /// 级联绑定菜单
    /// </summary>
    /// <param name="item"></param>
    private async Task OnCascadeBindSelectClick(SelectedItem item)
    {
        // 模拟异步通讯切换线程
        await Task.Delay(10);
        if (item.Value == "Beijing")
        {
            Items2 = new SelectedItem[]
            {
                    new SelectedItem("1","朝阳区") { Active = true},
                    new SelectedItem("2","海淀区"),
            };
        }
        else if (item.Value == "Shanghai")
        {
            Items2 = new SelectedItem[]
            {
                    new SelectedItem("1","静安区"),
                    new SelectedItem("2","黄浦区") { Active = true } ,
            };
        }
        else
        {
            Items2 = Enumerable.Empty<SelectedItem>();
        }
        StateHasChanged();
    }

    [NotNull]
    private BlockLogger? Trace { get; set; }

    private Task ShowMessage(SelectedItem e)
    {
        Trace.Log($"Dropdown Item Clicked: Value={e.Value} Text={e.Text}");
        return Task.CompletedTask;
    }

    private void AddItem()
    {
        Foos.Add(new SelectedItem($"{Foos.Count}", $"城市 {Foos.Count}"));
    }

    private void RemoveItem()
    {
        if (Foos.Any())
        {
            Foos.RemoveAt(0);
        }
    }

    private Task OnRadioItemChanged(IEnumerable<SelectedItem> values, SelectedItem item)
    {
        RadioDropDownItems.Add(new SelectedItem($"{RadioDropDownItems.Count + 1}", $"城市 {RadioDropDownItems.Count}"));
        StateHasChanged();
        return Task.CompletedTask;
    }

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Value",
                Description = Localizer["ADesc1"],
                Type = "TValue",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Class",
                Description = Localizer["ADesc2"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Color",
                Description = Localizer["ADesc3"],
                Type = "Color",
                ValueList = "Primary / Secondary / Info / Warning / Danger ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Direction",
                Description = Localizer["ADesc4"],
                Type = "Direction",
                ValueList = "Dropup / Dropright /  Dropleft",
                DefaultValue = " None "
            },
            new AttributeItem() {
                Name = "Items",
                Description = Localizer["ADesc5"],
                Type = "list",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "MenuAlignment",
                Description = Localizer["ADesc6"],
                Type = "Alignment",
                ValueList = "None / Left / Center / Right ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "MenuItem",
                Description = Localizer["ADesc7"],
                Type = "string",
                ValueList = "button / a ",
                DefaultValue = " a "
            },
            new AttributeItem() {
                Name = "Responsive",
                Description = Localizer["ADesc8"],
                Type = "string",
                ValueList = "dropdown-menu-right / dropdown-menu-right / dropdown-menu-{lg | md | sm }-{right | left}",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ShowSplit",
                Description = Localizer["ADesc9"],
                Type = "bool",
                ValueList = "true / false ",
                DefaultValue = " false "
            },
            new AttributeItem() {
                Name = "Size",
                Description = Localizer["ADesc10"],
                Type = "Size",
                ValueList = "None / ExtraSmall / Small / Medium / Large / ExtraLarge",
                DefaultValue = "None"
            },
            new AttributeItem() {
                Name = "TagName",
                Description = Localizer["ADesc11"],
                Type = "string",
                ValueList = " a / button ",
                DefaultValue = " — "
            },
    };

    private IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
            new EventItem()
            {
                Name = "OnSelectedItemChanged",
                Description= Localizer["EDesc1"],
                Type ="EventCallback<SelectedItem>"
            }
   };
}
