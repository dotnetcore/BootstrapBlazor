// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BootstrapBlazor.Controllers.Api;

/// <summary>
/// <para lang="zh">WebAssembly 定位示例接口，不公开密钥或离线数据。</para>
/// <para lang="en">Locator API for WebAssembly without exposing keys or offline data.</para>
/// </summary>
[ApiController]
public sealed class LocatorController(ISampleLocatorService locator) : ControllerBase
{
    /// <summary><para lang="zh">获取已注册的服务商。</para><para lang="en">Gets registered providers.</para></summary>
    [HttpGet("api/locator/providers")]
    public Task<List<string>> GetProviders(CancellationToken cancellationToken) => locator.GetProvidersAsync(cancellationToken);

    /// <summary><para lang="zh">验证 IP 与服务商后查询位置。</para><para lang="en">Validates the IP and provider before looking up the location.</para></summary>
    [HttpGet("api/locator")]
    public async Task<ActionResult<SampleLocatorResult>> Locate(string providerName, string ip, CancellationToken cancellationToken)
    {
        if (!IPAddress.TryParse(ip, out _) || !(await locator.GetProvidersAsync(cancellationToken)).Contains(providerName, StringComparer.Ordinal))
        {
            return Problem(detail: "A valid IP address and registered locator provider are required.", statusCode: 400);
        }
        Response.Headers.CacheControl = "no-store";
        return await locator.LocateAsync(providerName, ip, cancellationToken);
    }
}
