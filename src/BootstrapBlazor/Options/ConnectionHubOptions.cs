// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">ConnectionHubOptions 配置类</para>
/// <para lang="en">ConnectionHubOptions configuration class</para>
/// </summary>
public class ConnectionHubOptions
{
    /// <summary>
    /// <para lang="zh">获得/设置 是否开启 ConnectionHub 功能 默认 false 未开启</para>
    /// <para lang="en">Gets or sets whether to enable ConnectionHub feature default false</para>
    /// </summary>
    public bool Enable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否开启 IP 定位功能 默认 false 未开启</para>
    /// <para lang="en">Gets or sets whether to enable IP locator feature default false</para>
    /// </summary>
    public bool EnableIpLocator { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 过期扫描周期 默认 10 分钟</para>
    /// <para lang="en">Gets or sets expiration scan frequency default 10 minutes</para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">定期清理最后一次心跳超过 <see cref="TimeoutInterval"/> 的连接。在线数 <see cref="IConnectionService.Count"/> 按超时间隔计算，不受扫描周期影响。</para>
    /// <para lang="en">Periodically removes connections whose last heartbeat exceeds <see cref="TimeoutInterval"/>. The online count <see cref="IConnectionService.Count"/> is based on the timeout regardless of the scan frequency.</para>
    /// </remarks>
    public TimeSpan ExpirationScanFrequency { get; set; } = TimeSpan.FromMinutes(10);

    /// <summary>
    /// <para lang="zh">获得/设置 超时间隔 默认 10 秒</para>
    /// <para lang="en">Gets or sets timeout interval default 10 seconds</para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">建议至少为 <see cref="BeatInterval"/> 的两倍，并为网络延迟、后台标签页定时器限速留出余量。</para>
    /// <para lang="en">At least twice <see cref="BeatInterval"/> is recommended. Allow additional time for network latency and browser timer throttling in background tabs.</para>
    /// </remarks>
    public TimeSpan TimeoutInterval { get; set; } = TimeSpan.FromSeconds(10);

    /// <summary>
    /// <para lang="zh">获得/设置 ConnectionHub 组件心跳间隔 默认 5000 单位毫秒</para>
    /// <para lang="en">Gets or sets ConnectionHub component heartbeat interval default 5000 ms</para>
    /// </summary>
    public TimeSpan BeatInterval { get; set; } = TimeSpan.FromSeconds(5);
}
