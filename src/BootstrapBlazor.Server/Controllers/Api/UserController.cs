// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BootstrapBlazor.Server.Controllers.Api;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    public IActionResult Login(User user)
    {
        if (user.UserName == "admin" && user.Password == "123456")
        {
            return new JsonResult( new { Code = 200, Message = "登录成功" });
        }

        return new JsonResult(new { Code = 500, Message = "用户名或密码错误" });
    }
}
