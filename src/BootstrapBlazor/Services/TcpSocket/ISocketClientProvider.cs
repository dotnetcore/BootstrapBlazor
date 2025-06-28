// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Net;

namespace BootstrapBlazor.Components;

/// <summary>
/// Defines the contract for a socket client that provides asynchronous methods for connecting, sending, receiving, and
/// closing network connections.
/// </summary>
/// <remarks>This interface is designed to facilitate network communication using sockets. It provides methods for
/// establishing connections, transmitting data, and receiving data asynchronously. Implementations of this interface
/// should ensure proper resource management, including closing connections and releasing resources when no longer
/// needed.</remarks>
public interface ISocketClientProvider
{
    /// <summary>
    /// Gets a value indicating whether the connection is currently active.
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// Gets the local network endpoint that the socket is bound to.
    /// </summary>
    IPEndPoint LocalEndPoint { get; set; }

    /// <summary>
    /// Establishes an asynchronous connection to the specified endpoint.
    /// </summary>
    /// <remarks>This method attempts to establish a connection to the specified endpoint. If the connection
    /// fails,  the method returns <see langword="false"/> rather than throwing an exception. Ensure the endpoint is
    /// valid  and reachable before calling this method.</remarks>
    /// <param name="endPoint">The <see cref="IPEndPoint"/> representing the remote endpoint to connect to.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to observe while waiting for the connection to complete.</param>
    /// <returns>A <see cref="ValueTask{TResult}"/> that represents the asynchronous operation.  The result is <see
    /// langword="true"/> if the connection was successfully established; otherwise, <see langword="false"/>.</returns>
    ValueTask<bool> ConnectAsync(IPEndPoint endPoint, CancellationToken token = default);

    /// <summary>
    /// Sends the specified data asynchronously to the connected endpoint.
    /// </summary>
    /// <remarks>This method performs a non-blocking operation to send data. If the operation is canceled via
    /// the  <paramref name="token"/>, the returned task will not complete successfully. Ensure the connected  endpoint
    /// is ready to receive data before calling this method.</remarks>
    /// <param name="data">The data to send, represented as a read-only memory block of bytes.</param>
    /// <param name="token">An optional cancellation token that can be used to cancel the operation.</param>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation.  The result is <see
    /// langword="true"/> if the data was sent successfully; otherwise, <see langword="false"/>.</returns>
    ValueTask<bool> SendAsync(ReadOnlyMemory<byte> data, CancellationToken token = default);

    /// <summary>
    /// Asynchronously receives data from a source and writes it into the specified buffer.
    /// </summary>
    /// <remarks>This method does not guarantee that the buffer will be completely filled. The caller should
    /// check the return value to determine the number of bytes received.</remarks>
    /// <param name="buffer">The memory buffer where the received data will be stored. Must be large enough to hold the incoming data.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation. Defaults to <see langword="default"/> if not
    /// provided.</param>
    /// <returns>A <see cref="ValueTask{TResult}"/> representing the asynchronous operation. The result is the number of bytes
    /// successfully received and written into the buffer. Returns 0 if the end of the data stream is reached.</returns>
    ValueTask<int> ReceiveAsync(Memory<byte> buffer, CancellationToken token = default);

    /// <summary>
    /// Closes the current connection or resource, releasing any associated resources.
    /// </summary>
    /// <remarks>Once the connection or resource is closed, it cannot be reopened. Ensure that all necessary
    /// operations  are completed before calling this method. This method is typically used to clean up resources when
    /// they  are no longer needed.</remarks>
    ValueTask CloseAsync();
}
