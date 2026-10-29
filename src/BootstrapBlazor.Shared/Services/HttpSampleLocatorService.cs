// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// <para lang="zh">通过后台接口获取定位服务商并查询位置。</para>
/// <para lang="en">Gets locator providers and queries locations through the backend API.</para>
/// </summary>
public sealed class HttpSampleLocatorService(HttpClient client) : ISampleLocatorService
{
    /// <inheritdoc />
    public async Task<List<string>> GetProvidersAsync(CancellationToken cancellationToken = default) =>
        await client.GetFromJsonAsync<List<string>>("api/locator/providers", cancellationToken)
        ?? throw new InvalidOperationException("The server did not return locator providers.");

    /// <inheritdoc />
    public async Task<SampleLocatorResult> LocateAsync(string providerName, string ip, CancellationToken cancellationToken = default) =>
        await client.GetFromJsonAsync<SampleLocatorResult>($"api/locator?providerName={Uri.EscapeDataString(providerName)}&ip={Uri.EscapeDataString(ip)}", cancellationToken)
        ?? throw new InvalidOperationException("The server did not return a location.");
}
