// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Components;

public class ConnectionHubTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task AddConnection_Ok()
    {
        var mockData = new ClientInfo()
        {
            Id = "test_id",
            Ip = "192.168.0.1",
            OS = "ios",
            Browser = "chrome",
            Device = WebClientDeviceType.Mobile,
            Language = "zh",
            Engine = "engine",
            UserAgent = "test_agent"
        };

        var service = Context.Services.GetRequiredService<IConnectionService>();
        var cut = Context.RenderComponent<ConnectionHub>();
        await cut.InvokeAsync(() =>
        {
            cut.Instance.Callback(mockData);
        });
        Assert.Equal(1, service.Count);

        // 触发 Beat 时间
        await Task.Delay(1000);
        await cut.InvokeAsync(() =>
        {
            cut.Instance.Callback(mockData);
        });

        // 等待过期
        await Task.Delay(5000);
        Assert.True(service.TryGetValue(mockData.Id, out var _));
        Assert.Equal(0, service.Count);

        // 等待缓存清理
        await Task.Delay(2000);
        Assert.False(service.TryGetValue(mockData.Id, out var _));
    }
}
