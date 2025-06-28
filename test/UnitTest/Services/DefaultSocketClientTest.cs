// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;
using System.Reflection;

namespace UnitTest.Services;

public class DefaultSocketClientTest
{
    [Fact]
    public void Logger_Null()
    {
        // 测试 Logger 为 null 的情况
        var client = CreateClient();
        var baseType = client.GetType().BaseType;
        Assert.NotNull(baseType);

        // 获取 Logger 字段设置为 null 测试 Log 不会抛出异常
        var propertyInfo = baseType.GetProperty("Logger", BindingFlags.Public | BindingFlags.Instance);
        Assert.NotNull(propertyInfo);

        propertyInfo.SetValue(client, null);

        var methodInfo = baseType.GetMethod("Log", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.NotNull(methodInfo);
        methodInfo.Invoke(client, [LogLevel.Information, null!, "Test log message"]);
    }

    [Fact]
    public async Task DefaultSocketClient_Ok()
    {
        var port = 8894;
        var server = StartTcpServer(port, MockDelimiterPackageAsync);
        var client = CreateClient();

        // 获得 Client 泛型属性
        var baseType = client.GetType().BaseType;
        Assert.NotNull(baseType);

        // 建立连接
        var connect = await client.ConnectAsync("localhost", port);
        Assert.True(connect);

        var propertyInfo = baseType.GetProperty("Client", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.NotNull(propertyInfo);
        var instance = propertyInfo.GetValue(client);
        Assert.NotNull(instance);

        ISocketClient socketClient = (ISocketClient)instance;
        Assert.NotNull(socketClient);
        Assert.True(socketClient.IsConnected);

        await socketClient.CloseAsync();
        Assert.False(socketClient.IsConnected);

        var buffer = new byte[10];
        var len = await socketClient.ReceiveAsync(buffer);
        Assert.Equal(0, len);
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
        var client = factory.GetOrCreate("test", op => op.LocalEndPoint = Utility.ConvertToIpEndPoint("localhost", 0));
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

    class MockSendErrorHandler : DataPackageHandlerBase
    {
        public ITcpSocketClient? Socket { get; set; }

        public override ValueTask<ReadOnlyMemory<byte>> SendAsync(ReadOnlyMemory<byte> data, CancellationToken token = default)
        {
            throw new Exception("Mock send failed");
        }
    }

    class MockSendCancelHandler : DataPackageHandlerBase
    {
        public ITcpSocketClient? Socket { get; set; }

        public override async ValueTask<ReadOnlyMemory<byte>> SendAsync(ReadOnlyMemory<byte> data, CancellationToken token = default)
        {
            if (Socket != null)
            {
                await Socket.CloseAsync();
            }
            await Task.Delay(10, token);
            return data;
        }
    }

    class MockReceiveErrorHandler : DataPackageHandlerBase
    {
        public override ValueTask<ReadOnlyMemory<byte>> SendAsync(ReadOnlyMemory<byte> data, CancellationToken token = default)
        {
            return ValueTask.FromResult(data);
        }

        public override async ValueTask ReceiveAsync(ReadOnlyMemory<byte> data, CancellationToken token = default)
        {
            await base.ReceiveAsync(data, token);

            // 模拟接收数据时报错
            throw new InvalidOperationException("Test Error");
        }
    }

    class MockSendTimeoutHandler : DataPackageHandlerBase
    {
        public override async ValueTask<ReadOnlyMemory<byte>> SendAsync(ReadOnlyMemory<byte> data, CancellationToken token = default)
        {
            // 模拟发送超时
            await Task.Delay(200, token);
            return data;
        }
    }

    class MockReceiveTimeoutHandler : DataPackageHandlerBase
    {
        public override async ValueTask ReceiveAsync(ReadOnlyMemory<byte> data, CancellationToken token = default)
        {
            // 模拟接收超时
            await Task.Delay(200, token);
            await base.ReceiveAsync(data, token);
        }
    }
}
