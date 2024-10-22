// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
