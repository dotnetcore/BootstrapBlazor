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
