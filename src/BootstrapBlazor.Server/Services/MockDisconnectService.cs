// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Longbow.Tasks.Services;

/// <summary>
/// 模拟 Socket 自动断开服务端服务类
/// </summary>
internal class MockDisconnectServerService(ILogger<MockDisconnectServerService> logger) : BackgroundService
{
    /// <summary>
    /// 运行任务
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var server = new TcpListener(IPAddress.Loopback, 8901);
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
                // 发送数据
                await stream.WriteAsync(Encoding.UTF8.GetBytes(DateTime.Now.ToString("yyyyMMddHHmmss")), stoppingToken);
                await Task.Delay(2000, stoppingToken);

                // 主动关闭连接（关闭后 stream 已释放，必须退出循环）
                client.Close();
                break;
            }
            catch (OperationCanceledException) { break; }
            catch (ObjectDisposedException) { break; }
            catch (IOException) { break; }
            catch (SocketException) { break; }
            catch (Exception ex)
            {
                logger.LogError(ex, "MockDisconnectServerService encountered an error while sending data.");
                break;
            }
        }
    }
}
