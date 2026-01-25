// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">网络状态信息类</para>
/// <para lang="en">Network status information class</para>
/// </summary>
public class NetworkMonitorState
{
    /// <summary>
    /// <para lang="zh">获得/设置 当前网络类型</para>
    /// <para lang="en">Gets or sets the current network type</para>
    /// </summary>
    public string? NetworkType { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 下行速度（Mbps）</para>
    /// <para lang="en">Gets or sets the downlink speed in megabits per second (Mbps)</para>
    /// </summary>
    public double? Downlink { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 往返时间（RTT）（毫秒）</para>
    /// <para lang="en">Gets or sets the round-trip time (RTT) in milliseconds</para>
    /// </summary>
    public int RTT { get; set; }
}
