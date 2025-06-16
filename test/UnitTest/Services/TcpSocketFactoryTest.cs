// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Net;
using System.Net.Sockets;

namespace UnitTest.Services;

public class TcpSocketFactoryTest
{
    [Fact]
    public async Task TcpSocketFactory_Ok()
    {
        var server = StartTcpServer();

        var sc = new ServiceCollection();
        sc.AddBootstrapBlazorTcpSocketFactory();

        var provider = sc.BuildServiceProvider();
        var factory = provider.GetRequiredService<ITcpSocketFactory>();
        var client = factory.GetOrCreate("localhost", 0);

        // 测试 ConnectAsync 方法
        var connect = await client.ConnectAsync("localhost", 8888);
        Assert.True(connect);
        Assert.True(client.IsConnected);

        // 测试 SendAsync 方法
        var data = new Memory<byte>([1, 2, 3, 4, 5]);
        var result = await client.SendAsync(data);
        Assert.True(result);

        // 测试 ReceiveAsync 方法
        var buffer = await client.ReceiveAsync();
        Assert.Equal(buffer.ToArray(), [1, 2, 3, 4, 5]);
        StopTcpServer(server);
    }

    private static TcpListener StartTcpServer()
    {
        var server = new TcpListener(IPAddress.Loopback, 8888);
        server.Start();
        Task.Run(AcceptClientsAsync);
        return server;

        async Task AcceptClientsAsync()
        {
            while (true)
            {
                var client = await server.AcceptTcpClientAsync();
                _ = Task.Run(() => HandleClientAsync(client));
            }
        }

        async Task HandleClientAsync(TcpClient client)
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
                var block = new Memory<byte>(buffer, 0, len);
                await stream.WriteAsync(block, CancellationToken.None);
            }
        }
    }

    private static void StopTcpServer(TcpListener server)
    {
        server?.Stop();
    }
}
