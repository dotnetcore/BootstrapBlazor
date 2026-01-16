// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Linq.Expressions;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IQueryable 扩展方法</para>
/// <para lang="en">IQueryable extension methods</para>
/// </summary>
public static class IQueryableExtensions
{
    /// <summary>
    /// <para lang="zh">BootstrapBlazor 扩展 Where 方法</para>
    /// <para lang="en">BootstrapBlazor Where extension method</para>
    /// </summary>
    /// <typeparam name="T"><para lang="zh">泛型</para><para lang="en">Generic type</para></typeparam>
    /// <param name="queryable"><para lang="zh">数据源</para><para lang="en">Data source</para></param>
    /// <param name="predicate"><para lang="zh">过滤条件</para><para lang="en">Filter condition</para></param>
    /// <param name="condition"><para lang="zh">是否查询条件</para><para lang="en">Whether to query condition</para></param>
    /// <returns></returns>
    public static IQueryable<T> Where<T>(this IQueryable<T> queryable, Expression<Func<T, bool>> predicate, bool condition) => condition ? queryable.Where(predicate) : queryable;

    /// <summary>
    /// <para lang="zh">BootstrapBlazor 扩展 Sort 方法</para>
    /// <para lang="en">BootstrapBlazor Sort extension method</para>
    /// </summary>
    /// <typeparam name="T"><para lang="zh">泛型</para><para lang="en">Generic type</para></typeparam>
    /// <param name="queryable"><para lang="zh">数据源</para><para lang="en">Data source</para></param>
    /// <param name="sortName"><para lang="zh">排序名称</para><para lang="en">Sort name</para></param>
    /// <param name="sortOrder"><para lang="zh">排序策略</para><para lang="en">Sort order</para></param>
    /// <param name="condition"><para lang="zh">是否排序</para><para lang="en">Whether to sort</para></param>
    /// <returns></returns>
    public static IQueryable<T> Sort<T>(this IQueryable<T> queryable, string sortName, SortOrder sortOrder, bool condition) => condition ? queryable.Sort(sortName, sortOrder) : queryable;

    /// <summary>
    /// <para lang="zh">BootstrapBlazor 扩展 Page 方法</para>
    /// <para lang="en">BootstrapBlazor Page extension method</para>
    /// </summary>
    /// <typeparam name="T"><para lang="zh">泛型</para><para lang="en">Generic type</para></typeparam>
    /// <param name="queryable"><para lang="zh">数据源</para><para lang="en">Data source</para></param>
    /// <param name="skipCount"><para lang="zh">Skip 数量</para><para lang="en">Skip count</para></param>
    /// <param name="maxResultCount"><para lang="zh">Take 数量</para><para lang="en">Take count</para></param>
    /// <returns></returns>
    public static IQueryable<T> Page<T>(this IQueryable<T> queryable, int skipCount, int maxResultCount) => queryable.Skip(skipCount).Take(maxResultCount);

    /// <summary>
    /// <para lang="zh">BootstrapBlazor 扩展 Count 方法</para>
    /// <para lang="en">BootstrapBlazor Count extension method</para>
    /// </summary>
    /// <typeparam name="T"><para lang="zh">泛型</para><para lang="en">Generic type</para></typeparam>
    /// <param name="queryable"><para lang="zh">数据源</para><para lang="en">Data source</para></param>
    /// <param name="totalCount"><para lang="zh">结果数量</para><para lang="en">Total count</para></param>
    /// <returns></returns>
    public static IQueryable<T> Count<T>(this IQueryable<T> queryable, out int totalCount)
    {
        totalCount = queryable.Count();
        return queryable;
    }
}
