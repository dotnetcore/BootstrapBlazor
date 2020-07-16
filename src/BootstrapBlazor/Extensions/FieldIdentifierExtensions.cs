using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Microsoft.AspNetCore.Components.Forms
{
    /// <summary>
    /// FieldIdentifier 扩展操作类
    /// </summary>
    public static class FieldIdentifierExtensions
    {
        /// <summary>
        /// 获取显示名称方法
        /// </summary>
        /// <param name="fieldIdentifier"></param>
        /// <returns></returns>
        public static string GetDisplayName(this FieldIdentifier fieldIdentifier)
        {
            var cacheKey = (Type: fieldIdentifier.Model.GetType(), fieldIdentifier.FieldName);

            if (!DisplayNamesExtensions.TryGetValue(cacheKey, out var dn))
            {
                if (TryGetValidatableProperty(fieldIdentifier, out var propertyInfo))
                {
                    var displayNameAttribute = propertyInfo.GetCustomAttributes<DisplayNameAttribute>();
                    if (displayNameAttribute.Any())
                    {
                        dn = displayNameAttribute.First().DisplayName;

                        // add display name into cache
                        DisplayNamesExtensions.GetOrAdd((fieldIdentifier.Model.GetType(), fieldIdentifier.FieldName), key => dn);
                    }
                }
            }
            return dn ?? cacheKey.FieldName;
        }

        private static readonly ConcurrentDictionary<(Type ModelType, string FieldName), PropertyInfo> _propertyInfoCache = new ConcurrentDictionary<(Type, string), PropertyInfo>();

        private static bool TryGetValidatableProperty(in FieldIdentifier fieldIdentifier, out PropertyInfo propertyInfo)
        {
            var cacheKey = (ModelType: fieldIdentifier.Model.GetType(), fieldIdentifier.FieldName);
            if (!_propertyInfoCache.TryGetValue(cacheKey, out propertyInfo))
            {
                // Validator.TryValidateProperty 只能对 Public 属性生效
                propertyInfo = cacheKey.ModelType.GetProperty(cacheKey.FieldName);

                if (propertyInfo != null) _propertyInfoCache[cacheKey] = propertyInfo;
            }

            return propertyInfo != null;
        }
    }
}
