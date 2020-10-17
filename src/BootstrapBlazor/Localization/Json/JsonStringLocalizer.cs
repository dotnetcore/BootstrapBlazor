using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BootstrapBlazor.Localization.Json
{
    /// <summary>
    /// JsonStringLocalizer 实现类
    /// </summary>
    internal class JsonStringLocalizer : IStringLocalizer
    {
        private readonly ConcurrentDictionary<string, IEnumerable<KeyValuePair<string, string>>> _resourcesCache = new ConcurrentDictionary<string, IEnumerable<KeyValuePair<string, string>>>();

        private readonly string _resourcesPath;
        private readonly string? _resourceName;
        private readonly ILogger _logger;

        private string _searchedLocation = "";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="resourcesPath"></param>
        /// <param name="resourceName"></param>
        /// <param name="logger"></param>
        public JsonStringLocalizer(string resourcesPath, string? resourceName, ILogger logger)
        {
            _resourcesPath = resourcesPath;
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
                BuildResourcesCache(culture.Name);

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
            BuildResourcesCache(culture.Name);

            var ret = Enumerable.Empty<string>();
            if (_resourcesCache.TryGetValue(culture.Name, out var resources))
            {
                ret = resources.Select(r => r.Key);
            }
            return ret;
        }

        private void BuildResourcesCache(string culture)
        {
            _resourcesCache.GetOrAdd(culture, _ =>
            {
                var resourceFile = string.IsNullOrEmpty(_resourceName)
                    ? $"{culture}.json"
                    : $"{_resourceName}.{culture}.json";

                _searchedLocation = Path.Combine(_resourcesPath, resourceFile);

                if (!File.Exists(_searchedLocation))
                {
                    if (resourceFile.Any(r => r == '.'))
                    {
                        var resourceFileWithoutExtension = Path.GetFileNameWithoutExtension(resourceFile);
                        var resourceFileWithoutCulture = resourceFileWithoutExtension.Substring(0, resourceFileWithoutExtension.LastIndexOf('.'));
                        resourceFile = $"{resourceFileWithoutCulture.Replace('.', Path.DirectorySeparatorChar)}.{culture}.json";
                        _searchedLocation = Path.Combine(_resourcesPath, resourceFile);
                    }
                }

                var value = Enumerable.Empty<KeyValuePair<string, string>>();

                if (File.Exists(_searchedLocation))
                {
                    var builder = new ConfigurationBuilder()
                    .SetBasePath(_resourcesPath)
                    .AddJsonFile(resourceFile, optional: false, reloadOnChange: true);

                    var config = builder.Build();
                    value = config.AsEnumerable();
                }

                return value;
            });
        }
    }
}
