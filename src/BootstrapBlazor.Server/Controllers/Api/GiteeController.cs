// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BootstrapBlazor.Server.Controllers.Api;

/// <summary>
/// 
/// </summary>
[Route("api/[controller]/[action]")]
[ApiController]
public class GiteeController : ControllerBase
{
    /// <summary>
    /// Gitee Webhook
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Webhook([FromQuery] string? id, [FromServices] IConfiguration config, [FromServices] IDispatchService<GiteePostBody> dispatch, [FromBody] GiteePostBody payload)
    {
        bool ret = false;
        if (Check())
        {
            // 全局推送
            if (payload.HeadCommit != null || payload.Commits?.Count > 0)
            {
                dispatch.Dispatch(new DispatchEntry<GiteePostBody>()
                {
                    Name = "Gitee",
                    Entry = payload
                });
            }
            ret = true;
        }
        return ret ? Ok() : Unauthorized();

        bool Check()
        {
            var configId = config.GetValue<string>("WebHooks:Gitee:Id");
            var configToken = config.GetValue<string>("WebHooks:Gitee:Token");
            var token = "";
            if (Request.Headers.TryGetValue("X-Gitee-Token", out var vals))
            {
                token = vals.FirstOrDefault() ?? string.Empty;
            }
            return id == configId && token == configToken
                    && payload.Id == configId && payload.Password == configToken;
        }
    }

    /// <summary>
    /// Webhook 测试接口
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Webhook()
    {
        return Ok(new { Message = "Ok" });
    }

    /// <summary>
    /// 跨域握手协议
    /// </summary>
    /// <returns></returns>
    [HttpOptions]
    public string Options() => string.Empty;
}
