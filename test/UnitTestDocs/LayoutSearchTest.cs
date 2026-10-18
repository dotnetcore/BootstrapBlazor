// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using BootstrapBlazor.Controllers.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Text;
using System.Text.Json;

namespace UnitTestDocs;

public class LayoutSearchTest
{
    [Theory]
    [InlineData(null, "docs")]
    [InlineData("https://search.example/", "other")]
    public async Task Search_RejectsUnavailableIndex(string? url, string index)
    {
        using var handler = new SearchHandler(_ => throw new InvalidOperationException("The upstream search must not be called."));
        var controller = new LayoutSearchController(new SearchClientFactory(handler),
            Options.Create(new MeiliSearchOptions { Url = url, Index = "docs" }));

        var result = await controller.Search(index, JsonSerializer.SerializeToElement(new { q = "button" }), CancellationToken.None);

        Assert.IsType<NotFoundResult>(result);
    }

    [Theory]
    [InlineData(HttpStatusCode.OK)]
    [InlineData(HttpStatusCode.BadRequest)]
    public async Task Search_ForwardsQueryAndUpstreamStatus(HttpStatusCode statusCode)
    {
        using var handler = new SearchHandler(async request =>
        {
            Assert.Equal(HttpMethod.Post, request.Method);
            Assert.Equal("https://search.example/indexes/docs/search", request.RequestUri?.AbsoluteUri);
            Assert.Equal("Bearer", request.Headers.Authorization?.Scheme);
            Assert.Equal("test-search-key", request.Headers.Authorization?.Parameter);
            using var query = JsonDocument.Parse(await request.Content!.ReadAsStringAsync());
            Assert.Equal("button", query.RootElement.GetProperty("q").GetString());
            return new HttpResponseMessage(statusCode)
            {
                Content = new StringContent("{\"result\":\"upstream\"}", Encoding.UTF8, "application/json")
            };
        });
        var controller = new LayoutSearchController(new SearchClientFactory(handler),
            Options.Create(new MeiliSearchOptions { Url = "https://search.example/", Index = "docs", ApiKey = "test-search-key" }));

        var result = Assert.IsType<ContentResult>(await controller.Search("docs",
            JsonSerializer.SerializeToElement(new { q = "button" }), CancellationToken.None));

        Assert.Equal((int)statusCode, result.StatusCode);
        Assert.Equal("{\"result\":\"upstream\"}", result.Content);
        Assert.StartsWith("application/json", result.ContentType);
    }

    private sealed class SearchClientFactory(HttpMessageHandler handler) : IHttpClientFactory
    {
        public HttpClient CreateClient(string name) => new(handler, disposeHandler: false);
    }

    private sealed class SearchHandler(Func<HttpRequestMessage, Task<HttpResponseMessage>> sendAsync) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            => sendAsync(request);
    }
}
