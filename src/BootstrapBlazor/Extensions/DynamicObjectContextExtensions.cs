// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// DynamicObjectContext 扩展方法辅助类
    /// </summary>
    public static class DynamicObjectContextExtensions
    {
        /// <summary>
        /// 增加 RequiredAttribute 扩展方法
        /// </summary>
        /// <param name="context"></param>
        /// <param name="columnName"></param>
        /// <param name="errorMessage"></param>
        /// <param name="allowEmptyStrings"></param>
        public static void AddRequiredAttribute(this DynamicObjectContext context, string columnName, string? errorMessage = null, bool allowEmptyStrings = false)
        {
            var type = typeof(RequiredAttribute);
            var propertyInfos = new List<PropertyInfo>();
            var propertyValues = new List<object>();
            if (!string.IsNullOrEmpty(errorMessage))
            {
                propertyInfos.Add(type.GetProperty(nameof(RequiredAttribute.ErrorMessage))!);
                propertyValues.Add(errorMessage);
            }
            if (allowEmptyStrings)
            {
                propertyInfos.Add(type.GetProperty(nameof(RequiredAttribute.AllowEmptyStrings))!);
                propertyValues.Add(true);
            }
            context.AddAttribute(columnName, type, Type.EmptyTypes, Array.Empty<object>(), propertyInfos.ToArray(), propertyValues.ToArray());
        }

        /// <summary>
        /// 增加 AutoGenerateColumnAttribute 扩展方法
        /// </summary>
        /// <param name="context"></param>
        /// <param name="columnName"></param>
        /// <param name="parameters"></param>
        public static void AddAutoGenerateColumnAttribute(this DynamicObjectContext context, string columnName, IEnumerable<KeyValuePair<string, object?>> parameters)
        {
            var type = typeof(AutoGenerateColumnAttribute);
            var propertyInfos = new List<PropertyInfo>();
            var propertyValues = new List<object?>();
            foreach (var kv in parameters)
            {
                var pInfo = type.GetProperty(kv.Key);
                if (pInfo != null)
                {
                    propertyInfos.Add(pInfo);
                    propertyValues.Add(kv.Value);
                }
            }
            context.AddAttribute(columnName, type, Type.EmptyTypes, Array.Empty<object>(), propertyInfos.ToArray(), propertyValues.ToArray());
        }
    }
}
