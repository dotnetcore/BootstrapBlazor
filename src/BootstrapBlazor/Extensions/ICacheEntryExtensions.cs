// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Caching.Memory;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public static class ICacheEntryExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static ICacheEntry SetSlidingExpirationForDynamicAssembly(this ICacheEntry entry, TimeSpan? offset = null)
        {
            entry.SlidingExpiration = offset ?? TimeSpan.FromMinutes(5);
            return entry;
        }
    }
}
