using BootstrapBlazor.Components;
using System.Collections.Generic;
using System.Linq.Expressions;

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

#nullable disable
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
            return ret;
        }

        /// <summary>
        /// 通过属性名称获取其实例值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="t"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static TResult GetPropertyValue<T, TResult>(this T t, string name)
        {
            TResult ret = default;
            if (t != null)
            {
                var type = t.GetType();
                var p = type.GetProperty(name);
                if (p != null)
                {
                    var param_obj = Expression.Parameter(typeof(T));
                    var body = Expression.Property(Expression.Convert(param_obj, t.GetType()), p);
                    var getValue = Expression.Lambda<Func<T, TResult>>(Expression.Convert(body, typeof(TResult)), param_obj).Compile();
                    ret = getValue(t);
                }
            }
            return ret;
        }

        /// <summary>
        /// 根据属性名称设置属性的值
        /// </summary>
        /// <typeparam name="TModel">对象类型</typeparam>
        /// <param name="t">对象</param>
        /// <param name="name">属性名</param>
        /// <param name="value">属性的值</param>
        public static void SetPropertyValue<TModel>(this TModel t, string name, object value)
        {
            var type = t.GetType();
            var p = type.GetProperty(name);
            if (p != null)
            {
                var param_obj = Expression.Parameter(type);
                var param_val = Expression.Parameter(typeof(object));
                var body_val = Expression.Convert(param_val, p.PropertyType);

                //获取设置属性的值的方法
                var mi = p.GetSetMethod(true);
                if (mi != null)
                {
                    var body = Expression.Call(param_obj, mi, body_val);
                    var setValue = Expression.Lambda<Action<TModel, object>>(body, param_obj, param_val).Compile();
                    setValue(t, value);
                }
            }
        }

        /// <summary>
        /// 根据传参 object 类型强制转换为 TResult 泛型
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TResult Convert<TResult>(this object obj)
        {
            var param_obj = Expression.Parameter(typeof(object));
            var body = Expression.Convert(param_obj, typeof(TResult));
            var convert = Expression.Lambda<Func<object, TResult>>(body, param_obj).Compile();
            return convert(obj);
        }
#nullable restore
    }
}
