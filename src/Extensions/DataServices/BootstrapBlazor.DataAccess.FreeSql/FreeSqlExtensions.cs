// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using FreeSql.Internal.Model;

namespace BootstrapBlazor.DataAccess.FreeSql;

/// <summary>
/// FreeSql 扩展方法
/// </summary>
public static class FreeSqlExtensions
{
    /// <summary>
    /// QueryPageOptions 转化为 FreeSql ORM DynamicFilterInfo 类型扩展方法
    /// </summary>
    /// <param name="option"></param>
    /// <param name="dynamicFilterInfoConverter">当 <see cref="FilterAction"/> 的枚举值为 <see cref="FilterAction.CustomPredicate"/> 时的自定义转换方法。</param>
    /// <returns></returns>
    public static DynamicFilterInfo ToDynamicFilter(this QueryPageOptions option, Func<FilterKeyValueAction, DynamicFilterInfo>? dynamicFilterInfoConverter = null)
    {
        var ret = new DynamicFilterInfo() { Filters = [] };

        // 处理模糊搜索
        if (option.Searches.Count > 0)
        {
            ret.Filters.Add(new()
            {
                Logic = DynamicFilterLogic.Or,
                Filters = option.Searches.Select(i => i.ToDynamicFilter(dynamicFilterInfoConverter)).ToList()
            });
        }

        // 处理自定义搜索
        if (option.CustomerSearches.Count > 0)
        {
            ret.Filters.AddRange(option.CustomerSearches.Select(i => i.ToDynamicFilter(dynamicFilterInfoConverter)));
        }

        // 处理高级搜索
        if (option.AdvanceSearches.Count > 0)
        {
            ret.Filters.AddRange(option.AdvanceSearches.Select(i => i.ToDynamicFilter(dynamicFilterInfoConverter)));
        }

        // 处理表格过滤条件
        if (option.Filters.Count > 0)
        {
            ret.Filters.AddRange(option.Filters.Select(i => i.ToDynamicFilter(dynamicFilterInfoConverter)));
        }
        return ret;
    }

    /// <summary>
    /// IFilterAction 转化为 DynamicFilterInfo 扩展方法
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="dynamicFilterInfoConverter">当 <see cref="FilterAction"/> 的枚举值为 <see cref="FilterAction.CustomPredicate"/> 时的自定义转换方法。</param>
    /// <returns></returns>
    public static DynamicFilterInfo ToDynamicFilter(this IFilterAction filter, Func<FilterKeyValueAction, DynamicFilterInfo>? dynamicFilterInfoConverter = null)
    {
        var filterKeyValueAction = filter.GetFilterConditions();
        return filterKeyValueAction.ParseDynamicFilterInfo(dynamicFilterInfoConverter);
    }

    private static DynamicFilterInfo ParseDynamicFilterInfo(this FilterKeyValueAction filterKeyValueAction, Func<FilterKeyValueAction, DynamicFilterInfo>? dynamicFilterInfoConverter = null)
    {
        return filterKeyValueAction.FilterAction == FilterAction.CustomPredicate
            ? dynamicFilterInfoConverter?.Invoke(filterKeyValueAction) ?? throw new InvalidOperationException("The parameter dynamicFilterInfoConverter can't not null")
            : new()
            {
                Operator = filterKeyValueAction.FilterAction.ToDynamicFilterOperator(),
                Logic = filterKeyValueAction.FilterLogic.ToDynamicFilterLogic(),
                Field = filterKeyValueAction.FieldKey,
                Value = filterKeyValueAction.FieldValue,
                Filters = filterKeyValueAction.Filters?.Select(i => i.ParseDynamicFilterInfo(dynamicFilterInfoConverter)).ToList()
            };
    }

    private static DynamicFilterLogic ToDynamicFilterLogic(this FilterLogic logic) => logic switch
    {
        FilterLogic.And => DynamicFilterLogic.And,
        _ => DynamicFilterLogic.Or
    };

    private static DynamicFilterOperator ToDynamicFilterOperator(this FilterAction action) => action switch
    {
        FilterAction.Equal => DynamicFilterOperator.Equal,
        FilterAction.NotEqual => DynamicFilterOperator.NotEqual,
        FilterAction.Contains => DynamicFilterOperator.Contains,
        FilterAction.NotContains => DynamicFilterOperator.NotContains,
        FilterAction.GreaterThan => DynamicFilterOperator.GreaterThan,
        FilterAction.GreaterThanOrEqual => DynamicFilterOperator.GreaterThanOrEqual,
        FilterAction.LessThan => DynamicFilterOperator.LessThan,
        FilterAction.LessThanOrEqual => DynamicFilterOperator.LessThanOrEqual,
        _ => throw new NotSupportedException("Please use the ToDynamicFilter method second parameter to support CustomPredicate")
    };
}
