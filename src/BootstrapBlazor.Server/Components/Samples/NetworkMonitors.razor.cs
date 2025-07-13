// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// NetworkMonitor Sample code
/// </summary>
public partial class NetworkMonitors
{
    private ConsoleLogger _logger = null!;

    private Task OnNetworkStateChanged(NetworkMonitorState state)
    {
        _logger.Log($"Online: {state.IsOnline} NetworkType: {state.NetworkType} Downlink: {state.Downlink} RTT: {state.RTT}");
        return Task.CompletedTask;
    }
}
