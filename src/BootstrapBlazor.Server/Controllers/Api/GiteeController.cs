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
        /// Appveyor 私有服务器 Webhook
        /// </summary>
        /// <param name="client"></param>
        /// <param name="query"></param>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Webhook([FromServices] IConfiguration config, [FromServices] NotificationService notification, [FromBody] GiteePostBody payload)
        {
            bool ret = false;
            if (Request.Headers.TryGetValue("X-Gitee-Token", out var vals))
            {
                var token = vals.FirstOrDefault();
                if (config.GetValue<string>("WebHooks:GiteeToken", "").Equals(token))
                {
                    // 全局推送
                    notification.Dispatch(payload);
                    ret = true;
                }
            }
            return ret ? Ok() : Unauthorized();
        }

        /// <summary>
        /// 跨域握手协议
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public string Options()
        {
            return string.Empty;
        }
    }
}
