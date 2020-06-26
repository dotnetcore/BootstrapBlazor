using BootstrapBlazor.Components;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace System.Linq
{
    /// <summary>
    /// Lambda 表达式扩展类
    /// </summary>
    public static class LambdaExtensions
    {
        /// <summary>
        /// 指定集合获取 Lambda 表达式
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static Expression<Func<TItem, bool>> GetFilterLambda<TItem>(this IEnumerable<FilterKeyValueAction> filters)
        {
            Expression<Func<TItem, bool>>? ret = null;
            foreach (var filter in filters)
            {
                var exp = filter.GetFilterExpression<TItem>();
                if (ret == null)
                {
                    ret = exp;
                    continue;
                }

                var invokedExpr = Expression.Invoke(exp, ret.Parameters.Cast<Expression>());

                ret = filter.FilterLogic switch
                {
                    FilterLogic.And => Expression.Lambda<Func<TItem, bool>>(Expression.AndAlso(ret.Body, invokedExpr), ret.Parameters),
                    _ => Expression.Lambda<Func<TItem, bool>>(Expression.OrElse(ret.Body, invokedExpr), ret.Parameters),
                };
            }
            return ret ?? (t => true);
        }

        /// <summary>
        /// 指定集合获取委托
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static Func<TItem, bool> GetFilterFunc<TItem>(this IEnumerable<FilterKeyValueAction> filters) => filters.GetFilterLambda<TItem>().Compile();

        /// <summary>
        /// 指定 FilterKeyValueAction 获取 Lambda 表达式
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static Expression<Func<TItem, bool>> GetFilterExpression<TItem>(this FilterKeyValueAction filter)
        {
            Expression<Func<TItem, bool>> ret = t => true;
            if (!string.IsNullOrEmpty(filter.FieldKey))
            {
                var prop = typeof(TItem).GetProperty(filter.FieldKey);
                if (prop != null && filter.FieldValue != null)
                {
                    var p = Expression.Parameter(typeof(TItem));
                    var fieldExpression = Expression.Property(p, prop);
                    var eq = GetExpression(filter, fieldExpression);
                    ret = Expression.Lambda<Func<TItem, bool>>(eq, p);
                }
            }
            return ret;
        }

        /// <summary>
        /// 指定 FilterKeyValueAction 获取委托
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static Func<TItem, bool> GetFilterFunc<TItem>(this FilterKeyValueAction filter) => filter.GetFilterExpression<TItem>().Compile();

        private static Expression GetExpression(FilterKeyValueAction filter, Expression left)
        {
            var right = Expression.Constant(filter.FieldValue);
            return filter.FilterAction switch
            {
                FilterAction.Equal => Expression.Equal(left, right),
                FilterAction.NotEqual => Expression.NotEqual(left, right),
                FilterAction.GreaterThan => Expression.GreaterThan(left, right),
                FilterAction.GreaterThanOrEqual => Expression.GreaterThanOrEqual(left, right),
                FilterAction.LessThan => Expression.LessThan(left, right),
                FilterAction.LessThanOrEqual => Expression.LessThanOrEqual(left, right),
                FilterAction.Contains => left.Contains(right),
                FilterAction.NotContains => Expression.Not(left.Contains(right)),
                _ => Expression.Empty()
            };
        }

        private static Expression Contains(this Expression left, Expression right)
        {
            var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            return Expression.Call(left, method, right);
        }

        private delegate TResult FuncEx<T1, TOut, TResult>(T1 str, out TOut outValue);

        /// <summary>
        /// 尝试使用 TryParse 进行数据转换
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="source"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static bool TryParse<TItem>(this string source, out TItem v)
        {
            bool ret = false;
            var t = typeof(TItem);
            var p1 = Expression.Parameter(typeof(string));
            var p2 = Expression.Parameter(typeof(TItem).MakeByRefType());
            var method = t.GetMethod("TryParse", new Type[] { typeof(string), t.MakeByRefType() });
            TItem outValue = default;
#nullable disable
            if (method != null)
            {
                var tryParseLambda = Expression.Lambda<FuncEx<string, TItem, bool>>(Expression.Call(method, p1, p2), p1, p2);
                if (tryParseLambda.Compile().Invoke(source, out outValue))
                {
                    v = outValue;
                    ret = true;
                }
            }
            v = outValue;
#nullable restore
            return ret;
        }
    }
}
