// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace BootstrapBlazor.Localization.Json
{
    /// <summary>
    /// IStringLocalizer 实现类
    /// </summary>
    internal class StringLocalizer : IStringLocalizer
    {
        private readonly IStringLocalizer _localizer;

        public StringLocalizer(IStringLocalizerFactory factory)
        {
            _localizer = factory.Create(string.Empty, string.Empty);
        }

        public LocalizedString this[string name] => _localizer[name];

        public LocalizedString this[string name, params object[] arguments] => _localizer[name, arguments];

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => _localizer.GetAllStrings(includeParentCultures);
    }
}
