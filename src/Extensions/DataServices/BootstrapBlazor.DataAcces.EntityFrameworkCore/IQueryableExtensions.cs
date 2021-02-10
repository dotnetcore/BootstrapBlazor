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
    /// 
    /// </summary>
    public static class IQueryableExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="condition"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> queryable, bool condition, Expression<Func<T, bool>> predicate)
        {
            if (queryable == null)
            {
                throw new ArgumentNullException($"{nameof(queryable)}不可为空!");
            }
            return condition ? queryable.Where(predicate) : queryable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="condition"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public static IQueryable<T> SortIf<T>(this IQueryable<T> queryable, bool condition, QueryPageOptions option)
        {
            if (queryable == null)
            {
                throw new ArgumentNullException($"{nameof(queryable)}不可为空!");
            }
            return condition ? queryable.Sort(option.SortName!, option.SortOrder) : queryable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryable"></param>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <returns></returns>
        public static IQueryable<T> PageBy<T>(this IQueryable<T> queryable, int skipCount, int maxResultCount)
        {
            if (queryable == null)
            {
                throw new ArgumentNullException($"{nameof(queryable)}不可为空!");
            }
            return queryable.Skip(skipCount).Take(maxResultCount);
        }

        public static IQueryable<T> Count<T>(this IQueryable<T> queryable, out int totalCount)
        {
            if (queryable == null)
            {
                throw new ArgumentNullException($"{nameof(queryable)}不可为空!");
            }
            totalCount = queryable.Count();
            return queryable;
        }
    }
}
