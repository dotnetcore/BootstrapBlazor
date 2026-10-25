// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">异步上报客户端业务心跳，Server 更新本地存储，WebAssembly 通过扩展服务上报</para>
/// <para lang="en">Reports application heartbeats asynchronously, updating local storage on Server or using an extension transport on WebAssembly.</para>
/// </summary>
public interface IConnectionHeartbeatService
{
    /// <summary>
    /// <para lang="zh">上报心跳，服务器禁用心跳时返回 false</para>
    /// <para lang="en">Reports a heartbeat, returning false when heartbeats are disabled on the server.</para>
    /// </summary>
    /// <param name="client"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> ReportHeartbeatAsync(ClientInfo client, CancellationToken cancellationToken = default);
}
