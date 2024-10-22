// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// IP 地址定位接口
/// </summary>
[Obsolete("已弃用，请参考 https://www.blazor.zone/locator")]
public interface IIPLocator
{
    /// <summary>
    /// 定位方法
    /// </summary>
    /// <param name="option">定位器配置信息</param>
    /// <returns>定位器定位结果</returns>
    Task<string?> Locate(IPLocatorOption option);

    /// <summary>
    /// 获得/设置 接口地址
    /// </summary>
    public string? Url { get; set; }
}
