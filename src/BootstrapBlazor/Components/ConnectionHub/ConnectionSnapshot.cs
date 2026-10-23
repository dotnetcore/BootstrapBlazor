// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">服务器在线连接快照</para>
/// <para lang="en">Server online connection snapshot.</para>
/// </summary>
class ConnectionSnapshot
{
    /// <summary>
    /// <para lang="zh">获得/设置在线连接数，不包含已超时的连接</para>
    /// <para lang="en">Gets or sets the online count, excluding timed-out connections.</para>
    /// </summary>
    public long Count { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置连接集合，包括尚未被过期扫描移除的记录</para>
    /// <para lang="en">Gets or sets connections, including records not yet removed by the expiration scan.</para>
    /// </summary>
    public ICollection<ConnectionItem> Connections { get; set; } = Array.Empty<ConnectionItem>();
}
