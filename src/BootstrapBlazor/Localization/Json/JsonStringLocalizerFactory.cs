using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Reflection;

namespace BootstrapBlazor.Localization.Json
{
    /// <summary>
    /// IStringLocalizerFactory 实现类
    /// </summary>
    internal class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly string _resourcesRelativePath;
        private readonly ResourcesType _resourcesType = ResourcesType.TypeBased;
        private readonly ILoggerFactory _loggerFactory;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="localizationOptions"></param>
        /// <param name="loggerFactory"></param>
        public JsonStringLocalizerFactory(IOptions<JsonLocalizationOptions> localizationOptions, ILoggerFactory loggerFactory)
        {
            _resourcesRelativePath = localizationOptions.Value.ResourcesPath;
            _resourcesType = localizationOptions.Value.ResourcesType;
            _loggerFactory = loggerFactory;
        }

        /// <summary>
        /// 通过资源类型创建 IStringLocalizer 方法
        /// </summary>
        /// <param name="resourceSource"></param>
        /// <returns></returns>
        public IStringLocalizer Create(Type resourceSource)
        {
            if (resourceSource.Name == "Controller")
            {
                return CreateJsonStringLocalizer(Path.Combine(PathHelpers.GetApplicationRoot(), GetResourcePath(resourceSource.Assembly)), TryFixInnerClassPath("Controller"));
            }

            var typeInfo = resourceSource.GetTypeInfo();
            var assembly = typeInfo.Assembly;
            var assemblyName = resourceSource.Assembly.GetName().Name;
            var typeName = $"{assemblyName}.{typeInfo.Name}" == typeInfo.FullName
                ? typeInfo.Name
                : typeInfo.FullName.Substring(assemblyName.Length + 1);
            var resourcesPath = Path.Combine(PathHelpers.GetApplicationRoot(), GetResourcePath(assembly));

            typeName = TryFixInnerClassPath(typeName);

            return CreateJsonStringLocalizer(resourcesPath, typeName);
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
            var resourcesPath = Path.Combine(PathHelpers.GetApplicationRoot(), GetResourcePath(assembly));
            string? resourceName = null;

            if (_resourcesType == ResourcesType.TypeBased)
            {
                resourceName = TrimPrefix(baseName, location + ".");
            }

            return CreateJsonStringLocalizer(resourcesPath, resourceName);
        }

        /// <summary>
        /// 创建 IStringLocalizer 实例方法
        /// </summary>
        /// <param name="resourcesPath"></param>
        /// <param name="resourcename"></param>
        /// <returns></returns>
        protected virtual IStringLocalizer CreateJsonStringLocalizer(string resourcesPath, string? resourcename)
        {
            var logger = _loggerFactory.CreateLogger<JsonStringLocalizer>();

            return new JsonStringLocalizer(
                resourcesPath,
                _resourcesType == ResourcesType.TypeBased ? resourcename : null,
                logger);
        }

        private string GetResourcePath(Assembly assembly)
        {
            var resourceLocationAttribute = assembly.GetCustomAttribute<ResourceLocationAttribute>();

            return resourceLocationAttribute == null
                ? _resourcesRelativePath
                : resourceLocationAttribute.ResourceLocation;
        }

        private static string TrimPrefix(string name, string prefix)
        {
            if (name.StartsWith(prefix, StringComparison.Ordinal))
            {
                return name.Substring(prefix.Length);
            }

            return name;
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
