// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Dropdown 组件示例代码
/// </summary>
public sealed partial class Dropdowns
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private List<SelectedItem> Items { get; set; } = new();

    /// <summary>
    /// ShowMessage
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    private Task ShowMessage(SelectedItem e)
    {
        Logger.Log($"Dropdown Item Clicked: Value={e.Value} Text={e.Text}");
        return Task.CompletedTask;
    }

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = new List<SelectedItem>
        {
            new SelectedItem{ Text=Localizer["Item1"], Value="0"},
            new SelectedItem{ Text=Localizer["Item2"], Value="1"},
            new SelectedItem{ Text=Localizer["Item3"], Value="2"},
        };

        Foos = new List<SelectedItem>
        {
            new SelectedItem{ Text=Localizer["Item1"], Value="0"},
            new SelectedItem{ Text=Localizer["Item2"], Value="1"},
            new SelectedItem{ Text=Localizer["Item3"], Value="2"},
        };

        RadioDropDownItems = new List<SelectedItem>
        {
            new SelectedItem("1", Localizer["Item1"]) { Active = true },
            new SelectedItem("2", Localizer["Item2"]),
            new SelectedItem("3", Localizer["Item3"])
        };

        RadioItems = new List<SelectedItem>
        {
            new SelectedItem("1", Localizer["Item1"]) { Active = true },
            new SelectedItem("2", Localizer["Item2"])
        };

        Items3 = new SelectedItem[]
        {
            new SelectedItem ("", Localizer["DropdownCascadeItem1"]),
            new SelectedItem ("Beijing", Localizer["DropdownCascadeItem2"]) { Active = true },
            new SelectedItem ("Shanghai", Localizer["DropdownCascadeItem3"]),
            new SelectedItem ("Hangzhou", Localizer["DropdownCascadeItem4"])
        };
    }

    private static List<SelectedItem> EmptyList => new();

    private List<SelectedItem> Foos { get; set; } = new List<SelectedItem>();

    /// <summary>
    /// AddItem
    /// </summary>
    private void AddItem()
    {
        Foos.Add(new SelectedItem($"{Foos.Count}", $"{Localizer["City"]} {Foos.Count}"));
    }

    private List<SelectedItem> RadioItems { get; set; } = new List<SelectedItem>();

    private List<SelectedItem> RadioDropDownItems { get; set; } = new List<SelectedItem>();

    /// <summary>
    /// OnRadioItemChanged
    /// </summary>
    /// <param name="values"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    private Task OnRadioItemChanged(IEnumerable<SelectedItem> values, SelectedItem item)
    {
        RadioDropDownItems.Add(new SelectedItem($"{RadioDropDownItems.Count + 1}", $"{Localizer["City"]} {RadioDropDownItems.Count}"));
        StateHasChanged();
        return Task.CompletedTask;
    }

    /// <summary>
    /// RemoveItem
    /// </summary>
    private void RemoveItem()
    {
        if (Foos.Any())
        {
            Foos.RemoveAt(0);
        }
    }

    private IEnumerable<SelectedItem>? Items2 { get; set; }

    private IEnumerable<SelectedItem>? Items3 = new SelectedItem[] { };

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
                new SelectedItem("1",Localizer["DropdownCascadeItem5"]) { Active = true },
                new SelectedItem("2",Localizer["DropdownCascadeItem6"]),
            };
        }
        else if (item.Value == "Shanghai")
        {
            Items2 = new SelectedItem[]
            {
                new SelectedItem("1",Localizer["DropdownCascadeItem7"]),
                new SelectedItem("2",Localizer["DropdownCascadeItem8"]) { Active = true } ,
            };
        }
        else
        {
            Items2 = Enumerable.Empty<SelectedItem>();
        }
        StateHasChanged();
    }

    /// <summary>
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = "Value",
            Description = Localizer["ADesc1"],
            Type = "TValue",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Class",
            Description = Localizer["ADesc2"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "Color",
            Description = Localizer["ADesc3"],
            Type = "Color",
            ValueList = "Primary / Secondary / Info / Warning / Danger ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Direction",
            Description = Localizer["ADesc4"],
            Type = "Direction",
            ValueList = "Dropup / Dropright /  Dropleft",
            DefaultValue = " None "
        },
        new()
        {
            Name = "Items",
            Description = Localizer["ADesc5"],
            Type = "list",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "MenuAlignment",
            Description = Localizer["ADesc6"],
            Type = "Alignment",
            ValueList = "None / Left / Center / Right ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "MenuItem",
            Description = Localizer["ADesc7"],
            Type = "string",
            ValueList = "button / a ",
            DefaultValue = " a "
        },
        new()
        {
            Name = "Responsive",
            Description = Localizer["ADesc8"],
            Type = "string",
            ValueList = "dropdown-menu / dropdown-menu-end / dropdown-menu-{lg | md | sm }-{right | left}",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ShowSplit",
            Description = Localizer["ADesc9"],
            Type = "bool",
            ValueList = "true / false ",
            DefaultValue = " false "
        },
        new()
        {
            Name = "Size",
            Description = Localizer["ADesc10"],
            Type = "Size",
            ValueList = "None / ExtraSmall / Small / Medium / Large / ExtraLarge / ExtraExtraLarge",
            DefaultValue = "None"
        },
        new()
        {
            Name = "TagName",
            Description = Localizer["ADesc11"],
            Type = "string",
            ValueList = " a / button ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Dropdown<string>.FixedButtonText),
            Description = Localizer["FixedButtonText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };

    /// <summary>
    /// GetEvents
    /// </summary>
    /// <returns></returns>
    private IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
        new EventItem()
        {
            Name = "OnSelectedItemChanged",
            Description= Localizer["EDesc1"],
            Type ="Func<SelectedItem, Task>"
        }
   };
}
