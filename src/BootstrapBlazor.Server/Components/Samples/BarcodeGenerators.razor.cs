// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Web;
using System.ComponentModel;

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// BarcodeGenerators
/// </summary>
public partial class BarcodeGenerators
{

    private BarcodeGeneratorOption Options { get; set; } = new();

    private BarcodeGeneratorOption Options1 { get; set; } = new()
    {
        Value = "Hi!",
        FontSize = 40,
        Background = "#4b8b7f",
        LineColor = "#ffffff",
        Margin = 40,
        TextMargin = 80
    };

    private BarcodeGeneratorOption Options2 { get; set; } = new()
    {
        Value = "Hi!",
        TextAlign = EnumBarcodeTextAlign.left,
        TextPosition = EnumTextPosition.top,
        Font = EnumBarcodeFont.Cursive,
        FontOptions = EnumBarcodeFontOption.bold,
        FontSize = 40,
        TextMargin = 15,
        Text = "Special"
    };


    private BarcodeGeneratorOption Options3 { get; set; } = new()
    {
        Value = "1234",
        Type = EnumBarcodeType.pharmacode,
        DisplayValue = false,
        Height = 50,
        Width = 6
    };


    private Dictionary<string, string> defaultValues = new Dictionary<string, string>()
    {
        { "CODE128", "Example 1234"},
        { "CODE128A", "EXAMPLE"},
        { "CODE128B", "Example text"},
        { "CODE128C", "12345678"},
        { "EAN13", "1234567890128"},
        { "EAN8", "12345670"},
        { "EAN5", "12340"},
        { "EAN2", "10"},
        { "UPC", "123456789999"},
        { "CODE39", "EXAMPLE TEXT"},
        { "ITF14", "10012345000017"},
        { "ITF", "123456"},
        { "MSI", "123456"},
        { "MSI10", "123456"},
        { "MSI11", "123456"},
        { "MSI1010", "123456"},
        { "MSI1110", "123456"},
        { "pharmacode", "1234"}
    };

    [DisplayName("条码文字")]
    private string? value { get; set; } = "12345";

    [DisplayName("SVG内容")]
    private string? svg { get; set; }

    private Task OnResult(string value)
    {
        svg = value;
        return Task.CompletedTask;
    }

    private Task OnValueChanged(EnumBarcodeType type)
    {
        value = defaultValues[type.ToString()];
        return Task.CompletedTask;
    }

