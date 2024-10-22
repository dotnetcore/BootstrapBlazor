// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// IpLocator 配置类
/// </summary>
public class IpLocatorOptions
{
    /// <summary>
    /// 获得/设置 定位器名称 内置支持 BaiduIpLocatorProvider BaiduIpLocatorProviderV2
    /// </summary>
    public string? ProviderName { get; set; }

    /// <summary>
    /// 获得/设置 是否开启缓存降低请求频率 默认 true 缓存
    /// </summary>
    public bool EnableCache { get; set; } = true;

    /// <summary>
    /// 获得/设置 滑动过期时间 默认 5 分钟
    /// </summary>
    public TimeSpan SlidingExpiration { get; set; } = TimeSpan.FromMinutes(5);
}
