// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Reflection;
using System.Runtime.Versioning;
using System.Text;

namespace BootstrapBlazor.Components;

/// <summary>
/// <see cref="ITcpSocketClient"/> 扩展方法类
/// </summary>
[UnsupportedOSPlatform("browser")]
public static class ITcpSocketClientExtensions
{
    /// <summary>
    /// Sends the specified string content to the connected TCP socket client asynchronously.
    /// </summary>
    /// <remarks>This method converts the provided string content into a byte array using the specified
    /// encoding  (or UTF-8 by default) and sends it to the connected TCP socket client. Ensure the client is connected 
    /// before calling this method.</remarks>
    /// <param name="client">The TCP socket client to which the content will be sent. Cannot be <see langword="null"/>.</param>
    /// <param name="content">The string content to send. Cannot be <see langword="null"/> or empty.</param>
    /// <param name="encoding">The character encoding to use for converting the string content to bytes.  If <see langword="null"/>, UTF-8
    /// encoding is used by default.</param>
    /// <param name="token">A <see cref="CancellationToken"/> to observe while waiting for the operation to complete.</param>
    /// <returns>A <see cref="ValueTask{TResult}"/> that represents the asynchronous operation.  The result is <see
    /// langword="true"/> if the content was sent successfully; otherwise, <see langword="false"/>.</returns>
    public static ValueTask<bool> SendAsync(this ITcpSocketClient client, string content, Encoding? encoding = null, CancellationToken token = default)
    {
        var buffer = encoding?.GetBytes(content) ?? Encoding.UTF8.GetBytes(content);
        return client.SendAsync(buffer, token);
    }

    /// <summary>
    /// Establishes an asynchronous connection to the specified host and port.
    /// </summary>
    /// <param name="client">The TCP socket client to which the content will be sent. Cannot be <see langword="null"/>.</param>
    /// <param name="ipString">The hostname or IP address of the server to connect to. Cannot be null or empty.</param>
    /// <param name="port">The port number on the server to connect to. Must be a valid port number between 0 and 65535.</param>
    /// <param name="token">An optional <see cref="CancellationToken"/> to cancel the connection attempt. Defaults to <see
    /// langword="default"/> if not provided.</param>
    /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the connection
    /// is successfully established; otherwise, <see langword="false"/>.</returns>
    public static ValueTask<bool> ConnectAsync(this ITcpSocketClient client, string ipString, int port, CancellationToken token = default)
    {
        var endPoint = Utility.ConvertToIpEndPoint(ipString, port);
        return client.ConnectAsync(endPoint, token);
    }

    /// <summary>
    /// Configures the specified <see cref="ITcpSocketClient"/> to use the provided <see cref="IDataPackageAdapter"/> 
    /// for processing received data and sets a callback to handle processed data.
    /// </summary>
    /// <remarks>This method sets up a two-way data processing pipeline: <list type="bullet"> <item>
    /// <description>The <paramref name="client"/> is configured to pass received data to the <paramref name="adapter"/>
    /// for processing.</description> </item> <item> <description>The <paramref name="adapter"/> is configured to invoke
    /// the provided <paramref name="callback"/> with the processed data.</description> </item> </list> Use this method
    /// to integrate a custom data processing adapter with a TCP socket client.</remarks>
    /// <param name="client">The <see cref="ITcpSocketClient"/> instance to configure.</param>
    /// <param name="adapter">The <see cref="IDataPackageAdapter"/> used to process incoming data.</param>
    /// <param name="callback">A callback function invoked with the processed data. The function receives a <see cref="ReadOnlyMemory{T}"/> 
    /// containing the processed data and returns a <see cref="ValueTask"/>.</param>
    public static void SetDataPackageAdapter(this ITcpSocketClient client, IDataPackageAdapter adapter, Func<ReadOnlyMemory<byte>, ValueTask> callback)
    {
        // 设置 ITcpSocketClient 的回调函数
        client.ReceivedCallBack = async buffer =>
        {
            // 将接收到的数据传递给 DataPackageAdapter 进行数据处理合规数据触发 ReceivedCallBack 回调
            await adapter.HandlerAsync(buffer);
        };

        // 设置 DataPackageAdapter 的回调函数
        adapter.ReceivedCallBack = buffer => callback(buffer);
    }

