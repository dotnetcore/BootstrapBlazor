// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;

namespace UnitTest.Services;

public class TcpSocketFactoryTest
{
    [Fact]
    public void GetOrCreate_Ok()
    {
        // 测试 GetOrCreate 方法创建的 Client 销毁后继续 GetOrCreate 得到的对象是否可用
        var sc = new ServiceCollection();
        sc.AddLogging(builder =>
        {
            builder.AddProvider(new MockLoggerProvider());
        });
        sc.AddBootstrapBlazorTcpSocketFactory();

        var provider = sc.BuildServiceProvider();
        var factory = provider.GetRequiredService<ITcpSocketFactory>();
        var client1 = factory.GetOrCreate("localhost", 0);
        client1.Close();

        var client2 = factory.GetOrCreate("localhost", 0);
        Assert.Equal(client1, client2);
    }

    [Fact]
    public async Task ConnectAsync_Cancel()
    {
        var client = CreateClient();

        // 测试 ConnectAsync 方法连接取消逻辑
        var cst = new CancellationTokenSource();
        cst.Cancel();
        var connect = await client.ConnectAsync("localhost", 9999, cst.Token);
        Assert.False(connect);
    }

    [Fact]
    public async Task ConnectAsync_Failed()
    {
        var client = CreateClient();

        // 测试 ConnectAsync 方法连接失败
        var connect = await client.ConnectAsync("localhost", 9999);
        Assert.False(connect);
    }

    [Fact]
    public async Task FixLengthDataPackageHandler_Ok()
    {
        var port = 8888;
        var server = StartTcpServer(port, MockSplitPackageAsync);

        var sc = new ServiceCollection();
        sc.AddLogging(builder =>
        {
            builder.AddProvider(new MockLoggerProvider());
        });
        sc.AddBootstrapBlazorTcpSocketFactory();

        var provider = sc.BuildServiceProvider();
        var factory = provider.GetRequiredService<ITcpSocketFactory>();
        var client = factory.GetOrCreate("localhost", 0);

        // 测试 ConnectAsync 方法
        var connect = await client.ConnectAsync("localhost", port);
        Assert.True(connect);
        Assert.True(client.IsConnected);

        var tcs = new TaskCompletionSource();
        Memory<byte> receivedBuffer = Memory<byte>.Empty;

        // 增加数据处理适配器
        client.SetDataHandler(new FixLengthDataPackageHandler(7)
        {
            ReceivedCallBack = buffer =>
            {
                receivedBuffer = buffer;
                tcs.SetResult();
                return Task.CompletedTask;
            }
        });

        // 测试 SendAsync 方法
        var data = new Memory<byte>([1, 2, 3, 4, 5]);
        var result = await client.SendAsync(data);
        Assert.True(result);

        await tcs.Task;
        Assert.Equal(receivedBuffer.ToArray(), [1, 2, 3, 4, 5, 3, 4]);

        // 模拟延时等待内部继续读取逻辑完成，测试内部 _receiveCancellationTokenSource 取消逻辑
        await Task.Delay(10);

        // 关闭连接
        client.Close();
        StopTcpServer(server);
    }

    [Fact]
    public async Task FixLengthDataPackageHandler_Sticky()
    {
        var port = 8899;
        var server = StartTcpServer(port, MockStickyPackageAsync);

        var sc = new ServiceCollection();
        sc.AddLogging(builder =>
        {
            builder.AddProvider(new MockLoggerProvider());
        });
        sc.AddBootstrapBlazorTcpSocketFactory();

        var provider = sc.BuildServiceProvider();
        var factory = provider.GetRequiredService<ITcpSocketFactory>();
        var client = factory.GetOrCreate("localhost", 0);

        // 连接 TCP Server
        var connect = await client.ConnectAsync("localhost", port);

        var tcs = new TaskCompletionSource();
        Memory<byte> receivedBuffer = Memory<byte>.Empty;

        // 增加数据库处理适配器
        client.SetDataHandler(new FixLengthDataPackageHandler(7)
        {
            ReceivedCallBack = buffer =>
            {
                receivedBuffer = buffer;
                tcs.SetResult();
                return Task.CompletedTask;
            }
        });

        // 发送数据
        var data = new Memory<byte>([1, 2, 3, 4, 5]);
        await client.SendAsync(data);

        // 等待接收数据处理完成
        await tcs.Task;

        // 验证接收到的数据
        Assert.Equal(receivedBuffer.ToArray(), [1, 2, 3, 4, 5, 3, 4]);

        // 等待第二次数据
        receivedBuffer = Memory<byte>.Empty;
        tcs = new TaskCompletionSource();
        await tcs.Task;

        // 验证第二次收到的数据
        Assert.Equal(receivedBuffer.ToArray(), [1, 2, 3, 4, 5, 6, 7]);

        // 关闭连接
        client.Close();
        StopTcpServer(server);
    }

    private static TcpListener StartTcpServer(int port, Func<TcpClient, Task> handler)
    {
        var server = new TcpListener(IPAddress.Loopback, port);
        server.Start();
        Task.Run(() => AcceptClientsAsync(server, handler));
        return server;
    }

    private static async Task AcceptClientsAsync(TcpListener server, Func<TcpClient, Task> handler)
    {
        while (true)
        {
            var client = await server.AcceptTcpClientAsync();
            _ = Task.Run(() => handler(client));
        }
    }

    private static async Task MockSplitPackageAsync(TcpClient client)
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

            // 模拟延时
            await Task.Delay(50);

            // 模拟拆包发送第二段数据
            await stream.WriteAsync(new byte[] { 0x3, 0x4 }, CancellationToken.None);
        }
    }

    private static async Task MockStickyPackageAsync(TcpClient client)
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

            // 模拟延时
            await Task.Delay(50);

            // 模拟拆包发送第二段数据
            await stream.WriteAsync(new byte[] { 0x3, 0x4, 0x1, 0x2 }, CancellationToken.None);

            // 模拟延时
            await Task.Delay(50);

            // 模拟粘包发送后续数据
            await stream.WriteAsync(new byte[] { 0x3, 0x4, 0x5, 0x6, 0x7 }, CancellationToken.None);
        }
    }

    private static void StopTcpServer(TcpListener server)
    {
        server?.Stop();
    }

    private static ITcpSocketClient CreateClient()
    {
        var sc = new ServiceCollection();
        sc.AddLogging(builder =>
        {
            builder.AddProvider(new MockLoggerProvider());
        });
        sc.AddBootstrapBlazorTcpSocketFactory();
        var provider = sc.BuildServiceProvider();
        var factory = provider.GetRequiredService<ITcpSocketFactory>();
        var client = factory.GetOrCreate("localhost", 0);
        return client;
    }

    class MockLoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new MockLogger();
        }

        public void Dispose()
        {

        }
    }

    class MockLogger : ILogger
    {
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {

        }
    }
}
