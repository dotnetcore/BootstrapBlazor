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
public sealed partial class Radios
{
    [NotNull]
    private BlockLogger? Trace { get; set; }

    [NotNull]
    private BlockLogger? BinderLog { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? DemoValues { get; set; }

    private Task OnSelectedChanged(IEnumerable<SelectedItem> values, string val)
    {
        var value = values.FirstOrDefault();
        Trace.Log($"{Localizer["Log1"]} {value?.Value}  {Localizer["Log1"]}{value?.Text}  {Localizer["Log3"]}{val}");
        return Task.CompletedTask;
    }

    private Task OnItemChanged(IEnumerable<SelectedItem> values, SelectedItem val)
    {
        var value = values.FirstOrDefault();
        BinderLog.Log($"{Localizer["Log1"]} {value?.Value} {Localizer["Log1"]} {value?.Text}");
        return Task.CompletedTask;
    }

    [NotNull]
    private IEnumerable<SelectedItem>? Items { get; set; }

    private SelectedItem BindRadioItem { get; set; } = new SelectedItem();

    [NotNull]
    private EnumEducation? SelectedEnumItem { get; set; }

    [NotNull]
    private EnumEducation? SelectedEnumItem2 { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        DemoValues = new List<SelectedItem>(2)
            {
                new SelectedItem("1", Localizer["Item1"]!),
                new SelectedItem("2", Localizer["Item2"]!),
            };
        Items = new SelectedItem[]
        {
                new SelectedItem("1", Localizer["Add1"]!) { Active = true },
                new SelectedItem("2", Localizer["Add2"]!)
        };
    }

    private IEnumerable<AttributeItem> GetAttributes() => new[]
    {
        new AttributeItem() {
            Name = "DisplayText",
            Description = Localizer["Att1"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "—"
        },
        new AttributeItem() {
            Name = "GroupName",
            Description = Localizer["GroupName"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "—"
        },
        new AttributeItem() {
            Name = "NullItemText",
            Description = Localizer["Att2"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "—"
        },
        new AttributeItem() {
            Name = "IsDisabled",
            Description = Localizer["Att3"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsVertical",
            Description = Localizer["Att4"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsAutoAddNullItem",
            Description = Localizer["Att5"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "Items",
            Description = Localizer["Att6"],
            Type = "IEnumerable<TItem>",
            ValueList = " — ",
            DefaultValue = "—"
        }
    };

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
        new EventItem()
        {
            Name = "OnSelectedChanged",
            Description = Localizer["Event1"],
            Type ="Func<IEnumerable<SelectedItem>, TValue, Task>"
        }
    };
}
