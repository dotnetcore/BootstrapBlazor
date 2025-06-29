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
class MockReceiveSocketServerService(ILogger<MockReceiveSocketServerService> logger) : BackgroundService
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
                _ = Task.Run(() => MockSendAsync(client, stoppingToken), stoppingToken);
            }
            catch { }
        }
    }

    private async Task MockSendAsync(TcpClient client, CancellationToken stoppingToken)
    {
        // 方法目的:
        // 1. 模拟服务器间隔 10秒 发送当前时间戳数据包到客户端
        await using var stream = client.GetStream();
        while (stoppingToken is { IsCancellationRequested: false })
        {
            try
            {
                var data = System.Text.Encoding.UTF8.GetBytes(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                await stream.WriteAsync(data, stoppingToken);

                await Task.Delay(10 * 1000, stoppingToken);
            }
            catch (OperationCanceledException) { break; }
            catch (IOException) { break; }
            catch (SocketException) { break; }
            catch (Exception ex)
            {
                logger.LogError(ex, "MockReceiveSocketServerService encountered an error while sending data.");
                break;
            }
        }
    }
}
