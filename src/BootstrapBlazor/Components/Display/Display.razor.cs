// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Collections;
using System.Linq.Expressions;
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

    /// <summary>
    /// 获得 显示文本
    /// </summary>
    protected string? CurrentTextAsString { get; set; }

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

    [Inject]
    [NotNull]
    private ILookupService? LookupService { get; set; }

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
    /// <param name="parameters"></param>
    /// <returns></returns>
    public override Task SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);

        if (!string.IsNullOrEmpty(LookupServiceKey))
        {
            Lookup ??= LookupService.GetItemsByKey(LookupServiceKey, LookupServiceData);
        }

        // For derived components, retain the usual lifecycle with OnInit/OnParametersSet/etc.
        return base.SetParametersAsync(ParameterView.Empty);
    }

    /// <summary>
    /// <inheritdoc/>>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        CurrentTextAsString = await FormatTextAsString(Value);
    }

    /// <summary>
    /// 数值格式化委托方法
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private async Task<string?> FormatTextAsString(TValue value) => FormatterAsync != null
        ? await FormatterAsync(value)
        : (!string.IsNullOrEmpty(FormatString) && value != null
            ? Utility.Format(value, FormatString)
            : value == null
                ? FormatValueString()
                : FormatText(value));

    private string FormatText([DisallowNull] TValue value)
    {
        string ret;
        var type = typeof(TValue);
        if (type.IsEnum())
        {
            ret = Utility.GetDisplayName(type, value.ToString()!);
        }
        else if (type.IsArray)
        {
            ret = ConvertArrayToString(value);
        }
        else if (type.IsGenericType && type.IsAssignableTo(typeof(IEnumerable)))
        {
            // 泛型集合 IEnumerable<TValue>
            ret = ConvertEnumerableToString(value);
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

    private Func<TValue, string>? _converterArray;
    /// <summary>
    /// 获取属性方法 Lambda 表达式
    /// </summary>
    /// <returns></returns>
    private string ConvertArrayToString(TValue value)
    {
        return (_converterArray ??= ConvertArrayToStringLambda())(value);

        Func<TValue, string> ConvertArrayToStringLambda()
        {
            Func<TValue, string> ret = _ => "";
            var param = Expression.Parameter(typeof(Array));
            var targetType = typeof(TValue).UnderlyingSystemType;
            var methodType = ResolveArrayType();
            if (methodType != null)
            {
                // 调用 string.Join<T>(",", IEnumerable<T>) 方法
                var method = typeof(string).GetMethods().First(m => m is { Name: "Join", IsGenericMethod: true } && m.GetParameters()[0].ParameterType == typeof(string)).MakeGenericMethod(methodType);
                var body = Expression.Call(method, Expression.Constant(","), Expression.Convert(param, targetType));
                ret = Expression.Lambda<Func<TValue, string>>(body, param).Compile();
            }
            return ret;

            Type? ResolveArrayType()
            {
                Type? t = null;
                var typeName = targetType.FullName;
                if (!string.IsNullOrEmpty(typeName))
                {
                    typeName = typeName.Replace("[]", "");
                    if (typeName.Contains('+'))
                    {
                        typeName = typeName.Split('+', StringSplitOptions.RemoveEmptyEntries).Last();
                    }
                    t = Type.GetType(typeName, null, TypeResolver, false, true);
                }
                return t;
            }
        }
    }

    private static Func<TValue, string>? _convertEnumerableToString;
    private static Func<TValue, IEnumerable<string>>? _convertToEnumerableString;
    /// <summary>
    /// 获取属性方法 Lambda 表达式
    /// </summary>
    /// <returns></returns>
    private string ConvertEnumerableToString(TValue value)
    {
        return Lookup == null
            ? (_convertEnumerableToString ??= ConvertEnumerableToStringLambda())(value)
            : GetTextByValue((_convertToEnumerableString ??= ConvertToEnumerableStringLambda())(value));

        static Func<TValue, string> ConvertEnumerableToStringLambda()
        {
            var typeArguments = typeof(TValue).GenericTypeArguments;
            var param = Expression.Parameter(typeof(IEnumerable<>).MakeGenericType(typeArguments));
            var method = typeof(string).GetMethods().First(m => m is { Name: "Join", IsGenericMethod: true } && m.GetParameters()[0].ParameterType == typeof(string)).MakeGenericMethod(typeArguments);
            var body = Expression.Call(method, Expression.Constant(","), param);
            return Expression.Lambda<Func<TValue, string>>(body, param).Compile();
        }

        static Func<TValue, IEnumerable<string>> ConvertToEnumerableStringLambda()
        {
            var typeArguments = typeof(TValue).GenericTypeArguments;
            var param = Expression.Parameter(typeof(IEnumerable<>).MakeGenericType(typeArguments));

            var method = typeof(Display<>).MakeGenericType(typeof(TValue))
                .GetMethod(nameof(Cast), BindingFlags.NonPublic | BindingFlags.Static)!
                .MakeGenericMethod(typeArguments);
            var body = Expression.Call(method, param);
            return Expression.Lambda<Func<TValue, IEnumerable<string>>>(body, param).Compile();
        }
    }

    private static IEnumerable<string> Cast<TType>(IEnumerable<TType> source) => source.Select(i => i?.ToString() ?? string.Empty);

    private string GetTextByValue(IEnumerable<string> source) => string.Join(",", source.Aggregate(new List<string>(), (s, i) =>
    {
        var text = Lookup!.FirstOrDefault(d => d.Value.Equals(i, StringComparison.OrdinalIgnoreCase))?.Text;
        if (text != null)
        {
            s.Add(text);
        }
        return s;
    }));
}
