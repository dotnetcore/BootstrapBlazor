// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">连接对象实体类</para>
/// <para lang="en">Connection object entity class</para>
/// </summary>
public class ConnectionItem
{
    /// <summary>
    /// <para lang="zh">获得/设置 连接 Id</para>
    /// <para lang="en">Get/Set Connection Id</para>
    /// </summary>
    [NotNull]
    public string? Id { get; internal set; }

    /// <summary>
    /// <para lang="zh">获得/设置 连接 Ip 地址</para>
    /// <para lang="en">Get/Set Connection IP address</para>
    /// </summary>
    public ClientInfo? ClientInfo { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 开始连接时间</para>
    /// <para lang="en">Get/Set Connection start time</para>
    /// </summary>
    public DateTimeOffset ConnectionTime { get; internal set; }

    /// <summary>
    /// <para lang="zh">获得/设置 上次心跳时间</para>
    /// <para lang="en">Get/Set Last beat time</para>
    /// </summary>
    public DateTimeOffset LastBeatTime { get; internal set; }
}
