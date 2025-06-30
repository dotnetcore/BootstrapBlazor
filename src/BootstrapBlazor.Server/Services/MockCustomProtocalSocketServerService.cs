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
internal class MockCustomProtocolSocketServerService(ILogger<MockCustomProtocolSocketServerService> logger) : BackgroundService
{
    /// <summary>
    /// 运行任务
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var server = new TcpListener(IPAddress.Loopback, 8900);
        server.Start();
        while (stoppingToken is { IsCancellationRequested: false })
        {
            try
            {
                var client = await server.AcceptTcpClientAsync(stoppingToken);
                _ = Task.Run(() => OnDataHandlerAsync(client, stoppingToken), stoppingToken);
            }
            catch { }
        }
    }

    private async Task OnDataHandlerAsync(TcpClient client, CancellationToken stoppingToken)
    {
        // 方法目的:
        // 收到消息后发送自定义通讯协议的响应数据
        // 响应头 + 响应体
        await using var stream = client.GetStream();
        while (stoppingToken is { IsCancellationRequested: false })
        {
            try
            {
                // 接收数据
                var len = await stream.ReadAsync(new byte[1024], stoppingToken);
                if (len == 0)
                {
                    // 断开连接
                    break;
                }

                // 实际应用中需要解析接收到的数据进行处理，本示例中仅模拟接收数据后发送响应数据

                // 发送响应数据
                // 响应头: 4 字节表示响应体长度 [0x32, 0x30, 0x32, 0x35]
                // 响应体: 8 字节当前时间戳字符串 
                var data = new byte[12];
                "2025"u8.ToArray().CopyTo(data, 0);
                System.Text.Encoding.UTF8.GetBytes(DateTime.Now.ToString("ddHHmmss")).CopyTo(data, 4);
                await stream.WriteAsync(data, stoppingToken);
            }
            catch (OperationCanceledException) { break; }
            catch (IOException) { break; }
            catch (SocketException) { break; }
            catch (Exception ex)
            {
                logger.LogError(ex, "MockCustomProtocolSocketServerService encountered an error while sending data.");
                break;
            }
        }
    }
}
