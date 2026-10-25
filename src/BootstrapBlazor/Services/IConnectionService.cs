// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">在线连接服务，WebAssembly 同步读取通过 SignalR 获取的缓存快照</para>
/// <para lang="en">Online connection service. WebAssembly reads cached snapshots fetched through SignalR.</para>
/// </summary>
/// <remarks>
/// <para lang="zh">WebAssembly 默认提供空查询实现，关闭连接功能时无需扩展包。启用心跳或查询需引用 BootstrapBlazor.SignalR，并在 AddBootstrapBlazor 后调用 AddBootstrapBlazorConnectionClient。扩展每次快照请求完成间隔 3 秒刷新，首次获取前缓存为空，查询失败保留上次数据并记录错误。缓存变更不会自动触发组件重渲染。</para>
/// <para lang="en">WebAssembly provides empty queries by default; no extension is required when connections are disabled. Heartbeats and queries require BootstrapBlazor.SignalR and AddBootstrapBlazorConnectionClient after AddBootstrapBlazor. The extension refreshes snapshots three seconds after each request. The cache is initially empty; failed queries retain the previous snapshot and log errors. Cache changes do not trigger rendering.</para>
/// <para lang="zh">服务器实现保存当前进程内的连接信息。WebAssembly 心跳及查询使用同一个 SignalR Hub，服务器需引用 BootstrapBlazor.Middleware，注册 SignalR 服务并调用 MapBootstrapBlazorHub。快照查询需单独启用，可配置独立授权策略。</para>
/// <para lang="en">The server implementation stores connections in the current process. WebAssembly heartbeats and queries share the SignalR Hub. The server requires BootstrapBlazor.Middleware, SignalR service registration, and MapBootstrapBlazorHub. Snapshot queries require explicit opt-in and support a separate authorization policy.</para>
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
    [ExcludeFromCodeCoverage]
    [Obsolete("已弃用，请使用 AddOrUpdateAsync 方法; Deprecated. Use AddOrUpdateAsync instead")]
    public void AddOrUpdate(ClientInfo client) { }

    /// <summary>
    /// <para lang="zh">异步更新连接信息，WebAssembly 扩展通过 SignalR 上报心跳</para>
    /// <para lang="en">Updates connection information asynchronously. The WebAssembly extension reports a heartbeat through SignalR.</para>
    /// </summary>
    /// <param name="client"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task AddOrUpdateAsync(ClientInfo client, CancellationToken cancellationToken = default);

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
