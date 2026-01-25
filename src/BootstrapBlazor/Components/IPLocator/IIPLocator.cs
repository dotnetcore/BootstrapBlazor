// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">IP 地址定位接口</para>
/// <para lang="en">IP Address Locator Interface</para>
/// </summary>
[Obsolete("已弃用，请参考 https://www.blazor.zone/locator")]
public interface IIPLocator
{
    /// <summary>
    /// <para lang="zh">定位方法</para>
    /// <para lang="en">Locate Method</para>
    /// </summary>
    /// <param name="option"><para lang="zh">定位器配置信息</para><para lang="en">locatorconfiginfo</para></param>
    /// <returns><para lang="zh">定位器定位结果</para><para lang="en">locator定位result</para></returns>
    Task<string?> Locate(IPLocatorOption option);

    /// <summary>
    /// <para lang="zh">获得/设置 接口地址</para>
    /// <para lang="en">Gets or sets Interface URL</para>
    /// </summary>
    public string? Url { get; set; }
}
