// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared
{
    /// <summary>
    /// 
    /// </summary>
    internal static class CacheManagerHelper
    {
        public static string GetTitle(int id, Func<ICacheEntry, string> factory)
        {
            var key = $"Excel-Title-{id}";
            return GetOrCreate(key, entry => factory(entry));
        }

        public static List<KeyValuePair<string, string>> GetLocalizers(string codeFile, Func<ICacheEntry, List<KeyValuePair<string, string>>> factory)
        {
            var key = $"Localizer-{CultureInfo.CurrentUICulture.Name}-{nameof(GetLocalizers)}-{codeFile}";
            return GetOrCreate(key, entry => factory(entry));
        }

        public static string GetCode(string codeFile, string blockTitle, Func<ICacheEntry, string> factory)
        {
            var key = $"Snippet-{CultureInfo.CurrentUICulture.Name}-{nameof(GetCode)}-{codeFile}-{blockTitle}";
            return GetOrCreate(key, entry => factory(entry));
        }

        public static Task<string> GetContentFromFileAsync(string codeFile, Func<ICacheEntry, Task<string>> factory)
        {
            var key = $"Snippet-{CultureInfo.CurrentUICulture.Name}-{nameof(GetContentFromFileAsync)}-{codeFile}";
            return GetOrCreateAsync(key, entry => factory(entry));
        }

        private static T GetOrCreate<T>(string key, Func<ICacheEntry, T> factory) => CacheManager.GetOrCreate(key, factory);

        private static Task<T> GetOrCreateAsync<T>(string key, Func<ICacheEntry, Task<T>> factory) => CacheManager.GetOrCreateAsync(key, factory);
    }
}
