// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Longbow.TcpSocket;

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// <para lang="zh">浏览器 Socket 示例使用的内存客户端工厂。</para>
/// <para lang="en">In-memory client factory for browser Socket samples.</para>
/// </summary>
internal sealed class MockTcpSocketFactory(ILoggerFactory loggerFactory) : ITcpSocketFactory
{
    private readonly Dictionary<string, ITcpSocketClient> _clients = [];

    public ITcpSocketClient GetOrCreate(string name, Action<TcpSocketClientOptions>? configureOptions = null)
    {
        if (!string.IsNullOrEmpty(name) && _clients.TryGetValue(name, out var client))
        {
            return client;
        }

        var options = new TcpSocketClientOptions();
        configureOptions?.Invoke(options);
        client = new MockTcpSocketClient(options, loggerFactory.CreateLogger<MockTcpSocketClient>());
        if (!string.IsNullOrEmpty(name))
        {
            _clients.Add(name, client);
        }
        return client;
    }

    public ITcpSocketClient GetOrCreate(Action<TcpSocketClientOptions>? configureOptions = null) => GetOrCreate(string.Empty, configureOptions);

    public ITcpSocketClient? Remove(string name) => _clients.Remove(name, out var client) ? client : null;

    public async ValueTask DisposeAsync()
    {
        foreach (var client in _clients.Values)
        {
            await client.DisposeAsync();
        }
        _clients.Clear();
    }
}
