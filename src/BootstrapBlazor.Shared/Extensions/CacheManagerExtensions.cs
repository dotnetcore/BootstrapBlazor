// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Caching.Memory;
using System.Globalization;

namespace BootstrapBlazor.Shared.Extensions;

/// <summary>
/// CacheManager 扩展类
/// </summary>
internal static class CacheManagerExtensions
{
    /// <summary>
    /// 获得 指定代码文件当前文化设置的本地化资源集合
    /// </summary>
    /// <param name="cache"></param>
    /// <param name="typeName"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IEnumerable<LocalizedString> GetLocalizedStrings(this ICacheManager cache, string typeName, JsonLocalizationOptions options)
    {
        var key = $"Snippet-{CultureInfo.CurrentUICulture.Name}-{nameof(GetLocalizedStrings)}-{typeName}";
        return cache.GetOrCreate(key, entry =>
        {
            var type = typeName.Replace('\\', '.');
            return Utility.GetJsonStringByTypeName(options, typeof(CodeSnippetService).Assembly, $"BootstrapBlazor.Shared.Components.Samples.{type}");
        });
    }

    public static Task<string> GetContentFromFileAsync(this ICacheManager cache, string fileName, Func<ICacheEntry, Task<string>> factory)
    {
        var key = $"Snippet-{CultureInfo.CurrentUICulture.Name}-{nameof(GetContentFromFileAsync)}-{fileName}";
        return cache.GetOrCreateAsync(key, entry => factory(entry));
    }
}
