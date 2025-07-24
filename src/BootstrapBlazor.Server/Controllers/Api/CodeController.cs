// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace BootstrapBlazor.Controllers.Api;

/// <summary>
/// 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class CodeController : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="client"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<string> Get([FromQuery] string fileName, [FromServices] HttpClient client, [FromServices] IOptions<WebsiteOptions> options)
    {
        var ret = "";
        client.BaseAddress = new Uri(options.Value.SourceUrl);
        try
        {
            ret = await client.GetStringAsync(fileName);
        }
        catch (HttpRequestException ex) { ret = ex.StatusCode == HttpStatusCode.NotFound ? "无" : ex.StatusCode.ToString() ?? "网络错误"; }
        catch (Exception) { }
        return ret;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [HttpOptions]
    public string Options() => string.Empty;
}
