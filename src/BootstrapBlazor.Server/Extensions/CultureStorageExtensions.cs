// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    internal static class CultureStorageExtensions
    {
        /// <summary>
        /// 添加本地化持久化策略服务
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddCultureStorage(this IServiceCollection services)
        {
            services.TryAddSingleton<ICultureStorage, DefaultCultureStorage>();
            return services;
        }

        internal class DefaultCultureStorage : ICultureStorage
        {
            public CultureStorageMode Mode { get; set; }
        }
    }
}
