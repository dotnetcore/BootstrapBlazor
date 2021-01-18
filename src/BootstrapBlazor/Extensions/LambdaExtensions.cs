// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Linq
{
    /// <summary>
    /// Lambda 表达式扩展类
    /// </summary>
    public static class LambdaExtensions
    {
        /// <summary>
        /// 通过base.Visit(node)返回的Expression统一node变量
        /// </summary>
        private class ComboExpressionVisitor : ExpressionVisitor
        {
            private ParameterExpression exp_p { get; set; }

            /// <summary>
            /// 构造
            /// </summary>
            /// <param name="parameter"></param>
            public ComboExpressionVisitor(ParameterExpression parameter)
            {
                exp_p = parameter;
            }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="p"></param>
            /// <returns></returns>
            protected override Expression VisitParameter(ParameterExpression p) => exp_p;
        }

        /// <summary>
        /// 指定 FilterKeyValueAction 集合获取 Lambda 表达式
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static Expression<Func<TItem, bool>> GetFilterLambda<TItem>(this IEnumerable<FilterKeyValueAction> filters)
        {
            Expression<Func<TItem, bool>>? ret = null;
            var exp_p = Expression.Parameter(typeof(TItem));
            var visitor = new ComboExpressionVisitor(exp_p);

            foreach (var filter in filters)
            {
                var exp = filter.GetFilterLambda<TItem>();
                if (ret == null)
                {
                    ret = exp;
                    continue;
                }

                var left = visitor.Visit(ret.Body);
                var right = visitor.Visit(exp.Body);

                ret = filter.FilterLogic switch
                {
                    FilterLogic.And => Expression.Lambda<Func<TItem, bool>>(Expression.AndAlso(left, right), exp_p),
                    _ => Expression.Lambda<Func<TItem, bool>>(Expression.OrElse(left, right), exp_p),
                };
            }
            return ret ?? (r => true);
        }

        /// <summary>
        /// 表达式取 and 逻辑操作方法
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="expressions"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        private static Expression<Func<TItem, bool>> ExpressionAndLambda<TItem>(this IEnumerable<Expression<Func<TItem, bool>>> expressions, FilterLogic logic = FilterLogic.And)
        {
            Expression<Func<TItem, bool>>? ret = null;
            var exp_p = Expression.Parameter(typeof(TItem));
            var visitor = new ComboExpressionVisitor(exp_p);

            foreach (var exp in expressions)
            {
                if (ret == null)
                {
                    ret = exp;
                    continue;
                }

                var left = visitor.Visit(ret.Body);
                var right = visitor.Visit(exp.Body);
                ret = logic == FilterLogic.And
                    ? Expression.Lambda<Func<TItem, bool>>(Expression.AndAlso(left, right), exp_p)
                    : Expression.Lambda<Func<TItem, bool>>(Expression.OrElse(left, right), exp_p);
            }
            return ret ?? (r => true);
        }

        /// <summary>
        /// 指定 IFilter 集合获取委托
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="filters"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        public static Func<TItem, bool> GetFilterFunc<TItem>(this IEnumerable<IFilterAction> filters, FilterLogic logic = FilterLogic.And)
        {
            return filters.GetFilterLambda<TItem>(logic).Compile();
        }

        /// <summary>
        /// 指定 IFilter 集合获取 Lambda 表达式
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="filters"></param>
        /// <param name="logic"></param>
        /// <returns></returns>
        public static Expression<Func<TItem, bool>> GetFilterLambda<TItem>(this IEnumerable<IFilterAction> filters, FilterLogic logic = FilterLogic.And)
        {
            var exps = filters.Select(f => f.GetFilterConditions().GetFilterLambda<TItem>());
            return exps.ExpressionAndLambda(logic);
        }

        /// <summary>
        /// 指定 FilterKeyValueAction 获取 Lambda 表达式
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static Expression<Func<TItem, bool>> GetFilterLambda<TItem>(this FilterKeyValueAction filter)
        {
            Expression<Func<TItem, bool>> ret = t => true;
            if (!string.IsNullOrEmpty(filter.FieldKey) && filter.FieldValue != null)
            {
                var prop = typeof(TItem).GetProperty(filter.FieldKey);
                if (prop != null)
                {
                    var p = Expression.Parameter(typeof(TItem));
                    var fieldExpression = Expression.Property(p, prop);

                    Expression eq = fieldExpression;

                    // 可为空类型转化为具体类型
                    if (prop.PropertyType.IsGenericType &&
                        prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        eq = Expression.Convert(fieldExpression, prop.PropertyType.GenericTypeArguments[0]);
                    }
                    eq = filter.GetExpression(eq);
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
        public static Func<TItem, bool> GetFilterFunc<TItem>(this FilterKeyValueAction filter) => filter.GetFilterLambda<TItem>().Compile();

        private static Expression GetExpression(this FilterKeyValueAction filter, Expression left)
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
                FilterAction.CustomPredicate => filter.FieldValue switch
                {
                    LambdaExpression t => Expression.Invoke(t, left),
                    Delegate _ => Expression.Invoke(right, left),
                    _ => throw new ArgumentException(nameof(FilterKeyValueAction.FieldValue))
                },
                _ => Expression.Empty()
            };
        }

        private static Expression Contains(this Expression left, Expression right)
        {
            // https://gitee.com/LongbowEnterprise/BootstrapBlazor/issues/I2DIR4
            // 兼容 EFCore 与普通逻辑 EFCore 内自动处理空问题
            MethodInfo method = typeof(string).GetMethod("Contains", new Type[1] { typeof(string) })!;
            return Expression.AndAlso(Expression.NotEqual(left, Expression.Constant(null)), Expression.Call(left, method, right));
        }

        #region Sort
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static Expression<Func<IEnumerable<TItem>, string, SortOrder, IEnumerable<TItem>>> GetSortLambda<TItem>(this IEnumerable<TItem> items)
        {
            var exp_p1 = Expression.Parameter(typeof(IEnumerable<TItem>));
            var exp_p2 = Expression.Parameter(typeof(string));
            var exp_p3 = Expression.Parameter(typeof(SortOrder));

            var mi = typeof(LambdaExtensions).GetMethod("Sort")!.MakeGenericMethod(typeof(TItem));
            var body = Expression.Call(mi, exp_p1, exp_p2, exp_p3);
            return Expression.Lambda<Func<IEnumerable<TItem>, string, SortOrder, IEnumerable<TItem>>>(body, exp_p1, exp_p2, exp_p3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="items"></param>
        /// <param name="sortName"></param>
        /// <param name="sortOrder"></param>
        /// <returns></returns>
        public static IEnumerable<TItem> Sort<TItem>(this IEnumerable<TItem> items, string sortName, SortOrder sortOrder)
        {
            return sortOrder == SortOrder.Unset ? items : _OrderBy(items, sortName, sortOrder);
        }

        private static IEnumerable<TItem> _OrderBy<TItem>(IEnumerable<TItem> query, string propertyName, SortOrder sortOrder)
        {
            var methodName = sortOrder == SortOrder.Desc ? "OrderByDescendingInternal" : "OrderByInternal";

            var pi = typeof(TItem).GetProperty(propertyName);
            var mi = typeof(LambdaExtensions).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static)!
                                       .MakeGenericMethod(typeof(TItem), pi!.PropertyType);

            return mi?.Invoke(null, new object[] { query.AsQueryable(), pi }) as IOrderedQueryable<TItem> ?? query;
        }

        private static IOrderedQueryable<TItem> OrderByInternal<TItem, TKey>(IQueryable<TItem> query, System.Reflection.PropertyInfo memberProperty) => query.OrderBy(GetPropertyLambda<TItem, TKey>(memberProperty));

        private static IOrderedQueryable<TItem> OrderByDescendingInternal<TItem, TKey>(IQueryable<TItem> query, System.Reflection.PropertyInfo memberProperty) => query.OrderByDescending(GetPropertyLambda<TItem, TKey>(memberProperty));

        private static Expression<Func<TItem, TKey>> GetPropertyLambda<TItem, TKey>(PropertyInfo pi)
        {
            if (pi.PropertyType != typeof(TKey)) throw new InvalidOperationException();

            var exp_p1 = Expression.Parameter(typeof(TItem));
            return Expression.Lambda<Func<TItem, TKey>>(Expression.Property(exp_p1, pi), exp_p1);
        }
        #endregion

        #region ToString
        private static readonly ConcurrentDictionary<Type, Func<object, string, IFormatProvider?, string>> FormatLambdaCache = new ConcurrentDictionary<Type, Func<object, string, IFormatProvider?, string>>();

        /// <summary>
        /// 任意类型格式化方法
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static string Format(this object source, string format, IFormatProvider? provider = null)
        {
            var invoker = FormatLambdaCache.GetOrAdd(source.GetType(), key => source.GetFormatLambda().Compile());
            return invoker(source, format, provider);
        }

        /// <summary>
        /// 获取 Format 方法的 Lambda 表达式
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Expression<Func<object, string, IFormatProvider?, string>> GetFormatLambda(this object source)
        {
            var type = source.GetType();
            var exp_p1 = Expression.Parameter(typeof(object));
            var exp_p2 = Expression.Parameter(typeof(string));
            var exp_p3 = Expression.Parameter(typeof(IFormatProvider));
            Expression? body;
            if (type.IsSubclassOf(typeof(IFormattable)))
            {
                // 通过 IFormattable 接口格式化
                var mi = type.GetMethod("ToString", new Type[] { typeof(string), typeof(IFormatProvider) });
                body = Expression.Call(Expression.Convert(exp_p1, type), mi!, exp_p2, exp_p3);
            }
            else
            {
                // 通过 ToString(string format) 方法格式化
                var mi = type.GetMethod("ToString", new Type[] { typeof(string) });
                body = Expression.Call(Expression.Convert(exp_p1, type), mi!, exp_p2);
            }
            return Expression.Lambda<Func<object, string, IFormatProvider?, string>>(body, exp_p1, exp_p2, exp_p3);
        }

        private static readonly ConcurrentDictionary<Type, Func<object, IFormatProvider?, string>> FormatProviderLambdaCache = new ConcurrentDictionary<Type, Func<object, IFormatProvider?, string>>();

        /// <summary>
        /// 任意类型格式化方法
        /// </summary>
        /// <param name="source"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static string Format(this object source, IFormatProvider provider)
        {
            var invoker = FormatProviderLambdaCache.GetOrAdd(source.GetType(), key => source.GetFormatProviderLambda().Compile());
            return invoker(source, provider);
        }

        /// <summary>
        /// 获取 Format 方法的 Lambda 表达式
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Expression<Func<object, IFormatProvider?, string>> GetFormatProviderLambda(this object source)
        {
            var type = source.GetType();
            var exp_p1 = Expression.Parameter(typeof(object));
            var exp_p2 = Expression.Parameter(typeof(IFormatProvider));
            Expression? body;

            var mi = type.GetMethod("ToString", new Type[] { typeof(IFormatProvider) });
            if (mi != null)
            {
                // 通过 ToString(IFormatProvider? provider) 接口格式化
                body = Expression.Call(Expression.Convert(exp_p1, type), mi, exp_p2);
            }
            else
            {
                // 通过 ToString() 方法格式化
                mi = type.GetMethod("ToString", new Type[] { typeof(string) });
                body = Expression.Call(Expression.Convert(exp_p1, type), mi!);
            }
            return Expression.Lambda<Func<object, IFormatProvider?, string>>(body, exp_p1, exp_p2);
        }
        #endregion

        /// <summary>
        /// 大于等于 Lambda 表达式
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Expression<Func<TValue, object, bool>> GetGreaterThanOrEqualLambda<TValue>(this TValue v)
        {
            if (v == null) throw new ArgumentNullException(nameof(v));

            var left = Expression.Parameter(v.GetType());
            var right = Expression.Parameter(typeof(object));
            return Expression.Lambda<Func<TValue, object, bool>>(Expression.GreaterThanOrEqual(left, Expression.Convert(right, v.GetType())), left, right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static bool GreaterThanOrEqual<TValue>(this TValue v1, object v2)
        {
            var invoker = v1.GetGreaterThanOrEqualLambda().Compile();
            return invoker(v1, v2);
        }

        /// <summary>
        /// 通过属性名称获取其实例值
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="t"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TResult GetPropertyValue<TModel, TResult>(this TModel t, string name)
        {
            var invoker = t.GetPropertyValueLambda<TModel, TResult>(name).Compile();
            return invoker(t);
        }

        /// <summary>
        /// 获取属性方法 Lambda 表达式
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="item"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Expression<Func<TModel, TResult>> GetPropertyValueLambda<TModel, TResult>(this TModel item, string name)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            var p = item.GetType().GetProperty(name);
            if (p == null) throw new InvalidOperationException($"类型 {item.GetType().Name} 未找到 {name} 属性，无法获取其值");

            var param_p1 = Expression.Parameter(typeof(TModel));
            var body = Expression.Property(Expression.Convert(param_p1, item.GetType()), p);
            return Expression.Lambda<Func<TModel, TResult>>(Expression.Convert(body, typeof(TResult)), param_p1);
        }

        /// <summary>
        /// 根据属性名称设置属性的值
        /// </summary>
        /// <typeparam name="TItem">对象类型</typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="t">对象</param>
        /// <param name="name">属性名</param>
        /// <param name="value">属性的值</param>
        public static void SetPropertyValue<TItem, TValue>(this TItem t, string name, TValue value)
        {
            var invoker = t.SetPropertyValueLambda<TItem, TValue>(name).Compile();
            invoker(t, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="t"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Expression<Action<TItem, TValue>> SetPropertyValueLambda<TItem, TValue>(this TItem t, string name)
        {
            if (t == null) throw new ArgumentNullException(nameof(t));

            var p = t.GetType().GetProperty(name);
            if (p == null) throw new InvalidOperationException($"类型 {typeof(TItem).Name} 未找到 {name} 属性，无法设置其值");

            var param_p1 = Expression.Parameter(typeof(TItem));
            var param_p2 = Expression.Parameter(typeof(TValue));

            //获取设置属性的值的方法
            var mi = p.GetSetMethod(true);
            var body = Expression.Call(Expression.Convert(param_p1, t.GetType()), mi!, Expression.Convert(param_p2, p.PropertyType));
            return Expression.Lambda<Action<TItem, TValue>>(body, param_p1, param_p2);
        }

        #region TryParse
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="outValue"></param>
        /// <returns></returns>
        internal delegate TResult FuncEx<TIn, TOut, TResult>(TIn source, out TOut outValue);

        /// <summary>
        /// 尝试使用 TryParse 进行数据转换
        /// </summary>
        /// <returns></returns>
        internal static Expression<FuncEx<string, TValue, bool>> TryParse<TValue>()
        {
            var t = typeof(TValue);
            var p1 = Expression.Parameter(typeof(string));
            var p2 = Expression.Parameter(t.MakeByRefType());
            var method = t.GetMethod("TryParse", new Type[] { typeof(string), t.MakeByRefType() });
            var body = method != null ? Expression.Call(method, p1, p2) : Expression.Call(typeof(LambdaExtensions).GetMethod("TryParseEmpty", BindingFlags.NonPublic | BindingFlags.Static)!.MakeGenericMethod(typeof(TValue)), p1, p2);
            return Expression.Lambda<FuncEx<string, TValue, bool>>(body, p1, p2);
        }

        private static bool TryParseEmpty<TValue>(string source, out TValue val)
        {
            // TODO: 代码未完善
            val = default!;
            return false;
        }
        #endregion
    }
}
