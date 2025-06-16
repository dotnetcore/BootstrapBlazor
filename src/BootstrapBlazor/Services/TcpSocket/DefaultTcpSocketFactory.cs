// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Runtime.Versioning;

namespace BootstrapBlazor.Components;

[UnsupportedOSPlatform("browser")]
class DefaultTcpSocketFactory() : ITcpSocketFactory
{
    private readonly ConcurrentDictionary<string, ITcpSocketClient> _pool = new();

    public ITcpSocketClient GetOrCreate(string host, int port, SocketMode mode = SocketMode.Client)
    {
        return _pool.GetOrAdd($"{host}:{port}", key => new DefaultTcpSocketClient(host, port) { });
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            // 释放托管资源
            foreach (var socket in _pool.Values)
            {
                socket.Dispose();
            }
            _pool.Clear();
        }
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
