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
        private static ConcurrentDictionary<(Type ModelType, string FieldName), string> DisplayNameCache { get; } = new ConcurrentDictionary<(Type, string), string>();

        private static ConcurrentDictionary<(Type ModelType, string FieldName), PropertyInfo> PropertyInfoCache { get; } = new ConcurrentDictionary<(Type, string), PropertyInfo>();

        /// <summary>
        /// 获取显示名称方法
        /// </summary>
        /// <param name="fieldIdentifier"></param>
        /// <returns></returns>
        public static string GetDisplayName(this FieldIdentifier fieldIdentifier)
        {
            var cacheKey = (Type: fieldIdentifier.Model.GetType(), fieldIdentifier.FieldName);

            if (!DisplayNameCache.TryGetValue(cacheKey, out var dn))
            {
                if (TryGetValidatableProperty(fieldIdentifier, out var propertyInfo))
                {
                    var displayNameAttribute = propertyInfo.GetCustomAttributes<DisplayNameAttribute>();
                    if (displayNameAttribute.Any())
                    {
                        dn = displayNameAttribute.First().DisplayName;

                        // add display name into cache
                        DisplayNameCache.GetOrAdd((fieldIdentifier.Model.GetType(), fieldIdentifier.FieldName), key => dn);
                    }
                }
            }
            return dn ?? cacheKey.FieldName;
        }

        private static bool TryGetValidatableProperty(in FieldIdentifier fieldIdentifier, out PropertyInfo propertyInfo)
        {
            var cacheKey = (ModelType: fieldIdentifier.Model.GetType(), fieldIdentifier.FieldName);
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
