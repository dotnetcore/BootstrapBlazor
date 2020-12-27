// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace BootstrapBlazor.Server.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("[controller]/[action]")]
    public class CultureController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        /// <param name="redirectUri"></param>
        /// <returns></returns>
        public IActionResult SetCulture(string culture, string redirectUri)
        {
            if (culture != null)
            {
                HttpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture, culture)));
            }

            return LocalRedirect(redirectUri);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="redirectUri"></param>
        /// <returns></returns>
        public IActionResult ResetCulture(string redirectUri)
        {
            HttpContext.Response.Cookies.Delete(CookieRequestCultureProvider.DefaultCookieName);

            return LocalRedirect(redirectUri);
        }
    }
}
