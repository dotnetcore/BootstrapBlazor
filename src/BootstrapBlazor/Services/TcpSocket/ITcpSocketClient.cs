// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Net;

namespace BootstrapBlazor.Components;

/// <summary>
/// Represents a TCP socket for network communication.
/// </summary>
public interface ITcpSocketClient : IDisposable
{
    /// <summary>
    /// Gets a value indicating whether the system is currently connected.
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// Gets the local network endpoint that the socket is bound to.
    /// </summary>
    /// <remarks>This property provides information about the local endpoint of the socket, which is typically
    /// used to identify the local address and port being used for communication. If the socket is not  bound to a
    /// specific local endpoint, this property may return <see langword="null"/>.</remarks>
    IPEndPoint LocalEndPoint { get; }

    /// <summary>
    /// Configures the data handler to process incoming data packages.
    /// </summary>
    /// <param name="handler">The handler responsible for processing data packages. Cannot be null.</param>
    void SetDataHandler(IDataPackageHandler handler);

    /// <summary>
    /// Establishes an asynchronous connection to the specified host and port.
    /// </summary>
    /// <param name="host">The hostname or IP address of the server to connect to. Cannot be null or empty.</param>
    /// <param name="port">The port number on the server to connect to. Must be a valid port number between 0 and 65535.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the connection attempt. Defaults to <see
    /// langword="default"/> if not provided.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the connection
    /// is successfully established; otherwise, <see langword="false"/>.</returns>
    Task<bool> ConnectAsync(string host, int port, CancellationToken token = default);

    /// <summary>
    /// Establishes an asynchronous connection to the specified endpoint.
    /// </summary>
    /// <remarks>This method attempts to establish a connection to the specified endpoint. If the connection
    /// cannot be established, the method returns <see langword="false"/> rather than throwing an exception.</remarks>
    /// <param name="endPoint">The <see cref="IPEndPoint"/> representing the remote endpoint to connect to. Cannot be null.</param>
    /// <param name="token">A <see cref="CancellationToken"/> that can be used to cancel the connection attempt. Defaults to <see
    /// langword="default"/> if not provided.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the connection
    /// is successfully established; otherwise, <see langword="false"/>.</returns>
    Task<bool> ConnectAsync(IPEndPoint endPoint, CancellationToken token = default);

    /// <summary>
    /// Sends the specified data asynchronously to the target endpoint.
    /// </summary>
    /// <remarks>This method performs a non-blocking operation to send data. If the operation is canceled via
    /// the <paramref name="token"/>, the task will complete with a canceled state. Ensure the connection is properly
    /// initialized before calling this method.</remarks>
    /// <param name="data">The byte array containing the data to be sent. Cannot be null or empty.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to observe while waiting for the operation to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the data was
    /// sent successfully; otherwise, <see langword="false"/>.</returns>
    Task<bool> SendAsync(Memory<byte> data, CancellationToken token = default);

    /// <summary>
    /// Closes the current connection or resource, releasing any associated resources.
    /// </summary>
    /// <remarks>Once the connection or resource is closed, it cannot be reopened. Ensure that all necessary
    /// operations  are completed before calling this method. This method is typically used to clean up resources when
    /// they  are no longer needed.</remarks>
    void Close();
}
