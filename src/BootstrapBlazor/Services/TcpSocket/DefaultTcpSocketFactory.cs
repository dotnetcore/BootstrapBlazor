// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Runtime.Versioning;

namespace BootstrapBlazor.Components;

[UnsupportedOSPlatform("browser")]
class DefaultTcpSocketFactory(IServiceProvider provider) : ITcpSocketFactory
{
    private readonly ConcurrentDictionary<string, ITcpSocketClient> _pool = new();

    public ITcpSocketClient GetOrCreate(string host, int port = 0)
    {
        return _pool.GetOrAdd($"{host}:{port}", key =>
        {
            var client = new DefaultTcpSocketClient(host, port)
            {
                Logger = provider.GetService<ILogger<DefaultTcpSocketClient>>()
            };
            return client;
        });
    }

    public ITcpSocketClient? Remove(string host, int port)
    {
        ITcpSocketClient? client = null;
        if (_pool.TryRemove($"{host}:{port}", out var c))
        {
            client = c;
        }
        return client;
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
