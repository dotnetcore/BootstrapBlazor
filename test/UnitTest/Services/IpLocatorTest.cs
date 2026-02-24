// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;

namespace UnitTest.Services;

public class IpLocatorTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task BaiduIpLocatorProvider_Ok()
    {
        var factory = Context.Services.GetRequiredService<IHttpClientFactory>();
        var option = Context.Services.GetRequiredService<IOptions<BootstrapBlazorOptions>>();
        var logger = Context.Services.GetRequiredService<ILogger<MockProvider>>();
        var provider = new MockProvider(factory, option, logger);

        var result = await provider.Locate("127.0.0.1");
        Assert.Equal("localhost", result);

        result = await provider.Locate("");
        Assert.Equal("localhost", result);

        // 河南省漯河市舞阳县 中国移动
        result = await provider.Locate("223.91.188.112");
        Assert.Equal("美国", result);

        // 命中缓存
        await provider.Locate("223.91.188.112");
        Assert.Equal("美国", result);

        // 关闭缓存
        option.Value.IpLocatorOptions.EnableCache = false;
        option.Value.IpLocatorOptions.SlidingExpiration = TimeSpan.FromMinutes(5);
        option.Value.IpLocatorOptions.ProviderName = nameof(BaiduIpLocatorProvider);
        await provider.Locate("223.91.188.112");
        Assert.Equal("美国", result);
        Assert.Null(provider.LastError);
    }

    [Fact]
    public void Factory_Ok()
    {
        var factory = Context.Services.GetRequiredService<IIpLocatorFactory>();
        Assert.NotNull(factory.Create("BaiduIpLocatorProvider"));
        Assert.NotNull(factory.Create());

        Assert.NotNull(Context.Services.GetKeyedService<IIpLocatorProvider>("BaiduIpLocatorProvider"));
    }

    [Fact]
    public void Factory_KeyNotFoundException()
    {
        var factory = Context.Services.GetRequiredService<IIpLocatorFactory>();
        Assert.Throws<KeyNotFoundException>(() => factory.Create("BaiduIpLocatorProviderV0"));
    }

    [Fact]
    public async Task Fetch_Error()
    {
        var factory = Context.Services.GetRequiredService<IHttpClientFactory>();
        var option = Context.Services.GetRequiredService<IOptions<BootstrapBlazorOptions>>();
        var logger = Context.Services.GetRequiredService<ILogger<MockProviderFetchError>>();
        var provider = new MockProviderFetchError(factory, option, logger);
        var result = await provider.Locate("223.91.188.112");
        Assert.Null(result);

        var cancelProvider = new MockProviderFetchCancelError(factory, option, logger);
        result = await cancelProvider.Locate("223.91.188.112");
        Assert.Null(result);
    }

    [Fact]
    public async Task Fetch_Result_Fail()
    {
        var factory = Context.Services.GetRequiredService<IHttpClientFactory>();
        var option = Context.Services.GetRequiredService<IOptions<BootstrapBlazorOptions>>();
        option.Value.IpLocatorOptions.EnableCache = false;
        var logger = Context.Services.GetRequiredService<ILogger<MockBaiduProvider>>();
        var provider = new MockBaiduProvider(factory, option, logger);
        var result = await provider.Locate("223.91.188.112");
        Assert.Null(result);
    }

    [Fact]
    public async Task Fetch_Exception()
    {
        var factory = Context.Services.GetRequiredService<IHttpClientFactory>();
        var option = Context.Services.GetRequiredService<IOptions<BootstrapBlazorOptions>>();
        option.Value.IpLocatorOptions.EnableCache = false;
        var logger = Context.Services.GetRequiredService<ILogger<MockErrorProvider>>();
        var provider = new MockErrorProvider(factory, option, logger);
        var result = await provider.Locate("223.91.188.112");
        Assert.Null(result);
        Assert.NotNull(provider.LastError);
    }

    class MockProviderFetchError(IHttpClientFactory httpClientFactory, IOptions<BootstrapBlazorOptions> option, ILogger<MockProviderFetchError> logger) : BaiduIpLocatorProvider(httpClientFactory, option, logger)
    {
        protected override Task<string?> Fetch(string url, HttpClient client, CancellationToken token) => throw new InvalidOperationException();
    }

    class MockProviderFetchCancelError(IHttpClientFactory httpClientFactory, IOptions<BootstrapBlazorOptions> option, ILogger<MockProviderFetchError> logger) : BaiduIpLocatorProvider(httpClientFactory, option, logger)
    {
        protected override Task<string?> Fetch(string url, HttpClient client, CancellationToken token) => throw new TaskCanceledException();
    }

    class MockBaiduProvider(IHttpClientFactory httpClientFactory, IOptions<BootstrapBlazorOptions> option, ILogger<MockBaiduProvider> logger) : BaiduIpLocatorProvider(httpClientFactory, option, logger)
    {
        protected override Task<string?> Fetch(string url, HttpClient client, CancellationToken token)
        {
            client = new HttpClient(new MockHttpNullMessageHandler(), true);
            return base.Fetch(url, client, token);
        }
    }

    class MockProvider(IHttpClientFactory httpClientFactory, IOptions<BootstrapBlazorOptions> option, ILogger<MockProvider> logger) : BaiduIpLocatorProvider(httpClientFactory, option, logger)
    {
        protected override Task<string?> Fetch(string url, HttpClient client, CancellationToken token)
        {
            client = new HttpClient(new MockHttpSuccessMessageHandler(), true);
            return base.Fetch(url, client, token);
        }
    }

    class MockErrorProvider(IHttpClientFactory httpClientFactory, IOptions<BootstrapBlazorOptions> option, ILogger<MockErrorProvider> logger) : BaiduIpLocatorProvider(httpClientFactory, option, logger)
    {
        protected override Task<string?> Fetch(string url, HttpClient client, CancellationToken token)
        {
            client = new HttpClient(new MockHttpExceptionMessageHandler(), true);
            return base.Fetch(url, client, token);
        }
    }

    class MockHttpExceptionMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            throw new Exception("error test");
        }
    }

    class MockHttpNullMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("null")
            });
        }
    }

    class MockHttpSuccessMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("{\"status\":\"0\",\"t\":\"\",\"set_cache_time\":\"\",\"data\":[{\"ExtendedLocation\":\"\",\"OriginQuery\":\"20.205.243.166\",\"appinfo\":\"\",\"disp_type\":0,\"fetchkey\":\"20.205.243.166\",\"location\":\"美国\",\"origip\":\"20.205.243.166\",\"origipquery\":\"20.205.243.166\",\"resourceid\":\"6006\",\"role_id\":0,\"shareImage\":1,\"showLikeShare\":1,\"showlamp\":\"1\",\"titlecont\":\"IP地址查询\",\"tplt\":\"ip\"}]}")
            });
        }
    }
}
