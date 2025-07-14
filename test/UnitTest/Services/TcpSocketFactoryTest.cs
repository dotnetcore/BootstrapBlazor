// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;
using System.Buffers;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;

namespace UnitTest.Services;

public class TcpSocketFactoryTest
{
    [Fact]
    public async Task GetOrCreate_Ok()
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
        var client1 = factory.GetOrCreate("demo", op => op.LocalEndPoint = Utility.ConvertToIpEndPoint("localhost", 0));
        await client1.CloseAsync();

        var client2 = factory.GetOrCreate("demo", op => op.LocalEndPoint = Utility.ConvertToIpEndPoint("localhost", 0));
        Assert.Equal(client1, client2);

        var ip = Dns.GetHostAddresses(Dns.GetHostName(), AddressFamily.InterNetwork).FirstOrDefault() ?? IPAddress.Loopback;
        var client3 = factory.GetOrCreate("demo1", op => op.LocalEndPoint = Utility.ConvertToIpEndPoint(ip.ToString(), 0));

        // 测试不合格 IP 地址
        var client4 = factory.GetOrCreate("demo2", op => op.LocalEndPoint = Utility.ConvertToIpEndPoint("256.0.0.1", 0));

        var client5 = factory.Remove("demo2");
        Assert.Equal(client4, client5);
        Assert.NotNull(client5);

