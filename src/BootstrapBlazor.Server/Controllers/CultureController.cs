// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace BootstrapBlazor.Controllers;

/// <summary>
/// 文化 Controller
/// </summary>
[Route("[controller]/[action]")]
public class CultureController : Controller
{
    /// <summary>
    /// 设置文化方法
    /// </summary>
    /// <param name="culture"></param>
    /// <param name="redirectUri"></param>
    /// <returns></returns>
    public IActionResult SetCulture(string culture, string redirectUri)
    {
        if (string.IsNullOrEmpty(culture))
        {
            HttpContext.Response.Cookies.Delete(CookieRequestCultureProvider.DefaultCookieName);
        }
        else
        {
            HttpContext.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture, culture)), new CookieOptions()
                {
                    Expires = DateTimeOffset.Now.AddYears(1)
                });
        }

        return LocalRedirect(redirectUri);
    }

    /// <summary>
    /// 重置文化方法
    /// </summary>
    /// <param name="redirectUri"></param>
    /// <returns></returns>
    public IActionResult ResetCulture(string redirectUri)
    {
        HttpContext.Response.Cookies.Delete(CookieRequestCultureProvider.DefaultCookieName);

        return LocalRedirect(redirectUri);
    }
}
