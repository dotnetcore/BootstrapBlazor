// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using BootstrapBlazor.Shared.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace BootstrapBlazor.Server.Controllers.Api
{
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
        /// <param name="client"></param>
        /// <param name="query"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Webhook([FromQuery] string id, [FromServices] IConfiguration config, [FromServices] NotificationService notification, [FromBody] GiteePostBody payload)
        {
            bool ret = false;
            if (id == config.GetValue<string>("WebHooks:Gitee:Id", null) && Request.Headers.TryGetValue("X-Gitee-Token", out var vals))
            {
                var token = vals.FirstOrDefault();
                if (config.GetValue<string>("WebHooks:Gitee:Token", "").Equals(token))
                {
                    // 全局推送
                    notification.Dispatch(payload);
                    ret = true;
                }
            }
            return ret ? Ok() : Unauthorized();
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
}
