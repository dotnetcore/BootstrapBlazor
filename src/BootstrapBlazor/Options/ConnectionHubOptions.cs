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
    /// <para lang="zh">获得/设置 是否开启 CollectionHub 功能 默认 false 未开启</para>
    /// <para lang="en">Gets or sets whether to enable CollectionHub feature default false</para>
    /// </summary>
    public bool Enable { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 是否开启 IP 定位功能 默认 false 未开启</para>
    /// <para lang="en">Gets or sets whether to enable IP locator feature default false</para>
    /// </summary>
    public bool EnableIpLocator { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 过期扫描周期 默认 1 分钟</para>
    /// <para lang="en">Gets or sets expiration scan frequency default 1 minute</para>
    /// </summary>
    /// <remarks><para lang="zh">自动清理超时 5 倍心跳时间的客户端信息</para><para lang="en">Automatically clean up client information with timeout 5 times heartbeat time</para></remarks>
    public TimeSpan ExpirationScanFrequency { get; set; } = TimeSpan.FromMinutes(10);

    /// <summary>
    /// <para lang="zh">获得/设置 超时间隔 默认 10 秒</para>
    /// <para lang="en">Gets or sets timeout interval default 10 seconds</para>
    /// </summary>
    /// <remarks><para lang="zh">不能小于 <see cref="BeatInterval"/> 心跳间隔</para><para lang="en">Cannot be less than <see cref="BeatInterval"/> heartbeat interval</para></remarks>
    public TimeSpan TimeoutInterval { get; set; } = TimeSpan.FromSeconds(10);

    /// <summary>
    /// <para lang="zh">获得/设置 ConnectionHub 组件心跳间隔 默认 5000 单位毫秒</para>
    /// <para lang="en">Gets or sets ConnectionHub component heartbeat interval default 5000 ms</para>
    /// </summary>
    public TimeSpan BeatInterval { get; set; } = TimeSpan.FromSeconds(5);
}
