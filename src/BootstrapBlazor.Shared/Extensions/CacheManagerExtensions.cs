// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Localization.Json;
using BootstrapBlazor.Shared.Services;
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
    public static IEnumerable<LocalizedString> GetDemoLocalizedStrings(this ICacheManager cache, string typeName, JsonLocalizationOptions options)
    {
        var key = $"Snippet-{CultureInfo.CurrentUICulture.Name}-{nameof(GetDemoLocalizedStrings)}-{typeName}";
        return cache.GetOrCreate(key, (Func<ICacheEntry, IEnumerable<LocalizedString>>)(entry =>
        {
            return Utility.GetJsonStringByTypeName(options, typeof(CodeSnippetService).Assembly, $"BootstrapBlazor.Shared.{typeName}");
        }));
    }

    public static Task<string> GetContentFromDemoAsync(this ICacheManager cache, string demo, Func<ICacheEntry, Task<string>> factory)
    {
        var key = $"{nameof(GetContentFromDemoAsync)}-{demo}";
        return cache.GetOrCreateAsync(key, entry => factory(entry));
    }
}
