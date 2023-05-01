// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

namespace UnitTest.Services;

public class WebClientServiceTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task WebClientService_Ok()
    {
        var service = Context.Services.GetRequiredService<WebClientService>();
        service.SetData("test_id", "192.168.0.1", "ios", "chrome", "mobile", "zh", "engine", "test_agent");
        ClientInfo? client = null;
        _ = Task.Run(async () => client = await service.GetClientInfo());
        while (client == null)
        {
            await Task.Delay(100);
            service.SetData("test_id", "192.168.0.1", "ios", "chrome", "mobile", "zh", "engine", "test_agent");
        }
        client.City = "test_city";
        client.RequestUrl = "test_url";
        Assert.Equal("test_id", client.Id);
        Assert.Equal("192.168.0.1", client.Ip);
        Assert.Equal("ios", client.OS);
        Assert.Equal("chrome", client.Browser);
        Assert.Equal(WebClientDeviceType.Mobile, client.Device);
        Assert.Equal("zh", client.Language);
        Assert.Equal("engine", client.Engine);
        Assert.Equal("test_agent", client.UserAgent);
        Assert.Equal("test_city", client.City);
        Assert.Equal("test_url", client.RequestUrl);
    }

    [Fact]
    public async Task WebClientService_Dispose()
    {
        var service = Context.Services.GetRequiredService<WebClientService>() as IAsyncDisposable;
        await service.DisposeAsync();
    }
}
