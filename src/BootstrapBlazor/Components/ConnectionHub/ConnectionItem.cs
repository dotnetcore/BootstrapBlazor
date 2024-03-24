// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

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
