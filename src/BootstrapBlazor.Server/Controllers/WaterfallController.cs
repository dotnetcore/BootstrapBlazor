// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Mvc;

namespace BootstrapBlazor.Server.Controllers;

/// <summary>
/// 瀑布流控制器
/// </summary>
public class WaterfallController : Controller
{
    /// <summary>
    /// 获得图片方法
    /// </summary>
    /// <param name="env"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IActionResult> Index([FromServices] IWebHostEnvironment env, [FromQuery] string? id)
    {
        var interval = Random.Shared.Next(1, 2000);
        await Task.Delay(interval);
        var fileName = Path.Combine(env.WebRootPath, "images/waterfall", $"{id}.jpeg");
        return new PhysicalFileResult(fileName, "images/jpeg");
    }
}
