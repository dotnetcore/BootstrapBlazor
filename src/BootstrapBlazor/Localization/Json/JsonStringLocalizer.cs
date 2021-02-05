// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace BootstrapBlazor.Localization.Json
{
    /// <summary>
    /// JsonStringLocalizer 实现类
    /// </summary>
    internal class JsonStringLocalizer : IStringLocalizer
    {
        private readonly ConcurrentDictionary<string, IEnumerable<KeyValuePair<string, string>>> _resourcesCache = new();

        private readonly Assembly _assembly;
        private readonly string _typeName;
        private readonly ILogger _logger;
        private readonly JsonLocalizationOptions _options;

        private string _searchedLocation = "";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="typeName"></param>
        /// <param name="logger"></param>
        /// <param name="options"></param>
        public JsonStringLocalizer(Assembly assembly, string typeName, ILogger logger, JsonLocalizationOptions options)
        {
            _assembly = assembly;
            _typeName = typeName;
            _logger = logger;
            _options = options;
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

                return new LocalizedString(name, value, resourceNotFound: value == null, searchedLocation: _searchedLocation);
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
                var value = string.Format(format, arguments);

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
                yield return new LocalizedString(name, value, resourceNotFound: value == null, searchedLocation: _searchedLocation);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected virtual string GetStringSafely(string name)
        {
            string? value = null;
            if (_options.StringLocalizer != null)
            {
                var local = _options.StringLocalizer[name];
                if (!local.ResourceNotFound)
                {
                    value = local.Value;
                }
            }

            if (string.IsNullOrEmpty(value))
            {
                value = GetStringByCulture(CultureInfo.CurrentUICulture, name);
                if (string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(_options.FallbackCulture))
                {
                    value = GetStringByCulture(new CultureInfo(_options.FallbackCulture), name);
                }
            }
            return value ?? name;
        }

        private string? GetStringByCulture(CultureInfo culture, string name)
        {
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
                var builder = new ConfigurationBuilder();

                _searchedLocation = $"{_assembly.GetName().Name}.{_options.ResourcesPath}.{key}.json";
                using var res = _assembly.GetManifestResourceStream(_searchedLocation);

                if (res != null)
                {
                    builder.AddJsonStream(res);
                }

                var langHandler = new List<Stream>();
                if (_options.AdditionalAssemblies != null)
                {
                    foreach (var assembly in _options.AdditionalAssemblies)
                    {
                        var fileName = $"{assembly.GetName().Name}.{_options.ResourcesPath}.{key}.json";
                        var lang = assembly.GetManifestResourceStream(fileName);
                        if (lang != null)
                        {
                            langHandler.Add(lang);
                            builder.AddJsonStream(lang);
                        }
                    }
                }

                if (_options.AdditionalJsonFiles != null)
                {
                    var file = _options.AdditionalJsonFiles.FirstOrDefault(f =>
                    {
                        var fileName = Path.GetFileNameWithoutExtension(f);
                        return fileName.Equals(key, StringComparison.OrdinalIgnoreCase);
                    });
                    if (!string.IsNullOrEmpty(file))
                    {
                        builder.AddJsonFile(file, true, true);
                    }
                }

                var config = builder.Build();
                var v = config.GetChildren().FirstOrDefault(c => _typeName.Equals(c.Key, StringComparison.OrdinalIgnoreCase))?
                    .GetChildren()
                    .SelectMany(c => new KeyValuePair<string, string>[] { new KeyValuePair<string, string>(c.Key, c.Value) });

                // dispose json stream
                foreach (var h in langHandler)
                {
                    h.Dispose();
                }
                return v ?? Enumerable.Empty<KeyValuePair<string, string>>();
            });
        }
    }
}
