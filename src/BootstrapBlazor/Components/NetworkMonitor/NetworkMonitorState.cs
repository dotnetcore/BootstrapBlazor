// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 网络状态信息类
/// </summary>
public class NetworkMonitorState
{
    /// <summary>
    /// Gets or sets the current network type
    /// </summary>
    public string? NetworkType { get; set; }

    /// <summary>
    /// Gets or sets the downlink speed in megabits per second (Mbps).
    /// </summary>
    public double? Downlink { get; set; }

    /// <summary>
    /// Gets or sets the round-trip time (RTT) in milliseconds.
    /// </summary>
    public int RTT { get; set; }
}
