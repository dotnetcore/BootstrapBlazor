// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Net;

namespace BootstrapBlazor.Components;

/// <summary>
/// Provides a base implementation for a TCP socket client, enabling connection, data transmission, and reception over
/// TCP.
/// </summary>
/// <remarks>This abstract class serves as a foundation for implementing TCP socket clients. It provides methods
/// for connecting to a remote endpoint, sending and receiving data, and managing connection state. Derived classes can
/// extend or customize the behavior as needed.</remarks>
public abstract class TcpSocketClientBase : ITcpSocketClient
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public abstract bool IsConnected { get; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public IPEndPoint? LocalEndPoint { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public int ReceiveBufferSize { get; set; } = 1024 * 64;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public bool IsAutoReceive { get; set; } = true;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public Func<ReadOnlyMemory<byte>, ValueTask>? ReceivedCallBack { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public int ConnectTimeout { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public int SendTimeout { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public int ReceiveTimeout { get; set; }

    /// <summary>
    /// Gets or sets the handler responsible for processing data packages.
    /// </summary>
    public IDataPackageHandler? DataPackageHandler { get; protected set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public virtual void SetDataHandler(IDataPackageHandler handler)
    {
        DataPackageHandler = handler;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="endPoint"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public abstract ValueTask<bool> ConnectAsync(IPEndPoint endPoint, CancellationToken token = default);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="data"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public abstract ValueTask<bool> SendAsync(ReadOnlyMemory<byte> data, CancellationToken token = default);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public abstract ValueTask<Memory<byte>> ReceiveAsync(CancellationToken token = default);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public virtual ValueTask CloseAsync()
    {
        return DisposeAsync(true);
    }

    /// <summary>
    /// Releases the resources used by the current instance of the class.
    /// </summary>
    /// <remarks>This method is called to free both managed and unmanaged resources. If the <paramref
    /// name="disposing"/> parameter is <see langword="true"/>, the method releases managed resources in addition to
    /// unmanaged resources. Override this method in a derived class to provide custom cleanup logic.</remarks>
    /// <param name="disposing"><see langword="true"/> to release both managed and unmanaged resources; <see langword="false"/> to release only
    /// unmanaged resources.</param>
    protected virtual ValueTask DisposeAsync(bool disposing)
    {
        return ValueTask.CompletedTask;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }
}
