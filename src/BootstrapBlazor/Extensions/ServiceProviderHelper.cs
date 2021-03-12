// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    internal static class ServiceProviderHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static ServiceProvider ServiceProvider
        {
            get
            {
                if (_lazy == null)
                {
                    throw new InvalidOperationException("please add services.AddBootstrapBlazor() in Startup class.");
                }
                return _lazy.Value;
            }
        }

        [NotNull]
        private static Lazy<ServiceProvider>? _lazy = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterService(IServiceCollection services)
        {
            _lazy = new Lazy<ServiceProvider>(() => services.BuildServiceProvider());
        }
    }
}
