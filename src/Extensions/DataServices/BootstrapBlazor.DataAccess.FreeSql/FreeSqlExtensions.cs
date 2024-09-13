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
    /// <param name="customConvert">当 <see cref="FilterAction"/> 的枚举值为 <see cref="FilterAction.CustomPredicate"/> 时的自定义转换方法。</param>
    /// <returns></returns>
    public static DynamicFilterInfo ToDynamicFilter(this QueryPageOptions option, Action<CustomResult>? customConvert = null)
    {
        var ret = new DynamicFilterInfo() { Filters = [] };

        // 处理模糊搜索
        if (option.Searches.Count > 0)
        {
            ret.Filters.Add(new()
            {
                Logic = DynamicFilterLogic.Or,
                Filters = option.Searches.Select(i => i.ToDynamicFilter(customConvert)).ToList()
            });
        }

        // 处理自定义搜索
        if (option.CustomerSearches.Count > 0)
        {
            ret.Filters.AddRange(option.CustomerSearches.Select(i => i.ToDynamicFilter(customConvert)));
        }

        // 处理高级搜索
        if (option.AdvanceSearches.Count > 0)
        {
            ret.Filters.AddRange(option.AdvanceSearches.Select(i => i.ToDynamicFilter(customConvert)));
        }

        // 处理表格过滤条件
        if (option.Filters.Count > 0)
        {
            ret.Filters.AddRange(option.Filters.Select(i => i.ToDynamicFilter(customConvert)));
        }
        return ret;
    }

    /// <summary>
    /// IFilterAction 转化为 DynamicFilterInfo 扩展方法
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="customConvert">当 <see cref="FilterAction"/> 的枚举值为 <see cref="FilterAction.CustomPredicate"/> 时的自定义转换方法。</param>
    /// <returns></returns>
    public static DynamicFilterInfo ToDynamicFilter(this IFilterAction filter, Action<CustomResult>? customConvert = null)
    {
        var filterKeyValueAction = filter.GetFilterConditions();
        return filterKeyValueAction.ParseDynamicFilterInfo(customConvert);
    }

    private static DynamicFilterInfo ParseDynamicFilterInfo(this FilterKeyValueAction filterKeyValueAction, Action<CustomResult>? customConvert = null)
    {
        CustomResult? customValue = null;
        bool GetIsSetResult() => customValue != null && customValue.IsSetResult;
        if (filterKeyValueAction.FilterAction == FilterAction.CustomPredicate && customConvert != null)
        {
            customValue = new CustomResult(filterKeyValueAction);
            customConvert(customValue);
        }
        return new()
        {
            Operator = GetIsSetResult() ? customValue!.Operator!.Value : filterKeyValueAction.FilterAction.ToDynamicFilterOperator(),
            Logic = filterKeyValueAction.FilterLogic.ToDynamicFilterLogic(),
            Field = filterKeyValueAction.FieldKey,
            Value = GetIsSetResult() ? customValue!.Value : filterKeyValueAction.FieldValue,
            Filters = filterKeyValueAction.Filters?.Select(i => i.ParseDynamicFilterInfo(customConvert)).ToList()
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
        _ => throw new NotSupportedException()
    };

    /// <summary>
    /// 自定义转换返回值。
    /// </summary>
    public class CustomResult(FilterKeyValueAction filter)
    {
        /// <summary>
        /// 要自定转换的 <see cref="FilterKeyValueAction"/> 实例。
        /// </summary>
        public FilterKeyValueAction FilterKeyValueAction { get; } = filter;
        /// <summary>
        /// 将 <see cref="FilterAction.CustomPredicate"/> 为 <see cref="DynamicFilterOperator"/> 对应的枚举值。
        /// </summary>
        public DynamicFilterOperator? Operator { get; set; }
        /// <summary>
        /// 过滤条件值。
        /// </summary>
        public object? Value { get; set; }

        /// <summary>
        /// 获取是否已通过 <see cref="SetResult(DynamicFilterOperator, object?)"/> 设置了返回值，不设置表示忽略转换。
        /// </summary>
        public bool IsSetResult { get; private set; } = false;
        /// <summary>
        /// 设置转换后的结果。
        /// </summary>
        /// <param name="operator"></param>
        /// <param name="value"></param>
        public void SetResult(DynamicFilterOperator @operator, object? value)
        {
            IsSetResult = true;
            Operator = @operator;
            Value = value;
        }
    }
}
