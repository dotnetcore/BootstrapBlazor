// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

namespace BootstrapBlazor.Components;

/// <summary>
/// IIPLocatorFactory 接口实现类
/// </summary>
/// <param name="provider"></param>
class DefaultIPLocatorFactory(IServiceProvider provider) : IIPLocatorFactory
{
    private Dictionary<object, IIPLocatorProvider>? _providers;

    /// <summary>
    /// 创建 <see cref="IIPLocatorProvider"/> 实例方法
    /// </summary>
    /// <param name="key"></param>
    public IIPLocatorProvider Create(object? key = null)
    {
        if (_providers == null)
        {
            _providers = [];
            foreach (var p in provider.GetServices<IIPLocatorProvider>())
            {
                if (p.Key != null)
                {
                    _providers[p.Key] = p;
                }
            }
        }
        return key == null ? _providers.Values.Last() : _providers[key];
    }
}
