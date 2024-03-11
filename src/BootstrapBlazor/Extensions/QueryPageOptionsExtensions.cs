// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System.Linq.Expressions;

namespace BootstrapBlazor.Components;

/// <summary>
/// QueryPageOptions 扩展方法
/// </summary>
public static class QueryPageOptionsExtensions
{
    /// <summary>
    /// 将 QueryPageOptions 过滤条件转换为 <see cref="FilterKeyValueAction"/>
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public static FilterKeyValueAction ToFilter(this QueryPageOptions option)
    {
        var filter = new FilterKeyValueAction() { Filters = [] };

        // 处理模糊搜索
        if (option.Searches.Count != 0)
        {
            filter.Filters.Add(new FilterKeyValueAction()
            {
                FilterLogic = FilterLogic.Or,
                Filters = option.Searches.Select(i => i.GetFilterConditions()).ToList()
            });
        }

        // 处理自定义搜索
        if (option.CustomerSearches.Count != 0)
        {
            filter.Filters.AddRange(option.CustomerSearches.Select(i => i.GetFilterConditions()));
        }

        // 处理高级搜索
        if (option.AdvanceSearches.Count != 0)
        {
            filter.Filters.AddRange(option.AdvanceSearches.Select(i => i.GetFilterConditions()));
        }

        // 处理表格过滤条件
        if (option.Filters.Count != 0)
        {
            filter.Filters.AddRange(option.Filters.Select(i => i.GetFilterConditions()));
        }

        return filter;
    }

    /// <summary>
    /// 将 QueryPageOptions 过滤条件转换为 where 条件中的参数 <see cref="Func{T, TResult}"/>"/> 推荐 Linq 使用
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public static Func<TItem, bool> ToFilterFunc<TItem>(this QueryPageOptions option) => option.ToFilterLambda<TItem>().Compile();

    /// <summary>
    /// 将 QueryPageOptions 过滤条件转换为 <see cref="Expression{TDelegate}"/> 表达式"/> 推荐 EFCore <see cref="IQueryable"/> 使用
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public static Expression<Func<TItem, bool>> ToFilterLambda<TItem>(this QueryPageOptions option) => option.ToFilter().GetFilterLambda<TItem>();

    /// <summary>
    /// 是否包含过滤条件
    /// </summary>
    /// <param name="filterKeyValueAction"></param>
    /// <returns></returns>
    public static bool HasFilters(this FilterKeyValueAction filterKeyValueAction) => filterKeyValueAction.Filters != null && filterKeyValueAction.Filters.Count != 0;
}
