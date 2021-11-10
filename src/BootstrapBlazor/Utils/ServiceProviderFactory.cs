// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// ServiceProviderHelper 注入服务扩展类
    /// </summary>
    public static class ServiceProviderFactory
    {
        private static IServiceProvider? _provider;

        /// <summary>
        /// 获取系统 Root IServiceProvider 接口
        /// </summary>
        [NotNull]
        public static IServiceProvider? Services => _provider ?? throw new InvalidOperationException($"{nameof(ServiceProviderFactory.Services)} is null");

        /// <summary>
        /// 获取当前 Scope IServiceProvider 接口
        /// </summary>
        public static IServiceProvider ScopeServices => Services.CreateScope().ServiceProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        public static void Configure(this IServiceProvider provider)
        {
            _provider = provider;
        }
    }
}
