// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BootstrapBlazor.Server.Controllers.Api;

/// <summary>
/// 
/// </summary>
[Route("api/[controller]")]
[AllowAnonymous]
[ApiController]
public class LoginController : ControllerBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Post(User user)
    {
        IActionResult? response;
        if (user.UserName == "admin" && user.Password == "123456")
        {
            response = new JsonResult(new { Code = 200, Message = "登录成功" });
        }
        else
        {
            response = new JsonResult(new { Code = 500, Message = "用户名或密码错误" });
        }
        return response;
    }
}
