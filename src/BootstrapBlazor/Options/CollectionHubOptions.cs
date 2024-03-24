// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// CollectionHub 配置类
/// </summary>
public class CollectionHubOptions
{
    /// <summary>
    /// 获得/设置 过期扫描周期 默认 1 分钟
    /// </summary>
    public TimeSpan ExpirationScanFrequency { get; set; } = TimeSpan.FromMinutes(1);

    /// <summary>
    /// 获得/设置 ConnectionHub 组件心跳间隔
    /// </summary>
    public int BeatInterval { get; set; } = 5000;

    /// <summary>
    /// 获得/设置 是否开启 CollectionHub 功能 默认 false 未开启
    /// </summary>
    public bool Enable { get; set; }
}
