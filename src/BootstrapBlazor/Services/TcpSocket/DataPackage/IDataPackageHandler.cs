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
    Func<Memory<byte>, Task>? ReceivedCallBack { get; set; }

    /// <summary>
    /// Sends the specified data asynchronously to the target destination.
    /// </summary>
    /// <remarks>The method performs an asynchronous operation to send the provided data. The caller must
    /// ensure  that the data is valid and non-empty. The returned memory block may contain a response or acknowledgment
    /// depending on the implementation of the target destination.</remarks>
    /// <param name="data">The data to be sent, represented as a block of memory.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a  <see cref="Memory{T}"/> of <see
    /// cref="byte"/> representing the response or acknowledgment  received from the target destination.</returns>
    Task<Memory<byte>> SendAsync(Memory<byte> data);

    /// <summary>
    /// Asynchronously receives data and writes it into the specified memory buffer.
    /// </summary>
    /// <remarks>The method does not guarantee that the entire buffer will be filled. The amount of data
    /// written depends on the data available to be received.</remarks>
    /// <param name="data">The memory buffer where the received data will be written. The buffer must be large enough to hold the incoming
    /// data.</param>
    /// <returns>A task that represents the asynchronous receive operation.</returns>
    Task ReceiveAsync(Memory<byte> data);
}
