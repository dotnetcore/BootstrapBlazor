// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Text;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">BootstrapInputCurrency 组件</para>
/// <para lang="en">BootstrapInputCurrency component</para>
/// </summary>
public partial class BootstrapInputCurrency<TValue>
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否为只读，默认为 false</para>
    /// <para lang="en">Gets or sets whether readonly. Default is false</para>
    /// </summary>
    [Parameter]
    public bool Readonly { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 最小值</para>
    /// <para lang="en">Gets or sets Minimum Value</para>
    /// </summary>
    [Parameter]
    public string? Min { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 最大值</para>
    /// <para lang="en">Gets or sets Maximum Value</para>
    /// </summary>
    [Parameter]
    public string? Max { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 数值解析及格式字符串使用的文化信息，默认 null 未设置使用当前 UI 文化信息</para>
    /// <para lang="en">Gets or sets the culture used to parse and format numeric values. Default is null, which uses the current UI culture</para>
    /// </summary>
    [Parameter]
    public CultureInfo? CultureInfo { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义数值解析回调方法。解析成功后由组件格式化显示值</para>
    /// <para lang="en">Gets or sets the custom numeric parser. The component formats the value after successful parsing</para>
    /// </summary>
    [Parameter]
    public Func<string, CultureInfo, (bool Success, TValue? Value)>? Parser { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 清空文本框时的回调方法，默认为 null</para>
    /// <para lang="en">Gets or sets the callback method when clearing text box. Default is null</para>
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnClear { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示清空小按钮，默认为 false</para>
    /// <para lang="en">Gets or sets whether to show clear button. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsClearable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 清空小按钮图标，默认为 null</para>
    /// <para lang="en">Gets or sets the clear button icon. Default is null</para>
    /// </summary>
    [Parameter]
    public string? ClearIcon { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<BootstrapInputCurrency<TValue>>? Localizer { get; set; }

    private string? ReadonlyString => Readonly ? "true" : null;

    private string? ClearableIconString => CssBuilder.Default("form-control-clear-icon")
        .AddClass(ClearIcon)
        .Build();

    /// <summary>
    /// <para lang="zh">获得 文本框样式</para>
    /// <para lang="en">Get Text Box Style</para>
    /// </summary>
    protected string? InputClassString => CssBuilder.Default("form-control")
        .AddClass(CssClass).AddClass(ValidCss)
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? InputModeString => IsDecimalType() ? "decimal" : "numeric";


    private string? _lastInputValueString;

    private bool _manualInput;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        if (UseInputEvent)
        {
            _lastInputValueString ??= Value?.ToString();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ClearIcon ??= IconTheme.GetIconByKey(ComponentIcons.InputClearIcon);
        ParsingErrorMessage ??= Localizer[nameof(ParsingErrorMessage)];

        CultureInfo ??= CultureInfo.CurrentUICulture;
        FormatString ??= "C";

        if (Value is null)
        {
            _lastInputValueString = "";
        }

        if (UseInputEvent && !_manualInput)
        {
            _lastInputValueString = GetFormatString(Value);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="firstRender"></param>
    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        if (_manualInput)
        {
            _manualInput = false;
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override string? FormatParsingErrorMessage() => string.Format(CultureInfo.InvariantCulture, ParsingErrorMessage, DisplayText);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override string? FormatValueAsString(TValue? value) => UseInputEvent ? _lastInputValueString : GetFormatString(value);

    private string? GetFormatString(TValue? value) => Formatter != null
        ? Formatter.Invoke(value)
        : (!string.IsNullOrEmpty(FormatString) && value is IFormattable formattable
            ? formattable.ToString(FormatString, CultureInfo)
            : InternalFormat(value));

    /// <summary>
    /// <para lang="zh">InternalFormat 方法</para>
    /// <para lang="en">InternalFormat Method</para>
    /// </summary>
    /// <param name="value"></param>
    /// <exception cref="InvalidOperationException"></exception>
    protected virtual string? InternalFormat(TValue? value) => value switch
    {
        null => null,
        IFormattable formattable => formattable.ToString(null, CultureInfo.InvariantCulture),
        _ => throw new InvalidOperationException($"Unsupported type {value!.GetType()}")
    };

    private TValue ParseValue(string value)
    {
        return value.TryConvertTo<TValue>(CultureInfo, out var ret)
            ? ret
            : throw new InvalidOperationException($"Unsupported type {typeof(TValue)}");
    }

    private bool IsDecimalType()
    {
        // 检查是否允许带小数点数据类型
        var type = ValueType;
        return type == typeof(float) || type == typeof(double) || type == typeof(decimal);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnBlur()
    {
        if (!PreviousParsingAttemptFailed)
        {
            CurrentValue = SetMax(SetMin(Value));
        }
        else
        {
            CurrentValue = default!;
        }

        if (IsNullable() && string.IsNullOrEmpty(CurrentValueAsString))
        {
            // set component value empty
            await InvokeVoidAsync("clear", Id);
        }

        if (OnBlurAsync != null)
        {
            await OnBlurAsync(Value);
        }
    }

    private TValue? SetMin(TValue? val)
    {
        if (!string.IsNullOrEmpty(Min) && val != null)
        {
            var min = ParseValue(Min);
            if (Comparer<TValue>.Default.Compare(val, min) < 0)
            {
                val = min;
            }
        }
        return val;
    }

    private TValue? SetMax(TValue? val)
    {
        if (!string.IsNullOrEmpty(Max) && val != null)
        {
            var max = ParseValue(Max);
            if (Comparer<TValue>.Default.Compare(val, max) > 0)
            {
                val = max;
            }
        }
        return val;
    }

    private string NormalizeValue(string value)
    {
        var format = CultureInfo.NumberFormat;
        var decimalSeparators = new[] { format.NumberDecimalSeparator, format.CurrencyDecimalSeparator }
            .Where(s => !string.IsNullOrEmpty(s))
            .Distinct()
            .ToArray();
        var tokens = new[]
        {
            format.CurrencySymbol,
            format.NumberGroupSeparator,
            format.CurrencyGroupSeparator,
            format.NegativeSign,
            format.PositiveSign
        }
        .Where(s => !string.IsNullOrEmpty(s))
        .Distinct()
        .OrderByDescending(s => s.Length)
        .ToArray();

        var builder = new StringBuilder(value.Length);
        var hasDecimalSeparator = false;
        for (var index = 0; index < value.Length;)
        {
            var decimalSeparator = Array.Find(decimalSeparators, separator => value.AsSpan(index).StartsWith(separator, StringComparison.Ordinal));
            if (decimalSeparator != null)
            {
                if (!hasDecimalSeparator)
                {
                    builder.Append(decimalSeparator);
                    hasDecimalSeparator = true;
                }
                index += decimalSeparator.Length;
                continue;
            }

            var token = Array.Find(tokens, candidate => value.AsSpan(index).StartsWith(candidate, StringComparison.Ordinal));
            if (token != null)
            {
                builder.Append(token);
                index += token.Length;
                continue;
            }

            var character = value[index];
            if (char.IsDigit(character) || char.IsWhiteSpace(character) || character is '(' or ')')
            {
                builder.Append(character);
            }
            else if (IsDecimalType() && character is 'e' or 'E'
                && index > 0
                && index + 1 < value.Length
                && char.IsDigit(value[index - 1])
                && (char.IsDigit(value[index + 1]) || value[index + 1] is '+' or '-'))
            {
                builder.Append(character);
            }
            index++;
        }
        return builder.ToString();
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <param name="validationErrorMessage"></param>
    protected override bool TryParseValueFromString(string value, [MaybeNullWhen(false)] out TValue result, out string? validationErrorMessage)
    {
        bool ret;
        if (string.IsNullOrEmpty(value))
        {
            result = default;
            validationErrorMessage = null;

            // nullable data type do not run here
            _lastInputValueString = result!.ToString();
            ret = true;
        }
        else
        {
            var normalizedValue = NormalizeValue(value);
            if (Parser != null)
            {
                var parsedValue = Parser(normalizedValue, CultureInfo);
                ret = parsedValue.Success;
                result = ret ? parsedValue.Value! : default;
            }
            else
            {
                ret = normalizedValue.TryConvertTo(CultureInfo, out result);
            }

            validationErrorMessage = ret ? null : FormatParsingErrorMessage();

            if (ret && UseInputEvent)
            {
                _lastInputValueString = value;
            }
        }

        if (UseInputEvent)
        {
            _manualInput = true;
        }
        return ret;
    }

    private async Task OnClickClear()
    {
        if (OnClear != null)
        {
            await OnClear(Value);
        }
        CurrentValueAsString = "";
    }
}
