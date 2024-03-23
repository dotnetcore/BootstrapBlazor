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
    /// 获得/设置 过期扫描周期 默认 30秒
    /// </summary>
    public TimeSpan ExpirationScanFrequency { get; set; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// 获得/设置 ConnectionHub 组件心跳间隔
    /// </summary>
    public int BeatInterval { get; set; } = 5000;
}
