﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Server.Controllers;

/// <summary>
/// 瀑布流控制器
/// </summary>
public class WaterfallController : Controller
{
    /// <summary>
    /// 获得图片方法
    /// </summary>
    /// <param name="options"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<IActionResult> Index([FromServices] IOptions<WebsiteOptions> options, [FromQuery] string? id)
    {
        var interval = Random.Shared.Next(1, 2000);
        await Task.Delay(interval);

        var fileName = Path.Combine(options.Value.ContentRootPath, "../BootstrapBlazor.Shared/wwwroot/images/waterfall", $"{id}.jpeg");
        return new PhysicalFileResult(fileName, "images/jpeg");
    }
}
