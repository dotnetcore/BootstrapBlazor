// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">浏览器指纹接口</para>
/// <para lang="en">Browser Fingerprint Interface</para>
/// </summary>
public interface IBrowserFingerService
{
    /// <summary>
    /// <para lang="zh">获得当前浏览器指纹方法</para>
    /// <para lang="en">Get current browser fingerprint method</para>
    /// </summary>
    /// <returns></returns>
    Task<string?> GetFingerCodeAsync(CancellationToken token = default);

    /// <summary>
    /// <para lang="zh">获得当前连接的客户端 ID 由 BootstrapBlazor 组件框架提供</para>
    /// <para lang="en">Get current connected client ID provided by BootstrapBlazor component framework</para>
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<string?> GetClientHubIdAsync(CancellationToken token = default);
}
