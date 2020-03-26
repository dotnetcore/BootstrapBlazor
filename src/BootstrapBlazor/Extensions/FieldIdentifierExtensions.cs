using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

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
                if (BootstrapBlazor.Components.BootstrapBlazorEditContextDataAnnotationsExtensions.TryGetValidatableProperty(fieldIdentifier, out var propertyInfo))
                {
                    var displayNameAttribute = propertyInfo.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                    if (displayNameAttribute.Length > 0)
                    {
                        dn = ((DisplayNameAttribute)displayNameAttribute[0]).DisplayName;

                        // add display name into cache
                        DisplayNamesExtensions.GetOrAdd((fieldIdentifier.Model.GetType(), fieldIdentifier.FieldName), key => dn);
                    }
                }
            }
            return dn ?? cacheKey.FieldName;
        }
    }
}
