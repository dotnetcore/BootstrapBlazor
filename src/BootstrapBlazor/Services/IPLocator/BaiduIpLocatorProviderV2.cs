// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace BootstrapBlazor.Components;

/// <summary>
/// BaiduIPLocatorV2 第二个版本实现类
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
        /// 获得/设置 州
        /// </summary>
        public string? Continent { get; set; }

        /// <summary>
        /// 获得/设置 国家
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// 获得/设置 邮编
        /// </summary>
        public string? ZipCode { get; set; }

        /// <summary>
        /// 获得/设置 时区
        /// </summary>
        public string? TimeZone { get; set; }

        /// <summary>
        /// 获得/设置 精度
        /// </summary>
        public string? Accuracy { get; set; }

        /// <summary>
        /// 获得/设置 所属
        /// </summary>
        public string? Owner { get; set; }

        /// <summary>
        /// 获得/设置 运营商
        /// </summary>
        public string? Isp { get; set; }

        /// <summary>
        /// 获得/设置 来源
        /// </summary>
        public string? Source { get; set; }

        /// <summary>
        /// 获得/设置 区号
        /// </summary>
        public string? AreaCode { get; set; }

        /// <summary>
        /// 获得/设置 行政区划代码
        /// </summary>
        public string? AdCode { get; set; }

        /// <summary>
        /// 获得/设置 国家代码
        /// </summary>
        public string? AsNumber { get; set; }

        /// <summary>
        /// 获得/设置 经度
        /// </summary>
        public string? Lat { get; set; }

        /// <summary>
        /// 获得/设置 纬度
        /// </summary>
        public string? Lng { get; set; }

        /// <summary>
        /// 获得/设置 半径
        /// </summary>
        public string? Radius { get; set; }

        /// <summary>
        /// 获得/设置 省份
        /// </summary>
        public string? Prov { get; set; }

        /// <summary>
        /// 获得/设置 城市
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// 获得/设置 区县
        /// </summary>
        public string? District { get; set; }
    }
}
