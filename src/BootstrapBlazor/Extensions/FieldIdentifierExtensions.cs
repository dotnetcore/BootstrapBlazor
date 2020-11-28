// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Reflection;

namespace Microsoft.AspNetCore.Components.Forms
{
    /// <summary>
    /// FieldIdentifier 扩展操作类
    /// </summary>
    public static class FieldIdentifierExtensions
    {
        private static ConcurrentDictionary<(Type ModelType, string FieldName), string> DisplayNameCache { get; } = new ConcurrentDictionary<(Type, string), string>();

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
            var cacheKey = (Type: modelType, FieldName: fieldName);
            if (!DisplayNameCache.TryGetValue(cacheKey, out var dn))
            {
                if (TryGetValidatableProperty(cacheKey.Type, cacheKey.FieldName, out var propertyInfo))
                {
                    var displayNameAttribute = propertyInfo!.GetCustomAttribute<DisplayNameAttribute>();
                    if (displayNameAttribute != null)
                    {
                        dn = displayNameAttribute.DisplayName;

                        // add display name into cache
                        DisplayNameCache.GetOrAdd(cacheKey, key => dn);
                    }
                }
            }
            return dn ?? cacheKey.FieldName;
        }

        private static bool TryGetValidatableProperty(Type modelType, string fieldName, out PropertyInfo? propertyInfo)
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
