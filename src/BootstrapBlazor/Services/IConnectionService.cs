// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">在线连接服务，WebAssembly 同步读取定期从服务器获取的缓存快照</para>
/// <para lang="en">Online connection service. WebAssembly reads cached snapshots periodically fetched from the server.</para>
/// </summary>
/// <remarks>
/// <para lang="zh">WebAssembly 首次获取数据前缓存为空，每次请求完成后间隔 3 秒刷新；每个标签页独立轮询。缓存变更不会自动触发组件重渲染。查询失败保留上次数据并记录错误。</para>
/// <para lang="en">WebAssembly caches are empty until the first fetch and refresh three seconds after each request. Each tab polls independently. Cache changes do not trigger component rendering. Failed queries retain the previous snapshot and log errors.</para>
/// <para lang="zh">服务器实现保存当前进程内的连接信息。WebAssembly 通过服务器中间件上报心跳及查询数据，需要服务器引用支持查询的 BootstrapBlazor.Middleware 包并调用 UseBootstrapBlazor。</para>
/// <para lang="en">The server implementation stores connections in the current process. WebAssembly reports heartbeats and queries data through server middleware. Reference a query-capable BootstrapBlazor.Middleware package on the server and call UseBootstrapBlazor.</para>
/// </remarks>
public interface IConnectionService
{
    /// <summary>
    /// <para lang="zh">增加或更新当前 Key</para>
    /// <para lang="en">Add or Update Current Key</para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">仅支持服务器端；WebAssembly 请通过 ConnectionHub 上报心跳。</para>
    /// <para lang="en">Server-only. Use ConnectionHub to report WebAssembly heartbeats.</para>
    /// </remarks>
    void AddOrUpdate(ClientInfo client);

    /// <summary>
    /// <para lang="zh">获得指定 key 的连接信息</para>
    /// <para lang="en">Get connection information for specified key</para>
    /// </summary>
    bool TryGetValue(string key, [NotNullWhen(true)] out ConnectionItem? value);

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