    /// <summary>
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    protected AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = nameof(BarCodeGenerator.Type),
            Description = Localizer[nameof(BarCodeGenerator.Type)].Value,
            Type = "EnumBarcodeType",
            ValueList = "CODE128 / CODE128A / CODE128B / CODE128C / EAN13 / EAN8 / EAN5 / EAN2 / UPC / CODE39 / ITF14 / ITF / MSI / MSI10 / MSI11 / MSI1010 / MSI1110 / pharmacode",
            DefaultValue = "'auto' (CODE128)"
        },
        new()
        {
            Name = nameof(BarCodeGenerator.Value),
            Description = Localizer[nameof(BarCodeGenerator.Value)].Value,
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(BarCodeGenerator.Options),
            Description = Localizer[nameof(BarCodeGenerator.Options)].Value,
            Type = "BarcodeGeneratorOption",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(BarCodeGenerator.OnResult),
            Description = Localizer[nameof(BarCodeGenerator.OnResult)].Value,
            Type = "Func",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(BarCodeGenerator.OnError),
            Description = Localizer[nameof(BarCodeGenerator.OnError)].Value,
            Type = "Func",
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
            Name = nameof(BarcodeGeneratorOption.Type),
            Description = Localizer[nameof(BarcodeGeneratorOption.Type)].Value,
            Type = "EnumBarcodeType",
            ValueList = "CODE128 / CODE128A / CODE128B / CODE128C / EAN13 / EAN8 / EAN5 / EAN2 / UPC / CODE39 / ITF14 / ITF / MSI / MSI10 / MSI11 / MSI1010 / MSI1110 / pharmacode",
            DefaultValue = "'auto' (CODE128)"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.Value),
            Description = Localizer[nameof(BarcodeGeneratorOption.Value)].Value,
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.Width),
            Description = Localizer[nameof(BarcodeGeneratorOption.Width)].Value,
            Type = "int",
            ValueList = " — ",
            DefaultValue = "2"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.Height),
            Description = Localizer[nameof(BarcodeGeneratorOption.Height)].Value,
            Type = "int",
            ValueList = " — ",
            DefaultValue = "100"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.DisplayValue),
            Description = Localizer[nameof(BarcodeGeneratorOption.DisplayValue)].Value,
            Type = "bool",
            ValueList = " — ",
            DefaultValue = "true"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.Text),
            Description = Localizer[nameof(BarcodeGeneratorOption.Text)].Value,
            Type = "string",
            ValueList = " - ",
            DefaultValue = " - "
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.FontOptions),
            Description = Localizer[nameof(BarcodeGeneratorOption.FontOptions)].Value,
            Type = "EnumBarcodeFontOption",
            ValueList = "normal / bold / italic / bold italic",
            DefaultValue = "2"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.Font),
            Description = Localizer[nameof(BarcodeGeneratorOption.Font)].Value,
            Type = "EnumBarcodeFont",
            ValueList = "Monospace / SansSerif / Serif / Fantasy / Cursive",
            DefaultValue = "Monospace"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.TextAlign),
            Description = Localizer[nameof(BarcodeGeneratorOption.TextAlign)].Value,
            Type = "EnumBarcodeTextAlign",
            ValueList = "left / center / right",
            DefaultValue = "center"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.TextPosition),
            Description = Localizer[nameof(BarcodeGeneratorOption.TextPosition)].Value,
            Type = "EnumTextPosition",
            ValueList = "bottom / top",
            DefaultValue = "bottom"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.TextMargin),
            Description = Localizer[nameof(BarcodeGeneratorOption.TextMargin)].Value,
            Type = "int",
            ValueList = " — ",
            DefaultValue = "2"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.FontSize),
            Description = Localizer[nameof(BarcodeGeneratorOption.FontSize)].Value,
            Type = "int",
            ValueList = " — ",
            DefaultValue = "20"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.Background),
            Description = Localizer[nameof(BarcodeGeneratorOption.TextPosition)].Value,
            Type = "string",
            ValueList = " — ",
            DefaultValue = "#ffffff"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.LineColor),
            Description = Localizer[nameof(BarcodeGeneratorOption.LineColor)].Value,
            Type = "string",
            ValueList = " — ",
            DefaultValue = "#000000"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.Margin),
            Description = Localizer[nameof(BarcodeGeneratorOption.Margin)].Value,
            Type = "int",
            ValueList = " — ",
            DefaultValue = "10"
        },
        new()
        {
            Name = nameof(BarcodeGeneratorOption.MarginTop),
            Description = Localizer[nameof(BarcodeGeneratorOption.MarginTop)].Value,
            Type = "int",
            ValueList = " — ",
            DefaultValue = " - "
        }, 
        new()
        {
            Name = nameof(BarcodeGeneratorOption.MarginBottom),
            Description = Localizer[nameof(BarcodeGeneratorOption.MarginBottom)].Value,
            Type = "int",
            ValueList = " — ",
            DefaultValue = " - "
        }, 
        new()
        {
            Name = nameof(BarcodeGeneratorOption.MarginLeft),
            Description = Localizer[nameof(BarcodeGeneratorOption.MarginLeft)].Value,
            Type = "int",
            ValueList = " — ",
            DefaultValue = " - "
        }, 
        new()
        {
            Name = nameof(BarcodeGeneratorOption.MarginRight),
            Description = Localizer[nameof(BarcodeGeneratorOption.MarginRight)].Value,
            Type = "int",
            ValueList = " — ",
            DefaultValue = " - "
        }, 
        new()
        {
            Name = nameof(BarcodeGeneratorOption.Flat),
            Description = Localizer[nameof(BarcodeGeneratorOption.Flat)].Value,
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        }
    ];
}
