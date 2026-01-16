// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Linq.Expressions;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">QueryPageOptions 扩展方法</para>
/// <para lang="en">QueryPageOptions 扩展方法</para>
/// </summary>
public static class QueryPageOptionsExtensions
{
    /// <summary>
    /// <para lang="zh">将 QueryPageOptions 过滤条件转换为 <see cref="FilterKeyValueAction"/></para>
    /// <para lang="en">将 QueryPageOptions 过滤条件转换为 <see cref="FilterKeyValueAction"/></para>
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public static FilterKeyValueAction ToFilter(this QueryPageOptions option)
    {
        var filter = new FilterKeyValueAction();

        // 处理模糊搜索
        if (option.Searches.Count != 0)
        {
            filter.Filters.Add(new FilterKeyValueAction()
            {
                FilterLogic = FilterLogic.Or,
                Filters = [.. option.Searches.Select(i => i.GetFilterConditions())]
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
    /// <para lang="zh">将 QueryPageOptions 过滤条件转换为 where 条件中的参数 <see cref="Func{T, TResult}"/>"/> 推荐 Linq 使用</para>
    /// <para lang="en">将 QueryPageOptions 过滤条件转换为 where 条件中的参数 <see cref="Func{T, TResult}"/>"/> 推荐 Linq 使用</para>
    /// </summary>
    /// <param name="option"></param>
    /// <param name="comparison"><para lang="zh"><see cref="StringComparison"/> 实例，此方法不支持 EFCore Where 查询</para><para lang="en"><see cref="StringComparison"/> instance，此method不支持 EFCore Where 查询</para></param>
    /// <returns></returns>
    public static Func<TItem, bool> ToFilterFunc<TItem>(this QueryPageOptions option, StringComparison? comparison = null) => option.ToFilterLambda<TItem>(comparison).Compile();

    /// <summary>
    /// <para lang="zh">将 QueryPageOptions 过滤条件转换为 <see cref="Expression{TDelegate}"/> 表达式"/> 推荐 EFCore <see cref="IQueryable"/> 使用</para>
    /// <para lang="en">将 QueryPageOptions 过滤条件转换为 <see cref="Expression{TDelegate}"/> 表达式"/> 推荐 EFCore <see cref="IQueryable"/> 使用</para>
    /// </summary>
    /// <param name="option"></param>
    /// <param name="comparison"><para lang="zh"><see cref="StringComparison"/> 实例，此方法不支持 EFCore Where 查询</para><para lang="en"><see cref="StringComparison"/> instance，此method不支持 EFCore Where 查询</para></param>
    /// <returns></returns>
    public static Expression<Func<TItem, bool>> ToFilterLambda<TItem>(this QueryPageOptions option, StringComparison? comparison = null) => option.ToFilter().GetFilterLambda<TItem>(comparison);

    /// <summary>
    /// <para lang="zh">是否包含过滤条件</para>
    /// <para lang="en">whether包含过滤条件</para>
    /// </summary>
    /// <param name="filterKeyValueAction"></param>
    /// <returns></returns>
    public static bool HasFilters(this FilterKeyValueAction filterKeyValueAction) => filterKeyValueAction.Filters.Count != 0;
}
