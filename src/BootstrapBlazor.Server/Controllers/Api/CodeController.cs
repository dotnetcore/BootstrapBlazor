// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BootstrapBlazor.Server.Controllers.Api
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CodeController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="client"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<string> Get([FromQuery] string fileName, [FromServices] HttpClient client, [FromServices] IOptions<WebsiteOptions> options)
        {
            client.BaseAddress = new Uri(options.Value.RepositoryUrl);
            return client.GetStringAsync(fileName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public string Options() => "";
    }
}
