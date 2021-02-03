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
    internal class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly JsonLocalizationOptions _jsonOptions;
        private readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="localizationOptions"></param>
        /// <param name="loggerFactory"></param>
        public JsonStringLocalizerFactory(IOptions<JsonLocalizationOptions> localizationOptions, ILoggerFactory loggerFactory)
        {
            _jsonOptions = localizationOptions.Value;
            _loggerFactory = loggerFactory;
        }

        /// <summary>
        /// 通过资源类型创建 IStringLocalizer 方法
        /// </summary>
        /// <param name="resourceSource"></param>
        /// <returns></returns>
        public IStringLocalizer Create(Type resourceSource)
        {
            var typeInfo = resourceSource.GetTypeInfo();
            var typeName = typeInfo.FullName;
            if (string.IsNullOrEmpty(typeName)) throw new InvalidOperationException($"{nameof(resourceSource)} full name is null or String.Empty.");

            if (resourceSource.IsGenericType)
            {
                var index = typeName.IndexOf('`');
                typeName = typeName.Substring(0, index);
            }
            typeName = TryFixInnerClassPath(typeName);
            return CreateJsonStringLocalizer(typeInfo.Assembly, typeName);
        }

        /// <summary>
        /// 通过 baseName 与 location 创建 IStringLocalizer 方法
        /// </summary>
        /// <param name="baseName"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public IStringLocalizer Create(string baseName, string location)
        {
            baseName = TryFixInnerClassPath(baseName);

            Assembly? assembly;
            if (!string.IsNullOrEmpty(location))
            {
                var assemblyName = new AssemblyName(location);
                assembly = Assembly.Load(assemblyName);
            }
            else
            {
                assembly = GetType().Assembly;
            }

            return CreateJsonStringLocalizer(assembly, string.Empty);
        }

        /// <summary>
        /// 创建 IStringLocalizer 实例方法
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="typeName"></param>
        /// <returns></returns>
        protected virtual IStringLocalizer CreateJsonStringLocalizer(Assembly assembly, string typeName)
        {
            var logger = _loggerFactory.CreateLogger<JsonStringLocalizer>();
            return new JsonStringLocalizer(assembly, typeName, logger, _jsonOptions);
        }

        private static string TryFixInnerClassPath(string path)
        {
            const char innerClassSeparator = '+';
            var fixedPath = path;

            if (path.Contains(innerClassSeparator.ToString()))
            {
                fixedPath = path.Replace(innerClassSeparator, '.');
            }

            return fixedPath;
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
        /// <returns></returns>
        public static IStringLocalizer CreateLocalizer(Type type)
        {
            var options = ServiceProviderHelper.ServiceProvider.GetRequiredService<IOptions<JsonLocalizationOptions>>();
            var loggerFactory = ServiceProviderHelper.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var factory = new JsonStringLocalizerFactory(options, loggerFactory);
            return factory.Create(type);
        }
    }
}
