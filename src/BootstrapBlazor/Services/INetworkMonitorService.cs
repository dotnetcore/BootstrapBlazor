﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Defines a service for monitoring network state and retrieving the current network monitor status.
/// </summary>
public interface INetworkMonitorService
{
    /// <summary>
    /// Retrieves the current state of the network monitor.
    /// </summary>
    /// <param name="token">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task representing the asynchronous operation. The task result contains the current <see
    /// cref="NetworkMonitorState"/>.</returns>
    Task<NetworkMonitorState?> GetNetworkMonitorState(CancellationToken token = default);

    /// <summary>
    /// Registers a callback to be invoked when the network monitor state changes.
    /// </summary>
    /// <remarks>The callback is executed asynchronously whenever the network monitor detects a change in
    /// state. Ensure that the callback function is thread-safe and handles any exceptions that may occur during
    /// execution.</remarks>
    /// <param name="component">The component that will be associated with the callback. Cannot be null.</param>
    /// <param name="callback">A function to be called when the network monitor state changes. The function receives the new state and returns
    /// a task. Cannot be null.</param>
    Task RegisterStateChangedCallback(IComponent component, Func<NetworkMonitorState, Task> callback);

    /// <summary>
    /// Unregisters a previously registered callback for state changes on the specified component.
    /// </summary>
    /// <param name="component">The component for which the state change callback should be unregistered. Cannot be null.</param>
    void UnregisterStateChangedCallback(IComponent component);
}
