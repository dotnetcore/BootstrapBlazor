// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

using System.Linq.Expressions;

namespace BootstrapBlazor.Components;

/// <summary>
/// IQueryable 扩展方法
/// </summary>
public static class IQueryableExtensions
{
    /// <summary>
    /// BootstrapBlazor 扩展 Where 方法
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <param name="queryable">数据源</param>
    /// <param name="predicate">过滤条件</param>
    /// <param name="condition">是否查询条件</param>
    /// <returns></returns>
    public static IQueryable<T> Where<T>(this IQueryable<T> queryable, Expression<Func<T, bool>> predicate, bool condition) => condition ? queryable.Where(predicate) : queryable;

    /// <summary>
    /// BootstrapBlazor 扩展 Sort 方法
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <param name="queryable">数据源</param>
    /// <param name="sortName">排序名称</param>
    /// <param name="sortOrder">排序策略</param>
    /// <param name="condition">是否排序</param>
    /// <returns></returns>
    public static IQueryable<T> Sort<T>(this IQueryable<T> queryable, string sortName, SortOrder sortOrder, bool condition) => condition ? queryable.Sort(sortName, sortOrder) : queryable;

    /// <summary>
    /// BootstrapBlazor 扩展 Page 方法
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <param name="queryable">数据源</param>
    /// <param name="skipCount">Skip 数量</param>
    /// <param name="maxResultCount">Take 数量</param>
    /// <returns></returns>
    public static IQueryable<T> Page<T>(this IQueryable<T> queryable, int skipCount, int maxResultCount) => queryable.Skip(skipCount).Take(maxResultCount);

    /// <summary>
    /// BootstrapBlazor 扩展 Count 方法
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <param name="queryable">数据源</param>
    /// <param name="totalCount">结果数量</param>
    /// <returns></returns>
    public static IQueryable<T> Count<T>(this IQueryable<T> queryable, out int totalCount)
    {
        totalCount = queryable.Count();
        return queryable;
    }
}
