// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

namespace BootstrapBlazor.Components;

/// <summary>
/// IIPLocatorFactory 接口实现类
/// </summary>
class DefaultIpLocatorFactory : IIpLocatorFactory
{
    private readonly Dictionary<string, IIpLocatorProvider> _providers = [];

    private readonly IServiceProvider _serviceProvider;

    private readonly IOptionsMonitor<BootstrapBlazorOptions> _options;

    public DefaultIpLocatorFactory(IServiceProvider provider, IOptionsMonitor<BootstrapBlazorOptions> options)
    {
        _serviceProvider = provider;
        _options = options;

        foreach (var p in provider.GetServices<IIpLocatorProvider>())
        {
            if (p.Key != null)
            {
                _providers[p.Key] = p;
            }
        }
    }

    /// <summary>
    /// 创建 <see cref="IIpLocatorProvider"/> 实例方法
    /// </summary>
    /// <param name="key"></param>
    public IIpLocatorProvider Create(string? key = null)
    {
        var providerKey = key;
        if (string.IsNullOrEmpty(key))
        {
            providerKey = _options.CurrentValue.IpLocatorOptions.ProviderName;
        }
        return string.IsNullOrEmpty(providerKey) ? _providers.Values.Last() : _providers[providerKey];
    }
}