    /// <summary>
    /// Configures the specified <see cref="ITcpSocketClient"/> to use a data package adapter and a callback function
    /// for processing received data.
    /// </summary>
    /// <remarks>This method sets up the <paramref name="client"/> to process incoming data using the
    /// specified <paramref name="adapter"/> and  <paramref name="socketDataConverter"/>. The <paramref
    /// name="callback"/> is called with the converted entity whenever data is received.</remarks>
    /// <typeparam name="TEntity">The type of the entity that the data will be converted to.</typeparam>
    /// <param name="client">The TCP socket client to configure.</param>
    /// <param name="adapter">The data package adapter responsible for handling incoming data.</param>
    /// <param name="socketDataConverter">The converter used to transform the received data into the specified entity type.</param>
    /// <param name="callback">The callback function to be invoked with the converted entity.</param>
    public static void SetDataPackageAdapter<TEntity>(this ITcpSocketClient client, IDataPackageAdapter adapter, ISocketDataConverter<TEntity> socketDataConverter, Func<TEntity?, Task> callback)
    {
        // 设置 ITcpSocketClient 的回调函数
        client.ReceivedCallBack = async buffer =>
        {
            // 将接收到的数据传递给 DataPackageAdapter 进行数据处理合规数据触发 ReceivedCallBack 回调
            await adapter.HandlerAsync(buffer);
        };

        // 设置 DataPackageAdapter 的回调函数
        adapter.ReceivedCallBack = async buffer =>
        {
            TEntity? ret = default;
            if (socketDataConverter.TryConvertTo(buffer, out var t))
            {
                ret = t;
            }
            await callback(ret);
        };
    }

    /// <summary>
    /// Configures the specified <see cref="ITcpSocketClient"/> to use a custom data package adapter and callback
    /// function.
    /// </summary>
    /// <remarks>This method sets up the <paramref name="client"/> to use the specified <paramref
    /// name="adapter"/> for handling incoming data. If the <typeparamref name="TEntity"/> type is decorated with a <see
    /// cref="SocketDataConverterAttribute"/>, the associated converter is used to transform the data before invoking
    /// the <paramref name="callback"/>. The callback is called with the converted entity or <see langword="null"/> if
    /// conversion fails.</remarks>
    /// <typeparam name="TEntity">The type of entity that the data package adapter will handle.</typeparam>
    /// <param name="client">The TCP socket client to configure.</param>
    /// <param name="adapter">The data package adapter responsible for processing incoming data.</param>
    /// <param name="callback">The callback function to invoke with the processed entity of type <typeparamref name="TEntity"/>.</param>
    public static void SetDataPackageAdapter<TEntity>(this ITcpSocketClient client, IDataPackageAdapter adapter, Func<TEntity?, Task> callback)
    {
        // 设置 ITcpSocketClient 的回调函数
        client.ReceivedCallBack = async buffer =>
        {
            // 将接收到的数据传递给 DataPackageAdapter 进行数据处理合规数据触发 ReceivedCallBack 回调
            await adapter.HandlerAsync(buffer);
        };

        var type = typeof(TEntity);
        var converterType = type.GetCustomAttribute<SocketDataConverterAttribute>();
        if (converterType is { Type: not null })
        {
            if (Activator.CreateInstance(converterType.Type) is ISocketDataConverter<TEntity> socketDataConverter)
            {
                // 设置 DataPackageAdapter 的回调函数
                adapter.ReceivedCallBack = async buffer =>
                {
                    TEntity? ret = default;
                    if (socketDataConverter.TryConvertTo(buffer, out var t))
                    {
                        ret = t;
                    }
                    await callback(ret);
                };
            }
        }
        else
        {
            adapter.ReceivedCallBack = async buffer => await callback(default);
        }
    }
}
