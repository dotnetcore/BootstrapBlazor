// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ITcpSocketFactory Interface
/// </summary>
public interface ITcpSocketFactory : IDisposable
{
    /// <summary>
    /// Retrieves an existing TCP socket associated with the specified host and port, or creates a new one if none
    /// exists.
    /// </summary>
    /// <param name="host">The hostname or IP address of the remote endpoint. Cannot be null or empty.</param>
    /// <param name="port">The port number of the remote endpoint. Must be a valid port number between 0 and 65535.</param>
    /// <param name="mode">The mode of the socket, specifying whether it operates as a client or server. Defaults to <see
    /// cref="SocketMode.Client"/>.</param>
    /// <returns>An <see cref="ITcpSocketClient"/> instance representing the TCP socket for the specified host and port.</returns>
    ITcpSocketClient GetOrCreate(string host, int port, SocketMode mode = SocketMode.Client);
}
