// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IFilterAction 默认实现类</para>
/// <para lang="en">Default implementation class of IFilterAction</para>
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
/// <param name="action"></param>
public class SearchFilterAction(string name, object? value, FilterAction action = FilterAction.Contains) : IFilterAction
{
    /// <summary>
    /// <para lang="zh">获得/设置 过滤条件名称</para>
    /// <para lang="en">Gets or sets filter condition name</para>
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// <para lang="zh">获得/设置 过滤条件值</para>
    /// <para lang="en">Gets or sets filter condition value</para>
    /// </summary>
    public object? Value { get; set; } = value;

    /// <summary>
    /// <para lang="zh">获得/设置 过滤条件关系运算符</para>
    /// <para lang="en">Gets or sets filter condition relation operator</para>
    /// </summary>
    public FilterAction Action { get; set; } = action;

    /// <summary>
    /// <inheritdoc cref="IFilterAction.Reset"/>
    /// </summary>
    public void Reset()
    {
        Value = null;
    }

    /// <summary>
    /// <inheritdoc cref="IFilterAction.GetFilterConditions"/>
    /// </summary>
    public virtual FilterKeyValueAction GetFilterConditions() => new()
    {
        FieldKey = Name,
        FieldValue = Value,
        FilterAction = Action,
    };

    /// <summary>
    /// <inheritdoc cref="IFilterAction.SetFilterConditionsAsync(FilterKeyValueAction)"/>
    /// </summary>
    /// <param name="filter"></param>
    public Task SetFilterConditionsAsync(FilterKeyValueAction filter)
    {
        var first = filter.Filters.FirstOrDefault() ?? filter;
        if (first.FieldKey == Name)
        {
            Value = first.FieldValue;
        }
        return Task.CompletedTask;
    }
}
