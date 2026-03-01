// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">OTP 输入组件</para>
/// <para lang="en">OTP input component</para>
/// </summary>
[BootstrapModuleAutoLoader("Input/OtpInput.razor.js", JSObjectReference = true)]
public partial class OtpInput
{
    /// <summary>
    /// <para lang="zh">获得/设置 OTP 输入的长度。默认为 6</para>
    /// <para lang="en">Gets or sets the length of the OTP input. Default is 6</para>
    /// </summary>
    [Parameter]
    public int Digits { get; set; } = 6;

    /// <summary>
    /// <para lang="zh">获得/设置 OTP 输入是否只读。默认为 false</para>
    /// <para lang="en">Gets or sets whether the OTP input is readonly. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsReadonly { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 OTP 输入的值类型。默认为 <see cref="OtpInputType.Number"/></para>
    /// <para lang="en">Gets or sets the value type of the OTP input. Default is <see cref="OtpInputType.Number"/></para>
    /// </summary>
    [Parameter]
    public OtpInputType Type { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 OTP 输入的占位符。默认为 null</para>
    /// <para lang="en">Gets or sets the placeholder of the OTP input. Default is null</para>
    /// </summary>
    [Parameter]
    public string? PlaceHolder { get; set; }

    private string? ClassString => CssBuilder.Default("bb-otp-input")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? InputClassString => CssBuilder.Default("bb-otp-item")
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

    private string? ReadonlyString => IsReadonly ? "readonly" : null;

    private string? DisabledString => IsDisabled ? "disabled" : null;

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
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, Interop, nameof(TriggerSetValue));

    private char? GetValueString(int index)
    {
        return _values[index] != 0 ? _values[index] : null;
    }

    /// <summary>
    /// <para lang="zh">触发值更改事件回调。由 JavaScript 触发</para>
    /// <para lang="en">Trigger value changed event callback. Trigger by JavaScript</para>
    /// </summary>
    /// <param name="val"></param>
    [JSInvokable]
    public Task TriggerSetValue(string[] val)
    {
        SetValue(string.Join("", val.Select(i => string.IsNullOrEmpty(i) ? " " : i)));
        return Task.CompletedTask;
    }
}
