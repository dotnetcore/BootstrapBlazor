// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;
using System.Globalization;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">BootstrapInputCurrency 组件</para>
/// <para lang="en">BootstrapInputCurrency component</para>
/// </summary>
public partial class BootstrapInputCurrency
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
    public decimal? Min { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 最大值</para>
    /// <para lang="en">Gets or sets Maximum Value</para>
    /// </summary>
    [Parameter]
    public decimal? Max { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 数值解析及格式字符串使用的文化信息，默认 null 未设置使用当前 UI 文化信息</para>
    /// <para lang="en">Gets or sets the culture used to parse and format numeric values. Default is null, which uses the current UI culture</para>
    /// </summary>
    [Parameter]
    public string? CultureName { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 ISO 货币符号，默认 false 不显示</para>
    /// <para lang="en">Gets or sets whether to show ISO currency symbol. Default is false</para>
    /// </summary>
    [Parameter]
    public bool IsIsoSymbol { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 清空文本框时的回调方法，默认为 null</para>
    /// <para lang="en">Gets or sets the callback method when clearing text box. Default is null</para>
    /// </summary>
    [Parameter]
    public Func<decimal?, Task>? OnClear { get; set; }

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
    private IStringLocalizer<BootstrapInputCurrency>? Localizer { get; set; }

    private string? ReadonlyString => Readonly ? "true" : null;

    private string? ClearableIconString => CssBuilder.Default("form-control-clear-icon")
        .AddClass(ClearIcon)
        .Build();

    private string? _currencySymbol;

    private string? InputClassString => CssBuilder.Default("form-control")
        .AddClass(CssClass).AddClass(ValidCss)
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? InputStyleString => string.IsNullOrEmpty(_currencySymbol) ? null : CssBuilder.Default()
        .AddClass($"--bb-input-currency-symbol-length: {_currencySymbol.Length};")
        .AddClass($"--bb-input-currency-symbol: '{_currencySymbol}';")
        .Build();


    private CultureInfo? _currentCultureInfo;

    private string? InputType => _isEditing ? "number" : "text";
    private bool _isEditing;

    private string? _cultureValue
    {
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                CurrentValue = null;
            }
            else if (decimal.TryParse(value, NumberStyles.Any, _currentCultureInfo, out var result))
            {
                CurrentValue = result;
            }
        }
        get
        {
            if (_isEditing)
            {
                return CurrentValue?.ToString();
            }
            else
                return IsIsoSymbol ? CurrentValue?.ToString() : GetFormatString(CurrentValue);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override string? FormatValueAsString(decimal? value) => IsIsoSymbol ? CurrentValue?.ToString() : GetFormatString(value);

    private string? GetFormatString(decimal? value) => Formatter != null
        ? Formatter.Invoke(value)
        : (!string.IsNullOrEmpty(FormatString) && value is IFormattable formattable
            ? formattable.ToString(FormatString, _currentCultureInfo)
            : value?.ToString());

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ClearIcon ??= IconTheme.GetIconByKey(ComponentIcons.InputClearIcon);
        ParsingErrorMessage ??= Localizer[nameof(ParsingErrorMessage)];

        if (string.IsNullOrEmpty(CultureName))
        {
            _currentCultureInfo = CultureInfo.CurrentUICulture;

        }
        else
        {
            _currentCultureInfo = CultureInfo.GetCultureInfo(CultureName);
        }

        _currencySymbol = IsIsoSymbol ? GetIsoCurrencySymbol(_currentCultureInfo.Name) : _currentCultureInfo.NumberFormat.CurrencySymbol;
        if (IsIsoSymbol)
        {
            _cultureValue = CurrentValue?.ToString();
        }
    }

    private static string GetIsoCurrencySymbol(string cultureName)
    {
        var regionInfo = new RegionInfo(cultureName);
        return regionInfo.ISOCurrencySymbol;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override string? FormatParsingErrorMessage() => string.Format(CultureInfo.InvariantCulture, ParsingErrorMessage, DisplayText);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected Task OnFocus()
    {
        _isEditing = true;
        return Task.CompletedTask;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnBlur()
    {
        var max = Max ?? decimal.MaxValue;
        var min = Min ?? decimal.MinValue;
        var val = CurrentValue ?? 0;
        _isEditing = false;
        CurrentValue = Math.Clamp(val, min, max);

        if (OnBlurAsync != null)
        {
            await OnBlurAsync(Value);
        }
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
