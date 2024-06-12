// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// CheckboxLists
/// </summary>
public partial class CheckboxLists
{
    [NotNull]
    private IEnumerable<SelectedItem>? Items3 { get; set; }

    private IEnumerable<EnumEducation> SelectedEnumValues { get; set; } = new List<EnumEducation>
    {
        EnumEducation.Middle, EnumEducation.Primary
    };


    [NotNull]
    private IEnumerable<SelectedItem>? Items1 { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? Items2 { get; set; }

    private IEnumerable<int> ShowLabelValue1 { get; set; } = new int[] { 9, 10 };

    private IEnumerable<string> Value2 { get; set; } = new string[] { "13", "15" };

    private Foo Dummy { get; set; } = new Foo();

    [NotNull]
    private IEnumerable<SelectedItem>? Items4 { get; set; }

    /// <summary>
    /// OnInitialized method
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items1 = new List<SelectedItem>(new List<SelectedItem>
        {
            new SelectedItem { Text = "Item 9", Value = "9" },
            new SelectedItem { Text = "Item 10", Value = "10" },
            new SelectedItem { Text = "Item 11", Value = "11" },
            new SelectedItem { Text = "Item 12", Value = "12" },
        });

        Items2 = new List<SelectedItem>(new List<SelectedItem>
        {
            new SelectedItem { Text = "Item 13", Value = "13" },
            new SelectedItem { Text = "Item 14", Value = "14" },
            new SelectedItem { Text = "Item 15", Value = "15" },
            new SelectedItem { Text = "Item 16", Value = "16" },
        });

        Items3 = new List<SelectedItem>(new List<SelectedItem>
        {
            new SelectedItem { Text = Localizer["item1"], Value = Localizer["item1"] },
            new SelectedItem { Text = Localizer["item2"], Value = Localizer["item2"] },
            new SelectedItem { Text = Localizer["item3"], Value = Localizer["item3"] },
            new SelectedItem { Text = Localizer["item4"], Value = Localizer["item4"] },
        });

        Items4 = new List<SelectedItem>(new List<SelectedItem>
        {
            new SelectedItem { Text = Localizer["item1"], Value = Localizer["item1"] },
            new SelectedItem { Text = Localizer["item2"], Value = Localizer["item2"] },
            new SelectedItem { Text = Localizer["item3"], Value = Localizer["item3"] },
            new SelectedItem { Text = Localizer["item4"], Value = Localizer["item4"] },
        });

        Dummy = new Foo() { Name = Localizer["Foo"] };
        Model = Foo.Generate(LocalizerFoo);
        FooItems = Foo.GenerateHobbies(LocalizerFoo);
    }

    [NotNull]
    private Foo? Model { get; set; }

    [NotNull]
    private IEnumerable<string>? SelectedItems { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? FooItems { get; set; }

    private string? Value { get; set; }

    private string Value1 { get; set; } = "1,3";

    [NotNull]
    private ConsoleLogger? NormalLogger { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? Items { get; set; } = new List<SelectedItem>(new List<SelectedItem>
    {
        new SelectedItem { Text = "Item 1", Value = "1" },
        new SelectedItem { Text = "Item 2", Value = "2" , IsDisabled = true },
        new SelectedItem { Text = "Item 3", Value = "3" },
        new SelectedItem { Text = "Item 4", Value = "4" },
    });

    private Task OnSelectedChanged(IEnumerable<SelectedItem> items, string value)
    {
        NormalLogger.Log($"{Localizer["Header"]} {items.Count(i => i.Active)} {Localizer["Counter"]}：{value}");
        return Task.CompletedTask;
    }

    [Inject]
    [NotNull]
    private IStringLocalizer<CheckboxLists>? Localizer { get; set; }

    [Inject]
    [NotNull]
    IStringLocalizer<Foo>? LocalizerFoo { get; set; }

    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Items",
            Description = Localizer["Att1"],
            Type = "IEnumerable<SelectedItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsDisabled",
            Description = Localizer["Att1"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Value",
            Description = Localizer["Att1"],
            Type = "TValue",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsVertical",
            Description = Localizer["Att1"],
            Type = "boolean",
            ValueList = " true / false ",
            DefaultValue = " false "
        }
    ];

    /// <summary>
    /// Get event method
    /// </summary>
    /// <returns></returns>
    private EventItem[] GetEvents() =>
    [
        new()
        {
            Name = "OnSelectedChanged",
            Description = Localizer["Event1"],
            Type ="Func<IEnumerable<SelectedItem>, TValue, Task>"
        }
    ];
}
