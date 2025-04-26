// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// OTP input component
/// </summary>
[BootstrapModuleAutoLoader("Input/OtpInput.razor.js")]
public partial class OtpInput
{
    /// <summary>
    /// Gets or sets the length of the OTP input. Default is 6.
    /// </summary>
    [Parameter]
    public int Digits { get; set; } = 6;

    /// <summary>
    /// Gets or sets whether the OTP input is readonly. Default is false.
    /// </summary>
    [Parameter]
    public bool IsReadonly { get; set; }

    /// <summary>
    /// Gets or sets the value type of the OTP input. Default is <see cref="OtpInputType.Number"/>.
    /// </summary>
    [Parameter]
    public OtpInputType Type { get; set; }

    private string? ClassString => CssBuilder.Default("bb-opt-input")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? ItemClassString => CssBuilder.Default("bb-opt-item")
        .AddClass("input-number-fix", Type == OtpInputType.Number)
        .AddClass("disabled", IsDisabled)
        .AddClass(ValidCss)
        .Build();

    private string TypeString => Type switch
    {
        OtpInputType.Number => "number",
        OtpInputType.Password => "password",
        _ => "text"
    };

    private string? MaxLengthString => Type switch
    {
        OtpInputType.Text => "1",
        _ => null
    };

    private string? TypeModeString => Type switch
    {
        OtpInputType.Number => "numeric",
        _ => null
    };

    private char[] _values = [];

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        _values = new char[Digits];
        for (var index = 0; index < Digits; index++)
        {
            if (index < Value.Length)
            {
                _values[index] = Value[index];
            }
        }
    }

    private char? GetValueString(int index)
    {
        char? c = _values.ElementAtOrDefault(index);
        if (c == 0)
        {
            c = null;
        }
        return c;
    }

    private void OnChanged(string? v, int index)
    {
        if (index < Digits && !string.IsNullOrEmpty(v))
        {
            _values[index] = v[0];
        }
    }
}
