// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// IpLocator 配置类
/// </summary>
public class IpLocatorOptions
{
    /// <summary>
    /// 获得/设置 是否开启缓存降低请求频率 默认 true 缓存
    /// </summary>
    public bool EnableCache { get; set; } = true;

    /// <summary>
    /// 获得/设置 是否开启缓存降低请求频率
    /// </summary>
    public TimeSpan SlidingExpiration { get; set; } = TimeSpan.FromMinutes(5);

    /// <summary>
    /// 获得/设置 定位器名称 内置支持 BaiduIpLocatorProvider BaiduIpLocatorProviderV2
    /// </summary>
    public string? ProviderName { get; set; }
}
