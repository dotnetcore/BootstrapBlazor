// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// ConnectionHubOptions 配置类
/// </summary>
public class ConnectionHubOptions
{
    /// <summary>
    /// 获得/设置 是否开启 CollectionHub 功能 默认 false 未开启
    /// </summary>
    public bool Enable { get; set; }

    /// <summary>
    /// 获得/设置 是否开启 IP 定位功能 默认 false 未开启
    /// </summary>
    public bool EnableIpLocator { get; set; }

    /// <summary>
    /// 获得/设置 过期扫描周期 默认 1 分钟
    /// </summary>
    /// <remarks>自动清理超时 5 倍心跳时间的客户端信息</remarks>
    public TimeSpan ExpirationScanFrequency { get; set; } = TimeSpan.FromMinutes(10);

    /// <summary>
    /// 获得/设置 超时间隔 默认 10 秒
    /// </summary>
    /// <remarks>不能小于 <see cref="BeatInterval"/> 心跳间隔</remarks>
    public TimeSpan TimeoutInterval { get; set; } = TimeSpan.FromSeconds(10);

    /// <summary>
    /// 获得/设置 ConnectionHub 组件心跳间隔 默认 5000 单位毫秒
    /// </summary>
    public TimeSpan BeatInterval { get; set; } = TimeSpan.FromSeconds(5);
}
