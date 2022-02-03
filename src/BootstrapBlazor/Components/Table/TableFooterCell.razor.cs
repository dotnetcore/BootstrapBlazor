// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;
using System.Reflection;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
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
    /// 获得/设置 聚合方法枚举 默认 Sum
    /// </summary>
    [Parameter]
    public AggregateType Aggregate { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string? Field { get; set; }

    /// <summary>
    /// 获得/设置 是否为移动端模式
    /// </summary>
    [CascadingParameter(Name = "IsMobileMode")]
    private bool IsMobileMode { get; set; }

    /// <summary>
    /// 获得/设置 是否为移动端模式
    /// </summary>
    [CascadingParameter(Name = "TableFooterContext")]
    private object? DataSource { get; set; }

    private string? GetText() => Text ?? (GetCount(DataSource) == 0 ? "0" : (GetCountValue() ?? GetAggegateValue()));

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

            var mi = GetType().GetMethod(nameof(CreateCountMethod), BindingFlags.NonPublic | BindingFlags.Static)
                            ?.MakeGenericMethod(modelType);

            if (mi != null)
            {
                v = mi.Invoke(null, new object[] { DataSource })?.ToString();
            }
        }
        return v;
    }

    private string GetAggegateValue()
    {
        var v = "";
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

                // 构建 Aggegate
                // @context.Sum(i => i.Count)
                var aggegateMethod = Aggregate switch
                {
                    AggregateType.Average => propertyType.Name switch
                    {
                        nameof(Int32) or nameof(Int64) => GetType()
                            .GetMethod(nameof(CreateAggregateLambda), BindingFlags.NonPublic | BindingFlags.Static)
                            ?.MakeGenericMethod(typeof(Double)),
                        nameof(Decimal) or nameof(Double) or nameof(Single) => GetType()
                            .GetMethod(nameof(CreateAggregateLambda), BindingFlags.NonPublic | BindingFlags.Static)
                            ?.MakeGenericMethod(propertyType),
                        _ => null
                    },
                    _ => GetType().GetMethod(nameof(CreateAggregateLambda), BindingFlags.NonPublic | BindingFlags.Static)
                        ?.MakeGenericMethod(propertyType)
                };
                if (aggegateMethod != null)
                {
                    var invoker = aggegateMethod.Invoke(null, new object[] { Aggregate, type, modelType, propertyType });
                    if (invoker != null)
                    {
                        // 构建 Selector
                        var methodInfo = GetType().GetMethod(nameof(CreateSelector), BindingFlags.NonPublic | BindingFlags.Static)
                            ?.MakeGenericMethod(modelType, propertyType);
                        if (methodInfo != null)
                        {
                            var selector = methodInfo.Invoke(null, new object[] { Field });
                            if (selector != null)
                            {
                                // 执行委托
                                var val = (invoker as Delegate)?.DynamicInvoke(DataSource, selector);
                                v = val?.ToString() ?? "";
                            }
                        }
                    }
                }
            }
        }
        return v;
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

        if (propertyInfo == null)
        {
            throw new InvalidOperationException();
        }

        var fieldExpression = Expression.Property(p1, propertyInfo);
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
                Expression.Convert(p2, typeof(Func<,>).MakeGenericType(new Type[] { modelType, propertyType })));
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
                nameof(Decimal) or nameof(Double) or nameof(Single) => typeof(Enumerable).GetMethods()
                    .FirstOrDefault(m => m.Name == aggregate.ToString() && m.IsGenericMethod && m.ReturnType == propertyType),
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

            var mi = typeof(TableFooterCell).GetMethod(nameof(CreateCountMethod), BindingFlags.NonPublic | BindingFlags.Static)
                            ?.MakeGenericMethod(modelType);

            if (mi != null)
            {
                var v = mi.Invoke(null, new object[] { source })?.ToString();
                _ = int.TryParse(v, out ret);
            }
        }
        return ret;
    }
}
