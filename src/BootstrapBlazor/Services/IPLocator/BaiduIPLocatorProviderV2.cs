﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Logging;
using System.Net.Http.Json;

namespace BootstrapBlazor.Components;

/// <summary>
/// BaiduIPLocatorV2 第二个版本实现类
/// </summary>
class BaiduIPLocatorProviderV2(IHttpClientFactory httpClientFactory, ILogger<BaiduIPLocatorProvider> logger) : BaiduIPLocatorProvider(httpClientFactory, logger)
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
    /// <returns></returns>
    protected override async Task<string> Fetch(string url)
    {
        client ??= GetHttpClient();
        using var token = new CancellationTokenSource(3000);
        var result = await client.GetFromJsonAsync<LocationResultV2>(url, token.Token);
        return result is { Code: "Success" }
            ? $"{result.Data.Prov}{result.Data.City}{result.Data.District} {result.Data.Isp}"
            : "XX XX";
    }

    class LocationResultV2
    {
        public string? Code { get; set; }

        [NotNull]
        public LocationDataV2? Data { get; set; }

        public bool Charge { get; set; }

        public string? Msg { get; set; }

        public string? Ip { get; set; }

        public string? CoordSys { get; set; }
    }

    class LocationDataV2
    {
        public string? Continent { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }

        public string? TimeZone { get; set; }

        public string? Accuracy { get; set; }

        public string? Owner { get; set; }

        public string? Isp { get; set; }

        public string? Source { get; set; }

        public string? AreaCode { get; set; }

        public string? AdCode { get; set; }

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
