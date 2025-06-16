// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;
using System.Net.Sockets;
using System.Runtime.Versioning;

namespace BootstrapBlazor.Components;

[UnsupportedOSPlatform("browser")]
class DefaultTcpSocketClient(string host, int port) : ITcpSocketClient
{
    private TcpClient? _client;

    public bool IsConnected => _client?.Connected ?? false;

    public ILogger? Logger { get; set; }

    public async Task<bool> ConnectAsync(CancellationToken token = default)
    {
        var ret = false;

        try
        {
            _client ??= new TcpClient(host, port);
            await _client.ConnectAsync(host, port, token);
            ret = true;
        }
        catch (Exception ex)
        {
            Logger?.LogError(ex, "TCP Socket connection failed to {Host}:{Port}", host, port);
        }
        return ret;
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            // 释放托管资源
            if (_client != null)
            {
                try
                {
                    _client.Close();
                }
                catch (Exception ex)
                {
                    Logger?.LogError(ex, "Error closing TCP Socket connection to {Host}:{Port}", host, port);
                }
                finally
                {
                    _client = null;
                }
            }
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
