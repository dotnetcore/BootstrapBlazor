// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Net;
using System.Net.Sockets;

namespace Longbow.Tasks.Services;

/// <summary>
/// 模拟 Socket 服务端服务类
/// </summary>
internal class MockSocketServerService : BackgroundService
{
    /// <summary>
    /// 运行任务
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var server = new TcpListener(IPAddress.Loopback, 8800);
        server.Start();
        while (stoppingToken is { IsCancellationRequested: false })
        {
            try
            {
                var client = await server.AcceptTcpClientAsync(stoppingToken);
                _ = Task.Run(() => MockDelimiterPackageAsync(client), stoppingToken);
            }
            catch { }
        }
    }

    private static async Task MockDelimiterPackageAsync(TcpClient client)
    {
        using var stream = client.GetStream();
        while (true)
        {
            var buffer = new byte[10240];
            var len = await stream.ReadAsync(buffer);
            if (len == 0)
            {
                break;
            }

            // 回写数据到客户端
            var block = new ReadOnlyMemory<byte>(buffer, 0, len);
            await stream.WriteAsync(block, CancellationToken.None);

            await Task.Delay(20);

            // 模拟拆包发送第二段数据
            await stream.WriteAsync(new byte[] { 0x13, 0x10, 0x5, 0x6, 0x13, 0x10 }, CancellationToken.None);
        }
    }

}
