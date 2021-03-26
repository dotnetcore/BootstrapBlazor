// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// ServiceProviderHelper 注入服务扩展类
    /// </summary>
    public static class ServiceProviderHelper
    {
        private static IServiceProvider? _provider;

        private static IServiceCollection? _service;

        private static IServiceProvider? _serviceProvider;

        internal static void RegisterProvider(IServiceProvider provider) => _provider = provider;

        internal static void RegisterService(IServiceCollection services) => _service = services;

        /// <summary>
        /// 获取系统 IServiceProvider 接口
        /// </summary>
        public static IServiceProvider ServiceProvider => _provider ?? CreateProvider();

        private static IServiceProvider CreateProvider()
        {
            if (_serviceProvider == null)
            {
                _serviceProvider = _service.BuildServiceProvider();
            }
            return _serviceProvider;
        }
    }
}
