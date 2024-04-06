// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Localization;
using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// An input component for editing numeric values.
/// Supported numeric types are <see cref="int"/>, <see cref="long"/>, <see cref="short"/>, <see cref="float"/>, <see cref="double"/>, <see cref="decimal"/>.
/// </summary>
public partial class BootstrapInputNumber<TValue>
{
    /// <summary>
    /// 获得 按钮样式
    /// </summary>
    protected string? ButtonClassString => CssBuilder.Default("btn")
        .AddClass("btn-outline-secondary", Color == Color.None)
        .AddClass($"btn-outline-{Color.ToDescriptionString()}", Color != Color.None)
        .Build();

    /// <summary>
    /// 获得 文本框样式
    /// </summary>
    protected string? InputClassString => CssBuilder.Default("form-control")
        .AddClass(CssClass).AddClass(ValidCss)
        .AddClass("input-number-fix", ShowButton)
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    [NotNull]
    private string? StepString { get; set; }

    /// <summary>
    /// 获得/设置 数值增加时回调委托
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnIncrement { get; set; }

    /// <summary>
    /// 获得/设置 数值减少时回调委托
    /// </summary>
    [Parameter]
    public Func<TValue, Task>? OnDecrement { get; set; }

    /// <summary>
    /// 获得/设置 最小值
    /// </summary>
    [Parameter]
    public string? Min { get; set; }

    /// <summary>
    /// 获得/设置 最大值
    /// </summary>
    [Parameter]
    public string? Max { get; set; }

    /// <summary>
    /// 获得/设置 步长 默认为 null
    /// </summary>
    [Parameter]
    public string? Step { get; set; }

    /// <summary>
    /// 获得/设置 是否显示加减按钮
    /// </summary>
    [Parameter]
    public bool ShowButton { get; set; }

    /// <summary>
    /// 获得/设置 减小数值图标
    /// </summary>
    [Parameter]
    public string? MinusIcon { get; set; }

