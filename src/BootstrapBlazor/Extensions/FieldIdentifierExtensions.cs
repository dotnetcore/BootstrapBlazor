// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Localization.Json;
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;

namespace Microsoft.AspNetCore.Components.Forms
{
    /// <summary>
    /// FieldIdentifier 扩展操作类
    /// </summary>
    public static class FieldIdentifierExtensions
    {
        private static ConcurrentDictionary<(string CultureInfoName, Type ModelType, string FieldName), string> DisplayNameCache { get; } = new ConcurrentDictionary<(string, Type, string), string>();
        private static ConcurrentDictionary<(Type ModelType, string FieldName), string> PlaceHolderCache { get; } = new ConcurrentDictionary<(Type, string), string>();

        private static ConcurrentDictionary<(Type ModelType, string FieldName), PropertyInfo> PropertyInfoCache { get; } = new ConcurrentDictionary<(Type, string), PropertyInfo>();

        /// <summary>
        /// 获取显示名称方法
        /// </summary>
        /// <param name="fieldIdentifier"></param>
        /// <returns></returns>
        public static string GetDisplayName(this FieldIdentifier fieldIdentifier) => GetDisplayName(fieldIdentifier.Model, fieldIdentifier.FieldName);

        /// <summary>
        /// 获取显示名称方法
        /// </summary>
        /// <param name="model">模型实例</param>
        /// <param name="fieldName">字段名称</param>
        /// <returns></returns>
        public static string GetDisplayName(this object model, string fieldName) => GetDisplayName(model.GetType(), fieldName);

        /// <summary>
        /// 获取显示名称方法
        /// </summary>
        /// <param name="modelType">模型类型</param>
        /// <param name="fieldName">字段名称</param>
        /// <returns></returns>
        public static string GetDisplayName(this Type modelType, string fieldName)
        {
            var cacheKey = (CultureInfoName: CultureInfo.CurrentUICulture.Name, Type: modelType, FieldName: fieldName);
            if (!DisplayNameCache.TryGetValue(cacheKey, out var dn))
            {
                if (TryGetValidatableProperty(cacheKey.Type, cacheKey.FieldName, out var propertyInfo))
                {
                    var colNameAttribute = propertyInfo.GetCustomAttribute<ColumnNameAttribute>();
                    if (colNameAttribute != null)
                    {
                        dn = colNameAttribute.GetName();
                    }
                    if (string.IsNullOrEmpty(dn))
                    {
                        var displayNameAttribute = propertyInfo.GetCustomAttribute<DisplayAttribute>();
                        if (displayNameAttribute != null)
                        {
                            dn = displayNameAttribute.Name;
                        }
                    }
                    if (string.IsNullOrEmpty(dn))
                    {
                        var displayAttribute = propertyInfo.GetCustomAttribute<DisplayNameAttribute>();
                        if (displayAttribute != null)
                        {
                            dn = displayAttribute.DisplayName;
                        }
                    }
                    if (!string.IsNullOrEmpty(dn))
                    {
                        var localizer = JsonStringLocalizerFactory.CreateLocalizer(cacheKey.Type);
                        var stringLocalizer = localizer[dn];
                        if (!stringLocalizer.ResourceNotFound)
                        {
                            dn = stringLocalizer.Value;
                        }

                        // add display name into cache
                        DisplayNameCache.GetOrAdd(cacheKey, key => dn);
                    }
                }
            }
            return dn ?? cacheKey.FieldName;
        }

        /// <summary>
        /// 获取 PlaceHolder 方法
        /// </summary>
        /// <param name="fieldIdentifier"></param>
        /// <returns></returns>
        public static string? GetPlaceHolder(this FieldIdentifier fieldIdentifier) => GetPlaceHolder(fieldIdentifier.Model, fieldIdentifier.FieldName);

        /// <summary>
        /// 获取 PlaceHolder 方法
        /// </summary>
        /// <param name="model">模型实例</param>
        /// <param name="fieldName">字段名称</param>
        /// <returns></returns>
        public static string? GetPlaceHolder(this object model, string fieldName) => GetPlaceHolder(model.GetType(), fieldName);

        /// <summary>
        /// 获取 PlaceHolder 方法
        /// </summary>
        /// <param name="modelType">模型类型</param>
        /// <param name="fieldName">字段名称</param>
        /// <returns></returns>
        public static string? GetPlaceHolder(this Type modelType, string fieldName)
        {
            var cacheKey = (Type: modelType, FieldName: fieldName);
            if (!PlaceHolderCache.TryGetValue(cacheKey, out var dn))
            {
                if (TryGetValidatableProperty(cacheKey.Type, cacheKey.FieldName, out var propertyInfo))
                {
                    var placeHolderAttribute = propertyInfo.GetCustomAttribute<PlaceHolderAttribute>();
                    if (placeHolderAttribute != null)
                    {
                        dn = placeHolderAttribute.Text;
                    }
                    if (!string.IsNullOrEmpty(dn))
                    {
                        // add display name into cache
                        PlaceHolderCache.GetOrAdd(cacheKey, key => dn);
                    }
                }
            }
            return dn;
        }

        private static bool TryGetValidatableProperty(Type modelType, string fieldName, [NotNullWhen(true)] out PropertyInfo? propertyInfo)
        {
            var cacheKey = (ModelType: modelType, FieldName: fieldName);
            if (!PropertyInfoCache.TryGetValue(cacheKey, out propertyInfo))
            {
                // Validator.TryValidateProperty 只能对 Public 属性生效
                propertyInfo = cacheKey.ModelType.GetProperty(cacheKey.FieldName);

                if (propertyInfo != null) PropertyInfoCache[cacheKey] = propertyInfo;
            }
            return propertyInfo != null;
        }
    }
}
