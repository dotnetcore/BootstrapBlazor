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
    /// 获得/设置 是否为移动端模式
    /// </summary>
    [CascadingParameter(Name = "IsMobileMode")]
    private bool IsMobileMode { get; set; }

    /// <summary>
    /// 获得/设置 是否为移动端模式
    /// </summary>
    [CascadingParameter(Name = "TableFooterContext")]
    private object? DataSource { get; set; }

    private string? _value { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task OnParametersSetAsync()
    {
        _value = Text ?? (GetCount() == 0 ? "0" : (GetCountValue() ?? await GetAggregateValue()));
    }

    private int GetCount()
    {
        var ret = 0;
        if (DataSource != null)
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
                    var v = obj.ToString();
                    _ = int.TryParse(v, out ret);
                }
            }
        }
        return ret;
    }

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
                            nameof(Int32) or nameof(Int64) or nameof(Double) => GetType().GetMethod(nameof(CreateAggregateLambda), BindingFlags.NonPublic | BindingFlags.Static)!.MakeGenericMethod(typeof(Double)),
                            _ => GetType().GetMethod(nameof(CreateAggregateLambda), BindingFlags.NonPublic | BindingFlags.Static)!.MakeGenericMethod(propertyType),
                        },
                        _ => GetType().GetMethod(nameof(CreateAggregateLambda), BindingFlags.NonPublic | BindingFlags.Static)!.MakeGenericMethod(propertyType)
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
                var methodInfo = typeof(LambdaExtensions).GetMethod(nameof(LambdaExtensions.GetPropertyLambda), BindingFlags.NonPublic | BindingFlags.Static)!.MakeGenericMethod(modelType, propertyType);
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

    private static int CreateCountMethod<TSource>(IEnumerable<TSource> source) => source.Count();
}
