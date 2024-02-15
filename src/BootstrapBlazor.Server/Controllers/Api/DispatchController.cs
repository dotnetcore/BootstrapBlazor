// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace BootstrapBlazor.Server.Controllers.Api;

/// <summary>
/// 
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class DispatchController : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dispatchService"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get([FromServices] IDispatchService<RebootMessage> dispatchService, [FromQuery] string token = "")
    {
        var hash = MD5.HashData(Encoding.UTF8.GetBytes(token));
        var data = string.Join("", hash.Select(i => i.ToString("X2")));
        if (data == "B2467CFB16008BC208F674B1E2581149")
        {
            dispatchService.Dispatch(new DispatchEntry<RebootMessage>()
            {
                Entry = new()
                {
                    Title = "系统更新",
                    Content = "网站正在获取源码进行更新准备重启，其间短暂时间内将暂停提供服务稍后将自动恢复"
                },
                Name = "Reboot"
            });
        }
        return Ok();
    }
}
