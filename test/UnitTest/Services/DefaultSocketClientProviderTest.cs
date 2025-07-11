// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;

namespace UnitTest.Services;

public class DefaultSocketClientProviderTest
{
    [Fact]
    public async Task DefaultSocketClient_Ok()
    {
        var sc = new ServiceCollection();
        sc.AddBootstrapBlazorTcpSocketFactory();
        var provider = sc.BuildServiceProvider();
        var clientProvider = provider.GetRequiredService<ISocketClientProvider>();

        // 未建立连接时 IsConnected 应为 false
        Assert.False(clientProvider.IsConnected);

        // 未建立连接直接调用 ReceiveAsync 方法
        var buffer = new byte[1024];
        var len = await clientProvider.ReceiveAsync(buffer);
        Assert.Equal(0, len);
    }

    [Fact]
    public async Task ReceiveAsync_Ok()
    {
        var port = 8100;
        // 测试接收数据时服务器断开未连接的情况
        StartTcpServer(port);

        var sc = new ServiceCollection();
        sc.AddBootstrapBlazorTcpSocketFactory();
        var provider = sc.BuildServiceProvider();
        var factory = provider.GetRequiredService<ITcpSocketFactory>();
        var client = factory.GetOrCreate("provider", op =>
        {
            op.LocalEndPoint = Utility.ConvertToIpEndPoint("localhost", 0);
            op.IsAutoReceive = false;
            op.EnableLog = false;
        });

        await client.ConnectAsync("127.0.0.1", port);
        Assert.True(client.IsConnected);

        var buffer = await client.ReceiveAsync();
        Assert.Equal(2, buffer.Length);

        await Task.Delay(50);
        buffer = await client.ReceiveAsync();
        Assert.False(client.IsConnected);
    }

    [Fact]
    public void SocketClientOptions_Ok()
    {
        var options = new SocketClientOptions
        {
            ReceiveBufferSize = 1024 * 64,
            IsAutoReceive = true,
            ConnectTimeout = 1000,
            SendTimeout = 500,
            ReceiveTimeout = 500,
            LocalEndPoint = new IPEndPoint(IPAddress.Loopback, 0)
        };
        Assert.Equal(1024 * 64, options.ReceiveBufferSize);
        Assert.True(options.IsAutoReceive);
        Assert.Equal(1000, options.ConnectTimeout);
        Assert.Equal(500, options.SendTimeout);
        Assert.Equal(500, options.ReceiveTimeout);
        Assert.Equal(new IPEndPoint(IPAddress.Loopback, 0), options.LocalEndPoint);
    }

    private static TcpListener StartTcpServer(int port)
    {
        var server = new TcpListener(IPAddress.Loopback, port);
        server.Start();
        Task.Run(() => AcceptClientsAsync(server));
        return server;
    }

    private static async Task AcceptClientsAsync(TcpListener server)
    {
        while (true)
        {
            var client = await server.AcceptTcpClientAsync();
            _ = Task.Run(async () =>
            {
                using var stream = client.GetStream();
                while (true)
                {
                    var buffer = new byte[1024];

                    // 模拟拆包发送第二段数据
                    await stream.WriteAsync(new byte[] { 0x3, 0x4 }, CancellationToken.None);

                    // 等待 20ms
                    await Task.Delay(20);
                    client.Close();
                }
            });
        }
    }
}