        await client5.DisposeAsync();
        await factory.DisposeAsync();
    }

    [Fact]
    public async Task ConnectAsync_Timeout()
    {
        var client = CreateClient(builder =>
        {
            // 增加发送报错 MockSocket
            builder.AddTransient<ISocketClientProvider, MockConnectTimeoutSocketProvider>();
        });
        client.Options.ConnectTimeout = 10;

        var connect = await client.ConnectAsync("localhost", 9999);
        Assert.False(connect);
    }

    [Fact]
    public async Task ConnectAsync_Cancel()
    {
        var client = CreateClient(builder =>
        {
            builder.AddTransient<ISocketClientProvider, MockConnectCancelSocketProvider>();
        },
        options =>
        {
            options.ConnectTimeout = 500;
        });

        // 测试 ConnectAsync 方法连接取消逻辑
        var cst = new CancellationTokenSource();
        cst.Cancel();
        var connect = await client.ConnectAsync("localhost", 9999, cst.Token);

        // 由于信号量被取消，所以连接会失败
        Assert.False(connect);

        // 测试真正的连接被取消逻辑
        cst = new CancellationTokenSource(200);
        connect = await client.ConnectAsync("localhost", 9999, cst.Token);
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
    public async Task ConnectAsync_Error()
    {
        var client = CreateClient();

        // 反射设置 SocketClientProvider 为空
        var propertyInfo = client.GetType().GetProperty("ServiceProvider", BindingFlags.Public | BindingFlags.Instance);
        Assert.NotNull(propertyInfo);
        propertyInfo.SetValue(client, null);

        // 测试 ConnectAsync 方法连接失败
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await client.ConnectAsync("localhost", 9999));
        Assert.NotNull(ex);

        // 反射测试 Log 方法
        var methodInfo = client.GetType().GetMethod("Log", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.NotNull(methodInfo);
        methodInfo.Invoke(client, [LogLevel.Error, null!, "Test error log"]);
    }

    [Fact]
    public async Task ConnectAsync_Lock()
    {
        // 测试并发锁问题
        var provider = new MockAutoReconnectLockSocketProvider();
        var client = CreateClient(builder =>
        {
            builder.AddTransient<ISocketClientProvider>(p => provider);
        });

        // 开 5 个线程同时连接
        _ = Task.Run(async () =>
        {
            // 延时 150 保证有一个连接失败
            await Task.Delay(150);
            provider.SetConnected(true);
        });
        var results = await Task.WhenAll(Enumerable.Range(1, 5).Select(i => client.ConnectAsync("localhost", 0).AsTask()));
        // 期望结果是 1个 false 4个 true
        Assert.Equal(1, results.Count(r => !r));
        Assert.Equal(4, results.Count(r => r));
    }

    [Fact]
    public async Task Send_Timeout()
    {
        var port = 8887;
        var server = StartTcpServer(port, MockSplitPackageAsync);

        var client = CreateClient(builder =>
        {
            // 增加发送报错 MockSocket
            builder.AddTransient<ISocketClientProvider, MockSendTimeoutSocketProvider>();
        });
        client.Options.SendTimeout = 10;

        await client.ConnectAsync("localhost", port);

        var data = new ReadOnlyMemory<byte>([1, 2, 3, 4, 5]);
        var result = await client.SendAsync(data);
        Assert.False(result);
    }

    [Fact]
    public async Task SendAsync_Error()
    {
        var client = CreateClient(builder =>
        {
            // 增加发送报错 MockSocket
            builder.AddTransient<ISocketClientProvider, MockSendErrorSocketProvider>();
        });

        // 测试未建立连接前调用 SendAsync 方法报异常逻辑
        var data = new ReadOnlyMemory<byte>([1, 2, 3, 4, 5]);
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await client.SendAsync(data));
        Assert.NotNull(ex);

        // 测试发送失败
        var port = 8892;
        var server = StartTcpServer(port, MockSplitPackageAsync);

        await client.ConnectAsync("localhost", port);
        Assert.True(client.IsConnected);

        // 内部生成异常日志
        await client.SendAsync(data);
    }

    [Fact]
    public async Task SendAsync_Cancel()
    {
        var port = 8881;
        var server = StartTcpServer(port, MockSplitPackageAsync);

        var client = CreateClient();
        Assert.False(client.IsConnected);

        // 连接 TCP Server
        await client.ConnectAsync("localhost", port);
        Assert.True(client.IsConnected);

        // 测试 SendAsync 方法发送取消逻辑
        var cst = new CancellationTokenSource();
        cst.Cancel();

        var result = await client.SendAsync("test", null, cst.Token);
        Assert.False(result);

        result = await client.SendAsync("test", Encoding.UTF8, cst.Token);
        Assert.False(result);

        // 关闭连接
        StopTcpServer(server);
    }

    [Fact]
    public async Task ReceiveAsync_Timeout()
    {
        var port = 8888;
        var server = StartTcpServer(port, MockSplitPackageAsync);

        var client = CreateClient();
        client.Options.ReceiveTimeout = 100;

        await client.ConnectAsync("localhost", port);

        var data = new ReadOnlyMemory<byte>([1, 2, 3, 4, 5]);
        await client.SendAsync(data);
        await Task.Delay(220); // 等待接收超时
    }

    [Fact]
    public async Task ReceiveAsync_Cancel()
    {
        var port = 8889;
        var server = StartTcpServer(port, MockSplitPackageAsync);

        var client = CreateClient();
        await client.ConnectAsync("localhost", port);

        var data = new ReadOnlyMemory<byte>([1, 2, 3, 4, 5]);
        await client.SendAsync(data);

        // 通过反射取消令牌
        var type = client.GetType();
        Assert.NotNull(type);

        var fieldInfo = type.GetField("_receiveCancellationTokenSource", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.NotNull(fieldInfo);
        var tokenSource = fieldInfo.GetValue(client) as CancellationTokenSource;
        Assert.NotNull(tokenSource);
        tokenSource.Cancel();
        await Task.Delay(50);
    }

    [Fact]
    public async Task ReceiveAsync_InvalidOperationException()
    {
        // 未连接时调用 ReceiveAsync 方法会抛出 InvalidOperationException 异常
        var client = CreateClient();
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await client.ReceiveAsync());
        Assert.NotNull(ex);

        // 已连接但是启用了自动接收功能时调用 ReceiveAsync 方法会抛出 InvalidOperationException 异常
        var port = 8893;
        var server = StartTcpServer(port, MockSplitPackageAsync);

        client.Options.IsAutoReceive = true;
        var connected = await client.ConnectAsync("localhost", port);
        Assert.True(connected);

        ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await client.ReceiveAsync());
        Assert.NotNull(ex);
    }

    [Fact]
    public async Task ReceiveAsync_Ok()
    {
        var onConnecting = false;
        var onConnected = false;
        var port = 8891;
        var server = StartTcpServer(port, MockSplitPackageAsync);

        var client = CreateClient();
        client.Options.IsAutoReceive = false;
        client.OnConnecting = () =>
        {
            onConnecting = true;
            return Task.CompletedTask;
        };
        client.OnConnected = () =>
        {
            onConnected = true;
            return Task.CompletedTask;
        };
        var connected = await client.ConnectAsync("localhost", port);
        Assert.True(connected);
        Assert.True(onConnecting);
        Assert.True(onConnected);

        var data = new ReadOnlyMemory<byte>([1, 2, 3, 4, 5]);
        var send = await client.SendAsync(data);
        Assert.True(send);

        // 未设置数据处理器未开启自动接收时，调用 ReceiveAsync 方法获取数据
        // 需要自己处理粘包分包和业务问题
        var payload = await client.ReceiveAsync();
        Assert.Equal([1, 2, 3, 4, 5], payload.ToArray());

        // 由于服务器端模拟了拆包发送第二段数据，所以这里可以再次调用 ReceiveAsync 方法获取第二段数据
        payload = await client.ReceiveAsync();
        Assert.Equal([3, 4], payload.ToArray());
    }

    [Fact]
    public async Task ReceiveAsync_Error()
    {
        var client = CreateClient();

        // 测试未建立连接前调用 ReceiveAsync 方法报异常逻辑
        var type = client.GetType();
        Assert.NotNull(type);

        var methodInfo = type.GetMethod("AutoReceiveAsync", BindingFlags.NonPublic | BindingFlags.Instance);
        Assert.NotNull(methodInfo);

        var task = (ValueTask)methodInfo.Invoke(client, null)!;
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(async () => await task);
        Assert.NotNull(ex);

        var port = 8882;
        var server = StartTcpServer(port, MockSplitPackageAsync);

        Assert.Equal(1024 * 64, client.Options.ReceiveBufferSize);

        client.Options.ReceiveBufferSize = 1024 * 20;
        Assert.Equal(1024 * 20, client.Options.ReceiveBufferSize);

        ReadOnlyMemory<byte> buffer = ReadOnlyMemory<byte>.Empty;
        var tcs = new TaskCompletionSource();

        // 增加接收回调方法
        client.ReceivedCallBack = b =>
        {
            buffer = b;
            tcs.SetResult();
            return ValueTask.CompletedTask;
        };

        await client.ConnectAsync("localhost", port);

        // 发送数据导致接收数据异常
        var data = new ReadOnlyMemory<byte>([1, 2, 3, 4, 5]);
        await client.SendAsync(data);

        await tcs.Task;
        Assert.Equal([1, 2, 3, 4, 5], buffer.ToArray());

        // 关闭连接
        StopTcpServer(server);
    }

    [Fact]
    public async Task AutoReconnect_Ok()
    {
        var client = CreateClient(optionConfigure: options =>
        {
            options.IsAutoReconnect = true;
            options.ReconnectInterval = 200;
            options.IsAutoReceive = true;
        });

        // 使用场景自动接收数据，短线后自动重连
        var port = 8894;
        var connect = await client.ConnectAsync("localhost", port);
        Assert.False(connect);

        // 开启服务端后，可以自动重连上
        var server = StartTcpServer(port, LoopSendPackageAsync);
        await Task.Delay(250);
        Assert.True(client.IsConnected);

        await client.DisposeAsync();
    }

    [Fact]
    public async Task AutoReconnect_False()
    {
        var provider = new MockAutoReconnectSocketProvider();
        var client = CreateClient(builder =>
        {
            // 增加发送报错 MockSocket
            builder.AddTransient<ISocketClientProvider>(p => provider);
        },
        optionConfigure: options =>
        {
            options.IsAutoReconnect = true;
            options.ReconnectInterval = 200;
            options.IsAutoReceive = true;
        });

        // 使用场景自动接收数据，短线后自动重连
        var connect = await client.ConnectAsync("localhost", 0);
        Assert.False(connect);

        provider.SetConnected(true);
        await Task.Delay(250);
        Assert.True(client.IsConnected);
    }

    [Fact]
    public async Task AutoReconnect_Send_Ok()
    {
        // 发送数据时连接断开了，测试重连功能
        var provider = new MockAutoReconnectSocketProvider();
        var client = CreateClient(builder =>
        {
            // 增加发送报错 MockSocket
            builder.AddTransient<ISocketClientProvider>(p => provider);
        }, optionConfigure: options =>
        {
            options.IsAutoReconnect = true;
            options.ReconnectInterval = 200;
            options.IsAutoReceive = true;
        });

        provider.SetConnected(true);
        var connect = await client.ConnectAsync("localhost", 0);
        Assert.True(connect);

        // 发送时断开连接
        provider.SetSend(false);
        var send = await client.SendAsync("test");
        Assert.False(send);

        await Task.Delay(250);
        Assert.True(client.IsConnected);
    }

    [Fact]
    public async Task AutoReconnect_Receive_Ok()
    {
        // 接收数据时连接断开了，测试重连功能
        var provider = new MockAutoReconnectSocketProvider();
        var client = CreateClient(builder =>
        {
            // 增加发送报错 MockSocket
            builder.AddTransient<ISocketClientProvider>(p => provider);
        }, optionConfigure: options =>
        {
            options.IsAutoReconnect = true;
            options.ReconnectInterval = 200;
            options.IsAutoReceive = false;
        });

        provider.SetConnected(true);
        var connect = await client.ConnectAsync("localhost", 0);
        Assert.True(connect);

        // 发送时断开连接
        provider.SetReceive(false);
        var buffer = await client.ReceiveAsync();
        Assert.Equal(Memory<byte>.Empty, buffer);

        await Task.Delay(250);
        provider.SetReceive(true);
        buffer = await client.ReceiveAsync();
        Assert.Equal(5, buffer.Length);
    }

    [Fact]
    public async Task AutoReconnect_Cancel()
    {
        // 测试重连时取消逻辑
        var provider = new MockAutoReconnectSocketProvider();
        var client = CreateClient(builder =>
        {
            // 增加发送报错 MockSocket
            builder.AddTransient<ISocketClientProvider>(p => provider);
        }, optionConfigure: options =>
        {
            options.IsAutoReconnect = true;
            options.ReconnectInterval = 2000;
            options.IsAutoReceive = false;
        });

        await client.ConnectAsync("localhost", 0);
        await Task.Delay(100);
        await client.DisposeAsync();
    }

    [Fact]
    public async Task FixLengthDataPackageHandler_Ok()
    {
        var port = 8884;
        var server = StartTcpServer(port, MockSplitPackageAsync);
        var client = CreateClient();
        var tcs = new TaskCompletionSource();
        var receivedBuffer = new byte[1024];

        // 设置数据适配器
        var adapter = new DataPackageAdapter
        {
            DataPackageHandler = new FixLengthDataPackageHandler(7)
        };
        client.SetDataPackageAdapter(adapter, buffer =>
        {
            // buffer 即是接收到的数据
            buffer.CopyTo(receivedBuffer);
            receivedBuffer = receivedBuffer[..buffer.Length];
            tcs.SetResult();
            return ValueTask.CompletedTask;
        });

        // 测试 ConnectAsync 方法
        var connect = await client.ConnectAsync("localhost", port);
        Assert.True(connect);
        Assert.True(client.IsConnected);

        // 测试 SendAsync 方法
        var data = new ReadOnlyMemory<byte>([1, 2, 3, 4, 5]);
        var result = await client.SendAsync(data);
        Assert.True(result);

        await tcs.Task;
        Assert.Equal([1, 2, 3, 4, 5, 3, 4], receivedBuffer.ToArray());

        // 关闭连接
        await client.CloseAsync();
        StopTcpServer(server);
    }

    [Fact]
    public async Task FixLengthDataPackageHandler_Sticky()
    {
        var port = 8885;
        var server = StartTcpServer(port, MockStickyPackageAsync);
        var client = CreateClient();
        var tcs = new TaskCompletionSource();
        var receivedBuffer = new byte[128];

        // 连接 TCP Server
        var connect = await client.ConnectAsync("localhost", port);

        // 设置数据适配器
        var adapter = new DataPackageAdapter
        {
            DataPackageHandler = new FixLengthDataPackageHandler(7)
        };

        client.SetDataPackageAdapter(adapter, buffer =>
        {
            // buffer 即是接收到的数据
            buffer.CopyTo(receivedBuffer);
            receivedBuffer = receivedBuffer[..buffer.Length];
            tcs.SetResult();
            return ValueTask.CompletedTask;
        });

        // 发送数据
        var data = new ReadOnlyMemory<byte>([1, 2, 3, 4, 5]);
        await client.SendAsync(data);

        // 等待接收数据处理完成
        await tcs.Task;

        // 验证接收到的数据
        Assert.Equal([1, 2, 3, 4, 5, 3, 4], receivedBuffer.ToArray());

        // 重置接收缓冲区
        receivedBuffer = new byte[1024];
        tcs = new TaskCompletionSource();

        // 等待第二次数据
        await tcs.Task;

        // 验证第二次收到的数据
        Assert.Equal([2, 2, 3, 4, 5, 6, 7], receivedBuffer.ToArray());
        tcs = new TaskCompletionSource();
        await tcs.Task;

        // 验证第三次收到的数据
        Assert.Equal([3, 2, 3, 4, 5, 6, 7], receivedBuffer.ToArray());

        // 关闭连接
        await client.CloseAsync();
        StopTcpServer(server);
    }

    [Fact]
    public async Task DelimiterDataPackageHandler_Ok()
    {
        var port = 8883;
        var server = StartTcpServer(port, MockDelimiterPackageAsync);
        var client = CreateClient();
        var tcs = new TaskCompletionSource();
        var receivedBuffer = new byte[128];

        // 设置数据适配器
        var adapter = new DataPackageAdapter
        {
            DataPackageHandler = new DelimiterDataPackageHandler([13, 10]),
        };
        client.SetDataPackageAdapter(adapter, buffer =>
        {
            // buffer 即是接收到的数据
            buffer.CopyTo(receivedBuffer);
            receivedBuffer = receivedBuffer[..buffer.Length];
            tcs.SetResult();
            return ValueTask.CompletedTask;
        });

        // 连接 TCP Server
        var connect = await client.ConnectAsync("localhost", port);

        // 发送数据
        var data = new ReadOnlyMemory<byte>([1, 2, 3, 4, 5]);
        await client.SendAsync(data);

        // 等待接收数据处理完成
        await tcs.Task;

        // 验证接收到的数据
        Assert.Equal([1, 2, 3, 4, 5, 13, 10], receivedBuffer.ToArray());

        // 等待第二次数据
        receivedBuffer = new byte[1024];
        tcs = new TaskCompletionSource();
        await tcs.Task;

        // 验证接收到的数据
        Assert.Equal([5, 6, 13, 10], receivedBuffer.ToArray());

        // 关闭连接
        await client.CloseAsync();
        StopTcpServer(server);

        var handler = new DelimiterDataPackageHandler("\r\n");
        var ex = Assert.Throws<ArgumentNullException>(() => new DelimiterDataPackageHandler(string.Empty));
        Assert.NotNull(ex);

        ex = Assert.Throws<ArgumentNullException>(() => new DelimiterDataPackageHandler(null!));
        Assert.NotNull(ex);
    }

    [Fact]
    public async Task TryConvertTo_Ok()
    {
        var port = 8886;
        var server = StartTcpServer(port, MockSplitPackageAsync);
        var client = CreateClient();
        var tcs = new TaskCompletionSource();
        MockEntity? entity = null;

        // 设置数据适配器
        var adapter = new MockEntityDataPackageAdapter
        {
            DataPackageHandler = new FixLengthDataPackageHandler(7),
        };
        client.SetDataPackageAdapter<MockEntity>(adapter, t =>
        {
            entity = t;
            tcs.SetResult();
            return Task.CompletedTask;
        });

        // 连接 TCP Server
        var connect = await client.ConnectAsync("localhost", port);

        // 发送数据
        var data = new ReadOnlyMemory<byte>([1, 2, 3, 4, 5]);
        await client.SendAsync(data);
        await tcs.Task;

        Assert.NotNull(entity);
        Assert.Equal([1, 2, 3, 4, 5], entity.Header);
        Assert.Equal([3, 4], entity.Body);

        // 测试异常流程
        var adapter2 = new DataPackageAdapter();
        var result = adapter2.TryConvertTo(data, out var t);
        Assert.False(result);
        Assert.Null(t);
    }

    [Fact]
    public async Task TryConvertTo_Null()
    {
        var port = 8890;
        var server = StartTcpServer(port, MockSplitPackageAsync);
        var client = CreateClient();
        var tcs = new TaskCompletionSource();
        MockEntity? entity = null;

        // 设置数据适配器
        var adapter = new MockErrorEntityDataPackageAdapter
        {
            DataPackageHandler = new FixLengthDataPackageHandler(7),
        };
        client.SetDataPackageAdapter<MockEntity>(adapter, t =>
        {
            entity = t;
            tcs.SetResult();
            return Task.CompletedTask;
        });

        // 连接 TCP Server
        var connect = await client.ConnectAsync("localhost", port);

        // 发送数据
        var data = new ReadOnlyMemory<byte>([1, 2, 3, 4, 5]);
        await client.SendAsync(data);
        await tcs.Task;

        Assert.Null(entity);
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
            await stream.WriteAsync(new byte[] { 13, 10, 0x5, 0x6, 13, 10 }, CancellationToken.None);
        }
    }

    private static async Task MockSplitPackageAsync(TcpClient client)
    {
        using var stream = client.GetStream();
        while (true)
        {
            var buffer = new byte[1024];
            var len = await stream.ReadAsync(buffer);
            if (len == 0)
            {
                break;
            }

            // 回写数据到客户端
            var block = new ReadOnlyMemory<byte>(buffer, 0, len);
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
            var block = new ReadOnlyMemory<byte>(buffer, 0, len);
            await stream.WriteAsync(block, CancellationToken.None);

            // 模拟延时
            await Task.Delay(10);

            // 模拟拆包发送第二段数据
            await stream.WriteAsync(new byte[] { 0x3, 0x4, 0x2, 0x2 }, CancellationToken.None);

            // 模拟延时
            await Task.Delay(10);

            // 模拟粘包发送后续数据
            await stream.WriteAsync(new byte[] { 0x3, 0x4, 0x5, 0x6, 0x7, 0x3, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x1 }, CancellationToken.None);
        }
    }

    private static async Task LoopSendPackageAsync(TcpClient client)
    {
        using var stream = client.GetStream();
        while (true)
        {
            // 模拟发送数据
            var data = new byte[] { 1, 2, 3, 4, 5 };
            await stream.WriteAsync(data, CancellationToken.None);
            // 模拟延时
            await Task.Delay(500);
        }
    }

    private static void StopTcpServer(TcpListener server)
    {
        server?.Stop();
    }

    private static ITcpSocketClient CreateClient(Action<ServiceCollection>? builder = null, Action<SocketClientOptions>? optionConfigure = null)
    {
        var sc = new ServiceCollection();
        sc.AddLogging(builder =>
        {
            builder.AddProvider(new MockLoggerProvider());
        });
        sc.AddBootstrapBlazorTcpSocketFactory();
        builder?.Invoke(sc);

        var provider = sc.BuildServiceProvider();
        var factory = provider.GetRequiredService<ITcpSocketFactory>();
        var client = factory.GetOrCreate("test", op =>
        {
            op.LocalEndPoint = Utility.ConvertToIpEndPoint("localhost", 0);
            op.EnableLog = true;
            optionConfigure?.Invoke(op);
        });
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

    class MockSendErrorSocketProvider : ISocketClientProvider
    {
        public bool IsConnected { get; private set; }

        public IPEndPoint LocalEndPoint { get; set; } = new IPEndPoint(IPAddress.Any, 0);

        public ValueTask CloseAsync()
        {
            return ValueTask.CompletedTask;
        }

        public ValueTask<bool> ConnectAsync(IPEndPoint endPoint, CancellationToken token = default)
        {
            IsConnected = true;
            return ValueTask.FromResult(true);
        }

        public ValueTask<int> ReceiveAsync(Memory<byte> buffer, CancellationToken token = default)
        {
            return ValueTask.FromResult(0);
        }

        public ValueTask<bool> SendAsync(ReadOnlyMemory<byte> data, CancellationToken token = default)
        {
            throw new Exception("Mock send error");
        }
    }

    class MockConnectTimeoutSocketProvider : ISocketClientProvider
    {
        public bool IsConnected { get; private set; }

        public IPEndPoint LocalEndPoint { get; set; } = new IPEndPoint(IPAddress.Any, 0);

        public ValueTask CloseAsync()
        {
            return ValueTask.CompletedTask;
        }

        public async ValueTask<bool> ConnectAsync(IPEndPoint endPoint, CancellationToken token = default)
        {
            await Task.Delay(1000, token);
            IsConnected = false;
            return false;
        }

        public ValueTask<int> ReceiveAsync(Memory<byte> buffer, CancellationToken token = default)
        {
            return ValueTask.FromResult(0);
        }

        public ValueTask<bool> SendAsync(ReadOnlyMemory<byte> data, CancellationToken token = default)
        {
            return ValueTask.FromResult(true);
        }
    }

    class MockConnectCancelSocketProvider : ISocketClientProvider
    {
        public bool IsConnected { get; private set; }

        public IPEndPoint LocalEndPoint { get; set; } = new IPEndPoint(IPAddress.Any, 0);

        public ValueTask CloseAsync()
        {
            return ValueTask.CompletedTask;
        }

        public async ValueTask<bool> ConnectAsync(IPEndPoint endPoint, CancellationToken token = default)
        {
            await Task.Delay(250, token);
            return false;
        }

        public ValueTask<int> ReceiveAsync(Memory<byte> buffer, CancellationToken token = default)
        {
            return ValueTask.FromResult(0);
        }

        public ValueTask<bool> SendAsync(ReadOnlyMemory<byte> data, CancellationToken token = default)
        {
            return ValueTask.FromResult(true);
        }
    }

    class MockSendTimeoutSocketProvider : ISocketClientProvider
    {
        public bool IsConnected { get; private set; }

        public IPEndPoint LocalEndPoint { get; set; } = new IPEndPoint(IPAddress.Any, 0);

        public ValueTask CloseAsync()
        {
            return ValueTask.CompletedTask;
        }

        public ValueTask<bool> ConnectAsync(IPEndPoint endPoint, CancellationToken token = default)
        {
            IsConnected = true;
            return ValueTask.FromResult(true);
        }

        public ValueTask<int> ReceiveAsync(Memory<byte> buffer, CancellationToken token = default)
        {
            return ValueTask.FromResult(0);
        }

        public async ValueTask<bool> SendAsync(ReadOnlyMemory<byte> data, CancellationToken token = default)
        {
            // 模拟超时发送
            await Task.Delay(100, token);
            return false;
        }
    }

    class MockAutoReconnectLockSocketProvider : ISocketClientProvider
    {
        public bool IsConnected { get; private set; }

        public IPEndPoint LocalEndPoint { get; set; } = new IPEndPoint(IPAddress.Loopback, 0);

        public async ValueTask<bool> ConnectAsync(IPEndPoint endPoint, CancellationToken token = default)
        {
            await Task.Delay(100, token);
            return IsConnected;
        }

        public ValueTask<bool> SendAsync(ReadOnlyMemory<byte> data, CancellationToken token = default)
        {
            return ValueTask.FromResult(true);
        }

        public ValueTask<int> ReceiveAsync(Memory<byte> buffer, CancellationToken token = default)
        {
            byte[] data = [1, 2, 3, 4, 5];
            data.CopyTo(buffer);
            return ValueTask.FromResult(5);
        }

        public ValueTask CloseAsync()
        {
            return ValueTask.CompletedTask;
        }

        public void SetConnected(bool state)
        {
            IsConnected = state;
        }
    }

    class MockAutoReconnectSocketProvider : ISocketClientProvider
    {
        public bool IsConnected { get; private set; }

        public IPEndPoint LocalEndPoint { get; set; } = new IPEndPoint(IPAddress.Loopback, 0);

        public ValueTask<bool> ConnectAsync(IPEndPoint endPoint, CancellationToken token = default)
        {
            return ValueTask.FromResult(IsConnected);
        }

        private bool _sendState = true;
        public ValueTask<bool> SendAsync(ReadOnlyMemory<byte> data, CancellationToken token = default)
        {
            return ValueTask.FromResult(_sendState);
        }

        private bool _receiveState = true;
        public ValueTask<int> ReceiveAsync(Memory<byte> buffer, CancellationToken token = default)
        {
            if (_receiveState)
            {
                byte[] data = [1, 2, 3, 4, 5];
                data.CopyTo(buffer);
                return ValueTask.FromResult(5);
            }
            else
            {
                return ValueTask.FromResult(0);
            }
        }

        public ValueTask CloseAsync()
        {
            return ValueTask.CompletedTask;
        }

        public void SetConnected(bool state)
        {
            IsConnected = state;
        }

        public void SetSend(bool state)
        {
            _sendState = state;
        }

        public void SetReceive(bool state)
        {
            _receiveState = state;
        }
    }

    class MockEntityDataPackageAdapter : DataPackageAdapter
    {
        public override bool TryConvertTo(ReadOnlyMemory<byte> data, [NotNullWhen(true)] out object? entity)
        {
            entity = new MockEntity
            {
                Header = data[..5].ToArray(),
                Body = data[5..].ToArray()
            };
            return true;
        }
    }

    class MockErrorEntityDataPackageAdapter : DataPackageAdapter
    {
        public override bool TryConvertTo(ReadOnlyMemory<byte> data, [NotNullWhen(true)] out object? entity)
        {
            entity = new Foo();
            return true;
        }
    }

    class MockEntity
    {
        public byte[]? Header { get; set; }

        public byte[]? Body { get; set; }
    }
}
