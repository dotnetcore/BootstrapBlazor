// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Net;

namespace BootstrapBlazor.Components;

/// <summary>
/// Represents a TCP socket for network communication.
/// </summary>
public interface ITcpSocketClient : IAsyncDisposable
{
    /// <summary>
    /// Gets a value indicating whether the system is currently connected. Default is false.
    /// </summary>
    bool IsConnected { get; }

    /// <summary>
    /// Gets or sets the configuration options for the socket client.
    /// </summary>
    SocketClientOptions Options { get; }

    /// <summary>
    /// Gets the local network endpoint that the socket is bound to.
    /// </summary>
    /// <remarks>This property provides information about the local endpoint of the socket, which is typically
    /// used to identify the local address and port being used for communication. If the socket is not  bound to a
    /// specific local endpoint, this property may return <see langword="null"/>.</remarks>
    IPEndPoint LocalEndPoint { get; }

    /// <summary>
    /// Gets or sets the callback function to handle received data.
    /// </summary>
    /// <remarks>The callback function should be designed to handle the received data efficiently and
    /// asynchronously.  Ensure that the implementation does not block or perform long-running operations, as this may
    /// impact performance.</remarks>
    Func<ReadOnlyMemory<byte>, ValueTask>? ReceivedCallBack { get; set; }

    /// <summary>
    /// Gets or sets the callback function that is invoked when a connection attempt is initiated.
    /// </summary>
    Func<Task>? OnConnecting { get; set; }

    /// <summary>
    /// Gets or sets the delegate to be invoked when a connection is successfully established.
    /// </summary>
    Func<Task>? OnConnected { get; set; }

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
    ValueTask<bool> ConnectAsync(IPEndPoint endPoint, CancellationToken token = default);

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
    ValueTask<bool> SendAsync(ReadOnlyMemory<byte> data, CancellationToken token = default);

    /// <summary>
    /// Asynchronously receives a block of data from the underlying source.
    /// </summary>
    /// <remarks>This method is non-blocking and completes when data is available or the operation is
    /// canceled. If the operation is canceled, the returned task will be in a faulted state with a <see
    /// cref="OperationCanceledException"/>.</remarks>
    /// <param name="token">A cancellation token that can be used to cancel the operation. The default value is <see langword="default"/>.</param>
    /// <returns>A <see cref="ValueTask{TResult}"/> containing a <see cref="Memory{T}"/> of bytes representing the received data.
    /// The returned memory may be empty if no data is available.</returns>
    ValueTask<Memory<byte>> ReceiveAsync(CancellationToken token = default);

    /// <summary>
    /// Closes the current connection or resource, releasing any associated resources.
    /// </summary>
    /// <remarks>Once the connection or resource is closed, it cannot be reopened. Ensure that all necessary
    /// operations  are completed before calling this method. This method is typically used to clean up resources when
    /// they  are no longer needed.</remarks>
    ValueTask CloseAsync();
}
