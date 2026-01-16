// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Linq.Expressions;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TableFooterCell 组件</para>
/// <para lang="en">TableFooterCell component</para>
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
    /// <para lang="zh">获得/设置 单元格内容</para>
    /// <para lang="en">Gets or sets 单元格content</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Text { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 文字对齐方式 默认为 Alignment.None</para>
    /// <para lang="en">Gets or sets 文字对齐方式 Default is为 Alignment.None</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Alignment Align { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 格式化字符串 如时间类型设置 yyyy-MM-dd</para>
    /// <para lang="en">Gets or sets 格式化字符串 如时间typeSets yyyy-MM-dd</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? FormatString { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 列格式化回调委托</para>
    /// <para lang="en">Gets or sets 列格式化回调delegate</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<object?, Task<string?>>? Formatter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 聚合方法枚举 默认 Sum</para>
    /// <para lang="en">Gets or sets 聚合方法enum Default is Sum</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public AggregateType Aggregate { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 自定义统计列回调方法</para>
    /// <para lang="en">Gets or sets 自定义统计列callback method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<object?, string?, string>? CustomerAggregateCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 统计列名称 默认为 null 不参与统计仅作为显示单元格</para>
    /// <para lang="en">Gets or sets 统计列名称 Default is为 null 不参与统计仅作为display单元格</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public string? Field { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 colspan 值 默认 null 自己手动设置值</para>
    /// <para lang="en">Gets or sets colspan 值 Default is null 自己手动Sets值</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public Func<BreakPoint, int>? ColspanCallback { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为移动端模式</para>
    /// <para lang="en">Gets or sets whether为移动端模式</para>
    /// </summary>
    [CascadingParameter(Name = "IsMobileMode")]
    private bool IsMobileMode { get; set; }

    [CascadingParameter(Name = "TableBreakPoint")]
    private BreakPoint BreakPoint { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否为移动端模式</para>
    /// <para lang="en">Gets or sets whether为移动端模式</para>
    /// </summary>
    [CascadingParameter(Name = "TableFooterContext")]
    private object? DataSource { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 显示节点阈值 默认值 BreakPoint.None 未设置</para>
    /// <para lang="en">Gets or sets display节点阈值 Default is值 BreakPoint.None 未Sets</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    public BreakPoint ShownWithBreakPoint { get; set; }

    private string? _value { get; set; }

    /// <summary>
    /// <para lang="zh"><inheritdoc/></para>
    /// <para lang="en"><inheritdoc/></para>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnParametersSetAsync()
    {
        _value = Text ?? (GetCount(DataSource) == 0 ? "0" : (GetCountValue() ?? await GetAggregateValue()));
    }

    /// <summary>
    /// <para lang="zh">检查当前列是否显示方法</para>
    /// <para lang="en">检查当前列whetherdisplay方法</para>
    /// </summary>
    /// <returns></returns>
    protected bool CheckShownWithBreakpoint => BreakPoint >= ShownWithBreakPoint;

    /// <summary>
    /// <para lang="zh">解析 Count Aggregate</para>
    /// <para lang="en">解析 Count Aggregate</para>
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
    /// <para lang="zh">通过属性名称构建委托</para>
    /// <para lang="en">通过property名称构建delegate</para>
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
