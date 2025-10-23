// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// NetworkMonitor Sample code
/// </summary>
public partial class NetworkMonitors : IDisposable
{
    [Inject, NotNull]
    private INetworkMonitorService? NetworkMonitorService { get; set; }

    private ConsoleLogger? _logger;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        await NetworkMonitorService.RegisterStateChangedCallback(this, OnNetworkStateChanged);
    }

    private Task OnNetworkStateChanged(NetworkMonitorState state)
    {
        _logger?.Log($"Online: NetworkType: {state.NetworkType} Downlink: {state.Downlink} RTT: {state.RTT}");
        return Task.CompletedTask;
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            NetworkMonitorService.UnregisterStateChangedCallback(this);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
