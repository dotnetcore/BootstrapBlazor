// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Logging;

namespace UnitTest.Components;

public class IPLocatorTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task Locator_Ok()
    {
        var cut = Context.RenderComponent<IpLocatorTest>();
        var result = await cut.Instance.IPLocator.Locate("127.0.0.1");
        Assert.Equal("本地连接", result);
        result = await cut.Instance.IPLocator.Locate("");
        Assert.Equal("本地连接", result);
        var widenet = await cut.Instance.IPLocator.Locate("223.91.188.112");
        Assert.NotNull(widenet);
    }

    [Fact]
    public void BaiduIpLocator_Ok()
    {
        var locator = new BaiDuIPLocator
        {
            Status = "0"
        };
        var ret = locator.ToString();
        Assert.Equal("XX XX", ret);

        locator.Data = Array.Empty<LocationInfo>();
        ret = locator.ToString();
        Assert.Equal("XX XX", ret);

        locator.Data = new LocationInfo[]
        {
            new()
        };
        ret = locator.ToString();
        Assert.Equal("XX XX", ret);

        locator.Data = new LocationInfo[]
        {
            new()
            {
                Location = "Test"
            }
        };
        ret = locator.ToString();
        Assert.Equal("Test", ret);

        locator.Status = "1";
        ret = locator.ToString();
        Assert.Equal("Error", ret);
    }

    [Fact]
    public async Task BaiduIPLocator_Ok()
    {
        var locator = new BaiDuIPLocator();
        var ret = await locator.Locate(new IPLocatorOption()
        {
            RequestTimeout = 3000
        });
        Assert.Null(ret);
    }

    [Fact]
    public async Task DefaultIPLocator_Ok()
    {
        var locator = new DefaultIPLocator();
        var ret = await locator.Locate(new IPLocatorOption()
        {
            RequestTimeout = 3000
        });
        Assert.Null(ret);
    }

    [Fact]
    public async Task LocateOfT_Ok()
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        var locator = new MockLocator();
        await locator.Test("", null);
        await locator.Test("223.91.188.112", new HttpClient());
        await locator.Test("223.91.188.112", new HttpClient(), new MockLogger());
        await locator.TestMock("223.91.188.112", new HttpClient(), new MockLogger());
    }

    [Fact]
    public async Task LocateOfT_Fail()
    {
        var locator = new MockLocator();
        await locator.Test("223.91.188.112", new HttpClient());
        await locator.TestMock("223.91.188.112", null);
        await locator.TestMock("223.91.188.112", new HttpClient(), new MockLogger());
    }

    private class IpLocatorTest : ComponentBase
    {
        [Inject]
        [NotNull]
        public IIPLocatorProvider? IPLocator { get; set; }
    }

    private class MockLocator : DefaultIPLocator
    {
        public async Task Test(string? ip, HttpClient? httpClient, ILogger<IIPLocatorProvider>? logger = null)
        {
            Url = "https://sp0.baidu.com/8aQDcjqpAAV3otqbppnN2DJv/api.php?resource_id=6006&query={0}";
            await base.Locate<BaiDuIPLocator>(new MockOption(ip, httpClient, logger));
        }
        public async Task TestMock(string? ip, HttpClient? httpClient, ILogger<IIPLocatorProvider>? logger = null)
        {
            Url = "/test/{0}";
            await base.Locate<MockModel>(new MockOption(ip, httpClient, logger));
        }
    }

    private class MockModel
    {

    }

    private class MockOption : IPLocatorOption
    {
        public MockOption(string? ip, HttpClient? httpClient, ILogger<IIPLocatorProvider>? logger)
        {
            IP = ip;
            base.HttpClient = httpClient;
            base.Logger = logger;
        }
    }

    private class MockLogger : ILogger<IIPLocatorProvider>
    {
        public IDisposable BeginScope<TState>(TState state) => throw new NotImplementedException();

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {

        }
    }
}
