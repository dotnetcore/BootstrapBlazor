// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class CheckboxLists
{
    [NotNull]
    private IEnumerable<SelectedItem>? Items1 { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? Items2 { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? Items3 { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? Items4 { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? Items5 { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? Items6 { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<CheckboxLists>? Localizer { get; set; }

    private IEnumerable<EnumEducation> SelectedEnumValues { get; set; } = new List<EnumEducation> { EnumEducation.Middle, EnumEducation.Primary };

    private string Value1 { get; set; } = "1,3";

    private IEnumerable<int> Value2 { get; set; } = new int[] { 9, 10 };

    private IEnumerable<string> Value3 { get; set; } = new string[] { "13", "15" };
    private Foo Dummy { get; set; } = new Foo();
    private Foo Dummy1 { get; set; } = new Foo();
    private Foo Dummy2 { get; set; } = new Foo();

    private BlockLogger? Trace { get; set; }

    /// <summary>
    /// OnInitialized method
    /// </summary>
    /// <returns></returns>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items1 = new List<SelectedItem>(new List<SelectedItem>
        {
            new SelectedItem { Text = "Item 1", Value = "1" },
            new SelectedItem { Text = "Item 2", Value = "2" },
            new SelectedItem { Text = "Item 3", Value = "3" },
            new SelectedItem { Text = "Item 4", Value = "4" },
        });

        Items2 = new List<SelectedItem>(new List<SelectedItem>
        {
            new SelectedItem { Text = Localizer["SL1"], Value = Localizer["SL1"] },
            new SelectedItem { Text = Localizer["SL2"], Value = Localizer["SL2"] },
            new SelectedItem { Text = Localizer["SL3"], Value = Localizer["SL3"] },
            new SelectedItem { Text = Localizer["SL4"], Value = Localizer["SL4"] },
        });

        Items3 = new List<SelectedItem>(new List<SelectedItem>
        {
            new SelectedItem { Text = "Item 9", Value = "9" },
            new SelectedItem { Text = "Item 10", Value = "10" },
            new SelectedItem { Text = "Item 11", Value = "11" },
            new SelectedItem { Text = "Item 12", Value = "12" },
        });

        Items4 = new List<SelectedItem>(new List<SelectedItem>
        {
            new SelectedItem { Text = "Item 13", Value = "13" },
            new SelectedItem { Text = "Item 14", Value = "14" },
            new SelectedItem { Text = "Item 15", Value = "15" },
            new SelectedItem { Text = "Item 16", Value = "16" },
        });

        Items5 = new List<SelectedItem>(new List<SelectedItem>
        {
            new SelectedItem { Text = Localizer["SL10"], Value = Localizer["SL10"] },
            new SelectedItem { Text = Localizer["SL11"], Value = Localizer["SL11"] },
            new SelectedItem { Text = Localizer["SL12"], Value = Localizer["SL12"] },
            new SelectedItem { Text = Localizer["SL13"], Value = Localizer["SL13"] },
        });

        Items6 = new List<SelectedItem>(new List<SelectedItem>
        {
            new SelectedItem { Text = Localizer["SL21"], Value = Localizer["SL21"] },
            new SelectedItem { Text = Localizer["SL22"], Value = Localizer["SL22"] },
            new SelectedItem { Text = Localizer["SL23"], Value = Localizer["SL23"] },
            new SelectedItem { Text = Localizer["SL24"], Value = Localizer["SL24"] },
        });

        Dummy = new Foo() { Name = Localizer["Foo1"] };
        Dummy1 = new Foo() { Name = Localizer["Foo2"] };
        Dummy2 = new Foo() { Name = Localizer["Foo3"] };
    }

    private Task OnSelectedChanged(IEnumerable<SelectedItem> items, string value)
    {
        Trace?.Log($"{Localizer["Header"]} {items.Where(i => i.Active).Count()} {Localizer["Counter"]}：{value}");
        return Task.CompletedTask;
    }

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "Items",
            Description = Localizer["Att1"],
            Type = "IEnumerable<SelectedItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "IsDisabled",
            Description = Localizer["Att1"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new AttributeItem(){
            Name = "Value",
            Description = Localizer["Att1"],
            Type = "TValue",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem(){
            Name = "IsVertical",
            Description = Localizer["Att1"],
            Type = "boolean",
            ValueList = " true / false ",
            DefaultValue = " false "
        }
    };

    /// <summary>
    /// Get event method
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
