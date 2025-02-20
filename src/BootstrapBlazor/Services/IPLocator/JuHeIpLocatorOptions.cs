// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 聚合搜索引擎 IP 定位器配置类
/// </summary>
class JuHeIpLocatorOptions
{
    /// <summary>
    /// 聚合搜索引擎 IP 定位器 AppKey
    /// </summary>
    public string Key { get; set; } = "";

    /// <summary>
    /// 聚合搜索引擎 IP 定位器请求地址
    /// </summary>
    public string Url { get; set; } = "http://apis.juhe.cn/ip/ipNew";

    /// <summary>
    /// 聚合搜索引擎 IP 定位器请求超时时间 默认 5 秒
    /// </summary>
    public TimeSpan Timeout { get; set; }
}
