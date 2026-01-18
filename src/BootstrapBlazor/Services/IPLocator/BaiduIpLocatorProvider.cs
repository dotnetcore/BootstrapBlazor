// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">百度搜索引擎 IP 定位器</para>
/// <para lang="en">Baidu Search Engine IP Locator</para>
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
        catch (TaskCanceledException) { }
        catch (Exception ex)
        {
            logger.LogError(ex, "Url: {url}", url);
        }
        return ret;
    }

    /// <summary>
    /// <para lang="zh">获得 HttpClient 实例方法</para>
    /// <para lang="en">Get HttpClient Instance Method</para>
    /// </summary>
    /// <returns></returns>
    protected virtual HttpClient GetHttpClient() => httpClientFactory.CreateClient();

    /// <summary>
    /// <para lang="zh">获得 Url 地址</para>
    /// <para lang="en">Get URL Address</para>
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    protected virtual string GetUrl(string ip) => $"https://sp0.baidu.com/8aQDcjqpAAV3otqbppnN2DJv/api.php?resource_id=6006&query={ip}";

    /// <summary>
    /// <para lang="zh">请求获得地理位置接口方法</para>
    /// <para lang="en">Request Geolocation Interface Method</para>
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
    /// <para lang="zh">LocationResult 结构体</para>
    /// <para lang="en">LocationResult Structure</para>
    /// </summary>
    [ExcludeFromCodeCoverage]
    class LocationResult
    {
        /// <summary>
        /// <para lang="zh">获得/设置 结果状态返回码 为 0 时通讯正常</para>
        /// <para lang="en">Gets or sets Result Status Code, 0 is Normal</para>
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// <para lang="zh">获得/设置 定位信息</para>
        /// <para lang="en">Gets or sets Location Info</para>
        /// </summary>
        public List<LocationData>? Data { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// <para lang="en"><inheritdoc/></para>
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
        /// <para lang="zh">获得/设置 定位信息</para>
        /// <para lang="en">Gets or sets 定位信息</para>
        /// </summary>
        public string? Location { get; set; }
    }
}
