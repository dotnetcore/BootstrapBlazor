// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Collections.Concurrent;
using System.Runtime.Versioning;

namespace BootstrapBlazor.Components;

[UnsupportedOSPlatform("browser")]
sealed class DefaultTcpSocketFactory(IServiceProvider provider) : ITcpSocketFactory
{
    private readonly ConcurrentDictionary<string, ITcpSocketClient> _pool = new();

    public ITcpSocketClient GetOrCreate(string name, Action<SocketClientOptions> valueFactory)
    {
        return _pool.GetOrAdd(name, key =>
        {
            var options = new SocketClientOptions();
            valueFactory(options);
            var client = new DefaultTcpSocketClient(options)
            {
                ServiceProvider = provider,
            };
            return client;
        });
    }

    public ITcpSocketClient? Remove(string name)
    {
        ITcpSocketClient? client = null;
        if (_pool.TryRemove(name, out var c))
        {
            client = c;
        }
        return client;
    }

    private async ValueTask DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            // 释放托管资源
            foreach (var socket in _pool.Values)
            {
                await socket.DisposeAsync();
            }
            _pool.Clear();
        }
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
