// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 连接对象实体类
/// </summary>
public class ConnectionItem
{
    /// <summary>
    /// 获得/设置 连接 Id
    /// </summary>
    [NotNull]
    public string? Id { get; internal set; }

    /// <summary>
    /// 获得/设置 连接 Ip 地址
    /// </summary>
    public ClientInfo? ClientInfo { get; set; }

    /// <summary>
    /// 获得/设置 开始连接时间
    /// </summary>
    public DateTimeOffset ConnectionTime { get; internal set; }

    /// <summary>
    /// 获得/设置 上次心跳时间
    /// </summary>
    public DateTimeOffset LastBeatTime { get; internal set; }
}
