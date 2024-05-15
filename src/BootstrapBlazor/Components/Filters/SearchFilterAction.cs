﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
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
    /// 重置过滤条件方法
    /// </summary>
    public void Reset()
    {
        value = null;
    }

    /// <summary>
    /// 设置过滤条件方法
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public Task SetFilterConditionsAsync(FilterKeyValueAction filter)
    {
        var first = filter.Filters?.FirstOrDefault() ?? filter;
        if (first.FieldKey == name)
        {
            value = first.FieldValue;
        }
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获取所有过滤条件集合
    /// </summary>
    /// <returns></returns>
    public virtual FilterKeyValueAction GetFilterConditions() => new()
    {
        FieldKey = name,
        FieldValue = value,
        FilterAction = action,
    };
}
