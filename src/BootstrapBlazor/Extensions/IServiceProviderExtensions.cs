// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// IServiceProvider 接口扩展类
    /// </summary>
    public static class IServiceProviderExtensions
    {
        /// <summary>
        /// 配置 IServiceProvider 接口给 ServiceProviderHelper 使用
        /// </summary>
        /// <param name="provider"></param>
        public static void ConfigureProvider(this IServiceProvider provider)
        {
            ServiceProviderHelper.Configure(provider); ;
        }
    }
}
