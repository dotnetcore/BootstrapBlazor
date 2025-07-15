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
public class DataPackageAdapter : IDataPackageAdapter
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Func<ReadOnlyMemory<byte>, ValueTask>? ReceivedCallBack { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public IDataPackageHandler? DataPackageHandler { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="data"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public virtual async ValueTask HandlerAsync(ReadOnlyMemory<byte> data, CancellationToken token = default)
    {
        if (DataPackageHandler != null)
        {
            if (DataPackageHandler.ReceivedCallBack == null)
            {
                DataPackageHandler.ReceivedCallBack = OnHandlerReceivedCallBack;
            }

            // 如果存在数据处理器则调用其处理方法
            await DataPackageHandler.HandlerAsync(data, token);
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="data"></param>
    /// <param name="socketDataConverter"></param>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual bool TryConvertTo<TEntity>(ReadOnlyMemory<byte> data, ISocketDataConverter<TEntity> socketDataConverter, out TEntity? entity)
    {
        entity = default;
        var ret = socketDataConverter.TryConvertTo(data, out var v);
        if (ret)
        {
            entity = v;
        }
        return ret;
    }

    /// <summary>
    /// Handles incoming data by invoking a callback method, if one is defined.
    /// </summary>
    /// <remarks>This method is designed to be overridden in derived classes to provide custom handling of
    /// incoming data. If a callback method is assigned, it will be invoked asynchronously with the provided
    /// data.</remarks>
    /// <param name="data">The incoming data to be processed, represented as a read-only memory block of bytes.</param>
    /// <returns></returns>
    protected virtual async ValueTask OnHandlerReceivedCallBack(ReadOnlyMemory<byte> data)
    {
        if (ReceivedCallBack != null)
        {
            // 调用接收回调方法处理数据
            await ReceivedCallBack(data);
        }
    }
}
