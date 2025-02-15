// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Dropdown 组件示例代码
/// </summary>
public sealed partial class Dropdowns
{
    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private List<SelectedItem> Items { get; set; } = [];

    private List<SelectedItem> ItemTemplateList { get; set; } = [];

    /// <summary>
    /// ShowMessage
    /// </summary>
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

        Items =
        [
            new SelectedItem() { Text=Localizer["Item1"], Value="0"},
            new SelectedItem() { Text=Localizer["Item2"], Value="1"},
            new SelectedItem() { Text=Localizer["Item3"], Value="2"},
        ];

        ItemTemplateList =
        [
            new SelectedItem() { Text=Localizer["Item1"], Value="0"},
            new SelectedItem() { Text=Localizer["Item2"], Value="1"},
            new SelectedItem() { Text=Localizer["Item3"], Value="2"},
        ];

        Foos =
        [
            new SelectedItem{ Text=Localizer["Item1"], Value="0"},
            new SelectedItem{ Text=Localizer["Item2"], Value="1"},
            new SelectedItem{ Text=Localizer["Item3"], Value="2"},
        ];

        RadioDropDownItems =
        [
            new SelectedItem("1", Localizer["Item1"]) { Active = true },
            new SelectedItem("2", Localizer["Item2"]),
            new SelectedItem("3", Localizer["Item3"])
        ];

        RadioItems =
        [
            new SelectedItem("1", Localizer["Item1"]) { Active = true },
            new SelectedItem("2", Localizer["Item2"])
        ];

        Items3 = new SelectedItem[]
        {
            new ("", Localizer["DropdownCascadeItem1"]),
            new ("Beijing", Localizer["DropdownCascadeItem2"]) { Active = true },
            new ("Shanghai", Localizer["DropdownCascadeItem3"]),
            new ("Hangzhou", Localizer["DropdownCascadeItem4"])
        };
    }

    private static List<SelectedItem> EmptyList => [];

    private List<SelectedItem> Foos { get; set; } = [];

    /// <summary>
    /// AddItem
    /// </summary>
    private void AddItem()
    {
        Foos.Add(new SelectedItem($"{Foos.Count}", $"{Localizer["City"]} {Foos.Count}"));
    }

    private List<SelectedItem> RadioItems { get; set; } = [];

    private List<SelectedItem> RadioDropDownItems { get; set; } = [];

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
        if (Foos.Count != 0)
        {
            Foos.RemoveAt(0);
        }
    }

    private IEnumerable<SelectedItem>? Items2 { get; set; }

    private IEnumerable<SelectedItem>? Items3 = Array.Empty<SelectedItem>();

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
                new("1",Localizer["DropdownCascadeItem5"]) { Active = true },
                new("2",Localizer["DropdownCascadeItem6"]),
            };
        }
        else if (item.Value == "Shanghai")
        {
            Items2 = new SelectedItem[]
            {
                new("1",Localizer["DropdownCascadeItem7"]),
                new("2",Localizer["DropdownCascadeItem8"]) { Active = true } ,
            };
        }
        else
        {
            Items2 = Enumerable.Empty<SelectedItem>();
        }
        StateHasChanged();
    }

    private async Task OnIsAsyncClick()
    {
        // 模拟异步延时
        await Task.Delay(1000);

        // 提示任务完成
        await ToastService.Success("Dropdown IsAsync", "Job done!");
    }

    /// <summary>
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Value",
            Description = Localizer["AttributeValue"],
            Type = "TValue",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Class",
            Description = Localizer["AttributeClass"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "Color",
            Description = Localizer["AttributeColor"],
            Type = "Color",
            ValueList = "Primary / Secondary / Info / Warning / Danger ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Direction",
            Description = Localizer["AttributeDirection"],
            Type = "Direction",
            ValueList = "Dropup / Dropright /  Dropleft",
            DefaultValue = " None "
        },
        new()
        {
            Name = "Items",
            Description = Localizer["AttributeItems"],
            Type = "list",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "MenuAlignment",
            Description = Localizer["AttributeMenuAlignment"],
            Type = "Alignment",
            ValueList = "None / Left / Center / Right ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "MenuItem",
            Description = Localizer["AttributeMenuItem"],
            Type = "string",
            ValueList = "button / a ",
            DefaultValue = " a "
        },
        new()
        {
            Name = "Responsive",
            Description = Localizer["AttributeResponsive"],
            Type = "string",
            ValueList = "dropdown-menu / dropdown-menu-end / dropdown-menu-{lg | md | sm }-{right | left}",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ShowSplit",
            Description = Localizer["AttributeShowSplit"],
            Type = "bool",
            ValueList = "true / false ",
            DefaultValue = " false "
        },
        new()
        {
            Name = "IsAsync",
            Description = Localizer["AttributeIsAsync"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Size",
            Description = Localizer["AttributeSize"],
            Type = "Size",
            ValueList = "None / ExtraSmall / Small / Medium / Large / ExtraLarge / ExtraExtraLarge",
            DefaultValue = "None"
        },
        new()
        {
            Name = "TagName",
            Description = Localizer["AttributeTagName"],
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
    ];

    /// <summary>
    /// GetEvents
    /// </summary>
    /// <returns></returns>
    private EventItem[] GetEvents() =>
    [
        new()
        {
            Name = "OnClick",
            Description = Localizer["EventDesc1"],
            Type ="EventCallback<MouseEventArgs>"
        },
        new()
        {
            Name = "OnClickWithoutRender",
            Description = Localizer["EventDesc2"],
            Type ="Func<Task>"
        },
        new EventItem()
        {
            Name = "OnSelectedItemChanged",
            Description= Localizer["EventOnSelectedItemChanged"],
            Type ="Func<SelectedItem, Task>"
        }
   ];
}
