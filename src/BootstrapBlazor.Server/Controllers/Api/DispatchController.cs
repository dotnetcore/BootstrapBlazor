// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

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
