// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
    Task<string?> Locate(string ip);
}
