// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace BootstrapBlazor.Components;

/// <summary>
/// 聚合搜索引擎 IP 定位器
/// </summary>
/// <param name="httpClientFactory"></param>
/// <param name="configuration"></param>
/// <param name="options"></param>
/// <param name="logger"></param>
public class JuHeIpLocatorProvider(IHttpClientFactory httpClientFactory, IConfiguration configuration, IOptions<BootstrapBlazorOptions> options, ILogger<JuHeIpLocatorProvider> logger) : DefaultIpLocatorProvider(options)
{
    private HttpClient? _client;

    private JuHeIpLocatorOptions? _options;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="ip"></param>
    protected override async Task<string?> LocateByIp(string ip)
    {
        string? ret = null;
        _options ??= GetOptions();
        var url = $"{_options.Url}?ip={ip}&key={_options.Key}";
        try
        {
            _client ??= httpClientFactory.CreateClient();
            ret = await Fetch(url, _client);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Url: {url}", url);
        }
        return ret;
    }

    private JuHeIpLocatorOptions GetOptions()
    {
        var options = new JuHeIpLocatorOptions();

        try
        {
            configuration.Bind(nameof(JuHeIpLocatorOptions), options);
            if (string.IsNullOrEmpty(options.Key))
            {
                throw new InvalidOperationException($"{nameof(JuHeIpLocatorOptions)}:Key not value in appsettings configuration file. 未配置 {nameof(JuHeIpLocatorOptions)}:Key 请在 appsettings.json 中配置 {nameof(JuHeIpLocatorOptions)}:Key");
            }
            if (string.IsNullOrEmpty(options.Url))
            {
                options.Url = "http://apis.juhe.cn/ip/ipNewV3";
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{GetOptions} failed", nameof(GetOptions));
        }
        return options;
    }

    /// <summary>
    /// 请求获得地理位置接口方法
    /// </summary>
    /// <param name="url"></param>
    /// <param name="client"></param>
    /// <returns></returns>
    private async Task<string?> Fetch(string url, HttpClient client)
    {
        using var token = new CancellationTokenSource(_options?.Timeout ?? TimeSpan.FromSeconds(3));
        var result = await client.GetFromJsonAsync<LocationResult>(url, token.Token);
        if (result != null && result.ErrorCode != 0)
        {
            logger.LogError("ErrorCode: {ErrorCode} Reason: {Reason}", result.ErrorCode, result.Reason);
        }
        return result?.ToString();
    }

    /// <summary>
    /// LocationResult 结构体
    /// </summary>
    [ExcludeFromCodeCoverage]
    class LocationResult
    {
        /// <summary>
        /// 获得/设置 结果状态返回码 为 查询成功 时通讯正常
        /// </summary>
        public string? Reason { get; set; }

        /// <summary>
        /// 获得/设置 错误码
        /// </summary>
        [JsonPropertyName("error_code")]
        public int ErrorCode { get; set; }

        /// <summary>
        /// 获得/设置 定位信息
        /// </summary>
        public LocationData? Result { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns></returns>
        public override string? ToString()
        {
            string? ret = null;
            if (ErrorCode == 0)
            {
                ret = Result?.Country == "中国"
                    ? $"{Result?.Prov}{Result?.City}{Result?.District} {Result?.Isp}"
                    : $"{Result?.Continent} {Result?.Country} {Result?.City}";
            }
            return ret;
        }
    }


    [ExcludeFromCodeCoverage]
    class LocationData
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
