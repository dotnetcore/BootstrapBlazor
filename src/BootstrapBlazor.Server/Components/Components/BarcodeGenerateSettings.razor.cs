// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// BarcodeGenerateSettings 组件
/// </summary>
public partial class BarcodeGenerateSettings
{
    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public BarcodeGeneratorOption Options { get; set; } = new();

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public EventCallback<BarcodeGeneratorOption> OptionsChanged { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string Value { get; set; } = "12345";

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public EventCallback<string> ValueChanged { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string SvgString { get; set; } = "";

    private async Task OnValueChanged(string v)
    {
        Value = v;
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
    }

    private async Task OnFormatChanged(EnumBarcodeFormat format)
    {
        Value = DefaultValues[format.ToString()];
        if (ValueChanged.HasDelegate)
        {
            await ValueChanged.InvokeAsync(Value);
        }
        Options.Format = format;
        await TriggerOptionsChanged();
    }

    private async Task TriggerOptionsChanged()
    {
        if (OptionsChanged.HasDelegate)
        {
            await OptionsChanged.InvokeAsync(Options);
        }
    }

    private readonly Dictionary<string, string> DefaultValues = new()
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
        { "Pharmacode", "1234"}
    };

    private bool Flat
    {
        get
        {
            return Options.Flat ?? false;
        }
        set
        {
            Options.Flat = value;
            _ = TriggerOptionsChanged();
        }
    }

    private bool DisplayValue
    {
        get
        {
            return Options.DisplayValue ?? true;
        }
        set
        {
            Options.DisplayValue = value;
            _ = TriggerOptionsChanged();
        }
    }

    private string? Text
    {
        get
        {
            return Options.Text ?? "";
        }
        set
        {
            Options.Text = value;
            _ = TriggerOptionsChanged();
        }
    }

    private EnumBarcodeFormat Format
    {
        get
        {
            return Options.Format ?? EnumBarcodeFormat.CODE128;
        }
        set
        {
            Options.Format = value;
            _ = TriggerOptionsChanged();
        }
    }

    private EnumBarcodeTextAlign TextAlign
    {
        get
        {
            return Options.TextAlign ?? EnumBarcodeTextAlign.Center;
        }
        set
        {
            Options.TextAlign = value;
            _ = TriggerOptionsChanged();
        }
    }

    private EnumBarcodeTextFont Font
    {
        get
        {
            return Options.Font ?? EnumBarcodeTextFont.Monospace;
        }
        set
        {
            Options.Font = value;
            _ = TriggerOptionsChanged();
        }
    }

    private EnumBarcodeTextFontOption FontOptions
    {
        get
        {
            return Options.FontOptions ?? EnumBarcodeTextFontOption.Normal;
        }
        set
        {
            Options.FontOptions = value;
            _ = TriggerOptionsChanged();
        }
    }

    private int FontSize
    {
        get
        {
            return Options.FontSize ?? 20;
        }
        set
        {
            Options.FontSize = value;
            _ = TriggerOptionsChanged();
        }
    }

    private int TextMargin
    {
        get
        {
            return Options.TextMargin ?? 2;
        }
        set
        {
            Options.TextMargin = value;
            _ = TriggerOptionsChanged();
        }
    }

    private EnumBarcodeTextPosition TextPosition
    {
        get
        {
            return Options.TextPosition ?? EnumBarcodeTextPosition.Bottom;
        }
        set
        {
            Options.TextPosition = value;
            _ = TriggerOptionsChanged();
        }
    }

    private string Background
    {
        get
        {
            return Options.Background ?? "#FFFFFF";
        }
        set
        {
            Options.Background = value;
            _ = TriggerOptionsChanged();
        }
    }

    private string LineColor
    {
        get
        {
            return Options.LineColor ?? "#000000";
        }
        set
        {
            Options.LineColor = value;
            _ = TriggerOptionsChanged();
        }
    }

    private int Width
    {
        get
        {
            return Options.Width ?? 2;
        }
        set
        {
            Options.Width = value;
            _ = TriggerOptionsChanged();
        }
    }

    private int Height
    {
        get
        {
            return Options.Height ?? 100;
        }
        set
        {
            Options.Height = value;
            _ = TriggerOptionsChanged();
        }
    }

    private int Margin
    {
        get
        {
            return Options.Margin ?? 10;
        }
        set
        {
            Options.Margin = value;
            _ = TriggerOptionsChanged();
        }
    }

    private int MarginLeft
    {
        get
        {
            return Options.MarginLeft ?? 10;
        }
        set
        {
            Options.MarginLeft = value;
            _ = TriggerOptionsChanged();
        }
    }

    private int MarginTop
    {
        get
        {
            return Options.MarginTop ?? 10;
        }
        set
        {
            Options.MarginTop = value;
            _ = TriggerOptionsChanged();
        }
    }

    private int MarginRight
    {
        get
        {
            return Options.MarginRight ?? 10;
        }
        set
        {
            Options.MarginRight = value;
            _ = TriggerOptionsChanged();
        }
    }

    private int MarginBottom
    {
        get
        {
            return Options.MarginBottom ?? 10;
        }
        set
        {
            Options.MarginBottom = value;
            _ = TriggerOptionsChanged();
        }
    }
}
