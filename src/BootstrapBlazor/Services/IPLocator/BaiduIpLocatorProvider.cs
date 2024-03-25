// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace BootstrapBlazor.Components;

/// <summary>
/// 百度搜索引擎 IP 定位器
/// </summary>
public class BaiduIpLocatorProvider(IHttpClientFactory httpClientFactory, IOptions<BootstrapBlazorOptions> options, ILogger<BaiduIpLocatorProvider> logger) : DefaultIpLocatorProvider(options)
{
    private HttpClient? _client;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="ip"></param>
    protected override async Task<string?> LocateByIp(string ip)
    {
        string? ret = null;
        var url = GetUrl(ip);
        try
        {
            _client ??= GetHttpClient();
            using var token = new CancellationTokenSource(3000);
            ret = await Fetch(url, _client, token.Token);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Url: {url}", url);
        }
        return ret;
    }

    /// <summary>
    /// 获得 HttpClient 实例方法
    /// </summary>
    /// <returns></returns>
    protected virtual HttpClient GetHttpClient() => httpClientFactory.CreateClient();

    /// <summary>
    /// 获得 Url 地址
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    protected virtual string GetUrl(string ip) => $"https://sp0.baidu.com/8aQDcjqpAAV3otqbppnN2DJv/api.php?resource_id=6006&query={ip}";

    /// <summary>
    /// 请求获得地理位置接口方法
    /// </summary>
    /// <param name="url"></param>
    /// <param name="client"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    protected virtual async Task<string?> Fetch(string url, HttpClient client, CancellationToken token)
    {
        var result = await client.GetFromJsonAsync<LocationResult>(url, token);
        return result?.ToString();
    }

    /// <summary>
    /// LocationResult 结构体
    /// </summary>
    [ExcludeFromCodeCoverage]
    class LocationResult
    {
        /// <summary>
        /// 获得/设置 结果状态返回码 为 0 时通讯正常
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// 获得/设置 定位信息
        /// </summary>
        public List<LocationData>? Data { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override string? ToString()
        {
            string? ret = null;
            if (Status == "0")
            {
                ret = Data?.FirstOrDefault()?.Location;
            }
            return ret;
        }
    }

    [ExcludeFromCodeCoverage]
    class LocationData
    {
        /// <summary>
        /// 获得/设置 定位信息
        /// </summary>
        public string? Location { get; set; }
    }
}
