// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace BootstrapBlazor.Localization.Json
{
    /// <summary>
    /// JsonStringLocalizer 实现类
    /// </summary>
    internal class JsonStringLocalizer : ResourceManagerStringLocalizer
    {
        private readonly ConcurrentDictionary<string, IEnumerable<KeyValuePair<string, string>>> _resourcesCache = new();
        private readonly Assembly _assembly;
        private readonly string _typeName;
        private readonly ILogger _logger;
        private readonly JsonLocalizationOptions _options;
        private readonly IServiceProvider _provider;

        private string _searchedLocation = "";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="assembly"></param>
        /// <param name="typeName"></param>
        /// <param name="baseName"></param>
        /// <param name="logger"></param>
        /// <param name="options"></param>
        /// <param name="provider"></param>
        public JsonStringLocalizer(JsonStringLocalizerFactory factory, Assembly assembly, string typeName, string baseName, ILogger logger, JsonLocalizationOptions options, IServiceProvider provider) : base(new ResourceManager(baseName, assembly), assembly, baseName, factory.GetCache(), logger)
        {
            _assembly = assembly;
            _typeName = typeName;
            _logger = logger;
            _options = options;
            _provider = provider;
        }

        /// <summary>
        /// 通过指定键值获取多语言值信息索引
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override LocalizedString this[string name]
        {
            get
            {
                var value = GetStringFromInject(name)
                    ?? GetStringSafely(name, CultureInfo.CurrentUICulture)
                    ?? GetJsonStringSafely(name);

                return new LocalizedString(name, value ?? name, resourceNotFound: value == null, searchedLocation: _searchedLocation);
            }
        }

        private string? GetStringFromInject(string name)
        {
            string? ret = null;
            var factorys = _provider.GetService<IEnumerable<IStringLocalizerFactory>>();
            if (factorys != null)
            {
                var factory = factorys.LastOrDefault(a => a is not JsonStringLocalizerFactory);
                if (factory != null)
                {
                    var type = _assembly.GetType(_typeName);
                    if (type != null)
                    {
                        var localizer = factory.Create(type);
                        var l = localizer[name];
                        ret = l.ResourceNotFound ? null : l.Value;
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// 带格式化参数的通过指定键值获取多语言值信息索引
        /// </summary>
        /// <param name="name"></param>
        /// <param name="arguments"></param>
        /// <returns></returns>
        public override LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var format = GetStringFromInject(name)
                    ?? GetStringSafely(name, CultureInfo.CurrentUICulture)
                    ?? GetJsonStringSafely(name);
                var value = !string.IsNullOrEmpty(format) ? string.Format(format, arguments) : name;
                return new LocalizedString(name, value, resourceNotFound: format == null, searchedLocation: _searchedLocation);
            }
        }

        /// <summary>
        /// 获取当前语言的所有资源信息
        /// </summary>
        /// <param name="includeParentCultures"></param>
        /// <returns></returns>
        public override IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var ret = GetAllStringFromInject(includeParentCultures);
            if (ret == null)
            {
                try
                {
                    ret = base.GetAllStrings(includeParentCultures).ToList();
                }
                catch (MissingManifestResourceException)
                {
                    ret = Enumerable.Empty<LocalizedString>();
                }
            }
            if (!ret.Any())
            {
                ret = GetAllJsonStrings(includeParentCultures);
            }
            return ret;
        }

        private IEnumerable<LocalizedString>? GetAllStringFromInject(bool includeParentCultures)
        {
            IEnumerable<LocalizedString>? ret = null;
            var factorys = _provider.GetService<IEnumerable<IStringLocalizerFactory>>();
            if (factorys != null)
            {
                var factory = factorys.LastOrDefault(a => a is not JsonStringLocalizerFactory);
                if (factory != null)
                {
                    var type = _assembly.GetType(_typeName);
                    if (type != null)
                    {
                        var localizer = factory.Create(type);
                        ret = localizer.GetAllStrings(includeParentCultures);
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeParentCultures"></param>
        /// <returns></returns>
        protected virtual IEnumerable<LocalizedString> GetAllJsonStrings(bool includeParentCultures)
        {
            var cultureInfoName = GetCultureInfoName(CultureInfo.CurrentUICulture);
            var resourceNames = includeParentCultures
                ? GetAllStringsFromCultureHierarchy(CultureInfo.CurrentUICulture)
                : GetAllResourceStrings(cultureInfoName);

            foreach (var name in resourceNames)
            {
                var value = GetJsonStringSafely(name);
                yield return new LocalizedString(name, value ?? name, resourceNotFound: value == null, searchedLocation: _searchedLocation);
            }
        }

        private string GetCultureInfoName(CultureInfo culture)
        {
            var cultureInfoName = culture.Name;
            if (string.IsNullOrEmpty(cultureInfoName) && _options.FallBackToParentUICultures)
            {
                cultureInfoName = _options.FallbackCulture;
            }
            return cultureInfoName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected virtual string? GetJsonStringSafely(string name) => GetStringByCulture(CultureInfo.CurrentUICulture, name);

        private string? GetStringByCulture(CultureInfo culture, string name)
        {
            string? value = null;
            var cultureName = GetCultureInfoName(culture);
            BuildResourcesCache(cultureName);

            if (_resourcesCache.TryGetValue(cultureName, out var resources))
            {
                var resource = resources?.FirstOrDefault(s => s.Key == name);
                value = resource?.Value ?? null;
                _logger.LogDebug($"{nameof(JsonStringLocalizer)} searched for '{name}' in '{_searchedLocation}' with culture '{culture}'.");
            }
            return value;
        }

        private IEnumerable<string> GetAllStringsFromCultureHierarchy(CultureInfo culture)
        {
            var currentCulture = culture;
            var resourceNames = new HashSet<string>();
            while (currentCulture != currentCulture.Parent)
            {
                var cultureResourceNames = GetAllResourceStrings(GetCultureInfoName(currentCulture));
                foreach (var resourceName in cultureResourceNames)
                {
                    resourceNames.Add(resourceName);
                }
                currentCulture = currentCulture.Parent;
            }
            return resourceNames;
        }

        private IEnumerable<string> GetAllResourceStrings(string cultureInfoName)
        {
            BuildResourcesCache(cultureInfoName);
            _resourcesCache.TryGetValue(cultureInfoName, out var resources);
            return resources?.Select(r => r.Key) ?? Enumerable.Empty<string>();
        }

        private static StringSegment GetParentCultureName(StringSegment cultureInfoName)
        {
            var ret = new StringSegment();
            var index = cultureInfoName.IndexOf('-');
            if (index > 0)
            {
                ret = cultureInfoName.Subsegment(0, index);
            }
            return ret;
        }

        private List<Stream> GetLangHandlers(string cultureInfoName)
        {
            // 获取程序集中的资源文件
            var langHandler = GetResourceStream(_assembly, cultureInfoName);

            // 获取外部设置程序集中的资源文件
            if (_options.AdditionalJsonAssemblies != null)
            {
                foreach (var assembly in _options.AdditionalJsonAssemblies)
                {
                    langHandler.AddRange(GetResourceStream(assembly, cultureInfoName));
                }
            }
            return langHandler;
        }

        private List<Stream> GetResourceStream(Assembly assembly, string cultureInfoName)
        {
            var ret = new List<Stream>();
            if (_options.FallBackToParentUICultures)
            {
                // 查找回落资源
                var parentName = GetParentCultureName(cultureInfoName).Value;
                if (!string.IsNullOrEmpty(parentName))
                {
                    _searchedLocation = $"{assembly.GetName().Name}.{_options.ResourcesPath}.{parentName}.json";
                    var stream = assembly.GetManifestResourceStream(_searchedLocation);
                    if (stream != null)
                    {
                        ret.Add(stream);
                    }
                }
            }

            // 当前文化资源
            _searchedLocation = $"{assembly.GetName().Name}.{_options.ResourcesPath}.{cultureInfoName}.json";
            var s = assembly.GetManifestResourceStream(_searchedLocation);
            if (s != null)
            {
                ret.Add(s);
            }
            return ret;
        }

        private void BuildResourcesCache(string cultureInfoName)
        {
            _resourcesCache.GetOrAdd(cultureInfoName, key =>
            {
                // 获得程序集中的资源文件 stream
                var langHandler = GetLangHandlers(key);

                var builder = new ConfigurationBuilder();
                foreach (var h in langHandler)
                {
                    builder.AddJsonStream(h);
                }

                // 获得配置外置资源文件
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
