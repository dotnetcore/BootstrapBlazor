// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Provides a base implementation for handling data packages in a communication system.
/// </summary>
/// <remarks>This abstract class defines the core contract for receiving and sending data packages. Derived
/// classes should override and extend its functionality to implement specific data handling logic. The default
/// implementation simply returns the provided data.</remarks>
public abstract class DataPackageHandlerBase : IDataPackageHandler
{
    /// <summary>
    /// 当接收数据处理完成后，回调该函数执行接收
    /// </summary>
    public Func<Memory<byte>, Task>? ReceivedCallBack { get; set; }

    /// <summary>
    /// Sends the specified data asynchronously to the target destination.
    /// </summary>
    /// <remarks>The method performs an asynchronous operation to send the provided data. The caller must
    /// ensure  that the data is valid and non-empty. The returned memory block may contain a response or acknowledgment
    /// depending on the implementation of the target destination.</remarks>
    /// <param name="data">The data to be sent, represented as a block of memory.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a  <see cref="Memory{T}"/> of <see
    /// cref="byte"/> representing the response or acknowledgment  received from the target destination.</returns>
    public virtual Task<Memory<byte>> SendAsync(Memory<byte> data)
    {
        return Task.FromResult(data);
    }

    /// <summary>
    /// Asynchronously receives data and writes it into the specified memory buffer.
    /// </summary>
    /// <remarks>The method does not guarantee that the entire buffer will be filled. The amount of data
    /// written depends on the data available to be received.</remarks>
    /// <param name="data">The memory buffer where the received data will be written. The buffer must be large enough to hold the incoming
    /// data.</param>
    /// <returns>A task that represents the asynchronous receive operation.</returns>
    public virtual Task ReceiveAsync(Memory<byte> data)
    {
        return Task.FromResult(data);
    }
}
