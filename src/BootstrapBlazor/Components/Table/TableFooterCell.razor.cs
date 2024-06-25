// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Linq.Expressions;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// TableFooterCell 组件
/// </summary>
public partial class TableFooterCell
{
    private string? ClassString => CssBuilder.Default("table-cell")
        .AddClass("justify-content-start", Align == Alignment.Left)
        .AddClass("justify-content-center", Align == Alignment.Center)
        .AddClass("justify-content-end", Align == Alignment.Right)
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    /// <summary>
    /// 获得/设置 单元格内容
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// 获得/设置 文字对齐方式 默认为 Alignment.None
    /// </summary>
    [Parameter]
    public Alignment Align { get; set; }

    /// <summary>
    /// 获得/设置 格式化字符串 如时间类型设置 yyyy-MM-dd
    /// </summary>
    [Parameter]
    public string? FormatString { get; set; }

    /// <summary>
    /// 获得/设置 列格式化回调委托
    /// </summary>
    [Parameter]
    public Func<object?, Task<string?>>? Formatter { get; set; }

    /// <summary>
    /// 获得/设置 聚合方法枚举 默认 Sum
    /// </summary>
    [Parameter]
    public AggregateType Aggregate { get; set; }

    /// <summary>
    /// 获得/设置 自定义统计列回调方法
    /// </summary>
    [Parameter]
    public Func<object?, string?, string>? CustomerAggregateCallback { get; set; }

    /// <summary>
    /// 获得/设置 统计列名称 默认为 null 不参与统计仅作为显示单元格
    /// </summary>
    [Parameter]
    public string? Field { get; set; }

    /// <summary>
    /// 获得/设置 colspan 值 默认 null 自己手动设置值
    /// </summary>
    [Parameter]
    public Func<BreakPoint, int>? ColspanCallback { get; set; }

    /// <summary>
    /// 获得/设置 是否为移动端模式
    /// </summary>
    [CascadingParameter(Name = "IsMobileMode")]
    private bool IsMobileMode { get; set; }

    [CascadingParameter(Name = "TableBreakPoint")]
    private BreakPoint BreakPoint { get; set; }

    /// <summary>
    /// 获得/设置 是否为移动端模式
    /// </summary>
    [CascadingParameter(Name = "TableFooterContext")]
    private object? DataSource { get; set; }

    /// <summary>
    /// 获得/设置 显示节点阈值 默认值 BreakPoint.None 未设置
    /// </summary>
    [Parameter]
    public BreakPoint ShownWithBreakPoint { get; set; }

