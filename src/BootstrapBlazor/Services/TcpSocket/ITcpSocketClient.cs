// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
    /// Asynchronously establishes a connection to the server.
    /// </summary>
    /// <remarks>This method attempts to connect to the server and returns a value indicating whether the
    /// connection was successful.  If the connection cannot be established within the timeout period specified by the
    /// <paramref name="token"/>,  the operation is canceled.</remarks>
    /// <param name="token">A <see cref="CancellationToken"/> that can be used to cancel the connection attempt.  If not provided, the
    /// default token is used.</param>
    /// <returns><see langword="true"/> if the connection is successfully established; otherwise, <see langword="false"/>.</returns>
    Task<bool> ConnectAsync(CancellationToken token = default);

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
    Task<bool> SendAsync(byte[] data, CancellationToken token = default);

    /// <summary>
    /// Asynchronously receives data from the underlying connection.
    /// </summary>
    /// <remarks>The method waits for data to be available and returns it as a byte array. If the operation is
    /// canceled via the <paramref name="token"/>, the returned task will be in a canceled state.</remarks>
    /// <param name="token">A <see cref="CancellationToken"/> that can be used to cancel the operation. The default value is <see
    /// langword="default"/>.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a byte array with the received data.</returns>
    Task<byte[]> ReceiveAsync(CancellationToken token = default);
}
