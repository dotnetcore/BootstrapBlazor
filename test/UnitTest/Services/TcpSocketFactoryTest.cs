// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;
using System.Buffers;
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
        sc.AddLogging(builder =>
        {
            builder.AddProvider(new MockLoggerProvider());
        });
        sc.AddBootstrapBlazorTcpSocketFactory();

        var provider = sc.BuildServiceProvider();
        var factory = provider.GetRequiredService<ITcpSocketFactory>();
        var client = factory.GetOrCreate("localhost", 0);

        // 测试 ConnectAsync 方法
        var connect = await client.ConnectAsync("localhost", 8888);
        Assert.True(connect);
        Assert.True(client.IsConnected);

        // 增加数据库处理适配器
        client.SetDataHandler(new MockDataHandler()
        {
            //ReceivedCallBack = buffer =>
            //{
            //}
        });

        // 测试 SendAsync 方法
        var data = new Memory<byte>([1, 2, 3, 4, 5]);
        var result = await client.SendAsync(data);
        Assert.True(result);

        //Assert.Equal(buffer.ToArray(), [1, 2, 3, 4, 5, 1, 2]);
        StopTcpServer(server);
    }

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
    public async Task FixLengthDataPackageHandler_Ok()
    {
        var server = StartDataPackageTcpServer();

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
        var connect = await client.ConnectAsync("localhost", 8899);
        Assert.True(connect);
        Assert.True(client.IsConnected);

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

        // 测试 SendAsync 方法
        var data = new Memory<byte>([1, 2, 3, 4, 5]);
        var result = await client.SendAsync(data);
        Assert.True(result);

        await tcs.Task;
        Assert.Equal(receivedBuffer.ToArray(), [1, 2, 3, 4, 5, 3, 4]);
        StopTcpServer(server);
    }

    private static TcpListener StartTcpServer(int port = 8888)
    {
        var server = new TcpListener(IPAddress.Loopback, port);
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

    private static TcpListener StartDataPackageTcpServer(int port = 8899)
    {
        var server = new TcpListener(IPAddress.Loopback, port);
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

                // 模拟延时
                await Task.Delay(50);

                // 模拟拆包发送第二段数据
                await stream.WriteAsync(new byte[2] { 0x3, 0x4 }, CancellationToken.None);
            }
        }
    }

    private static void StopTcpServer(TcpListener server)
    {
        server?.Stop();
    }

    class MockDataHandler : DataPackageHandlerBase
    {
        public override Task<Memory<byte>> ReceiveAsync(Memory<byte> data)
        {
            using var buffer = MemoryPool<byte>.Shared.Rent(data.Length + 2);
            data.CopyTo(buffer.Memory);
            buffer.Memory.Span[data.Length] = 0x01;
            buffer.Memory.Span[data.Length + 1] = 0x02;
            return Task.FromResult(buffer.Memory[..(data.Length + 2)]);
        }
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
