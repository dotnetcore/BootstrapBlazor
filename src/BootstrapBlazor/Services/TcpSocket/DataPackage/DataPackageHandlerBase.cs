// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Buffers;

namespace BootstrapBlazor.Components;

/// <summary>
/// Provides a base implementation for handling data packages in a communication system.
/// </summary>
/// <remarks>This abstract class defines the core contract for receiving and sending data packages. Derived
/// classes should override and extend its functionality to implement specific data handling logic. The default
/// implementation simply returns the provided data.</remarks>
public abstract class DataPackageHandlerBase : IDataPackageHandler
{
    private Memory<byte> _lastReceiveBuffer = Memory<byte>.Empty;

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
    /// Processes the received data asynchronously.
    /// </summary>
    /// <param name="data">A memory buffer containing the data to be processed. The buffer must not be empty.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public virtual Task ReceiveAsync(Memory<byte> data)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Handles the processing of a sticky package by adjusting the provided buffer and length.
    /// </summary>
    /// <remarks>This method processes the portion of the buffer beyond the specified length and updates the
    /// internal state accordingly. The caller must ensure that the <paramref name="buffer"/> contains sufficient data
    /// for the specified <paramref name="length"/>.</remarks>
    /// <param name="buffer">The memory buffer containing the data to process.</param>
    /// <param name="length">The length of the valid data within the buffer.</param>
    protected void HandlerStickyPackage(Memory<byte> buffer, int length)
    {
        var len = buffer.Length - length;
        if (len > 0)
        {
            var memoryBlock = MemoryPool<byte>.Shared.Rent(len);
            buffer[length..].CopyTo(memoryBlock.Memory);
            _lastReceiveBuffer = memoryBlock.Memory[..len];
        }
    }

    /// <summary>
    /// Concatenates the provided buffer with any previously stored data and returns the combined result.
    /// </summary>
    /// <remarks>This method combines the provided buffer with any data stored in the internal buffer.  After
    /// concatenation, the internal buffer is cleared. The returned memory block is allocated  from a shared memory pool
    /// and should be used promptly to avoid holding onto pooled resources.</remarks>
    /// <param name="buffer">The buffer to concatenate with the previously stored data. Must not be empty.</param>
    /// <returns>A <see cref="Memory{T}"/> instance containing the concatenated data.  If no previously stored data exists, the
    /// method returns the input <paramref name="buffer"/>.</returns>
    protected Memory<byte> ConcatBuffer(Memory<byte> buffer)
    {
        if (_lastReceiveBuffer.IsEmpty)
        {
            return buffer;
        }

        // 计算缓存区长度
        var len = _lastReceiveBuffer.Length + buffer.Length;

        // 申请缓存
        var memoryBlock = MemoryPool<byte>.Shared.Rent(len);

        // 拷贝数据到缓存区
        _lastReceiveBuffer.CopyTo(memoryBlock.Memory);
        buffer.CopyTo(memoryBlock.Memory[_lastReceiveBuffer.Length..]);

        // 清空粘包缓存数据
        _lastReceiveBuffer = Memory<byte>.Empty;
        return memoryBlock.Memory[..len];
    }
}
