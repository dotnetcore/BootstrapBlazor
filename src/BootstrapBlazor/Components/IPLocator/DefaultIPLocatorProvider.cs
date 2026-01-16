// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">默认 IP 地理位置定位器</para>
///  <para lang="en">Default IP Geolocation Provider</para>
/// </summary>
[Obsolete("已弃用，请参考 https://www.blazor.zone/locator")]
[ExcludeFromCodeCoverage]
internal class DefaultIPLocatorProvider : IIPLocatorProvider
{
    private readonly IPLocatorOption _option;

    private readonly IServiceProvider _provider;

    /// <summary>
    ///  <para lang="zh">构造函数</para>
    ///  <para lang="en">Constructor</para>
    /// </summary>
    /// <param name="provider"></param>
    /// <param name="factory"></param>
    /// <param name="logger"></param>
    /// <param name="option"></param>
    public DefaultIPLocatorProvider(IServiceProvider provider, IHttpClientFactory factory, ILogger<DefaultIPLocatorProvider> logger, IOptionsMonitor<IPLocatorOption> option)
    {
        _provider = provider;
        _option = option.CurrentValue;
        _option.HttpClient = factory.CreateClient();
        _option.Logger = logger;
    }

    /// <summary>
    ///  <para lang="zh">定位方法</para>
    ///  <para lang="en">Locate Method</para>
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
