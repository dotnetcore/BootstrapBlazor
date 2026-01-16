// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">BaiduIPLocatorV2 第二个版本实现类</para>
/// <para lang="en">BaiduIPLocatorV2 Implementation Class</para>
/// </summary>
public class BaiduIpLocatorProviderV2(IHttpClientFactory httpClientFactory, IOptions<BootstrapBlazorOptions> options, ILogger<BaiduIpLocatorProvider> logger) : BaiduIpLocatorProvider(httpClientFactory, options, logger)
{
    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="ip"></param>
    /// <returns></returns>
    protected override string GetUrl(string ip) => $"https://qifu-api.baidubce.com/ip/geo/v1/district?ip={ip}";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="url"></param>
    /// <param name="client"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    protected override async Task<string?> Fetch(string url, HttpClient client, CancellationToken token)
    {
        var result = await client.GetFromJsonAsync<LocationResultV2>(url, token);
        return result?.ToString();
    }

    [ExcludeFromCodeCoverage]
    class LocationResultV2
    {
        public string? Code { get; set; }

        [NotNull]
        public LocationDataV2? Data { get; set; }

        public bool Charge { get; set; }

        public string? Msg { get; set; }

        public string? Ip { get; set; }

        public string? CoordSys { get; set; }

        public override string? ToString()
        {
            string? ret = null;
            if (Code == "Success")
            {
                ret = Data?.Country == "中国"
                    ? $"{Data?.Prov}{Data?.City}{Data?.District} {Data?.Isp}"
                    : $"{Data?.Continent} {Data?.Country} {Data?.City}";
            }
            return ret;
        }
    }

    [ExcludeFromCodeCoverage]
    class LocationDataV2
    {
        /// <summary>
        /// <para lang="zh">获得/设置 州</para>
        /// <para lang="en">Get/Set Continent</para>
        /// </summary>
        public string? Continent { get; set; }

        /// <summary>
        /// <para lang="zh">获得/设置 国家</para>
        /// <para lang="en">Get/Set Country</para>
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// <para lang="zh">获得/设置 邮编</para>
        /// <para lang="en">Get/Set ZipCode</para>
        /// </summary>
        public string? ZipCode { get; set; }

        /// <summary>
        /// <para lang="zh">获得/设置 时区</para>
        /// <para lang="en">Get/Set TimeZone</para>
        /// </summary>
        public string? TimeZone { get; set; }

        /// <summary>
        /// <para lang="zh">获得/设置 精度</para>
        /// <para lang="en">Get/Set Accuracy</para>
        /// </summary>
        public string? Accuracy { get; set; }

        /// <summary>
        /// <para lang="zh">获得/设置 所属</para>
        /// <para lang="en">Get/Set Owner</para>
        /// </summary>
        public string? Owner { get; set; }

        /// <summary>
        /// <para lang="zh">获得/设置 运营商</para>
        /// <para lang="en">Get/Set ISP</para>
        /// </summary>
        public string? Isp { get; set; }

        /// <summary>
        /// <para lang="zh">获得/设置 来源</para>
        /// <para lang="en">Get/Set Source</para>
        /// </summary>
        public string? Source { get; set; }

        /// <summary>
        /// <para lang="zh">获得/设置 区号</para>
        /// <para lang="en">Get/Set Area Code</para>
        /// </summary>
        public string? AreaCode { get; set; }

        /// <summary>
        /// <para lang="zh">获得/设置 行政区划代码</para>
        /// <para lang="en">Get/Set AdCode</para>
        /// </summary>
        public string? AdCode { get; set; }

        /// <summary>
        /// <para lang="zh">获得/设置 国家代码</para>
        /// <para lang="en">Get/Set Country Code</para>
        /// </summary>
        public string? AsNumber { get; set; }

        /// <summary>
        /// <para lang="zh">获得/设置 经度</para>
        /// <para lang="en">Get/Set Latitude</para>
        /// </summary>
        public string? Lat { get; set; }

        /// <summary>
        /// <para lang="zh">获得/设置 纬度</para>
        /// <para lang="en">Get/Set Longitude</para>
        /// </summary>
        public string? Lng { get; set; }

        /// <summary>
        /// <para lang="zh">获得/设置 半径</para>
        /// <para lang="en">Get/Set Radius</para>
        /// </summary>
        public string? Radius { get; set; }

        /// <summary>
        /// <para lang="zh">获得/设置 省份</para>
        /// <para lang="en">Get/Set Province</para>
        /// </summary>
        public string? Prov { get; set; }

        /// <summary>
        /// <para lang="zh">获得/设置 城市</para>
        /// <para lang="en">Get/Set City</para>
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// <para lang="zh">获得/设置 区县</para>
        /// <para lang="en">Get/Set District</para>
        /// </summary>
        public string? District { get; set; }
    }
}
