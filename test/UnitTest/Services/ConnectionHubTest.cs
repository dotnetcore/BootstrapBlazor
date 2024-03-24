﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace UnitTest.Services;

public class ConnectionHubTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task AddConnection_Ok()
    {
        var client = Context.Services.GetRequiredService<WebClientService>();
        var service = Context.Services.GetRequiredService<IConnectionService>();
        var cut = Context.RenderComponent<ConnectionHub>();
        await cut.InvokeAsync(async () =>
        {
            client.SetData(new ClientInfo() { Id = "test_id", Ip = "::1" });
            await cut.Instance.Callback("test_id");
        });
        Assert.Equal(1, service.Count);

        // 触发 Beat 时间
        await Task.Delay(100);
        await cut.InvokeAsync(async () =>
        {
            await cut.Instance.Callback("test_id");
        });
        Assert.True(service.TryGetValue("test_id", out var item));
        Assert.NotNull(item?.ClientInfo);
        Assert.True(item?.ConnectionTime < DateTimeOffset.Now);
    }

    [Fact]
    public async Task ExpirationScanFrequency_Ok()
    {
        var services = new ServiceCollection();
        services.AddBootstrapBlazor();

        var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<IOptions<BootstrapBlazorOptions>>();
        options.Value.ConnectionHubOptions = new()
        {
            Enable = true,
            ExpirationScanFrequency = TimeSpan.FromMicroseconds(200),
            BeatInterval = 100
        };

        var service = provider.GetRequiredService<IConnectionService>();
        service.AddOrUpdate(new ClientInfo() { Id = "test_id" });
        Assert.Equal(1, service.Count);
        Assert.Single(service.Connections);

        await Task.Delay(200);
        Assert.Equal(0, service.Count);
    }

    [Fact]
    public void ConnectionHubOptions_Ok()
    {
        var services = new ServiceCollection();
        services.AddBootstrapBlazor();

        var provider = services.BuildServiceProvider();
        var service = provider.GetRequiredService<IConnectionService>();
        service.AddOrUpdate(new ClientInfo() { Id = "test_id" });
        Assert.Equal(1, service.Count);
    }
}

public class ConnectionServiceTest : TestBase
{
    [Fact]
    public void ConnectionService_Ok()
    {
        var type = Type.GetType("BootstrapBlazor.Components.DefaultConnectionService, BootstrapBlazor");
        Assert.NotNull(type);

        var service = Activator.CreateInstance(type, new BootstrapBlazorOptions());
        Assert.NotNull(service);

        var fieldInfo = type.GetField("_cancellationTokenSource", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        Assert.NotNull(fieldInfo);

        var token = fieldInfo.GetValue(service) as CancellationTokenSource;
        Assert.NotNull(token);

        token.Cancel();
    }
}
