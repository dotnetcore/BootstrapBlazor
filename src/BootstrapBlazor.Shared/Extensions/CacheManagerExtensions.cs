// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;

namespace BootstrapBlazor.Shared.Exntensions;

/// <summary>
/// CacheManager 扩展类
/// </summary>
internal static class CacheManagerExtensions
{
    /// <summary>
    /// 获得 指定代码文件当前文化设置的本地化资源集合
    /// </summary>
    /// <param name="cache"></param>
    /// <param name="codeFile"></param>
    /// <param name="factory"></param>
    /// <returns></returns>
    public static List<KeyValuePair<string, string>> GetLocalizers(this ICacheManager cache, string codeFile, Func<ICacheEntry, List<KeyValuePair<string, string>>> factory)
    {
        var key = $"Localizer-{CultureInfo.CurrentUICulture.Name}-{nameof(GetLocalizers)}-{codeFile}";
        return cache.GetOrCreate(key, entry => factory(entry));
    }

    /// <summary>
    /// 获得 指定代码文件内当前代码块当前文化的代码片段
    /// </summary>
    /// <param name="cache"></param>
    /// <param name="codeFile"></param>
    /// <param name="blockTitle"></param>
    /// <param name="factory"></param>
    /// <returns></returns>
    public static string GetCode(this ICacheManager cache, string codeFile, string blockTitle, Func<ICacheEntry, string> factory)
    {
        var key = $"Snippet-{CultureInfo.CurrentUICulture.Name}-{nameof(GetCode)}-{codeFile}-{blockTitle}";
        return cache.GetOrCreate(key, entry => factory(entry));
    }

    public static Task<string> GetContentFromFileAsync(this ICacheManager cache, string codeFile, Func<ICacheEntry, Task<string>> factory)
    {
        var key = $"Snippet-{CultureInfo.CurrentUICulture.Name}-{nameof(GetContentFromFileAsync)}-{codeFile}";
        return cache.GetOrCreateAsync(key, entry => factory(entry));
    }
}
