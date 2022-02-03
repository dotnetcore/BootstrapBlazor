// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
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
        .AddClass($"border-{Color.ToDescriptionString()} shadow-{Color.ToDescriptionString()}", Color != Color.None)
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

    [Inject]
    [NotNull]
    private IStringLocalizer<BootstrapInputNumber<TValue>>? Localizer { get; set; }

    static BootstrapInputNumber()
    {
        // Unwrap Nullable<T>, because InputBase already deals with the Nullable aspect
        // of it for us. We will only get asked to parse the T for nonempty inputs.
        var targetType = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);
        if (!typeof(TValue).IsNumber())
        {
            throw new InvalidOperationException($"The type '{targetType}' is not a supported numeric type.");
        }
    }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        ParsingErrorMessage ??= Localizer[nameof(ParsingErrorMessage)];
    }

    /// <summary>
    /// OnParametersSet 方法
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        SetStep();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected override string? FormatParsingErrorMessage() => string.Format(CultureInfo.InvariantCulture, ParsingErrorMessage, DisplayText);

    /// <summary>
    /// Formats the value as a string. Derived classes can override this to determine the formatting used for <c>CurrentValueAsString</c>.
    /// </summary>
    /// <param name="value">The value to format.</param>
    /// <returns>A string representation of the value.</returns>
    protected override string? FormatValueAsString(TValue value) => Formatter != null
        ? Formatter.Invoke(value)
        : (!string.IsNullOrEmpty(FormatString) && value != null
            ? Utility.Format(value, FormatString)
            : value switch
            {
                null => null,
                int @int => BindConverter.FormatValue(@int, CultureInfo.InvariantCulture),
                long @long => BindConverter.FormatValue(@long, CultureInfo.InvariantCulture),
                short @short => BindConverter.FormatValue(@short, CultureInfo.InvariantCulture),
                float @float => BindConverter.FormatValue(@float, CultureInfo.InvariantCulture),
                double @double => BindConverter.FormatValue(@double, CultureInfo.InvariantCulture),
                decimal @decimal => BindConverter.FormatValue(@decimal, CultureInfo.InvariantCulture),
                _ => throw new InvalidOperationException($"Unsupported type {value!.GetType()}"),
            });

    private void SetStep()
    {
        var val = CurrentValue;
        switch (val)
        {
            case int:
            case long:
            case short:
                StepString = Step ?? "1";
                break;
            case float:
            case double:
            case decimal:
                StepString = Step ?? "0.01";
                break;
        }
    }

    /// <summary>
    /// 点击减少按钮式时回调此方法
    /// </summary>
    /// <returns></returns>
    private async Task OnClickDec()
    {
        var val = CurrentValue;
        switch (val)
        {
            case int @int:
                val = (TValue)(object)(@int - int.Parse(StepString));
                break;
            case long @long:
                val = (TValue)(object)(@long - long.Parse(StepString));
                break;
            case short @short:
                val = (TValue)(object)(short)(@short - short.Parse(StepString));
                break;
            case float @float:
                val = (TValue)(object)(@float - float.Parse(StepString));
                break;
            case double @double:
                val = (TValue)(object)(@double - double.Parse(StepString));
                break;
            case decimal @decimal:
                val = (TValue)(object)(@decimal - decimal.Parse(StepString));
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
        switch (val)
        {
            case int @int:
                val = (TValue)(object)(@int + int.Parse(StepString));
                break;
            case long @long:
                val = (TValue)(object)(@long + long.Parse(StepString));
                break;
            case short @short:
                val = (TValue)(object)(short)(@short + short.Parse(StepString));
                break;
            case float @float:
                val = (TValue)(object)(@float + float.Parse(StepString));
                break;
            case double @double:
                val = (TValue)(object)(@double + double.Parse(StepString));
                break;
            case decimal @decimal:
                val = (TValue)(object)(@decimal + decimal.Parse(StepString));
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
    private void OnBlur()
    {
        if (!PreviousParsingAttemptFailed)
        {
            CurrentValue = SetMax(SetMin(Value));
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
}
