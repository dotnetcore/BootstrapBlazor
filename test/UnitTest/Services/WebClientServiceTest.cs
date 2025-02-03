// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Options;

namespace UnitTest.Services;

public class WebClientServiceTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task WebClientService_Ok()
    {
        var mockData = new ClientInfo()
        {
            Id = "test_id",
            Ip = "192.168.0.1",
            City = "保留",
            OS = "ios",
            Browser = "chrome",
            Device = WebClientDeviceType.Mobile,
            Language = "zh",
            Engine = "engine",
            UserAgent = "test_agent"
        };
        var service = Context.Services.GetRequiredService<WebClientService>();
        service.SetData(mockData);
        ClientInfo? client = null;
        _ = Task.Run(async () => client = await service.GetClientInfo());
        while (client == null)
        {
            await Task.Delay(100);
            service.SetData(mockData);
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
    public async Task WebClientOptions_Ok()
    {
        var options = Context.Services.GetRequiredService<IOptionsMonitor<BootstrapBlazorOptions>>();
        options.CurrentValue.WebClientOptions.EnableIpLocator = true;

        var service = Context.Services.GetRequiredService<WebClientService>();
        var client = await service.GetClientInfo();
        Assert.Null(client.Ip);
    }

    [Fact]
    public async Task GetClientInfo_Error()
    {
        var service = Context.Services.GetRequiredService<WebClientService>();
        var client = await service.GetClientInfo();

        // TimeoutException
        Assert.Null(client.Ip);

        // Exception
        Context.JSInterop.SetupVoid("ping", _ => true).SetException(new Exception("test-exception"));
        client = await service.GetClientInfo();
    }

    [Fact]
    public async Task SetData_Ok()
    {
        var service = Context.Services.GetRequiredService<WebClientService>();

        // 内部 ReturnTask 为空
        var fieldInfo = service.GetType().GetField("_taskCompletionSource", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        fieldInfo!.SetValue(service, null);

        service.SetData(new ClientInfo() { Id = "test" });
        _ = Task.Run(async () =>
        {
            await Task.Delay(150);
            service.SetData(new ClientInfo() { Id = "test-id", Ip = "192.168.0.1" });
        });
        var client = await service.GetClientInfo();
        Assert.Equal("192.168.0.1", client.Ip);
    }

    [Fact]
    public async Task WebClientService_Dispose()
    {
        await (Context.Services.GetRequiredService<WebClientService>() as IAsyncDisposable).DisposeAsync();
    }
}
