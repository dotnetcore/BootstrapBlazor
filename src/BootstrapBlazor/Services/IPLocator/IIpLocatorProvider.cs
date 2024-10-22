// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// IP 地址定位服务接口
/// </summary>
public interface IIpLocatorProvider
{
    /// <summary>
    /// 获得/设置 定位器 Key 默认 null 使用已注册的最后一个 Provider
    /// </summary>
    string? Key { get; set; }

    /// <summary>
    /// 通过 IP 地址定位地理位置信息
    /// </summary>
    /// <param name="ip"></param>
    Task<string?> Locate(string? ip);
}
