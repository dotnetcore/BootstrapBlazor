// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// IFilterAction 类默认实现类
/// </summary>
/// <param name="name"></param>
/// <param name="value"></param>
/// <param name="action"></param>
public class SearchFilterAction(string name, object? value, FilterAction action = FilterAction.Contains) : IFilterAction
{
    /// <summary>
    /// 获得/设置 过滤条件名称
    /// </summary>
    public string Name { get; set; } = name;

    /// <summary>
    /// 获得/设置 过滤条件值
    /// </summary>
    public object? Value { get; set; } = value;

    /// <summary>
    /// 获得/设置 过滤条件关系运算符
    /// </summary>
    public FilterAction Action { get; set; } = action;

    /// <summary>
    /// 重置过滤条件方法
    /// </summary>
    public void Reset()
    {
        Value = null;
    }

    /// <summary>
    /// 设置过滤条件方法
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public Task SetFilterConditionsAsync(FilterKeyValueAction filter)
    {
        var first = filter.Filters?.FirstOrDefault() ?? filter;
        if (first.FieldKey == Name)
        {
            Value = first.FieldValue;
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获取所有过滤条件集合
    /// </summary>
    /// <returns></returns>
    public virtual FilterKeyValueAction GetFilterConditions() => new()
    {
        FieldKey = Name,
        FieldValue = Value,
        FilterAction = Action,
    };
}
