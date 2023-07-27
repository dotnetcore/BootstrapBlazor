// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

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

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        DemoValues = new List<SelectedItem>(2)
        {
            new SelectedItem("1", Localizer["RadiosItem1"]),
            new SelectedItem("2", Localizer["RadiosItem2"])
        };

        DisableValues = new List<SelectedItem>(2)
        {
            new SelectedItem("1", Localizer["RadiosItem1"]),
            new SelectedItem("2", Localizer["RadiosItem2"]) { IsDisabled = true }
        };

        Items = new SelectedItem[]
        {
            new SelectedItem("1", Localizer["RadiosAdd1"]),
            new SelectedItem("2", Localizer["RadiosAdd2"])
        };

        IconDemoValues = new List<IconSelectedItem>()
        {
            new IconSelectedItem() { Text = "Item1", Value = "1", Icon = "fa-solid fa-users" },
            new IconSelectedItem() { Text = "Item2", Value = "2", Icon = "fa-solid fa-users-gear" }
        };

        Model = Foo.Generate(LocalizerFoo);
        FooItems = Foo.GetCompleteItems(LocalizerFoo);
    }

    private Task OnItemChanged(IEnumerable<SelectedItem> values, SelectedItem val)
    {
        var value = values.FirstOrDefault();
        BindLogger.Log($"{Localizer["RadiosLog1"]} {value?.Value} {Localizer["RadiosLog1"]} {value?.Text}");
        return Task.CompletedTask;
    }


    class IconSelectedItem : SelectedItem
    {
        public string? Icon { get; set; }
    }

    private IEnumerable<AttributeItem> GetAttributes() => new[]
    {
        new AttributeItem() {
            Name = "DisplayText",
            Description = Localizer["RadiosDisplayText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "—"
        },
        new AttributeItem() {
            Name = "GroupName",
            Description = Localizer["RadiosGroupName"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "—"
        },
        new AttributeItem() {
            Name = "NullItemText",
            Description = Localizer["RadiosNullItemText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "—"
        },
        new AttributeItem() {
            Name = "IsDisabled",
            Description = Localizer["RadiosIsDisabled"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsVertical",
            Description = Localizer["RadiosIsVertical"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = nameof(RadioList<string>.IsButton),
            Description = Localizer["RadiosIsButton"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsAutoAddNullItem",
            Description = Localizer["RadiosIsAutoAddNullItem"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "Items",
            Description = Localizer["RadiosItems"],
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
            Description = Localizer["RadiosOnSelectedChangedEvent"],
            Type ="Func<IEnumerable<SelectedItem>, TValue, Task>"
        }
    };
}
