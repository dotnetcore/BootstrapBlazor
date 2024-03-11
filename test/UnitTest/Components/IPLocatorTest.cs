// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text;

namespace UnitTest.Components;

public class IPLocatorTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task BaiduIPLocatorProviderV2_Ok()
    {
        var factory = Context.Services.GetRequiredService<IIPLocatorFactory>();
        var provider = factory.Create();

        var result = await provider.Locate("127.0.0.1");
        Assert.Equal("本地连接", result);

        result = await provider.Locate("");
        Assert.Equal("本地连接", result);

        result = await provider.Locate("223.91.188.112");
        Assert.Equal("河南省漯河市舞阳县 中国移动", result);
    }

    [Fact]
    public async Task BaiduIPLocatorProvider_Ok()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        var factory = Context.Services.GetRequiredService<IIPLocatorFactory>();
        var provider = factory.Create("BaiduIPLocatorProvider");

        var result = await provider.Locate("127.0.0.1");
        Assert.Equal("本地连接", result);

        result = await provider.Locate("");
        Assert.Equal("本地连接", result);

        result = await provider.Locate("223.91.188.112");
        Assert.Equal("河南省漯河市 移动", result);
    }

    [Fact]
    public void Factory_KeyNotFoundException()
    {
        var factory = Context.Services.GetRequiredService<IIPLocatorFactory>();
        Assert.Throws<KeyNotFoundException>(() => factory.Create("BaiduIPLocatorProviderV0"));
    }

    [Fact]
    public async Task Fetch_Error()
    {
        var factory = Context.Services.GetRequiredService<IHttpClientFactory>();
        var logger = Context.Services.GetRequiredService<ILogger<MockProviderFetchError>>();
        var provider = new MockProviderFetchError(factory, logger);
        var result = await provider.Locate("223.91.188.112");
        Assert.Null(result);
    }

    [Fact]
    public async Task Fetch_Result_Fail()
    {
        var factory = Context.Services.GetRequiredService<IHttpClientFactory>();
        var logger = Context.Services.GetRequiredService<ILogger<MockBaiduProviderHttpClient>>();
        var provider = new MockBaiduProviderHttpClient(factory, logger);
        var result = await provider.Locate("223.91.188.112");
        Assert.Null(result);

        var loggerV2 = Context.Services.GetRequiredService<ILogger<MockBaiduProviderV2HttpClient>>();
        var providerV2 = new MockBaiduProviderV2HttpClient(factory, loggerV2);
        result = await providerV2.Locate("223.91.188.112");
        Assert.Null(result);
    }

    class MockProviderFetchError(IHttpClientFactory httpClientFactory, ILogger<MockProviderFetchError> logger) : BaiduIPLocatorProvider(httpClientFactory, logger)
    {
        protected override Task<string?> Fetch(string url, HttpClient client, CancellationToken token) => throw new InvalidOperationException();
    }

    class MockBaiduProviderHttpClient(IHttpClientFactory httpClientFactory, ILogger<MockBaiduProviderHttpClient> logger) : BaiduIPLocatorProvider(httpClientFactory, logger)
    {
        protected override Task<string?> Fetch(string url, HttpClient client, CancellationToken token)
        {
            client = new HttpClient(new MockHttpMessageHandler(), true);
            return base.Fetch(url, client, token);
        }
    }

    class MockBaiduProviderV2HttpClient(IHttpClientFactory httpClientFactory, ILogger<MockBaiduProviderV2HttpClient> logger) : BaiduIPLocatorProviderV2(httpClientFactory, logger)
    {
        protected override Task<string?> Fetch(string url, HttpClient client, CancellationToken token)
        {
            client = new HttpClient(new MockHttpMessageHandler(), true);
            return base.Fetch(url, client, token);
        }
    }

    class MockHttpMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("null")
            });
        }
    }
}
