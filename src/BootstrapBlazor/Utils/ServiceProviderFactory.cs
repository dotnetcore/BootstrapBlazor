// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// ServiceProviderHelper 注入服务扩展类
    /// </summary>
    internal static class ServiceProviderFactory
    {
        private static IServiceProvider? _provider;

        /// <summary>
        /// 获取系统 IServiceProvider 接口
        /// </summary>
        [NotNull]
        public static IServiceProvider? Services => _provider ?? throw new InvalidOperationException($"Please add app.ApplicationServices.ConfigureProvider() in Configure function of Startup");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        internal static void Configure(IServiceProvider provider)
        {
            _provider = provider;
        }
    }
}
