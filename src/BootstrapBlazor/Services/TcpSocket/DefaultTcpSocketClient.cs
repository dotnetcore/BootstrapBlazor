// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Net;
using System.Runtime.Versioning;

namespace BootstrapBlazor.Components;

[UnsupportedOSPlatform("browser")]
sealed class DefaultTcpSocketClient : TcpSocketClientBase<SocketClientBase>
{
    public DefaultTcpSocketClient(SocketClientOptions options)
    {
        ReceiveBufferSize = Math.Max(1024, options.ReceiveBufferSize);
        IsAutoReceive = options.IsAutoReceive;
        ConnectTimeout = options.ConnectTimeout;
        SendTimeout = options.SendTimeout;
        ReceiveTimeout = options.ReceiveTimeout;
        LocalEndPoint = options.LocalEndPoint;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="localEndPoint"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    protected override SocketClientBase CreateSocketClient(IPEndPoint localEndPoint)
    {
        return new SocketClientBase(localEndPoint);
    }
}
