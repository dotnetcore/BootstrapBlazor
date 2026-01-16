// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IFilterAction 类默认实现类</para>
/// <para lang="en">Default Implementation Class of IFilterAction</para>
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
/// <param name="action"></param>
public class SearchFilterAction(string name, object? value, FilterAction action = FilterAction.Contains) : IFilterAction
{
    /// <summary>
    /// <para lang="zh">获得/设置 过滤条件名称</para>
    /// <para lang="en">Get/Set Filter Condition Name</para>
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// <para lang="zh">获得/设置 过滤条件值</para>
    /// <para lang="en">Get/Set Filter Condition Value</para>
    /// </summary>
    public object? Value { get; set; } = value;

    /// <summary>
    /// <para lang="zh">获得/设置 过滤条件关系运算符</para>
    /// <para lang="en">Get/Set Filter Condition Relation Operator</para>
    /// </summary>
    public FilterAction Action { get; set; } = action;

    /// <summary>
    /// <para lang="zh">重置过滤条件方法</para>
    /// <para lang="en">Reset Filter Conditions Method</para>
    /// </summary>
    public void Reset()
    {
        Value = null;
    }

    /// <summary>
    /// <para lang="zh">设置过滤条件方法</para>
    /// <para lang="en">Set Filter Conditions Method</para>
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public Task SetFilterConditionsAsync(FilterKeyValueAction filter)
    {
        var first = filter.Filters.FirstOrDefault() ?? filter;
        if (first.FieldKey == Name)
        {
            Value = first.FieldValue;
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// <para lang="zh">获取所有过滤条件集合</para>
    /// <para lang="en">Get All Filter Conditions Collection</para>
    /// </summary>
    /// <returns></returns>
    public virtual FilterKeyValueAction GetFilterConditions() => new()
    {
        FieldKey = Name,
        FieldValue = Value,
        FilterAction = Action,
    };
}
