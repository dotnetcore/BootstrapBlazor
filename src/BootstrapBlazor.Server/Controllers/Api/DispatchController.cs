// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace BootstrapBlazor.Server.Controllers.Api;

/// <summary>
/// Dispatch 接口
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class DispatchController : ControllerBase
{
    /// <summary>
    /// 消息分发接口
    /// </summary>
    /// <param name="dispatchService"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Get([FromServices] IDispatchService<bool> dispatchService, [FromQuery] string token = "")
    {
        var hash = MD5.HashData(Encoding.UTF8.GetBytes(token));
        var data = string.Join("", hash.Select(i => i.ToString("X2")));
        if (data == "96BD3413D0780A6E4F69CC48C835BB80")
        {
            dispatchService.Dispatch(new DispatchEntry<bool>()
            {
                Entry = true,
                Name = "Reboot"
            });
        }
        else if(data == "57D2EADDEC65BE22E104ADB97778E6B0")
        {
            dispatchService.Dispatch(new DispatchEntry<bool>()
            {
                Entry = false,
                Name = "Reboot"
            });
        }
        return Ok();
    }
}
