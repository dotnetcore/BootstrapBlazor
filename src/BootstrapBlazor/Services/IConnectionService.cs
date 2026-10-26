// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">在线连接服务，提供连接信息的异步更新和同步查询</para>
/// <para lang="en">Online connection service providing asynchronous updates and synchronous queries.</para>
/// </summary>
/// <remarks>
/// <para lang="zh">默认服务器实现保存当前进程内的连接信息，不自动汇总多个服务器实例的数据。在线数不包含已超时的连接，但查询结果中仍可能包含尚未被过期扫描移除的记录。</para>
/// <para lang="en">The default server implementation stores connections in the current process and does not aggregate multiple server instances. The online count excludes timed-out connections, but queries may include records not yet removed by the expiration scan.</para>
/// <para lang="zh">WebAssembly 应通过依赖注入注册自己的实现。使用 BootstrapBlazor.SignalR 扩展时，在 AddBootstrapBlazor 后调用 AddBootstrapBlazorConnectionClient，心跳通过 SignalR 上报，同步查询读取最近一次获取的服务器快照，不会发起实时网络请求。首次获取前缓存为空，缓存变更不会自动触发组件重渲染。</para>
/// <para lang="en">WebAssembly should register its own implementation through dependency injection. With BootstrapBlazor.SignalR, call AddBootstrapBlazorConnectionClient after AddBootstrapBlazor. Heartbeats are reported through SignalR; synchronous queries read the latest cached server snapshot without making a network request. The cache is initially empty, and cache changes do not trigger rendering.</para>
/// <para lang="zh">SignalR 扩展在每次快照请求完成后间隔 3 秒刷新，临时查询失败时保留上次数据并记录错误。查询被禁用、未授权或 Hub 请求发生永久 HTTP 错误时，清空快照并停止轮询，重新加载页面后再次尝试；心跳上报不受快照轮询停止影响。</para>
/// <para lang="en">The SignalR extension refreshes snapshots three seconds after each request completes. Transient query failures retain the previous snapshot and log errors. Disabled or unauthorized queries and permanent Hub HTTP errors clear the snapshot and stop polling until the page reloads. Heartbeat reporting remains independent of snapshot polling.</para>
/// <para lang="zh">WebAssembly 心跳及查询使用同一个 SignalR Hub，服务器需引用 BootstrapBlazor.Middleware，注册 SignalR 服务并调用 MapBootstrapBlazorHub。快照查询需单独启用，可配置独立授权策略。</para>
/// <para lang="en">WebAssembly heartbeats and queries share the SignalR Hub. The server requires BootstrapBlazor.Middleware, SignalR service registration, and MapBootstrapBlazorHub. Snapshot queries require explicit opt-in and support a separate authorization policy.</para>
/// </remarks>
public interface IConnectionService
{
    /// <summary>
    /// <para lang="zh">已弃用的同步连接更新方法，请改用并等待 <see cref="AddOrUpdateAsync"/></para>
    /// <para lang="en">Deprecated synchronous connection update method. Use and await <see cref="AddOrUpdateAsync"/> instead.</para>
    /// <para><version>11.0.0</version></para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">此方法仅为兼容旧接口保留，接口默认实现不执行任何操作，不会转发到异步方法。</para>
    /// <para lang="en">Retained only for compatibility. The default interface implementation does nothing and does not forward to the asynchronous method.</para>
    /// </remarks>
    /// <param name="client"><para lang="zh">客户端连接信息</para><para lang="en">The client connection information.</para></param>
    [ExcludeFromCodeCoverage]
    [Obsolete("已弃用，请使用 AddOrUpdateAsync 方法; Deprecated. Use AddOrUpdateAsync instead")]
    public void AddOrUpdate(ClientInfo client) { }

    /// <summary>
    /// <para lang="zh">以 <see cref="ClientInfo.Id"/> 为标识异步增加或更新连接信息及心跳时间</para>
    /// <para lang="en">Asynchronously adds or updates connection information and the heartbeat time, using <see cref="ClientInfo.Id"/> as the identifier.</para>
    /// <para><version>11.0.0</version></para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">默认服务器实现直接更新内存中的连接信息；WebAssembly 扩展通过 SignalR 上报心跳。使用 <see cref="ConnectionHub"/> 时由组件自动调用，手动上报时应等待返回的任务。</para>
    /// <para lang="en">The default server implementation updates in-memory connections directly; the WebAssembly extension reports heartbeats through SignalR. <see cref="ConnectionHub"/> calls this method automatically. Await the returned task when reporting manually.</para>
    /// </remarks>
    /// <param name="client"><para lang="zh">客户端连接信息，默认服务器实现忽略 Id 为空的记录</para><para lang="en">The client connection information. The default server implementation ignores records with a null or empty Id.</para></param>
    /// <param name="cancellationToken"><para lang="zh">用于取消更新操作的令牌</para><para lang="en">The token used to cancel the update.</para></param>
    /// <returns><para lang="zh">表示更新操作的任务</para><para lang="en">A task representing the update operation.</para></returns>
    /// <exception cref="OperationCanceledException"><para lang="zh">取消令牌已请求取消</para><para lang="en">Cancellation has been requested.</para></exception>
    Task AddOrUpdateAsync(ClientInfo client, CancellationToken cancellationToken = default);

    /// <summary>
    /// <para lang="zh">尝试获得指定连接 Id 的连接信息</para>
    /// <para lang="en">Attempts to get connection information for the specified connection Id.</para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">默认服务器实现可能返回尚未被过期扫描移除的超时记录；WebAssembly 实现读取本地缓存快照。</para>
    /// <para lang="en">The default server implementation may return timed-out records not yet removed by the expiration scan. The WebAssembly implementation reads the locally cached snapshot.</para>
    /// </remarks>
    /// <param name="key"><para lang="zh">连接 Id</para><para lang="en">The connection Id.</para></param>
    /// <param name="value"><para lang="zh">查找成功时为连接信息，否则为 null</para><para lang="en">The connection information when found; otherwise, null.</para></param>
    /// <returns><para lang="zh">找到连接时返回 true，否则返回 false</para><para lang="en">true if the connection is found; otherwise, false.</para></returns>
    bool TryGetValue(string key, [NotNullWhen(true)] out ConnectionItem? value);

    /// <summary>
    /// <para lang="zh">获得连接集合，包含尚未被过期扫描移除的超时记录</para>
    /// <para lang="en">Gets the connection collection, including timed-out records not yet removed by the expiration scan.</para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">WebAssembly 实现返回最近一次缓存快照中的连接集合。在线人数请使用 <see cref="Count"/>，不要使用集合元素数量代替。</para>
    /// <para lang="en">The WebAssembly implementation returns connections from the latest cached snapshot. Use <see cref="Count"/> for the online count rather than the collection size.</para>
    /// </remarks>
    ICollection<ConnectionItem> Connections { get; }

    /// <summary>
    /// <para lang="zh">获得在线连接数，不包含已超时的连接</para>
    /// <para lang="en">Gets the online connection count, excluding timed-out connections.</para>
    /// </summary>
    /// <remarks>
    /// <para lang="zh">默认服务器实现根据当前时间和超时间隔计算；WebAssembly 实现返回最近一次缓存快照中的在线数，可能与服务器当前值存在延迟。</para>
    /// <para lang="en">The default server implementation calculates this using the current time and timeout interval. The WebAssembly implementation returns the online count from the latest cached snapshot, which may lag behind the server.</para>
    /// </remarks>
    long Count { get; }
}
