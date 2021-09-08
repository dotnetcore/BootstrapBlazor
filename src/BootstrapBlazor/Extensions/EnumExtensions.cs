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
using System.Globalization;
using System.Reflection;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 枚举类型扩展方法
    /// </summary>
    public static class EnumExtensions
    {
        private static ConcurrentDictionary<(string CultureInfoName, Type ModelType, string FieldName), string> DisplayNameCache { get; } = new ConcurrentDictionary<(string, Type, string), string>();

        /// <summary>
        /// 获取 DescriptionAttribute 标签方法
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string ToDescriptionString<TEnum>(this TEnum val) where TEnum : Enum => typeof(TEnum).ToDescriptionString(val.ToString());

        /// <summary>
        /// 通过字段名称获取 DescriptionAttribute 标签值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static string ToDescriptionString(this Type? type, string? fieldName)
        {
            var ret = string.Empty;
            if (type != null && !string.IsNullOrEmpty(fieldName))
            {
                var t = Nullable.GetUnderlyingType(type) ?? type;
                var attributes = t.GetField(fieldName)?.GetCustomAttribute<DescriptionAttribute>(true);
                ret = attributes?.Description ?? fieldName;
            }
            return ret;
        }

        /// <summary>
        /// 通过字段名称获取 DisplayAttribute/DescriptionAttribute 标签值
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static string ToEnumDisplayName<TEnum>(string? fieldName) => ToEnumDisplayName(typeof(TEnum), fieldName);

        /// <summary>
        /// 通过字段名称获取 DisplayAttribute/DescriptionAttribute 标签值
        /// </summary>
        /// <param name="type"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        internal static string ToEnumDisplayName(this Type? type, string? fieldName)
        {
            string? dn = null;
            if (type != null && !string.IsNullOrEmpty(fieldName))
            {
                var t = Nullable.GetUnderlyingType(type) ?? type;

                var cacheKey = (CultureInfoName: CultureInfo.CurrentUICulture.Name, Type: t, FieldName: fieldName);
                if (!DisplayNameCache.TryGetValue(cacheKey, out dn))
                {
                    // search in Localization
                    var localizer = JsonStringLocalizerFactory.CreateLocalizer(t);
                    var stringLocalizer = localizer?[fieldName];
                    if (stringLocalizer != null && !stringLocalizer.ResourceNotFound)
                    {
                        dn = stringLocalizer.Value;
                    }
                    else
                    {
                        var field = t.GetField(fieldName);
                        dn = field?.GetCustomAttribute<DisplayAttribute>(true)?.Name
                            ?? field?.GetCustomAttribute<DescriptionAttribute>(true)?.Description;

                        // search in Localization again
                        if (!string.IsNullOrEmpty(dn))
                        {
                            var resxType = ServiceProviderHelper.ServiceProvider?.GetRequiredService<IOptions<JsonLocalizationOptions>>();
                            if (resxType?.Value.ResourceManagerStringLocalizerType != null)
                            {
                                localizer = JsonStringLocalizerFactory.CreateLocalizer(resxType.Value.ResourceManagerStringLocalizerType);
                                stringLocalizer = localizer?[dn];
                                if (stringLocalizer != null && !stringLocalizer.ResourceNotFound)
                                {
                                    dn = stringLocalizer.Value;
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
            }
            return dn ?? fieldName ?? string.Empty;
        }

        /// <summary>
        /// 获取指定枚举类型的枚举值集合，默认通过 DisplayAttribute DescriptionAttribute 标签显示 DisplayName 支持资源文件 回退机制显示字段名称
        /// </summary>
        /// <param name="type"></param>
        /// <param name="addtionalItem"></param>
        /// <returns></returns>
        public static IEnumerable<SelectedItem> ToSelectList(this Type type, SelectedItem? addtionalItem = null)
        {
            var ret = new List<SelectedItem>();
            if (addtionalItem != null) ret.Add(addtionalItem);

            if (type.IsEnum())
            {
                var t = Nullable.GetUnderlyingType(type) ?? type;
                foreach (var field in Enum.GetNames(t))
                {
                    var desc = t.ToEnumDisplayName(field);
                    if (string.IsNullOrEmpty(desc)) desc = field;
                    ret.Add(new SelectedItem(field, desc));
                }
            }
            return ret;
        }

        /// <summary>
        /// 判断类型是否为枚举类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsEnum(this Type? type)
        {
            var ret = false;
            if (type != null)
            {
                var t = Nullable.GetUnderlyingType(type) ?? type;
                ret = t.IsEnum;
            }
            return ret;
        }
    }
}
