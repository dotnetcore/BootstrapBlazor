// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.MeiliSearch.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace BootstrapBlazor.Server.Controllers.Api;

/// <summary>
/// 搜索控制器
/// </summary>
[Route("api/[controller]/[action]")]
[AllowAnonymous]
[ApiController]
public class SearchController : ControllerBase
{
    /// <summary>
    /// Search Webhook
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public IActionResult WebHook([FromServices] IConfiguration configuration, [FromServices] IMeiliSearch meiliSearch, [FromQuery] string? id, [FromBody] GiteePostBody payload)
    {
        IActionResult ret = Unauthorized();
        var valid = Valid(configuration, id, payload);
        if (valid)
        {
            if (meiliSearch.Status.Code == 0 && (payload.Ref == "refs/heads/main" || payload.Ref == "refs/heads/master"))
            {
                ret = Ok(meiliSearch.Build());
            }
            else
            {
                ret = Ok(meiliSearch.Status);
            }
        }
        return ret;
    }

    private bool Valid(IConfiguration configuration, string? id, GiteePostBody payload)
    {
        var configId = configuration.GetValue<string>("WebHooks:MeiliSearch:Id");
        var configToken = configuration.GetValue<string>("WebHooks:MeiliSearch:Token");
        var token = "";
        if (Request.Headers.TryGetValue("X-Gitee-Token", out var val))
        {
            token = val.FirstOrDefault() ?? string.Empty;
        }
        return id == configId && token == configToken && payload.Id == configId && payload.Password == configToken;
    }

    /// <summary>
    /// Search 测试接口
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult WebHook([FromServices] IMeiliSearch meiliSearch)
    {
        return Ok(meiliSearch.Status);
    }

    /// <summary>
    /// 跨域握手协议
    /// </summary>
    /// <returns></returns>
    [HttpOptions]
    public string Options() => string.Empty;
}
