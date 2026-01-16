// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">IpLocator 配置类</para>
///  <para lang="en">IpLocator Options</para>
/// </summary>
public class IpLocatorOptions
{
    /// <summary>
    ///  <para lang="zh">获得/设置 定位器名称 内置支持 BaiduIpLocatorProvider BaiduIpLocatorProviderV2</para>
    ///  <para lang="en">Get/Set Locator Name, built-in support for BaiduIpLocatorProvider, BaiduIpLocatorProviderV2</para>
    /// </summary>
    public string? ProviderName { get; set; }

    /// <summary>
    ///  <para lang="zh">获得/设置 是否开启缓存降低请求频率 默认 true 缓存</para>
    ///  <para lang="en">Get/Set Enable Cache to reduce request frequency, default true</para>
    /// </summary>
    public bool EnableCache { get; set; } = true;

    /// <summary>
    ///  <para lang="zh">获得/设置 滑动过期时间 默认 5 分钟</para>
    ///  <para lang="en">Get/Set Sliding Expiration, default 5 minutes</para>
    /// </summary>
    public TimeSpan SlidingExpiration { get; set; } = TimeSpan.FromMinutes(5);
}
