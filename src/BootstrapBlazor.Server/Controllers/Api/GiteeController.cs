// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace BootstrapBlazor.Controllers.Api;

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
    public async Task<IActionResult> Webhook([FromQuery] string? id, [FromServices] IConfiguration config, [FromServices] IDispatchService<CommitNotification> dispatch, [FromBody] GiteePostBody payload)
    {
        bool ret = false;
        if (Check())
        {
            // 全局推送
            if (payload.HeadCommit != null || payload.Commits?.Count > 0)
            {
                var commit = payload.HeadCommit ?? payload.Commits!.First();
                await dispatch.DispatchAsync(new DispatchEntry<CommitNotification>()
                {
                    Name = "Gitee",
                    Entry = new CommitNotification(commit.Author.Name, commit.Timestamp, commit.Message,
                        commit.Url, payload.GetBranchName(), payload.Commits?.Count ?? 1)
                }, HttpContext.RequestAborted);
            }
            ret = true;
        }
        return ret ? Ok() : Unauthorized();

        bool Check()
        {
            var configId = config.GetValue<string>("WebHooks:Gitee:Id");
            var configToken = config.GetValue<string>("WebHooks:Gitee:Token");
            var token = "";
            if (Request.Headers.TryGetValue("X-Gitee-Token", out var val))
            {
                token = val.FirstOrDefault() ?? string.Empty;
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
