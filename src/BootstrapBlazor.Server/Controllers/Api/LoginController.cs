// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace BootstrapBlazor.Server.Controllers.Api;

/// <summary>
/// 登录控制器
/// </summary>
[Route("api/[controller]")]
[AllowAnonymous]
[ApiController]
public class LoginController : ControllerBase
{
    /// <summary>
    /// 认证方法
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Post(User user) => user is { UserName: "admin", Password: "123456" } ? new JsonResult(new { Code = 200, Message = "登录成功" }) : new JsonResult(new { Code = 500, Message = "用户名或密码错误" });
}
