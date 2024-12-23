// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// Display 组件
/// </summary>
public partial class Display<TValue>
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
    /// 获得/设置 数据集用于 CheckboxList Select 组件 通过 Value 显示 Text 使用 默认 null
    /// </summary>
    /// <remarks>设置 <see cref="Lookup"/> 参收后，<see cref="LookupServiceKey"/> 和 <see cref="LookupServiceData"/> 两个参数均失效</remarks>
    [Parameter]
    public IEnumerable<SelectedItem>? Lookup { get; set; }

    /// <summary>
    /// 获得/设置 <see cref="ILookupService"/> 服务获取 Lookup 数据集合键值 常用于外键自动转换为名称操作，可以通过 <see cref="LookupServiceData"/> 传递自定义数据
    /// </summary>
    /// <remarks>未设置 <see cref="Lookup"/> 时生效</remarks>
    [Parameter]
    public string? LookupServiceKey { get; set; }

    /// <summary>
    /// 获得/设置 <see cref="ILookupService"/> 服务获取 Lookup 数据集合键值自定义数据，通过 <see cref="LookupServiceKey"/> 指定键值
    /// </summary>
    /// <remarks>未设置 <see cref="Lookup"/> 时生效</remarks>
    [Parameter]
    public object? LookupServiceData { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    [Parameter]
    public ILookupService? LookupService { get; set; }

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
                ? FormatValueString()
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
            ret = FormatValueString();
        }
        return ret;
    }

    private string FormatValueString()
    {
        string? ret = null;

        // 检查 数据源
        var valueString = Value?.ToString();
        if (Lookup != null)
        {
            ret = Lookup.FirstOrDefault(i => i.Value.Equals(valueString ?? "", StringComparison.OrdinalIgnoreCase))?.Text;
        }
        return ret ?? valueString ?? string.Empty;
    }

    private Func<TValue, string>? _arrayConvertoString;
    private string ArrayConvertToString(TValue value)
    {
        _arrayConvertoString ??= LambdaExtensions.ArrayConvertToStringLambda<TValue>(TypeResolver).Compile();
        return _arrayConvertoString(value);
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

    private ILookupService GetLookupService() => LookupService ?? InjectLookupService;

    private IEnumerable<SelectedItem>? _lookupData;
    private async Task<IEnumerable<SelectedItem>?> GetLookup()
    {
        if (Lookup != null)
        {
            return Lookup;
        }

        var lookupService = GetLookupService();
        _lookupData ??= await lookupService.GetItemsAsync(LookupServiceKey, LookupServiceData);
        return _lookupData;
    }
}
