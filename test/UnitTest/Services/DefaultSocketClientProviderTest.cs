// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Net;

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
}
