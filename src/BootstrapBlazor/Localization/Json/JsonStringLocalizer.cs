// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace BootstrapBlazor.Localization.Json
{
    /// <summary>
    /// JsonStringLocalizer 实现类
    /// </summary>
    internal class JsonStringLocalizer : IStringLocalizer
    {
        private readonly ConcurrentDictionary<string, IEnumerable<KeyValuePair<string, string>>> _resourcesCache = new ConcurrentDictionary<string, IEnumerable<KeyValuePair<string, string>>>();

        private readonly Assembly _assembly;
        private readonly string? _resourceName;
        private readonly string _typeName;
        private readonly ILogger _logger;

        private string _searchedLocation = "";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="resourceName"></param>
        /// <param name="typeName"></param>
        /// <param name="logger"></param>
        public JsonStringLocalizer(Assembly assembly, string? resourceName, string typeName, ILogger logger)
        {
            _assembly = assembly;
            _typeName = typeName;
            _resourceName = resourceName;
            _logger = logger;
        }

        /// <summary>
        /// 通过指定键值获取多语言值信息索引
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public LocalizedString this[string name]
        {
            get
            {
                var value = GetStringSafely(name);

                return new LocalizedString(name, value ?? name, resourceNotFound: value == null, searchedLocation: _searchedLocation);
            }
        }

        /// <summary>
        /// 带格式化参数的通过指定键值获取多语言值信息索引
        /// </summary>
        /// <param name="name"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var format = GetStringSafely(name);
                var value = string.Format(format ?? name, arguments);

                return new LocalizedString(name, value, resourceNotFound: format == null, searchedLocation: _searchedLocation);
            }
        }

        /// <summary>
        /// 获取当前语言的所有资源信息
        /// </summary>
        /// <param name="includeParentCultures"></param>
        /// <returns></returns>
        public virtual IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) =>
            GetAllStrings(includeParentCultures, CultureInfo.CurrentUICulture);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        [Obsolete("This method is obsolete. Use `CurrentCulture` and `CurrentUICulture` instead.")]
        public IStringLocalizer WithCulture(CultureInfo culture) => this;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeParentCultures"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        protected virtual IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures, CultureInfo culture)
        {
            var resourceNames = includeParentCultures
                ? GetAllStringsFromCultureHierarchy(culture)
                : GetAllResourceStrings(culture);

            foreach (var name in resourceNames)
            {
                var value = GetStringSafely(name);
                yield return new LocalizedString(name, value ?? name, resourceNotFound: value == null, searchedLocation: _searchedLocation);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected virtual string? GetStringSafely(string name)
        {
            var culture = CultureInfo.CurrentUICulture;
            string? value = null;

            while (culture != culture.Parent)
            {
                BuildResourcesCache(culture);

                if (_resourcesCache.TryGetValue(culture.Name, out var resources))
                {
                    var resource = resources?.SingleOrDefault(s => s.Key == name);

                    value = resource?.Value ?? null;
                    _logger.LogDebug($"{nameof(JsonStringLocalizer)} searched for '{name}' in '{_searchedLocation}' with culture '{culture}'.");

                    if (value != null)
                    {
                        break;
                    }

                    culture = culture.Parent;
                }
            }

            return value;
        }

        private IEnumerable<string> GetAllStringsFromCultureHierarchy(CultureInfo startingCulture)
        {
            var currentCulture = startingCulture;
            var resourceNames = new HashSet<string>();

            while (currentCulture != currentCulture.Parent)
            {
                var cultureResourceNames = GetAllResourceStrings(currentCulture);

                foreach (var resourceName in cultureResourceNames)
                {
                    resourceNames.Add(resourceName);
                }

                currentCulture = currentCulture.Parent;
            }

            return resourceNames;
        }

        private IEnumerable<string> GetAllResourceStrings(CultureInfo culture)
        {
            BuildResourcesCache(culture);

            var ret = Enumerable.Empty<string>();
            if (_resourcesCache.TryGetValue(culture.Name, out var resources))
            {
                ret = resources.Select(r => r.Key);
            }
            return ret;
        }

        private void BuildResourcesCache(CultureInfo culture)
        {
            _resourcesCache.GetOrAdd(culture.Name, key =>
            {
                var value = Enumerable.Empty<KeyValuePair<string, string>>();

                _searchedLocation = $"{_resourceName}.{key}.json";

                using var res = _assembly.GetManifestResourceStream(_searchedLocation);

                if (res != null)
                {
                    var config = new ConfigurationBuilder()
                        .AddJsonStream(res)
                        .Build();
                    var v = config.GetChildren().FirstOrDefault(c => _typeName.Equals(c.Key, StringComparison.OrdinalIgnoreCase))?.GetChildren().SelectMany(c => new KeyValuePair<string, string>[] { new KeyValuePair<string, string>(c.Key, c.Value) });

                    if (v != null) value = v;
                }
                return value;
            });
        }
    }
}
