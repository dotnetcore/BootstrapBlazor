// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using System.Runtime.Versioning;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">服务器端在线连接服务，不支持在 WebAssembly 中查询服务器连接信息</para>
/// <para lang="en">Server-side online connection service. Querying server connections from WebAssembly is not supported.</para>
/// </summary>
/// <remarks>
/// <para lang="zh">默认服务仅在服务器端注册，不在 WebAssembly 中注册。客户端无法通过此接口读取服务器在线连接信息。</para>
/// <para lang="en">The default service is registered on the server only, not in WebAssembly. Clients cannot use this interface to read server-side online connections.</para>
/// <para lang="zh">默认实现仅保存当前进程内的连接信息。WebAssembly 中的 ConnectionHub 通过服务器中间件上报心跳，在线连接统计应在服务器端读取。需要引用 BootstrapBlazor.Middleware 包，调用其 UseBootstrapBlazor 方法</para>
/// <para lang="en">The default implementation only stores connection information within the current process. In WebAssembly, ConnectionHub reports heartbeats through server middleware, and online connection statistics should be read on the server side. You need to reference the BootstrapBlazor.Middleware package and call its UseBootstrapBlazor method.</para>
/// </remarks>
[UnsupportedOSPlatform("browser")]
public interface IConnectionService
{
    /// <summary>
    /// <para lang="zh">增加或更新当前 Key</para>
    /// <para lang="en">Add or Update Current Key</para>
    /// </summary>
    void AddOrUpdate(ClientInfo client);

    /// <summary>
    /// <para lang="zh">获得指定 key 的连接信息</para>
    /// <para lang="en">Get connection information for specified key</para>
    /// </summary>
    bool TryGetValue(string key, out ConnectionItem? value);

    /// <summary>
    /// <para lang="zh">获得 链接集合</para>
    /// <para lang="en">Get Connections Collection</para>
    /// </summary>
    ICollection<ConnectionItem> Connections { get; }

    /// <summary>
    /// <para lang="zh">获得 在线连接数</para>
    /// <para lang="en">Get Online Connection Count</para>
    /// </summary>
    long Count { get; }
}
