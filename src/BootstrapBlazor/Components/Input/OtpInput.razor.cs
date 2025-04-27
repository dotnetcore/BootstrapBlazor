// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// OTP input component
/// </summary>
[BootstrapModuleAutoLoader("Input/OtpInput.razor.js", JSObjectReference = true)]
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

    /// <summary>
    /// Gets or sets the placeholder of the OTP input. Default is null.
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    private string? ClassString => CssBuilder.Default("bb-opt-input")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? ItemClassString => CssBuilder.Default("bb-opt-item")
        .AddClass("disabled", IsDisabled)
        .AddClass(ValidCss)
        .Build();

    private string? InputClassString => CssBuilder.Default("bb-opt-item")
        .AddClass("input-number-fix", Type == OtpInputType.Number)
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
        OtpInputType.Number => null,
        _ => "1"
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

        Value ??= "";
        _values = new char[Digits];
        for (var index = 0; index < Digits; index++)
        {
            if (index < Value.Length)
            {
                _values[index] = Value[index];
            }
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(TriggerSetValue));

    private char? GetValueString(int index)
    {
        return _values[index] != 0 ? _values[index] : null;
    }


    /// <summary>
    /// Trigger value changed event callback. Trigger by JavaScript.
    /// </summary>
    /// <param name="val"></param>
    /// <returns></returns>
    [JSInvokable]
    public Task TriggerSetValue(string val)
    {
        SetValue(val);
        return Task.CompletedTask;
    }
}
