// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BootstrapBlazor.Components;

/// <summary>
/// 默认 IP 地理位置定位器
/// </summary>
internal class DefaultIPLocatorProvider : IIPLocatorProvider
{
    private readonly IPLocatorOption _option;

    private readonly IServiceProvider _provider;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="factory"></param>
    /// <param name="logger"></param>
    /// <param name="option"></param>
    public DefaultIPLocatorProvider(IServiceProvider provider, IHttpClientFactory factory, ILogger<DefaultIPLocatorProvider> logger, IOptions<IPLocatorOption> option)
    {
        _provider = provider;
        _option = option.Value;
        _option.HttpClient = factory.CreateClient();
        _option.Logger = logger;
    }

    /// <summary>
    /// 定位方法
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    public async Task<string?> Locate(string ip)
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
            if (_option.LocatorFactory != null)
            {
                var locator = _option.LocatorFactory(_provider);
                if (locator != null)
                {
                    ret = await locator.Locate(_option);
                }
            }
        }
        return ret;
    }
}
