// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Logging;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 默认 IP 地理位置定位器
    /// </summary>
    internal class DefaultIPLocatorProvider : IIPLocatorProvider
    {
        private readonly IPLocatorOption _option = new();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="logger"></param>
        public DefaultIPLocatorProvider(IHttpClientFactory factory, ILogger<DefaultIPLocatorProvider> logger)
        {
            _option.HttpClient = factory.CreateClient();
            _option.Logger = logger;
        }

        /// <summary>
        /// 定位方法
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public async Task<string> Locate(string ip)
        {
            string? ret = null;

            // 解析本机地址
            if (string.IsNullOrEmpty(ip) || _option.Localhosts.Any(p => p == ip))
            {
                ret = "本地连接";
            }
            else
            {
                // IP定向器地址未设置
                _option.IP = ip;
                if (string.IsNullOrEmpty(_option.Url))
                {
                    ret = string.Empty;
                }
                var locator = _option.LocatorFactory(_option.LocatorName);
                ret = await locator.Locate(_option);
            }
            return ret ?? string.Empty;
        }
    }
}
