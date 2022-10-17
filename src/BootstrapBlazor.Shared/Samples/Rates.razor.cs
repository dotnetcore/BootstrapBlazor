// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Rates
{
    private int BindValue { get; set; } = 3;

    private int BindValue1 { get; set; } = 2;

    private bool IsDisable { get; set; }

    private List<string> IconList { get; } = new List<string>()
    {
        "fa-solid fa-face-sad-cry",
        "fa-solid fa-face-sad-tear",
        "fa-solid fa-face-smile",
        "fa-solid fa-face-surprise",
        "fa-solid fa-face-grin-stars"
    };

    private string GetIconList(int index) => IconList[index - 1];

    private int IconListValue { get; set; } = 1;

    private BlockLogger? Trace { get; set; }

    private void OnValueChanged(int val)
    {
        BindValue = val;
        Trace?.Log($"{Localizer["Log"]} {val}");
    }

    private string GetIconValueChanged() => (IconListValue - 1) switch
    {
        0 => Localizer["Cry"],
        1 => Localizer["Tear"],
        2 => Localizer["Smile"],
        3 => Localizer["Surprise"],
        _ => Localizer["Grin"]
    };

    private IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
        new EventItem()
        {
            Name = "ValueChanged",
            Description =Localizer["Event1"],
            Type ="EventCallback<int>"
        }
    };

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "Value",
            Description = Localizer["Att1"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Max",
            Description = Localizer["AttMax"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "IsDisabled",
            Description = Localizer["Att2"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "ItemTemplate",
            Description = Localizer["AttItemTemplate"],
            Type = "RenderFragment<int>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
