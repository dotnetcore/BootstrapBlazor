﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// Display 组件
/// </summary>
public partial class Display<TValue> : ILookup
{
    private string? ClassString => CssBuilder.Default("form-control is-display")
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    private string? _displayText;

    /// <summary>
    /// 获得/设置 异步格式化字符串
    /// </summary>
    [Parameter]
    public Func<TValue, Task<string?>>? FormatterAsync { get; set; }

    /// <summary>
    /// 获得/设置 格式化字符串 如时间类型设置 yyyy-MM-dd
    /// </summary>
    [Parameter]
    public string? FormatString { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public IEnumerable<SelectedItem>? Lookup { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public ILookupService? LookupService { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public string? LookupServiceKey { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public object? LookupServiceData { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public StringComparison LookupStringComparison { get; set; } = StringComparison.OrdinalIgnoreCase;

    [Inject]
    [NotNull]
    private ILookupService? InjectLookupService { get; set; }

    /// <summary>
    /// 获得/设置 类型解析回调方法 组件泛型为 Array 时内部调用
    /// </summary>
    [Parameter]
    public Func<Assembly?, string, bool, Type?>? TypeResolver { get; set; }

    /// <summary>
    /// 获得/设置 是否显示 Tooltip 多用于标签文字过长导致裁减时使用 默认 false 不显示
    /// </summary>
    [Parameter]
    public bool ShowTooltip { get; set; }

    /// <summary>
    /// <inheritdoc/>>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        _lookupData = null;
        _displayText = await FormatDisplayText(Value);
    }

    /// <summary>
    /// 数值格式化委托方法
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private async Task<string?> FormatDisplayText(TValue value) => FormatterAsync != null
        ? await FormatterAsync(value)
        : (!string.IsNullOrEmpty(FormatString) && value != null
            ? Utility.Format(value, FormatString)
            : value == null
                ? await FormatValueString()
                : await FormatText(value));

    private async Task<string> FormatText([DisallowNull] TValue value)
    {
        string ret;
        var type = typeof(TValue);
        if (type.IsEnum())
        {
            ret = Utility.GetDisplayName(type, value.ToString()!);
        }
        else if (type.IsArray)
        {
            ret = ArrayConvertToString(value);
        }
        else if (type.IsGenericType && type.IsAssignableTo(typeof(IEnumerable)))
        {
            // 泛型集合 IEnumerable<TValue>
            ret = await ConvertEnumerableToString(value);
        }
        else
        {
            ret = await FormatValueString();
        }
        return ret;
    }

    private async Task<string> FormatValueString()
    {
        var ret = Value?.ToString();
        var lookup = await GetLookup();
        if (lookup != null)
        {
            ret = lookup.FirstOrDefault(i => i.Value.Equals(ret, LookupStringComparison))?.Text;
        }
        return ret ?? string.Empty;
    }

    private Func<TValue, string>? _arrayConvertToString;
    private string ArrayConvertToString(TValue value)
    {
        _arrayConvertToString ??= LambdaExtensions.ArrayConvertToStringLambda<TValue>(TypeResolver).Compile();
        return _arrayConvertToString(value);
    }

    private static Func<TValue, string>? _enumerableConvertToString;
    private async Task<string> ConvertEnumerableToString(TValue value)
    {
        _enumerableConvertToString ??= LambdaExtensions.EnumerableConvertToStringLambda<TValue>().Compile();
        var lookup = await GetLookup();
        return lookup == null
            ? _enumerableConvertToString(value)
            : GetTextByValue(lookup, value);
    }

    private static Func<TValue, IEnumerable<string>>? _convertToStringEnumerable;
    private static string GetTextByValue(IEnumerable<SelectedItem> lookup, TValue value)
    {
        _convertToStringEnumerable ??= LambdaExtensions.ConvertToStringEnumerableLambda<TValue>().Compile();
        var source = _convertToStringEnumerable(value);
        return string.Join(",", source.Aggregate(new List<string>(), (s, i) =>
        {
            var text = lookup.FirstOrDefault(d => d.Value.Equals(i, StringComparison.OrdinalIgnoreCase))?.Text;
            if (text != null)
            {
                s.Add(text);
            }
            return s;
        }));
    }

    private IEnumerable<SelectedItem>? _lookupData;
    private async Task<IEnumerable<SelectedItem>?> GetLookup()
    {
        _lookupData ??= await this.GetItemsAsync(InjectLookupService, LookupServiceKey, LookupServiceData);
        return _lookupData;
    }
}
