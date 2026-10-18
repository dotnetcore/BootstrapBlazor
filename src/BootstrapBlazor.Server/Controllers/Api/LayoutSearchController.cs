// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BootstrapBlazor.Controllers.Api;

/// <summary>
/// Proxies browser searches without publishing the configured search credential.
/// </summary>
[ApiController]
public class LayoutSearchController(IHttpClientFactory factory, IOptions<MeiliSearchOptions> options) : ControllerBase
{
    /// <summary>
    /// Searches the configured documentation index.
    /// </summary>
    [HttpPost("api/layout-search/indexes/{index}/search")]
    public async Task<IActionResult> Search(string index, [FromBody] JsonElement query, CancellationToken cancellationToken)
    {
        var settings = options.Value;
        if (string.IsNullOrEmpty(settings.Url) || !string.Equals(index, settings.Index, StringComparison.Ordinal))
        {
            return NotFound();
        }

        var baseUri = new Uri(settings.Url.TrimEnd('/') + "/");
        using var request = new HttpRequestMessage(HttpMethod.Post, new Uri(baseUri, $"indexes/{Uri.EscapeDataString(index)}/search"))
        {
            Content = new StringContent(query.GetRawText(), Encoding.UTF8, "application/json")
        };
        if (!string.IsNullOrEmpty(settings.ApiKey))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", settings.ApiKey);
        }

        using var client = factory.CreateClient();
        using var response = await client.SendAsync(request, cancellationToken);
        return new ContentResult
        {
            StatusCode = (int)response.StatusCode,
            ContentType = response.Content.Headers.ContentType?.ToString() ?? "application/json",
            Content = await response.Content.ReadAsStringAsync(cancellationToken)
        };
    }
}