    private string? _value { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnParametersSetAsync()
    {
        _value = Text ?? (GetCount(DataSource) == 0 ? "0" : (GetCountValue() ?? await GetAggregateValue()));
    }

    /// <summary>
    /// 检查当前列是否显示方法
    /// </summary>
    /// <returns></returns>
    protected bool CheckShownWithBreakpoint => BreakPoint >= ShownWithBreakPoint;

    /// <summary>
    /// 解析 Count Aggregate
    /// </summary>
    /// <returns></returns>
    private string? GetCountValue()
    {
        string? v = null;
        if (Aggregate == AggregateType.Count && DataSource != null)
        {
            // 绑定数据源类型
            var type = DataSource.GetType();
            // 数据源泛型 TModel 类型
            var modelType = type.GenericTypeArguments[0];

            var mi = GetType().GetMethod(nameof(CreateCountMethod), BindingFlags.NonPublic | BindingFlags.Static)!.MakeGenericMethod(modelType);

            if (mi != null)
            {
                var obj = mi.Invoke(null, new object[] { DataSource });
                if (obj != null)
                {
                    v = obj.ToString();
                }
            }
        }
        return v;
    }

    private int? GetColspanValue()
    {
        int? ret = null;
        if (ColspanCallback != null)
        {
            ret = ColspanCallback(BreakPoint);
        }
        else if (AdditionalAttributes != null && AdditionalAttributes.TryGetValue("colspan", out var colspan) && int.TryParse(colspan.ToString(), out var d))
        {
            ret = d;
        }
        return ret;
    }

    private async Task<string?> GetAggregateValue()
    {
        return Aggregate == AggregateType.Customer ? AggregateCustomerValue() : await AggregateNumberValue();

        string? AggregateCustomerValue()
        {
            string? v = null;
            if (CustomerAggregateCallback != null)
            {
                v = CustomerAggregateCallback(DataSource, Field);
            }
            return v;
        }

        async Task<string?> AggregateNumberValue()
        {
            string? v = null;
            if (!string.IsNullOrEmpty(Field) && DataSource != null)
            {
                // 绑定数据源类型
                var type = DataSource.GetType();
                // 数据源泛型 TModel 类型
                var modelType = type.GenericTypeArguments[0];

                // 通过 Field 获取到 TModel 属性
                var propertyInfo = modelType.GetProperty(Field);
                if (propertyInfo != null)
                {
                    // @context.Sum(i => i.Count)
                    // Count 属性类型
                    var propertyType = propertyInfo.PropertyType;

                    // 构建 Aggregate
                    // @context.Sum(i => i.Count)
                    var aggregateMethod = Aggregate switch
                    {
                        AggregateType.Average => propertyType.Name switch
                        {
                            nameof(Int32) or nameof(Int64) or nameof(Double) => GetType()
                                .GetMethod(nameof(CreateAggregateLambda), BindingFlags.NonPublic | BindingFlags.Static)!
                                .MakeGenericMethod(typeof(Double)),
                            _ => GetType()
                                .GetMethod(nameof(CreateAggregateLambda), BindingFlags.NonPublic | BindingFlags.Static)!
                                .MakeGenericMethod(propertyType),
                        },
                        _ => GetType().GetMethod(nameof(CreateAggregateLambda), BindingFlags.NonPublic | BindingFlags.Static)!
                            .MakeGenericMethod(propertyType)
                    };
                    if (aggregateMethod != null)
                    {
                        v = await AggregateMethodInvoker(aggregateMethod, type, modelType, propertyType);
                    }
                }
            }
            return v;
        }

        async Task<string?> AggregateMethodInvoker(MethodInfo aggregateMethod, Type type, Type modelType, Type propertyType)
        {
            string? v = null;
            var invoker = aggregateMethod.Invoke(null, new object[] { Aggregate, type, modelType, propertyType });
            if (invoker != null)
            {
                // 构建 Selector
                var methodInfo = GetType().GetMethod(nameof(CreateSelector), BindingFlags.NonPublic | BindingFlags.Static)!
                    .MakeGenericMethod(modelType, propertyType);
                if (methodInfo != null)
                {
                    var selector = methodInfo.Invoke(null, new object[] { Field });
                    if (selector != null)
                    {
                        // 执行委托
                        if (invoker is Delegate d)
                        {
                            var val = d.DynamicInvoke(DataSource, selector);
                            v = await GetValue(val);
                        }
                    }
                }
            }
            return v;
        }

        async Task<string?> GetValue(object? val)
        {
            string? ret = null;
            if (Formatter != null)
            {
                // 格式化回调委托
                ret = await Formatter(val);
            }
            else if (!string.IsNullOrEmpty(FormatString))
            {
                // 格式化字符串
                ret = Utility.Format(val, FormatString);
            }
            else
            {
                ret = val?.ToString();
            }
            return ret;
        }
    }

    /// <summary>
    /// 通过属性名称构建委托
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="field"></param>
    /// <returns></returns>
    private static Func<TModel, TValue> CreateSelector<TModel, TValue>(string field)
    {
        var type = typeof(TModel);
        var p1 = Expression.Parameter(type);
        var propertyInfo = type.GetProperty(field);
        var fieldExpression = Expression.Property(p1, propertyInfo!);
        return Expression.Lambda<Func<TModel, TValue>>(fieldExpression, p1).Compile();
    }

    private static Func<object, object, TValue?> CreateAggregateLambda<TValue>(AggregateType aggregate, Type type, Type modelType, Type propertyType)
    {
        Func<object, object, TValue?> ret = (_, _) => default;
        // 获得 Enumerable.Sum 方法
        var mi = GetMethodInfoByAggregate(aggregate, modelType, propertyType);
        if (mi != null)
        {
            var p1 = Expression.Parameter(typeof(object));
            var p2 = Expression.Parameter(typeof(object));
            var body = Expression.Call(mi,
                Expression.Convert(p1, type),
                Expression.Convert(p2, typeof(Func<,>).MakeGenericType([modelType, propertyType])));
            ret = Expression.Lambda<Func<object, object, TValue?>>(body, p1, p2).Compile();
        }
        return ret;
    }

    private static MethodInfo? GetMethodInfoByAggregate(AggregateType aggregate, Type modelType, Type propertyType)
    {
        var mi = aggregate switch
        {
            AggregateType.Average => propertyType.Name switch
            {
                nameof(Int32) => typeof(Enumerable).GetMethods()
                    .FirstOrDefault(m => m.Name == aggregate.ToString() && m.IsGenericMethod
                        && m.ReturnType == typeof(Double) && m.GetParameters().Length == 2
                        && m.GetParameters()[1].ParameterType.GenericTypeArguments[1] == typeof(Int32)),
                nameof(Int64) => typeof(Enumerable).GetMethods()
                    .FirstOrDefault(m => m.Name == aggregate.ToString() && m.IsGenericMethod
                        && m.ReturnType == typeof(Double) && m.GetParameters().Length == 2
                        && m.GetParameters()[1].ParameterType.GenericTypeArguments[1] == typeof(Int64)),
                nameof(Double) => typeof(Enumerable).GetMethods()
                    .FirstOrDefault(m => m.Name == aggregate.ToString() && m.IsGenericMethod
                        && m.ReturnType == typeof(Double) && m.GetParameters().Length == 2
                        && m.GetParameters()[1].ParameterType.GenericTypeArguments[1] == typeof(Double)),
                nameof(Decimal) => typeof(Enumerable).GetMethods()
                    .FirstOrDefault(m => m.Name == aggregate.ToString() && m.IsGenericMethod
                        && m.ReturnType == typeof(Decimal) && m.GetParameters().Length == 2
                        && m.GetParameters()[1].ParameterType.GenericTypeArguments[1] == typeof(Decimal)),
                nameof(Single) => typeof(Enumerable).GetMethods()
                    .FirstOrDefault(m => m.Name == aggregate.ToString() && m.IsGenericMethod
                        && m.ReturnType == typeof(Single) && m.GetParameters().Length == 2
                        && m.GetParameters()[1].ParameterType.GenericTypeArguments[1] == typeof(Single)),
                _ => null
            },
            _ => typeof(Enumerable).GetMethods()
                    .FirstOrDefault(m => m.Name == aggregate.ToString() && m.IsGenericMethod && m.ReturnType == propertyType)
        };
        return mi?.MakeGenericMethod(modelType);
    }

    private static int CreateCountMethod<TSource>(IEnumerable<TSource> source) => source.Count();

    private static int GetCount(object? source)
    {
        var ret = 0;
        if (source != null)
        {
            // 绑定数据源类型
            var type = source.GetType();

            // 数据源泛型 TModel 类型
            var modelType = type.GenericTypeArguments[0];

            var mi = typeof(TableFooterCell).GetMethod(nameof(CreateCountMethod), BindingFlags.NonPublic | BindingFlags.Static)!.MakeGenericMethod(modelType);

            if (mi != null)
            {
                var obj = mi.Invoke(null, new object[] { source });
                if (obj != null)
                {
                    var v = obj.ToString();
                    _ = int.TryParse(v, out ret);
                }
            }
        }
        return ret;
    }
}
