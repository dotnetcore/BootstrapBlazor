// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// ServiceProviderHelper 注入服务扩展类
    /// </summary>
    public static class ServiceProviderHelper
    {
        private static IServiceProvider? _provider;

        internal static void RegisterProvider(IServiceProvider? provider) => _provider = provider;

        /// <summary>
        /// 获取系统 IServiceProvider 接口
        /// </summary>
        public static IServiceProvider? ServiceProvider => _provider;
    }
}
