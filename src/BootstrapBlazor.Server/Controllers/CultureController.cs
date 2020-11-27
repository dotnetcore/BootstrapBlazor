// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

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
