// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">分发服务接口</para>
/// <para lang="en">Dispatcher Service Interface</para>
/// </summary>
/// <remarks>
/// <para lang="zh">Server 默认提供进程内分发。WebAssembly/Auto 的 Client 工程需引用 BootstrapBlazor.SignalR 并调用 AddBootstrapBlazorSignalRClient；Server 工程需注册 SignalR 分发并映射 Hub。</para>
/// <para lang="en">Server provides in-process dispatch by default. WebAssembly/Auto clients must reference BootstrapBlazor.SignalR and call AddBootstrapBlazorSignalRClient; the server must register SignalR dispatch and map its Hub.</para>
/// </remarks>
public interface IDispatchService<TEntry>
{
    /// <summary>
    /// <para lang="zh">分发方法</para>
    /// <para lang="en">Dispatch Method</para>
    /// </summary>
    /// <param name="entry"></param>
    [ExcludeFromCodeCoverage]
    [Obsolete("已弃用，请使用 DispatchAsync 方法; Deprecated. Use DispatchAsync instead")]
    public void Dispatch(DispatchEntry<TEntry> entry) { }

    /// <summary>
    /// <para lang="zh">订阅分发</para>
    /// <para lang="en">Subscribe Dispatch</para>
    /// </summary>
    /// <param name="callback"></param>
    [ExcludeFromCodeCoverage]
    [Obsolete("已弃用，请使用 SubscribeAsync 方法; Deprecated. Use SubscribeAsync instead")]
    public void Subscribe(Func<DispatchEntry<TEntry>, Task> callback) { }

    /// <summary>
    /// <para lang="zh">取消订阅分发</para>
    /// <para lang="en">Unsubscribe Dispatch</para>
    /// </summary>
    /// <param name="callback"></param>
    /// <exception cref="NotImplementedException"></exception>
    [ExcludeFromCodeCoverage]
    [Obsolete("已弃用，请使用 UnSubscribeAsync 方法; Deprecated. Use UnSubscribeAsync instead")]
    public void UnSubscribe(Func<DispatchEntry<TEntry>, Task> callback) { }

    /// <summary>
    /// <para lang="zh">异步分发消息并等待处理完成</para>
    /// <para lang="en">Dispatch a message and await completion.</para>
    /// </summary>
    Task DispatchAsync(DispatchEntry<TEntry> entry, CancellationToken cancellationToken = default);

    /// <summary>
    /// <para lang="zh">订阅消息并等待远程订阅就绪</para>
    /// <para lang="en">Subscribe and wait until the remote subscription is ready.</para>
    /// </summary>
    Task SubscribeAsync(Func<DispatchEntry<TEntry>, Task> callback, CancellationToken cancellationToken = default);

    /// <summary>
    /// <para lang="zh">取消订阅分发</para>
    /// <para lang="en">Unsubscribe Dispatch</para>
    /// </summary>
    Task UnSubscribeAsync(Func<DispatchEntry<TEntry>, Task> callback, CancellationToken cancellationToken = default);
}
