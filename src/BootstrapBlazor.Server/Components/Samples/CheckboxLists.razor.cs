// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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

    [NotNull]
    private IEnumerable<SelectedItem>? Items5 { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem<Foo>>? GenericItems { get; set; }

    private List<Foo>? _selectedFoos;

    /// <summary>
    /// OnInitialized method
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items1 = new List<SelectedItem>
        {
            new() { Text = "Item 9", Value = "9" },
            new() { Text = "Item 10", Value = "10" },
            new() { Text = "Item 11", Value = "11" },
            new() { Text = "Item 12", Value = "12" },
        };

        Items2 = new List<SelectedItem>
        {
            new() { Text = "Item 13", Value = "13" },
            new() { Text = "Item 14", Value = "14" },
            new() { Text = "Item 15", Value = "15" },
            new() { Text = "Item 16", Value = "16" },
        };

        Items3 = new List<SelectedItem>
        {
            new() { Text = Localizer["item1"], Value = Localizer["item1"] },
            new() { Text = Localizer["item2"], Value = Localizer["item2"] },
            new() { Text = Localizer["item3"], Value = Localizer["item3"] },
            new() { Text = Localizer["item4"], Value = Localizer["item4"] },
        };

        Items4 = new List<SelectedItem>
        {
            new() { Text = Localizer["item1"], Value = Localizer["item1"] },
            new() { Text = Localizer["item2"], Value = Localizer["item2"] },
            new() { Text = Localizer["item3"], Value = Localizer["item3"] },
            new() { Text = Localizer["item4"], Value = Localizer["item4"] },
        };

        Items5 = new List<SelectedItem>
        {
            new() { Text = Localizer["item1"], Value = Localizer["item1"] },
            new() { Text = Localizer["item2"], Value = Localizer["item2"] },
            new() { Text = Localizer["item3"], Value = Localizer["item3"] },
            new() { Text = Localizer["item4"], Value = Localizer["item4"] },
        };

        IconDemoValues = new List<IconSelectedItem>()
        {
            new() { Text = "Item1", Value = "1", Icon = "fa-solid fa-users" },
            new() { Text = "Item2", Value = "2", Icon = "fa-solid fa-users-gear" }
        };

        Dummy = new Foo() { Name = Localizer["Foo"] };
        Model = Foo.Generate(LocalizerFoo);
        FooItems = Foo.GenerateHobbies(LocalizerFoo);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        GenericItems = new List<SelectedItem<Foo>>()
        {
            new() { Text = Localizer["item1"], Value = new Foo() { Name = LocalizerFoo["Foo.Name", "001"] } },
            new() { Text = Localizer["item2"], Value = new Foo() { Name = LocalizerFoo["Foo.Name", "002"] } },
            new() { Text = Localizer["item3"], Value = new Foo() { Name = LocalizerFoo["Foo.Name", "003"] } },
        };
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
    private IEnumerable<SelectedItem>? Items { get; set; } = new List<SelectedItem>
    {
        new() { Text = "Item 1", Value = "1" },
        new() { Text = "Item 2", Value = "2" , IsDisabled = true },
        new() { Text = "Item 3", Value = "3" },
        new() { Text = "Item 4", Value = "4" },
    };

    [NotNull]
    private IEnumerable<IconSelectedItem>? IconDemoValues { get; set; }

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

    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

    private Task OnMaxSelectedCountExceed()
    {
        return ToastService.Information(Localizer["OnMaxSelectedCountExceedTitle"], Localizer["OnMaxSelectedCountExceedContent", 2]);
    }

    class IconSelectedItem : SelectedItem
    {
        public string? Icon { get; init; }
    }

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
        },
        new()
        {
            Name = nameof(CheckboxList<string>.MaxSelectedCount),
            Description = Localizer["AttributeMaxSelectedCount"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "0"
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
        },
        new()
        {
            Name = nameof(CheckboxList<string>.OnMaxSelectedCountExceed),
            Description = Localizer["AttributeOnMaxSelectedCountExceed"],
            Type = "Func<Task>"
        }
    ];
}
