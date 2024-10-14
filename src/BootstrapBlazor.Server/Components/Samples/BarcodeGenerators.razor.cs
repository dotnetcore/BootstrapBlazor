// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.ComponentModel;

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
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    protected AttributeItem[] GetAttributes() =>
    [
        //new()
        //{
        //    Name = nameof(BarcodeGenerator.Type),
        //    Description = Localizer[nameof(BarcodeGenerator.Type)].Value,
        //    Type = "EnumBarcodeType",
        //    ValueList = "CODE128 / CODE128A / CODE128B / CODE128C / EAN13 / EAN8 / EAN5 / EAN2 / UPC / CODE39 / ITF14 / ITF / MSI / MSI10 / MSI11 / MSI1010 / MSI1110 / Pharmacode",
        //    DefaultValue = "'auto' (CODE128)"
        //},
        new()
        {
            Name = nameof(BarcodeGenerator.Value),
            Description = Localizer[nameof(BarcodeGenerator.Value)].Value,
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(BarcodeGenerator.Options),
            Description = Localizer[nameof(BarcodeGenerator.Options)].Value,
            Type = "BarcodeGeneratorOption",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];

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
            Type = "EnumBarcodeType",
            ValueList = "CODE128 / CODE128A / CODE128B / CODE128C / EAN13 / EAN8 / EAN5 / EAN2 / UPC / CODE39 / ITF14 / ITF / MSI / MSI10 / MSI11 / MSI1010 / MSI1110 / Pharmacode",
            DefaultValue = "'auto' (CODE128)"
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
            Type = "EnumBarcodeFontOption",
            ValueList = "normal / bold / italic / bold italic",
            DefaultValue = "2"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.Font),
            Description = Localizer[nameof(BarcodeGeneratorOption.Font)],
            Type = "EnumBarcodeFont",
            ValueList = "Monospace / SansSerif / Serif / Fantasy / Cursive",
            DefaultValue = "Monospace"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.TextAlign),
            Description = Localizer[nameof(BarcodeGeneratorOption.TextAlign)],
            Type = "EnumBarcodeTextAlign",
            ValueList = "left / center / right",
            DefaultValue = "center"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.TextPosition),
            Description = Localizer[nameof(BarcodeGeneratorOption.TextPosition)],
            Type = "EnumTextPosition",
            ValueList = "bottom / top",
            DefaultValue = "bottom"
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
