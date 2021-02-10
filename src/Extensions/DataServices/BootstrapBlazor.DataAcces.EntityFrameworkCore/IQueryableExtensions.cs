// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace BootstrapBlazor.DataAcces.EntityFrameworkCore
{
    /// <summary>
    /// IQueryable 扩展方法
    /// </summary>
    public static class IQueryableExtensions
    {
        /// <summary>
        /// 条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IQueryable<T> Where<T>(this IQueryable<T> queryable, Expression<Func<T, bool>> predicate, bool condition = true) => condition ? queryable.Where(predicate) : queryable;

        /// <summary>
        /// 排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="sortName"></param>
        /// <param name="sortOrder"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static IQueryable<T> Sort<T>(this IQueryable<T> queryable, string sortName, SortOrder sortOrder, bool condition = true) => condition ? queryable.Sort(sortName, sortOrder) : queryable;

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <returns></returns>
        public static IQueryable<T> Page<T>(this IQueryable<T> queryable, int skipCount, int maxResultCount) => queryable.Skip(skipCount).Take(maxResultCount);

        /// <summary>
        /// 总数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public static IQueryable<T> Count<T>(this IQueryable<T> queryable, out int totalCount)
        {
            totalCount = queryable.Count();
            return queryable;
        }
    }
}
