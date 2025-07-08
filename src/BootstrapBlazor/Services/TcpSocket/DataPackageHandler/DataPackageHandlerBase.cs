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
    private Memory<byte> _lastReceiveBuffer = Memory<byte>.Empty;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Func<ReadOnlyMemory<byte>, ValueTask>? ReceivedCallBack { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="data"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public abstract ValueTask HandlerAsync(ReadOnlyMemory<byte> data, CancellationToken token = default);

    /// <summary>
    /// Handles the processing of a sticky package by adjusting the provided buffer and length.
    /// </summary>
    /// <remarks>This method processes the portion of the buffer beyond the specified length and updates the
    /// internal state accordingly. The caller must ensure that the <paramref name="buffer"/> contains sufficient data
    /// for the specified <paramref name="length"/>.</remarks>
    /// <param name="buffer">The memory buffer containing the data to process.</param>
    /// <param name="length">The length of the valid data within the buffer.</param>
    protected void SlicePackage(ReadOnlyMemory<byte> buffer, int length)
    {
        _lastReceiveBuffer = buffer[length..].ToArray().AsMemory();
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
    protected ReadOnlyMemory<byte> ConcatBuffer(ReadOnlyMemory<byte> buffer)
    {
        if (_lastReceiveBuffer.IsEmpty)
        {
            return buffer;
        }

        // 计算缓存区长度
        Memory<byte> merged = new byte[_lastReceiveBuffer.Length + buffer.Length];
        _lastReceiveBuffer.CopyTo(merged);
        buffer.CopyTo(merged[_lastReceiveBuffer.Length..]);

        // Clear the sticky buffer
        _lastReceiveBuffer = Memory<byte>.Empty;
        return merged;
    }
}
