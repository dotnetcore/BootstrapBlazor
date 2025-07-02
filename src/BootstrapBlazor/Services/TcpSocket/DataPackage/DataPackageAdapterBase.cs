// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Provides a base implementation for adapting data packages between different systems or formats.
/// </summary>
/// <remarks>This abstract class serves as a foundation for implementing custom data package adapters. It defines
/// common methods for sending, receiving, and handling data packages, as well as a property for accessing the
/// associated data package handler. Derived classes should override the virtual methods to provide specific behavior
/// for handling data packages.</remarks>
public abstract class DataPackageAdapterBase : IDataPackageAdapter
{
    private TaskCompletionSource<bool> _taskCompletionSource = new();
    private Memory<byte> _buffer = Memory<byte>.Empty;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public IDataPackageHandler? DataPackageHandler { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public ITcpSocketClient? TcpSocketClient { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual async ValueTask<ReadOnlyMemory<byte>> ReceiveAsync(CancellationToken token = default)
    {
        var ret = Memory<byte>.Empty;
        var result = await _taskCompletionSource.Task;
        if (result)
        {
            // 如果成功返回缓冲区数据
            ret = _buffer;
        }
        return ret;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="data"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual ValueTask<bool> HandlerAsync(ReadOnlyMemory<byte> data, CancellationToken token = default)
    {
        if (DataPackageHandler != null)
        {
            // 如果存在数据处理器则调用其处理方法
            DataPackageHandler.ReceiveAsync(data, token);
        }
        return ValueTask.FromResult(true);
    }
}
