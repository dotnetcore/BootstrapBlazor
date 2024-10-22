// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// CountUps
/// </summary>
public partial class CountUps
{
    private static readonly CountUpOption _option = new() { DecimalPlaces = 2 };

    private double Value2 { get; set; }

    private double Value { get; set; }

    private ConsoleLogger? _logger;

    private bool _useOnCompleted;

    private readonly List<SelectedItem> _items = [];

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        _items.Add(new SelectedItem("", "Default (\"1234\")"));
        _items.Add(new SelectedItem("1", "Eastern Arabic (\"١٢٣٤\")"));
        OnUpdate();
    }

    private void OnUpdate()
    {
        Value = Value2;
    }

    private Task OnCompleted()
    {
        if (_useOnCompleted)
        {
            _logger?.Log($"{DateTime.Now}: from {_option.StartValue} to {Value2}");
        }
        return Task.CompletedTask;
    }

    private Task OnSelectedItemChanged(SelectedItem item)
    {
        var index = _items.IndexOf(item);
        if (index == 0)
        {
            _option.Numerals = null;
        }
        else if (index == 1)
        {
            _option.Numerals = ['٠', '١', '٢', '٣', '٤', '٥', '٦', '٧', '٨', '٩'];
        }
        return Task.CompletedTask;
    }

    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Value",
            Description = Localizer["Value"],
            Type = "TValue",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Option",
            Description = Localizer["CountOption"],
            Type = "CountOption",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnCompleted",
            Description = Localizer["OnCompleted"],
            Type = "Func<Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];

    private AttributeItem[] GetOptionAttributes() =>
    [
        new()
        {
            Name = nameof(CountUpOption.StartValue),
            Description = Localizer["StartValue"],
            Type = "decimal",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new()
        {
            Name = nameof(CountUpOption.Duration),
            Description = Localizer["Duration"],
            Type = "float",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new()
        {
            Name = nameof(CountUpOption.UseEasing),
            Description = Localizer["UseEasing"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(CountUpOption.DecimalPlaces),
            Description = Localizer["DecimalPlaces"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new()
        {
            Name = nameof(CountUpOption.Decimal),
            Description = Localizer["Decimal"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(CountUpOption.UseGrouping),
            Description = Localizer["UseGrouping"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(CountUpOption.UseIndianSeparators),
            Description = Localizer["UseIndianSeparators"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(CountUpOption.Separator),
            Description = Localizer["Separator"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(CountUpOption.Prefix),
            Description = Localizer["Prefix"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(CountUpOption.Suffix),
            Description = Localizer["Suffix"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(CountUpOption.SmartEasingAmount),
            Description = Localizer["SmartEasingAmount"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(CountUpOption.SmartEasingThreshold),
            Description = Localizer["SmartEasingThreshold"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(CountUpOption.EnableScrollSpy),
            Description = Localizer["EnableScrollSpy"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(CountUpOption.ScrollSpyDelay),
            Description = Localizer["ScrollSpyDelay"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "200"
        },
        new()
        {
            Name = nameof(CountUpOption.ScrollSpyOnce),
            Description = Localizer["ScrollSpyOnce"],
            Type = "boolean",
            ValueList = " — ",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(CountUpOption.Numerals),
            Description = Localizer["Numerals"],
            Type = "char[]",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
