// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.Extensions.Localization;
using System;

namespace BootstrapBlazor.Localization.Json
{
    /// <summary>
    /// An <see cref="T:BootstrapBlazor.Localization.Json.IHtmlLocalizerFactory" /> that creates instances of <see cref="T:BootstrapBlazor.Localization.Json.IHtmlLocalizer" /> using the
    /// registered <see cref="T:BootstrapBlazor.Localization.Json.IStringLocalizerFactory" />.
    /// </summary>
    public class JsonHtmlLocalizerFactory : IHtmlLocalizerFactory
    {
        private readonly IStringLocalizerFactory _stringLocalizerFactory;

        /// <summary>
        /// Creates a new <see cref="T:BootstrapBlazor.Localization.Json.JsonHtmlLocalizerFactory" />.
        /// </summary>
        /// <param name="stringLocalizerFactory"></param>
        public JsonHtmlLocalizerFactory(IStringLocalizerFactory stringLocalizerFactory)
        {
            _stringLocalizerFactory = stringLocalizerFactory;
        }

        /// <summary>
        /// Creates an <see cref="T:BootstrapBlazor.Localization.Json.IHtmlLocalizer" /> using the specified <see cref="T:System.Type" />.
        /// </summary>
        /// <param name="resourceSource"></param>
        /// <returns></returns>
        public IHtmlLocalizer Create(Type resourceSource)
            => new HtmlLocalizer(_stringLocalizerFactory.Create(resourceSource));

        /// <summary>
        /// Creates an <see cref="T:BootstrapBlazor.Localization.Json.IHtmlLocalizer" /> using the specified base name and location.
        /// </summary>
        /// <param name="baseName">The base name of the resource to load strings from.</param>
        /// <param name="location">The location to load resources from.</param>
        /// <returns>The <see cref="T:BootstrapBlazor.Localization.Json.IHtmlLocalizer" />.</returns>
        public IHtmlLocalizer Create(string baseName, string location)
        {
            var index = 0;
            if (baseName.StartsWith(location, StringComparison.OrdinalIgnoreCase))
            {
                index = location.Length;
            }

            if (baseName.Length > index && baseName[index] == '.')
            {
                index += 1;
            }

            var relativeName = baseName.Substring(index);

            return new HtmlLocalizer(_stringLocalizerFactory.Create(baseName, location));
        }
    }
}
