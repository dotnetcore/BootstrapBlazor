// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public static class Utility
    {
        private static ConcurrentDictionary<(string CultureInfoName, Type ModelType, string FieldName), string> DisplayNameCache { get; } = new ConcurrentDictionary<(string, Type, string), string>();
        private static ConcurrentDictionary<(Type ModelType, string FieldName), PropertyInfo> PropertyInfoCache { get; } = new ConcurrentDictionary<(Type, string), PropertyInfo>();
        private static ConcurrentDictionary<(Type ModelType, string FieldName), string> PlaceHolderCache { get; } = new ConcurrentDictionary<(Type, string), string>();

        /// <summary>
        /// 获取资源文件中 DisplayAttribute/DisplayNameAttribute 标签名称方法
        /// </summary>
        /// <param name="model">模型实例</param>
        /// <param name="fieldName">字段名称</param>
        /// <returns></returns>
        public static string GetDisplayName(object model, string fieldName) => GetDisplayName(model.GetType(), fieldName);

        /// <summary>
        /// 获取显示名称方法
        /// </summary>
        /// <param name="modelType">模型类型</param>
        /// <param name="fieldName">字段名称</param>
        /// <returns></returns>
        public static string GetDisplayName(Type modelType, string fieldName)
        {
            var cacheKey = (CultureInfoName: CultureInfo.CurrentUICulture.Name, Type: modelType, FieldName: fieldName);
            if (!DisplayNameCache.TryGetValue(cacheKey, out var dn))
            {
                // 显示名称为空时通过资源文件查找 FieldName 项
                var localizer = JsonStringLocalizerFactory.CreateLocalizer(cacheKey.Type);
                var stringLocalizer = localizer?[fieldName];
                if (stringLocalizer != null && !stringLocalizer.ResourceNotFound)
                {
                    dn = stringLocalizer.Value;
                }
                else if (TryGetProperty(cacheKey.Type, cacheKey.FieldName, out var propertyInfo))
                {
                    // 回退查找 Display 标签
                    dn = propertyInfo.GetCustomAttribute<DisplayAttribute>()?.Name
                        ?? propertyInfo.GetCustomAttribute<DisplayNameAttribute>()?.DisplayName;

                    // 回退查找资源文件通过 dn 查找匹配项 用于支持 Validation
                    if (!string.IsNullOrEmpty(dn))
                    {
                        var resxType = ServiceProviderHelper.ServiceProvider.GetRequiredService<IOptions<JsonLocalizationOptions>>();
                        if (resxType?.Value.ResourceManagerStringLocalizerType != null)
                        {
                            localizer = JsonStringLocalizerFactory.CreateLocalizer(resxType.Value.ResourceManagerStringLocalizerType);
                            if (localizer != null)
                            {
                                stringLocalizer = localizer[dn];
                                if (!stringLocalizer.ResourceNotFound)
                                {
                                    dn = stringLocalizer.Value;
                                }
                            }
                        }
                    }
                }

                // add display name into cache
                if (!string.IsNullOrEmpty(dn))
                {
                    DisplayNameCache.GetOrAdd(cacheKey, key => dn);
                }
            }
            return dn ?? cacheKey.FieldName;
        }

        /// <summary>
        /// 获取 PlaceHolder 方法
        /// </summary>
        /// <param name="model">模型实例</param>
        /// <param name="fieldName">字段名称</param>
        /// <returns></returns>
        public static string? GetPlaceHolder(object model, string fieldName) => GetPlaceHolder(model.GetType(), fieldName);

        /// <summary>
        /// 获取 PlaceHolder 方法
        /// </summary>
        /// <param name="modelType">模型类型</param>
        /// <param name="fieldName">字段名称</param>
        /// <returns></returns>
        public static string? GetPlaceHolder(Type modelType, string fieldName)
        {
            var cacheKey = (Type: modelType, FieldName: fieldName);
            if (!PlaceHolderCache.TryGetValue(cacheKey, out var placeHolder))
            {
                // 通过资源文件查找 FieldName 项
                var localizer = JsonStringLocalizerFactory.CreateLocalizer(cacheKey.Type);
                var stringLocalizer = localizer?[$"{fieldName}.PlaceHolder"];
                if (stringLocalizer != null && !stringLocalizer.ResourceNotFound)
                {
                    placeHolder = stringLocalizer.Value;
                }
                else if (Utility.TryGetProperty(cacheKey.Type, cacheKey.FieldName, out var propertyInfo))
                {
                    var placeHolderAttribute = propertyInfo.GetCustomAttribute<PlaceHolderAttribute>();
                    if (placeHolderAttribute != null)
                    {
                        placeHolder = placeHolderAttribute.Text;
                    }
                    if (!string.IsNullOrEmpty(placeHolder))
                    {
                        // add display name into cache
                        PlaceHolderCache.GetOrAdd(cacheKey, key => placeHolder);
                    }
                }
            }
            return placeHolder;
        }

        private static bool TryGetProperty(Type modelType, string fieldName, [NotNullWhen(true)] out PropertyInfo? propertyInfo)
        {
            var cacheKey = (ModelType: modelType, FieldName: fieldName);
            if (!PropertyInfoCache.TryGetValue(cacheKey, out propertyInfo))
            {
                // Validator.TryValidateProperty 只能对 Public 属性生效
                propertyInfo = cacheKey.ModelType.GetProperty(cacheKey.FieldName);

                if (propertyInfo != null)
                {
                    PropertyInfoCache[cacheKey] = propertyInfo;
                }
            }
            return propertyInfo != null;
        }

        /// <summary>
        /// 重置对象属性值到默认值方法
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        public static void Reset<TModel>(TModel source) where TModel : class, new()
        {
            var v = new TModel();
            foreach (var pi in source.GetType().GetProperties())
            {
                pi.SetValue(source, v.GetType().GetProperty(pi.Name)!.GetValue(v));
            }
        }

        /// <summary>
        /// 泛型 Clone 方法
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static TModel Clone<TModel>(TModel item)
        {
            var ret = item;
            if (item != null)
            {
                var type = item.GetType();
                if (typeof(ICloneable).IsAssignableFrom(type))
                {
                    var clv = type.GetMethod("Clone")?.Invoke(item, null);
                    if (clv != null)
                    {
                        ret = (TModel)clv;
                        return ret;
                    }
                }
                if (type.IsClass)
                {
                    ret = Activator.CreateInstance<TModel>();
                    var valType = ret?.GetType();
                    if (valType != null)
                    {
                        // 20200608 tian_teng@outlook.com 支持字段和只读属性
                        type.GetFields().ToList().ForEach(f =>
                        {
                            var v = f.GetValue(item);
                            valType.GetField(f.Name)?.SetValue(ret, v);
                        });
                        type.GetProperties().ToList().ForEach(p =>
                        {
                            if (p.CanWrite)
                            {
                                var v = p.GetValue(item);
                                valType.GetProperty(p.Name)?.SetValue(ret, v);
                            }
                        });
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// 泛型 Copy 方法
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static void Copy<TModel>(TModel source, TModel destination) where TModel : class
        {
            if (source != null && destination != null)
            {
                var type = source.GetType();
                var valType = destination.GetType();
                if (valType != null)
                {
                    type.GetFields().ToList().ForEach(f =>
                    {
                        var v = f.GetValue(source);
                        valType.GetField(f.Name)?.SetValue(destination, v);
                    });
                    type.GetProperties().ToList().ForEach(p =>
                    {
                        if (p.CanWrite)
                        {
                            var v = p.GetValue(source);
                            valType.GetProperty(p.Name)?.SetValue(destination, v);
                        }
                    });
                }
            }
        }

        /// <summary>
        /// 通过指定 Model 获得 IEditorItem 集合方法
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IEnumerable<ITableColumn> GenerateColumns<TModel>(Func<ITableColumn, bool>? predicate = null)
        {
            if (predicate == null)
            {
                predicate = p => true;
            }

            return InternalTableColumn.GetProperties<TModel>().Where(predicate);
        }

        #region Format
        private static readonly ConcurrentDictionary<Type, Func<object, string, IFormatProvider?, string>> FormatLambdaCache = new();

        /// <summary>
        /// 任意类型格式化方法
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static string Format(object? source, string format, IFormatProvider? provider = null)
        {
            var ret = string.Empty;
            if (source != null)
            {
                var invoker = FormatLambdaCache.GetOrAdd(source.GetType(), key => GetFormatLambda(source).Compile());
                ret = invoker(source, format, provider);
            }
            return ret;
        }

        /// <summary>
        /// 获取 Format 方法的 Lambda 表达式
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private static Expression<Func<object, string, IFormatProvider?, string>> GetFormatLambda(object source)
        {
            var type = source.GetType();
            var exp_p1 = Expression.Parameter(typeof(object));
            var exp_p2 = Expression.Parameter(typeof(string));
            var exp_p3 = Expression.Parameter(typeof(IFormatProvider));
            Expression? body = null;
            if (type.IsSubclassOf(typeof(IFormattable)))
            {
                // 通过 IFormattable 接口格式化
                var mi = type.GetMethod("ToString", new Type[] { typeof(string), typeof(IFormatProvider) });
                if (mi != null)
                {
                    body = Expression.Call(Expression.Convert(exp_p1, type), mi, exp_p2, exp_p3);
                }
            }
            else
            {
                // 通过 ToString(string format) 方法格式化
                var mi = type.GetMethod("ToString", new Type[] { typeof(string) });
                if (mi != null)
                {
                    body = Expression.Call(Expression.Convert(exp_p1, type), mi, exp_p2);
                }
            }
            return body == null
                ? (s, f, provider) => s.ToString() ?? ""
                : Expression.Lambda<Func<object, string, IFormatProvider?, string>>(body, exp_p1, exp_p2, exp_p3);
        }

        private static readonly ConcurrentDictionary<Type, Func<object, IFormatProvider?, string>> FormatProviderLambdaCache = new();

        /// <summary>
        /// 任意类型格式化方法
        /// </summary>
        /// <param name="source"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static string Format(object? source, IFormatProvider provider)
        {
            var ret = string.Empty;
            if (source != null)
            {
                var invoker = FormatProviderLambdaCache.GetOrAdd(source.GetType(), key => GetFormatProviderLambda(source).Compile());
                ret = invoker(source, provider);
            }
            return ret;
        }

        /// <summary>
        /// 获取 Format 方法的 Lambda 表达式
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private static Expression<Func<object, IFormatProvider?, string>> GetFormatProviderLambda(object source)
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
    }
}
