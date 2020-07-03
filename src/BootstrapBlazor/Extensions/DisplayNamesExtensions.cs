using System;
using System.Collections.Concurrent;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 显示名称扩展方法类
    /// </summary>
    internal static class DisplayNamesExtensions
    {
        private static readonly ConcurrentDictionary<(Type ModelType, string FieldName), string> _displayNameCache = new ConcurrentDictionary<(Type, string), string>();

        /// <summary>
        /// 尝试获取指定 Model 指定属性值的显示名称
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        public static bool TryGetValue((Type ModelType, string FieldName) cacheKey, out string? displayName) => _displayNameCache.TryGetValue(cacheKey, out displayName);

        /// <summary>
        /// 获得或者添加指定 Model 的指定属性值得显示名称
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="valueFactory"></param>
        /// <returns></returns>
        public static string GetOrAdd((Type ModelType, string FieldName) cacheKey, Func<(Type, string), string> valueFactory) => _displayNameCache.GetOrAdd(cacheKey, valueFactory);
    }
}
