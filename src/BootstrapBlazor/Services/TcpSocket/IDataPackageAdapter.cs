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
public interface IDataPackageAdapter
{
    /// <summary>
    /// Asynchronously receives data and writes it into the specified memory buffer.
    /// </summary>
    /// <remarks>The method does not guarantee that the entire buffer will be filled. The amount of data
    /// written depends on the data available to be received.</remarks>
    /// <param name="memory">The memory buffer where the received data will be written. The buffer must be large enough to hold the incoming
    /// data.</param>
    /// <returns>A task that represents the asynchronous receive operation.</returns>
    Task<Memory<byte>> ReceiveAsync(Memory<byte> memory);
}
