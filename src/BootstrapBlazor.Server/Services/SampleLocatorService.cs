// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Services;

/// <summary>
/// <para lang="zh">保留 Server 已注册的定位服务商与工厂调用。</para>
/// <para lang="en">Preserves the Server's registered locator providers and factory calls.</para>
/// </summary>
public sealed class SampleLocatorService(IIpLocatorFactory factory, IEnumerable<IIpLocatorProvider> providers) : ISampleLocatorService
{
    /// <inheritdoc />
    public Task<List<string>> GetProvidersAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(providers.Select(provider => provider.GetType().Name).Distinct().ToList());
    }

    /// <inheritdoc />
    public async Task<SampleLocatorResult> LocateAsync(string providerName, string ip, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var provider = factory.Create(providerName);
        var location = await provider.Locate(ip);
        cancellationToken.ThrowIfCancellationRequested();
        return new() { Location = location, ErrorMessage = provider.LastError };
    }
}
