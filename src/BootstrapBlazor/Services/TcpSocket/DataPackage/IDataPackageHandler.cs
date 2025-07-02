// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Defines an interface for adapting data packages to and from a TCP socket connection.
/// </summary>
/// <remarks>Implementations of this interface are responsible for converting raw data received from a TCP socket
/// into structured data packages and vice versa. This allows for custom serialization and deserialization logic
/// tailored to specific application protocols.</remarks>
public interface IDataPackageHandler
{
    /// <summary>
    /// Gets or sets the callback function to be invoked when data is received asynchronously.
    /// </summary>
    Func<ReadOnlyMemory<byte>, ValueTask>? ReceivedCallBack { get; set; }

    /// <summary>
    /// Sends the specified data asynchronously to the target destination.
    /// </summary>
    /// <remarks>The method performs an asynchronous operation to send the provided data. The caller must
    /// ensure  that the data is valid and non-empty. The returned memory block may contain a response or acknowledgment
    /// depending on the implementation of the target destination.</remarks>
    /// <param name="data">The data to be sent, represented as a block of memory.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to observe while waiting for the operation to complete.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a  <see cref="Memory{T}"/> of <see
    /// cref="byte"/> representing the response or acknowledgment  received from the target destination.</returns>
    ValueTask<ReadOnlyMemory<byte>> SendAsync(ReadOnlyMemory<byte> data, CancellationToken token = default);

    /// <summary>
    /// Asynchronously receives data and processes it.
    /// </summary>
    /// <remarks>The method is designed for asynchronous operations and may be used in scenarios where
    /// efficient handling of data streams is required. Ensure that the <paramref name="data"/> parameter contains valid
    /// data for processing, and handle potential cancellation using the <paramref name="token"/>.</remarks>
    /// <param name="data">The data to be received, represented as a read-only memory block of bytes.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation. Defaults to <see langword="default"/> if not
    /// provided.</param>
    /// <returns>A <see cref="ValueTask{TResult}"/> containing <see langword="true"/> if the data was successfully received and
    /// processed; otherwise, <see langword="false"/>.</returns>
    ValueTask<bool> ReceiveAsync(ReadOnlyMemory<byte> data, CancellationToken token = default);
}
