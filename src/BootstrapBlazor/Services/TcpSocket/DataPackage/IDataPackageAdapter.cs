// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Defines an adapter for handling and transmitting data packages to a target destination.
/// </summary>
/// <remarks>This interface provides methods for sending data asynchronously and configuring a data handler.
/// Implementations of this interface are responsible for managing the interaction between the caller and the underlying
/// data transmission mechanism.</remarks>
public interface IDataPackageAdapter
{
    /// <summary>
    /// Gets the handler responsible for processing data packages.
    /// </summary>
    IDataPackageHandler? DataPackageHandler { get; }

    /// <summary>
    /// Gets or sets the TCP socket client used for network communication.
    /// </summary>
    ITcpSocketClient? TcpSocketClient { get; set; }

    /// <summary>
    /// Asynchronously receives data from the underlying source.
    /// </summary>
    /// <remarks>This method is non-blocking and returns immediately with a task representing the asynchronous
    /// operation.  The caller can await the task to retrieve the received data. If the operation is canceled, the task
    /// will  complete with a <see cref="TaskCanceledException"/>.</remarks>
    /// <param name="token">A <see cref="CancellationToken"/> that can be used to cancel the operation.</param>
    /// <returns>A <see cref="ValueTask{TResult}"/> containing a <see cref="ReadOnlyMemory{T}"/> of bytes representing the
    /// received data.  If no data is available, the returned memory may be empty.</returns>
    ValueTask<ReadOnlyMemory<byte>> ReceiveAsync(CancellationToken token = default);

    /// <summary>
    /// Processes the provided data asynchronously and returns a value indicating whether the operation was successful.
    /// </summary>
    /// <remarks>The method performs an asynchronous operation and supports cancellation via the <paramref
    /// name="token"/> parameter. Ensure that the <paramref name="data"/> parameter contains valid input for the
    /// operation to succeed.</remarks>
    /// <param name="data">The data to be processed, represented as a read-only block of memory.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation. Defaults to <see langword="default"/> if not
    /// provided.</param>
    /// <returns>A <see cref="ValueTask{TResult}"/> containing <see langword="true"/> if the operation was successful; otherwise,
    /// <see langword="false"/>.</returns>
    ValueTask<bool> HandlerAsync(ReadOnlyMemory<byte> data, CancellationToken token = default);
}
