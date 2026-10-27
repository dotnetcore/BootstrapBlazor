// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">连接快照记录</para>
/// <para lang="en">Connection snapshot record</para>
/// </summary>
public record ConnectionItem
{
    /// <summary>
    /// <para lang="zh">获得连接 Id</para>
    /// <para lang="en">Gets the connection Id</para>
    /// </summary>
    public string? Id { get; init; }

    /// <summary>
    /// <para lang="zh">获得连接 Ip 地址</para>
    /// <para lang="en">Gets the connection IP address</para>
    /// </summary>
    public ClientInfo? ClientInfo { get; init; }

    /// <summary>
    /// <para lang="zh">获得连接开始时间</para>
    /// <para lang="en">Gets the connection start time</para>
    /// </summary>
    public DateTimeOffset ConnectionTime { get; init; }

    /// <summary>
    /// <para lang="zh">获得上次心跳时间</para>
    /// <para lang="en">Gets the last heartbeat time</para>
    /// </summary>
    public DateTimeOffset LastBeatTime { get; init; }
}
