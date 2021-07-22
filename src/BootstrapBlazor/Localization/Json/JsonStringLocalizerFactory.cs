// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace BootstrapBlazor.Localization.Json
{
    /// <summary>
    /// IStringLocalizerFactory 实现类
    /// </summary>
    internal class JsonStringLocalizerFactory : ResourceManagerStringLocalizerFactory
    {
        private readonly JsonLocalizationOptions _jsonOptions;
        private readonly ILoggerFactory _loggerFactory;
        private string? _typeName;
        private readonly IServiceProvider _provider;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="jsonOptions"></param>
        /// <param name="resxOptions"></param>
        /// <param name="options"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="provider"></param>
        public JsonStringLocalizerFactory(IOptions<JsonLocalizationOptions> jsonOptions, IOptions<LocalizationOptions> resxOptions, IOptions<BootstrapBlazorOptions> options, ILoggerFactory loggerFactory, IServiceProvider provider) : base(resxOptions, loggerFactory)
        {
            _jsonOptions = jsonOptions.Value;
            _jsonOptions.FallbackCulture = options.Value.FallbackCulture;
            _jsonOptions.FallBackToParentUICultures = options.Value.FallBackToParentUICultures;
            _jsonOptions.SupportedCultures.AddRange(options.Value.GetSupportedCultures());
            _loggerFactory = loggerFactory;
            _provider = provider;
        }

        protected override string GetResourcePrefix(TypeInfo typeInfo)
        {
            var typeName = typeInfo.FullName;
            if (string.IsNullOrEmpty(typeName))
            {
                throw new InvalidOperationException($"{nameof(typeInfo)} full name is null or String.Empty.");
            }

            if (typeInfo.IsGenericType)
            {
                var index = typeName.IndexOf('`');
                typeName = typeName.Substring(0, index);
            }
            _typeName = TryFixInnerClassPath(typeName);

            return base.GetResourcePrefix(typeInfo);
        }

        private const char InnerClassSeparator = '+';
        private static string TryFixInnerClassPath(string path)
        {
            var fixedPath = path;

            if (path.Contains(InnerClassSeparator.ToString()))
            {
                fixedPath = path.Replace(InnerClassSeparator, '.');
            }

            return fixedPath;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="baseName"></param>
        /// <returns></returns>
        protected override ResourceManagerStringLocalizer CreateResourceManagerStringLocalizer(Assembly assembly, string baseName)
        {
            return new JsonStringLocalizer(this, assembly, _typeName ?? "", baseName, _loggerFactory.CreateLogger<JsonStringLocalizer>(), _jsonOptions, _provider);
        }

        /// <summary>
        /// 获得 IResourceNamesCache 实例
        /// </summary>
        /// <returns></returns>
        internal IResourceNamesCache GetCache()
        {
            var field = this.GetType().BaseType?.GetField("_resourceNamesCache", BindingFlags.NonPublic | BindingFlags.Instance);
            var ret = field?.GetValue(this) as IResourceNamesCache;
            return ret!;
        }

        /// <summary>
        /// 通过指定类型创建 IStringLocalizer 实例
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <returns></returns>
        public static IStringLocalizer? CreateLocalizer<TType>() => CreateLocalizer(typeof(TType));

        /// <summary>
        /// 通过指定类型创建 IStringLocalizer 实例
        /// </summary>
        /// <param name="resourceSource"></param>
        /// <returns></returns>
        public static IStringLocalizer? CreateLocalizer(Type resourceSource) => ServiceProviderHelper.ServiceProvider?.GetRequiredService<IStringLocalizerFactory>().Create(resourceSource);

        /// <summary>
        /// 获取指定 Type 的资源文件
        /// </summary>
        /// <param name="localizer"></param>
        /// <param name="key"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool TryGetLocalizerString(IStringLocalizer? localizer, string key, [MaybeNullWhen(false)] out string? text)
        {
            text = null;
            var l = localizer?[key];
            var ret = !(l?.ResourceNotFound ?? false);
            if (ret)
            {
                text = l?.Value;
            }
            return ret;
        }
    }
}
