// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Radios
/// </summary>
public sealed partial class Radios
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? DemoValues { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? AutoSelectValues { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? DisableValues { get; set; }

    [NotNull]
    private EnumEducation? SelectedEnumItem { get; set; }

    [NotNull]
    private EnumEducation? SelectedEnumItem2 { get; set; }

    [NotNull]
    private EnumEducation? SelectedEnumItem3 { get; set; }

    [NotNull]
    private ConsoleLogger? BindLogger { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? Items { get; set; }

    private SelectedItem BindRadioItem { get; set; } = new SelectedItem();

    [NotNull]
    private IEnumerable<IconSelectedItem>? IconDemoValues { get; set; }

    [NotNull]
    private Foo? Model { get; set; }

    [NotNull]
    private List<SelectedItem>? FooItems { get; set; }

    private Task OnSelectedChanged(IEnumerable<SelectedItem> values, string val)
    {
        var value = values.FirstOrDefault();
        Logger.Log($"{Localizer["RadiosLog1"]} {value?.Value}  {Localizer["RadiosLog2"]}{value?.Text}  {Localizer["RadiosLog3"]}{val}");
        return Task.CompletedTask;
    }

    [NotNull]
    private IEnumerable<SelectedItem<Foo>>? GenericItems { get; set; }

    private Foo _selectedFoo = new() { Id = 1 };

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        DemoValues = new List<SelectedItem>(2)
        {
            new("1", Localizer["RadiosItem1"]),
            new("2", Localizer["RadiosItem2"])
        };

        AutoSelectValues = new List<SelectedItem>(2)
        {
            new("1", Localizer["RadiosItem1"]),
            new("2", Localizer["RadiosItem2"])
        };

        DisableValues = new List<SelectedItem>(2)
        {
            new("1", Localizer["RadiosItem1"]),
            new("2", Localizer["RadiosItem2"]) { IsDisabled = true }
        };

        Items =
        [
            new("1", Localizer["RadiosAdd1"]),
            new("2", Localizer["RadiosAdd2"])
        ];

        IconDemoValues = new List<IconSelectedItem>()
        {
            new() { Text = "Item1", Value = "1", Icon = "fa-solid fa-users" },
            new() { Text = "Item2", Value = "2", Icon = "fa-solid fa-users-gear" }
        };

        Model = Foo.Generate(LocalizerFoo);
        FooItems = Foo.GetCompleteItems(LocalizerFoo);

        _selectedFoo.Name = LocalizerFoo["Foo.Name", "001"];
        GenericItems = new List<SelectedItem<Foo>>
        {
            new() { Text = Localizer["Item1"], Value = _selectedFoo },
            new() { Text = Localizer["Item2"], Value = new Foo { Id = 2, Name = LocalizerFoo["Foo.Name", "002"] } },
            new() { Text = Localizer["Item3"], Value = new Foo { Id = 3, Name = LocalizerFoo["Foo.Name", "003"] } },
        };
    }

    private Task OnItemChanged(IEnumerable<SelectedItem> values, SelectedItem val)
    {
        var value = values.FirstOrDefault();
        BindLogger.Log($"{Localizer["RadiosLog1"]} {value?.Value} {Localizer["RadiosLog1"]} {value?.Text}");
        return Task.CompletedTask;
    }

    class IconSelectedItem : SelectedItem
    {
        public string? Icon { get; init; }
    }

    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "DisplayText",
            Description = Localizer["RadiosDisplayText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "—"
        },
        new()
        {
            Name = "GroupName",
            Description = Localizer["RadiosGroupName"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "—"
        },
        new()
        {
            Name = "NullItemText",
            Description = Localizer["RadiosNullItemText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "—"
        },
        new()
        {
            Name = "IsDisabled",
            Description = Localizer["RadiosIsDisabled"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsVertical",
            Description = Localizer["RadiosIsVertical"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(RadioList<string>.IsButton),
            Description = Localizer["RadiosIsButton"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsAutoAddNullItem",
            Description = Localizer["RadiosIsAutoAddNullItem"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Items",
            Description = Localizer["RadiosItems"],
            Type = "IEnumerable<TItem>",
            ValueList = " — ",
            DefaultValue = "—"
        },
        new()
        {
            Name = "AutoSelectFirstWhenValueIsNull",
            Description = Localizer["RadiosAutoSelectFirstWhenValueIsNull"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        }
    ];

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private EventItem[] GetEvents() =>
    [
        new()
        {
            Name = "OnSelectedChanged",
            Description = Localizer["RadiosOnSelectedChangedEvent"],
            Type ="Func<IEnumerable<SelectedItem>, TValue, Task>"
        }
    ];
}
