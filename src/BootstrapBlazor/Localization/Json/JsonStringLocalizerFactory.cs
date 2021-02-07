// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
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

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="jsonOptions"></param>
        /// <param name="resxOptions"></param>
        /// <param name="options"></param>
        /// <param name="loggerFactory"></param>
        public JsonStringLocalizerFactory(IOptions<JsonLocalizationOptions> jsonOptions, IOptions<LocalizationOptions> resxOptions, IOptions<BootstrapBlazorOptions> options, ILoggerFactory loggerFactory) : base(resxOptions, loggerFactory)
        {
            _jsonOptions = jsonOptions.Value;
            _jsonOptions.FallbackCulture = options.Value.FallbackCultureName;
            _loggerFactory = loggerFactory;
        }

        protected override string GetResourcePrefix(TypeInfo typeInfo)
        {
            var typeName = typeInfo.FullName;
            if (string.IsNullOrEmpty(typeName)) throw new InvalidOperationException($"{nameof(typeInfo)} full name is null or String.Empty.");

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
            return new JsonStringLocalizer(this, assembly, _typeName ?? "", baseName, _loggerFactory.CreateLogger<JsonStringLocalizer>(), _jsonOptions);
        }

        /// <summary>
        /// 获得 IResourceNamesCache 实例
        /// </summary>
        /// <returns></returns>
        public IResourceNamesCache? GetCache()
        {
            var field = this.GetType().BaseType?.GetField("_resourceNamesCache", BindingFlags.NonPublic | BindingFlags.Instance);
            return field?.GetValue(this) as IResourceNamesCache;
        }

        /// <summary>
        /// 通过指定类型创建 IStringLocalizer 实例
        /// </summary>
        /// <typeparam name="TType"></typeparam>
        /// <returns></returns>
        public static IStringLocalizer CreateLocalizer<TType>() => CreateLocalizer(typeof(TType));

        /// <summary>
        /// 通过指定类型创建 IStringLocalizer 实例
        /// </summary>
        /// <param name="resourceSource"></param>
        /// <returns></returns>
        public static IStringLocalizer CreateLocalizer(Type resourceSource) => ServiceProviderHelper.ServiceProvider.GetRequiredService<IStringLocalizerFactory>().Create(resourceSource);
    }
}
