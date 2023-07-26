// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Rates
/// </summary>
public sealed partial class Rates
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private double BindValue { get; set; } = 3.0;

    private double BindValue1 { get; set; } = 2;

    private double BindValue2 { get; set; } = 2;

    private double BindValue3 { get; set; } = 2.8;

    private bool IsDisable { get; set; }

    private void OnValueChanged(double val)
    {
        BindValue = val;
        Logger.Log($"{Localizer["RatesLog"]} {val}");
    }

    private double IconListValue { get; set; } = 1;

    private List<string> IconList { get; } = new List<string>()
    {
        "fa-solid fa-face-sad-cry",
        "fa-solid fa-face-sad-tear",
        "fa-solid fa-face-smile",
        "fa-solid fa-face-surprise",
        "fa-solid fa-face-grin-stars"
    };

    private string GetIconList(int index) => IconList[index - 1];

    private string GetIconValueChanged() => (IconListValue - 1) switch
    {
        0 => Localizer["RatesCry"],
        1 => Localizer["RatesTear"],
        2 => Localizer["RatesSmile"],
        3 => Localizer["RatesSurprise"],
        _ => Localizer["RatesGrin"]
    };

    private IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
        new EventItem()
        {
            Name = "ValueChanged",
            Description =Localizer["RatesEvent1"],
            Type ="EventCallback<int>"
        }
    };

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "Value",
            Description = Localizer["RatesValue"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Max",
            Description = Localizer["RatesMax"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "IsDisabled",
            Description = Localizer["RatesIsDisabled"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "IsReadonly",
            Description = Localizer["RatesIsReadonly"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "IsWrap",
            Description = Localizer["RatesIsWrap"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "ShowValue",
            Description = Localizer["RatesShowValue"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "ItemTemplate",
            Description = Localizer["RatesItemTemplate"],
            Type = "RenderFragment<int>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
