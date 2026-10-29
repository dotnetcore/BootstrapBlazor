// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// <para lang="zh">两种运行模式共用的定位示例服务。</para>
/// <para lang="en">The locator sample service shared by both hosting modes.</para>
/// </summary>
public interface ISampleLocatorService
{
    /// <summary><para lang="zh">获取后台已注册的定位服务商名称。</para><para lang="en">Gets the names of locator providers registered on the server.</para></summary>
    Task<List<string>> GetProvidersAsync(CancellationToken cancellationToken = default);

    /// <summary><para lang="zh">通过指定服务商查询 IP 位置。</para><para lang="en">Looks up an IP address using the selected provider.</para></summary>
    Task<SampleLocatorResult> LocateAsync(string providerName, string ip, CancellationToken cancellationToken = default);
}
