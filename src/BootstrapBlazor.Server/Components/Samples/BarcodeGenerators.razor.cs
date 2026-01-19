// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// BarcodeGenerators
/// </summary>
public partial class BarcodeGenerators
{
    private BarcodeGeneratorOption Options { get; set; } = new();

    private string Value { get; set; } = "12345";

    private string? _svgString;

    private Task OnCompletedAsync(string? val)
    {
        _svgString = val;
        StateHasChanged();
        return Task.CompletedTask;
    }

    /// <summary>
    /// GetOptionsAttributes
    /// </summary>
    /// <returns></returns>
    protected AttributeItem[] GetOptionsAttributes() =>
    [
        new()
        {
            Name = nameof(BarcodeGeneratorOption.Format),
            Description = Localizer[nameof(BarcodeGeneratorOption.Format)],
            Type = nameof(EnumBarcodeFormat),
            ValueList = " — ",
            DefaultValue = "CODE128"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.Width),
            Description = Localizer[nameof(BarcodeGeneratorOption.Width)],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "2"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.Height),
            Description = Localizer[nameof(BarcodeGeneratorOption.Height)],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "100"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.DisplayValue),
            Description = Localizer[nameof(BarcodeGeneratorOption.DisplayValue)],
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.Text),
            Description = Localizer[nameof(BarcodeGeneratorOption.Text)],
            Type = "string",
            ValueList = " - ",
            DefaultValue = " - "
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.FontOptions),
            Description = Localizer[nameof(BarcodeGeneratorOption.FontOptions)],
            Type = nameof(EnumBarcodeTextFontOption),
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.Font),
            Description = Localizer[nameof(BarcodeGeneratorOption.Font)],
            Type = nameof(EnumBarcodeTextFont),
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.TextAlign),
            Description = Localizer[nameof(BarcodeGeneratorOption.TextAlign)],
            Type = nameof(EnumBarcodeTextAlign),
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.TextPosition),
            Description = Localizer[nameof(BarcodeGeneratorOption.TextPosition)],
            Type = nameof(EnumBarcodeTextPosition),
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.TextMargin),
            Description = Localizer[nameof(BarcodeGeneratorOption.TextMargin)],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "2"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.FontSize),
            Description = Localizer[nameof(BarcodeGeneratorOption.FontSize)],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "20"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.Background),
            Description = Localizer[nameof(BarcodeGeneratorOption.TextPosition)],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "#FFFFFF"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.LineColor),
            Description = Localizer[nameof(BarcodeGeneratorOption.LineColor)],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "#000000"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.Margin),
            Description = Localizer[nameof(BarcodeGeneratorOption.Margin)],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "10"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.MarginTop),
            Description = Localizer[nameof(BarcodeGeneratorOption.MarginTop)],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.MarginBottom),
            Description = Localizer[nameof(BarcodeGeneratorOption.MarginBottom)],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.MarginLeft),
            Description = Localizer[nameof(BarcodeGeneratorOption.MarginLeft)],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.MarginRight),
            Description = Localizer[nameof(BarcodeGeneratorOption.MarginRight)],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.Flat),
            Description = Localizer[nameof(BarcodeGeneratorOption.Flat)],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        }
    ];
}
