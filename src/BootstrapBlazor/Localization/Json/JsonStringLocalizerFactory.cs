// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

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
        private readonly string _resourcesRelativePath;
        private readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="localizationOptions"></param>
        /// <param name="loggerFactory"></param>
        public JsonStringLocalizerFactory(IOptions<JsonLocalizationOptions> localizationOptions, ILoggerFactory loggerFactory)
        {
            _resourcesRelativePath = localizationOptions.Value.ResourcesPath;
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
            if (string.IsNullOrEmpty(typeName)) throw new InvalidOperationException($"{nameof(resourceSource)} full name is null.");
            var assemblyName = resourceSource.Assembly.GetName().Name;

            if (resourceSource.IsGenericType)
            {
                var index = typeName.IndexOf('`');
                typeName = typeName.Substring(0, index);
            }
            typeName = TryFixInnerClassPath(typeName);
            return CreateJsonStringLocalizer(typeInfo.Assembly, typeName, $"{assemblyName}.{_resourcesRelativePath}");
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

            var assemblyName = new AssemblyName(location);
            var assembly = Assembly.Load(assemblyName);
            string? resourceName = null;

            return CreateJsonStringLocalizer(assembly, string.Empty, resourceName);
        }

        /// <summary>
        /// 创建 IStringLocalizer 实例方法
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="typeName"></param>
        /// <param name="resourceName"></param>
        /// <returns></returns>
        protected virtual IStringLocalizer CreateJsonStringLocalizer(Assembly assembly, string typeName, string? resourceName)
        {
            var logger = _loggerFactory.CreateLogger<JsonStringLocalizer>();

            return new JsonStringLocalizer(assembly, resourceName, typeName, logger);
        }

        private string TryFixInnerClassPath(string path)
        {
            const char innerClassSeparator = '+';
            var fixedPath = path;

            if (path.Contains(innerClassSeparator.ToString()))
            {
                fixedPath = path.Replace(innerClassSeparator, '.');
            }

            return fixedPath;
        }
    }
}