    /// <summary>
    /// 获得/设置 增加数值图标
    /// </summary>
    [Parameter]
    public string? PlusIcon { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<BootstrapInputNumber<TValue>>? Localizer { get; set; }

    [Inject]
    [NotNull]
    private IIconTheme? IconTheme { get; set; }

    [Inject]
    [NotNull]
    private IOptions<BootstrapBlazorOptions>? StepOption { get; set; }

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

        ParsingErrorMessage ??= Localizer[nameof(ParsingErrorMessage)];
        MinusIcon ??= IconTheme.GetIconByKey(ComponentIcons.InputNumberMinusIcon);
        PlusIcon ??= IconTheme.GetIconByKey(ComponentIcons.InputNumberPlusIcon);

        StepString = Step ?? StepOption.Value.GetStep<TValue>() ?? "any";

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
    /// <returns></returns>
    protected override string? FormatParsingErrorMessage() => string.Format(CultureInfo.InvariantCulture, ParsingErrorMessage, DisplayText);

    /// <summary>
    /// Formats the value as a string. Derived classes can override this to determine the formatting used for <see cref="ValidateBase{TValue}.CurrentValueAsString"/>.
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <returns>A string representation of the value.</returns>
    protected override string? FormatValueAsString(TValue value) => UseInputEvent ? _lastInputValueString : GetFormatString(value);

    private string? GetFormatString(TValue value) => Formatter != null
        ? Formatter.Invoke(value)
        : (!string.IsNullOrEmpty(FormatString) && value != null
            ? Utility.Format(value, FormatString)
            : InternalFormat(value));

    /// <summary>
    /// InternalFormat 方法
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    protected virtual string? InternalFormat(TValue value) => value switch
    {
        null => null,
        int @int => BindConverter.FormatValue(@int, CultureInfo.InvariantCulture),
        long @long => BindConverter.FormatValue(@long, CultureInfo.InvariantCulture),
        short @short => BindConverter.FormatValue(@short, CultureInfo.InvariantCulture),
        float @float => BindConverter.FormatValue(@float, CultureInfo.InvariantCulture),
        double @double => BindConverter.FormatValue(@double, CultureInfo.InvariantCulture),
        decimal @decimal => BindConverter.FormatValue(@decimal, CultureInfo.InvariantCulture),
        _ => throw new InvalidOperationException($"Unsupported type {value!.GetType()}"),
    };

    private string GetStepString() => (string.IsNullOrEmpty(StepString) || StepString.Equals("any", StringComparison.OrdinalIgnoreCase)) ? "1" : StepString;

    /// <summary>
    /// 点击减少按钮式时回调此方法
    /// </summary>
    /// <returns></returns>
    private async Task OnClickDec()
    {
        var val = CurrentValue;
        var step = GetStepString();
        switch (val)
        {
            case int @int:
                val = (TValue)(object)(@int - int.Parse(step));
                break;
            case long @long:
                val = (TValue)(object)(@long - long.Parse(step));
                break;
            case short @short:
                val = (TValue)(object)(short)(@short - short.Parse(step));
                break;
            case float @float:
                val = (TValue)(object)(@float - float.Parse(step));
                break;
            case double @double:
                val = (TValue)(object)(@double - double.Parse(step));
                break;
            case decimal @decimal:
                val = (TValue)(object)(@decimal - decimal.Parse(step));
                break;
        }
        CurrentValue = SetMax(SetMin(val));
        if (OnDecrement != null)
        {
            await OnDecrement(CurrentValue);
        }
    }

    /// <summary>
    /// 点击增加按钮式时回调此方法
    /// </summary>
    /// <returns></returns>
    private async Task OnClickInc()
    {
        var val = CurrentValue;
        var step = GetStepString();
        switch (val)
        {
            case int @int:
                val = (TValue)(object)(@int + int.Parse(step));
                break;
            case long @long:
                val = (TValue)(object)(@long + long.Parse(step));
                break;
            case short @short:
                val = (TValue)(object)(short)(@short + short.Parse(step));
                break;
            case float @float:
                val = (TValue)(object)(@float + float.Parse(step));
                break;
            case double @double:
                val = (TValue)(object)(@double + double.Parse(step));
                break;
            case decimal @decimal:
                val = (TValue)(object)(@decimal + decimal.Parse(step));
                break;
        }
        CurrentValue = SetMax(SetMin(val));
        if (OnIncrement != null)
        {
            await OnIncrement(CurrentValue);
        }
    }

    /// <summary>
    /// 失去焦点是触发此方法
    /// </summary>
    /// <returns></returns>
    private async Task OnBlur()
    {
        if (!PreviousParsingAttemptFailed)
        {
            CurrentValue = SetMax(SetMin(Value));
        }
        else
        {
            CurrentValue = default!;
        }

        if (NullableUnderlyingType != null && string.IsNullOrEmpty(CurrentValueAsString))
        {
            // set component value empty
            await InvokeVoidAsync("clear", Id);
        }
    }

    private TValue SetMin(TValue val)
    {
        if (!string.IsNullOrEmpty(Min))
        {
            switch (val)
            {
                case int @int:
                    val = (TValue)(object)Math.Max(@int, int.Parse(Min));
                    break;
                case long @long:
                    val = (TValue)(object)Math.Max(@long, long.Parse(Min));
                    break;
                case short @short:
                    val = (TValue)(object)Math.Max(@short, short.Parse(Min));
                    break;
                case float @float:
                    val = (TValue)(object)Math.Max(@float, float.Parse(Min));
                    break;
                case double @double:
                    val = (TValue)(object)Math.Max(@double, double.Parse(Min));
                    break;
                case decimal @decimal:
                    val = (TValue)(object)Math.Max(@decimal, decimal.Parse(Min));
                    break;
            }
        }
        return val;
    }

    private TValue SetMax(TValue val)
    {
        if (!string.IsNullOrEmpty(Max))
        {
            switch (val)
            {
                case int @int:
                    val = (TValue)(object)Math.Min(@int, int.Parse(Max));
                    break;
                case long @long:
                    val = (TValue)(object)Math.Min(@long, long.Parse(Max));
                    break;
                case short @short:
                    val = (TValue)(object)Math.Min(@short, short.Parse(Max));
                    break;
                case float @float:
                    val = (TValue)(object)Math.Min(@float, float.Parse(Max));
                    break;
                case double @double:
                    val = (TValue)(object)Math.Min(@double, double.Parse(Max));
                    break;
                case decimal @decimal:
                    val = (TValue)(object)Math.Min(@decimal, decimal.Parse(Max));
                    break;
            }
        }
        return val;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <param name="validationErrorMessage"></param>
    /// <returns></returns>
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
            ret = base.TryParseValueFromString(value, out result, out validationErrorMessage);
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
}
