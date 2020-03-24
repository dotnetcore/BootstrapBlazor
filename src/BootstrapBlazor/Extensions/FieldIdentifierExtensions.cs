using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;

namespace Microsoft.AspNetCore.Components.Forms
{
    /// <summary>
    ///
    /// </summary>
    public static class FieldIdentifierExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="fieldIdentifier"></param>
        /// <returns></returns>
        public static string GetDisplayName(this FieldIdentifier fieldIdentifier)
        {
            var cacheKey = (Type: fieldIdentifier.Model.GetType(), FieldName: fieldIdentifier.FieldName);
            if (!DisplayNamesExtensions.TryGetValue(cacheKey, out var dn))
            {
                if (BootstrapBlazor.Components.EditContextDataAnnotationsExtensions.TryGetValidatableProperty(fieldIdentifier, out var propertyInfo))
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
