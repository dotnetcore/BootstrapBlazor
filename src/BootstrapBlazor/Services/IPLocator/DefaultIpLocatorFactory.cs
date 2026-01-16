// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.DependencyInjection;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">IIPLocatorFactory 接口实现类</para>
///  <para lang="en">IIPLocatorFactory Implementation Class</para>
/// </summary>
class DefaultIpLocatorFactory(IServiceProvider provider, IOptionsMonitor<BootstrapBlazorOptions> options) : IIpLocatorFactory
{
    private Dictionary<string, IIpLocatorProvider>? _providers = null;

    /// <summary>
    ///  <para lang="zh">创建 <see cref="IIpLocatorProvider"/> 实例方法</para>
    ///  <para lang="en">Create <see cref="IIpLocatorProvider"/> Instance Method</para>
    /// </summary>
    /// <param name="key"></param>
    public IIpLocatorProvider Create(string? key = null)
    {
        _providers ??= GenerateProviders();
        var providerKey = key ?? options.CurrentValue.IpLocatorOptions.ProviderName;
        return string.IsNullOrEmpty(providerKey) ? _providers.Values.Last() : _providers[providerKey];
    }

    private Dictionary<string, IIpLocatorProvider> GenerateProviders()
    {
        var providers = new Dictionary<string, IIpLocatorProvider>();
        foreach (var p in provider.GetServices<IIpLocatorProvider>())
        {
            if (p.Key != null)
            {
                providers[p.Key] = p;
            }
        }
        return providers;
    }
}
