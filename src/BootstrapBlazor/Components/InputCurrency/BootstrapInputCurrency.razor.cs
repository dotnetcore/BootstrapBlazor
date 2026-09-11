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
[BootstrapModuleAutoLoader("InputCurrency/BootstrapInputCurrency.razor.js", JSObjectReference = true)]
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
    /// <para lang="zh">获得/设置小数位数，默认 null，使用当前货币文化的小数位数。设置 <c>FormatString</c> 或 <c>Formatter</c> 时本参数不生效</para>
    /// <para lang="en">Gets or sets the number of decimal places. The default is null, which uses the current currency culture. This parameter is ignored when <c>FormatString</c> or <c>Formatter</c> is set</para>
    /// </summary>
    [Parameter]
    public int? Decimals { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否显示 ISO 货币符号，默认 false 不显示</para>
    /// <para lang="en">Gets or sets whether to show ISO currency symbol. Default is false</para>
    /// </summary>
    [Parameter]
    public bool ShowIsoCurrencySymbol { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 货币符号模板</para>
    /// <para lang="en">Gets or sets the currency symbol template</para>
    /// </summary>
    [Parameter]
    public RenderFragment? SymbolTemplate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 清空文本框时的回调方法，默认为 null</para>
    /// <para lang="en">Gets or sets the callback method when clearing text box. Default is null</para>
    /// </summary>
    [Parameter]
    public Func<decimal, Task>? OnClear { get; set; }

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

    private string? InputClassString => CssBuilder.Default("bb-input-currencyinput form-control")
        .AddClass(CssClass).AddClass(ValidCss)
        .AddClass($"border-{Color.ToDescriptionString()}", Color != Color.None)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private CultureInfo _currentCultureInfo = CultureInfo.CurrentUICulture;

    private string InputId => $"{Id}_input";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override string? GetInputId() => InputId;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        ClearIcon ??= IconTheme.GetIconByKey(ComponentIcons.InputClearIcon);
        ParsingErrorMessage ??= Localizer[nameof(ParsingErrorMessage)];

        if (Decimals is < 0 or > 28)
        {
            throw new ArgumentOutOfRangeException(nameof(Decimals), Decimals, "DecimalPlaces must be between 0 and 28.");
        }

        _currentCultureInfo = string.IsNullOrEmpty(CultureName) ? CultureInfo.CurrentUICulture : CultureInfo.GetCultureInfo(CultureName);
        _currencySymbol = ShowIsoCurrencySymbol ? GetIsoCurrencySymbol(_currentCultureInfo) : _currentCultureInfo.NumberFormat.CurrencySymbol;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override Task InvokeInitAsync() => InvokeVoidAsync("init", Id, GetAllowedInputTokens());

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
        {
            await InvokeVoidAsync("update", Id, GetAllowedInputTokens());
        }
    }

    private string[] GetAllowedInputTokens()
    {
        var format = _currentCultureInfo.NumberFormat;
        return
        [
            format.NumberDecimalSeparator,
            format.NumberGroupSeparator,
            format.CurrencyDecimalSeparator,
            format.CurrencyGroupSeparator,
            format.PositiveSign,
            format.NegativeSign
        ];
    }

    private static string GetIsoCurrencySymbol(CultureInfo culture)
    {
        return new RegionInfo(culture.Name).ISOCurrencySymbol;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override bool TryParseValueFromString(string value, [MaybeNullWhen(false)] out decimal result, out string? validationErrorMessage)
    {
        var ret = decimal.TryParse(value, NumberStyles.Currency, _currentCultureInfo, out result);
        validationErrorMessage = ret ? null : FormatParsingErrorMessage();
        if (!ret)
        {
            result = default;
        }

        return ret;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    protected override string? FormatValueAsString(decimal value)
    {
        if (Formatter != null)
        {
            return Formatter(value);
        }

        var decimalPlaces = Decimals ?? _currentCultureInfo.NumberFormat.CurrencyDecimalDigits;
        var format = string.IsNullOrEmpty(FormatString) ? $"N{decimalPlaces}" : FormatString;
        return value.ToString(format, _currentCultureInfo);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override string? FormatParsingErrorMessage() => string.Format(CultureInfo.InvariantCulture, ParsingErrorMessage, DisplayText);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnBlur()
    {
        CurrentValue = Math.Clamp(CurrentValue, Min ?? decimal.MinValue, Max ?? decimal.MaxValue);
        await base.OnBlur();
    }

    private async Task OnClickClear()
    {
        if (OnClear != null)
        {
            await OnClear(Value);
        }

        CurrentValue = default;
    }
}
