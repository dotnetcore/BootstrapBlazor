// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Longbow.GiteeAuth;
using Longbow.GitHubAuth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace BootstrapBlazor.Server.Controllers
{
    /// <summary>
    /// Account controller.
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// Gitee 认证
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Gitee()
        {
            return Challenge(GiteeDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// GitHub 认证
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GitHub()
        {
            return Challenge(GitHubDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return LocalRedirect("~/ai-chat");
        }
    }
}
