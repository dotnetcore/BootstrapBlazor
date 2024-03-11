// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
